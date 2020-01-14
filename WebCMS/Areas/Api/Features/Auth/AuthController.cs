using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Areas.Api.Features.Auth.Requests;
using WebCMS.Areas.Api.Models;
using WebCMS.Controllers;
using WebCMS.Data;
using Data.Entities;
using WebCMS.Filters;
using WebCMS.Helpers;
using Application.Services.User;
using System.Threading.Tasks;
using Application.Interfaces.Providers;
using Application.Auth;
using Application.Exceptions;

namespace WebCMS.Areas.Api.Features.Auth
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly SigninManager signinManager;
        private readonly IHashProvider hashProvider;
        private readonly UserService userService;
        private readonly AppDbContext dbContext;

        public AuthController(AppDbContext dbContext, UserService userService, SigninManager signinManager)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.signinManager = signinManager;
        }

        [ValidateModel]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = await signinManager.Signin(request.Email, request.Password);

                SignedTokenResult signedToken = JwtHelper.Sign(user);

                UserModel userModel = Mapper.Map<UserModel>(user);

                return Ok(new { signedToken.Token, signedToken.ExpiresIn, User = userModel });
            }
            catch (AccessForbiddenException)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            catch
            {
                return Unauthorized(new { message = "Kullanıcı adı veya şifre hatalı" });
            }
        }

        [ValidateModel]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            UserEntity user = dbContext.Users.FirstOrDefault(w => w.Email == request.Email.Trim() &&
            !w.IsDeleted);

            if (user != null)
            {
                return BadRequest(new { message = "This email is already registered" });
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password.Trim());

            user = new UserEntity()
            {
                Email = request.Email.Trim(),
                Password = hashedPassword
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();

            SignedTokenResult signedToken = JwtHelper.Sign(user);

            UserModel userModel = Mapper.Map<UserModel>(user);

            return Ok(new { signedToken.Token, signedToken.ExpiresIn, User = userModel });
        }
    }
}
