using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Services.User;
using Application.Services.User.Commands;
using AutoMapper;
using Data.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Data;

namespace WebCMS.Areas.Admin.Features.Auth
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserService userService;
        private readonly AppDbContext dbContext;

        public AuthController(AppDbContext dbContext, UserService userService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var isSuperAdminExist = dbContext.UserRoles.Any(w => w.Role == RoleType.SuperAdmin);

            if(!isSuperAdminExist)
            {
                return RedirectToAction("Register");
            }

            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            //HttpContext.User
            var claims = new List<Claim>
            {
                new Claim("Id", "3"),
                new Claim(ClaimTypes.Name, "muraterd"),
                new Claim("Email", "muraterd@fdsfds.com")
            };

            var identity = new ClaimsIdentity(claims, "Jwt");
            var principal = new ClaimsPrincipal(identity);

            HttpContext.User = principal;

            await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);


            return Redirect("/admin");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/admin");
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var command = Mapper.Map<CreateUserCommand>(request);
            await userService.CreateUser(command);

            return View();
        }
    }
}
