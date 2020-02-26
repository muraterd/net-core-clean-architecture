using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.MediatR.Admin.Auth.Commands.CreateSuperAdmin;
using Application.MediatR.Admin.Auth.Queries.IsSuperAdminExist;
using Application.MediatR.Common.Auth.Commands;
using Application.MediatR.Common.Auth.Queries.IsSuperAdminExist;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using UI.Areas.Admin.Features.Auth.ForgotPassword;
using UI.Areas.Admin.Features.Auth.Login;
using UI.Areas.Admin.Features.Auth.Register;
using UI.Areas.Admin.Features.Auth.ResetPassword;
using UI.Areas.Admin.Features.Base;
using UI.Data;

namespace UI.Areas.Admin.Features.Auth
{
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class AuthController : BaseController
    {
        private readonly ILogger<AuthController> logger;
        private readonly IStringLocalizer<AuthController> localizer;

        public AuthController(ILogger<AuthController> logger, IStringLocalizer<AuthController> localizer)
        {
            this.logger = logger;
            this.localizer = localizer;

            var hede = localizer["Hello"];

        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            var isSuperAdminExist = await Mediator.Send(new IsSuperAdminExistQuery());

            if (!isSuperAdminExist)
            {
                return RedirectToAction("Register");
            }

            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture: "en", uiCulture: "en")));

            return View(new LoginViewModel());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var user = await Mediator.Send(viewModel.ToLoginCommand());

                await user.LoginWithCookie(HttpContext, viewModel.RememberMe);

                return Redirect("/admin");
            }
            catch (AccessDeniedException)
            {
                viewModel.ErrorMessage = localizer["Error_WrongEmailOrPassword"];
            }

            return View(viewModel);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/admin");
        }

        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
            if (await Mediator.Send(new IsSuperAdminExistQuery()))
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (await Mediator.Send(new IsSuperAdminExistQuery()))
            {
                return RedirectToAction("Login");
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                var cm = viewModel.ToCreateSuperAdminCommand();
                var user = await Mediator.Send(viewModel.ToCreateSuperAdminCommand());
                await user.LoginWithCookie(HttpContext);

                return Redirect("/admin");
            }
            catch (DuplicateResultException)
            {
                ViewBag.ErrorMessage = localizer["Error_ AlreadyRegistered"];
                return View(viewModel);
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
        public async Task<IActionResult> ResetPassword(string token)
        {
            bool isTokenValid = await Mediator.Send(new ValidatePasswordResetTokenQuery() { Token = token });

            if (!isTokenValid)
            {
                return RedirectToAction("Login");
            }

            var viewModel = new ResetPasswordViewModel();
            viewModel.Token = token;

            return View(viewModel);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            bool isTokenValid = await Mediator.Send(viewModel.ToValidatePasswordResetTokenQuery());

            if (!isTokenValid)
            {
                return RedirectToAction("Login");
            }

            var user = await Mediator.Send(viewModel.ToResetPasswordCommand());
            await user.LoginWithCookie(HttpContext, false);

            return RedirectToAction("Index", "Dashboard");
        }
    }
}
