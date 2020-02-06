using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.MediatR.Auth.Commands.CreateSuperAdmin;
using Application.MediatR.Auth.Commands.Login;
using Application.MediatR.Auth.Queries.IsSuperAdminExist;
using Application.Services.User;
using MediatR;
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
        private readonly IMediator mediator;
        private readonly UserService userService;
        private readonly AppDbContext dbContext;

        public AuthController(AppDbContext dbContext, UserService userService, IMediator mediator)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.mediator = mediator;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            var isSuperAdminExist = await mediator.Send(new IsSuperAdminExistQuery());

            if (!isSuperAdminExist)
            {
                return RedirectToAction("Register");
            }

            return View(new AdminLoginCommand());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AdminLoginCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            try
            {
                var user = await mediator.Send(command);

                await user.LoginWithCookie(HttpContext, command.RememberMe);

                return Redirect("/admin");
            }
            catch (AccessDeniedException)
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı";
            }

            return View();
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
        public async Task<IActionResult> Register(CreateSuperAdminCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            try
            {
                var user = await mediator.Send(command);
                await user.LoginWithCookie(HttpContext);

                return Redirect("/admin");
            }
            catch (DuplicateResultException)
            {
                ViewBag.ErrorMessage = "Bu email ile bir kullanıcı zaten kayıtlı";
                return View();
            }
        }
    }
}
