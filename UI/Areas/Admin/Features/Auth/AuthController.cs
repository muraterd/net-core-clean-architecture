using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.MediatR.Admin.Auth.Commands.CreateSuperAdmin;
using Application.MediatR.Admin.Auth.Commands.Login;
using Application.MediatR.Admin.Auth.Queries.IsSuperAdminExist;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UI.Areas.Admin.Features.Auth.ForgotPassword;
using UI.Areas.Admin.Features.Base;
using UI.Data;

namespace UI.Areas.Admin.Features.Auth
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class AuthController : BaseController
    {
        private readonly ILogger<AuthController> logger;

        public AuthController(ILogger<AuthController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            var isSuperAdminExist = await Mediator.Send(new IsSuperAdminExistQuery());

            if (!isSuperAdminExist)
            {
                return RedirectToAction("Register");
            }

            return View(new LoginCommand());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            try
            {
                var user = await Mediator.Send(command);

                await user.LoginWithCookie(HttpContext, command.RememberMe);

                return Redirect("/admin");
            }
            catch (AccessDeniedException)
            {
                ViewBag.ErrorMessage = "Kullanıcı adı veya şifre hatalı";
            }

            return View(new LoginCommand());
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
                var user = await Mediator.Send(command);
                await user.LoginWithCookie(HttpContext);

                return Redirect("/admin");
            }
            catch (DuplicateResultException)
            {
                ViewBag.ErrorMessage = "Bu email ile bir kullanıcı zaten kayıtlı";
                return View();
            }
        }

        [HttpGet("forgot-password")]
        public IActionResult ForgotPassword(string email)
        {
            var viewModel = new ForgotPasswordViewModel
            {
                Email = email
            };

            return View(viewModel);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                await Mediator.Send(viewModel.ToSendPasswordResetMailCommand());

                viewModel.ScreenState = Models.Base.ScreenState.Success;
            }
            catch (NotFoundException ex)
            {
                // For security reason we show success state
                viewModel.ScreenState = Models.Base.ScreenState.Success;
            }
            catch
            {
                viewModel.ScreenState = Models.Base.ScreenState.Error;
            }

            return View(viewModel);
        }

        [HttpGet("reset-password")]
        public IActionResult ResetPassword(string token)
        {
            //var viewModel = new ForgotPasswordViewModel
            //{
            //    Email = email
            //};

            return Ok($"reset {token}");
        }
    }
}
