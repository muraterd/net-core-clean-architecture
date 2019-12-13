using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Areas.Api.Features.Auth.Requests;
using WebCMS.Areas.Api.Models;
using WebCMS.Controllers;
using WebCMS.Data;
using WebCMS.Data.Entities;
using WebCMS.Filters;
using WebCMS.Helpers;

namespace WebCMS.Areas.Api.Features.Auth
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly AppDbContext dbContext;

        public AuthController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            dbContext.Database.EnsureCreated();
            return Ok("Auth");
        }

        [HttpPost("login2")]
        public IActionResult Login2()
        {
            return Ok("Hede");
        }

        [ValidateModel]
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            UserEntity user = dbContext.Users.FirstOrDefault(w => w.Email == request.Email.Trim() &&  
            !w.IsDeleted);

            if (user == null)
            {
                return Unauthorized(new { message = "Kullanıcı adı veya şifre hatalı" });
            }

            if (!user.IsActive)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            if(!BCrypt.Net.BCrypt.Verify(request.Password.Trim(), user.Password))
            {
                return Unauthorized(new { message = "Kullanıcı adı veya şifre hatalı" });
            }

            SignedTokenResult signedToken = JwtHelper.Sign(user);

            UserModel userModel = Mapper.Map<UserModel>(user);

            return Ok(new { signedToken.Token, signedToken.ExpiresIn, User = userModel });
        }

        [ValidateModel]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            UserEntity user = dbContext.Users.FirstOrDefault(w => w.Email == request.Email.Trim() && 
            !w.IsDeleted);

            if(user != null)
            {
                return BadRequest(new { message = "This email is already registered" });
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password.Trim());

            user = new UserEntity() {
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
