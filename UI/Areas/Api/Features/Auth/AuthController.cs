using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UI.Areas.Api.Models;
using System.Threading.Tasks;
using Application.Interfaces.Providers;
using Application.MediatR.Api.Auth.Commands.Register;
using Application.MediatR.Common.Auth.Commands;
using Microsoft.Extensions.Localization;

namespace UI.Areas.Api.Features.Auth
{
    [Route("api/[controller]")]
    public class AuthController : ApiBaseController
    {
        private readonly ITokenProvider tokenProvider;
        private readonly IStringLocalizer<AuthController> localizer;

        public AuthController(ITokenProvider tokenProvider, IStringLocalizer<AuthController> localizer)
        {
            this.tokenProvider = tokenProvider;
            this.localizer = localizer;

            var hede = localizer["Hello"];
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var user = await Mediator.Send(command);

            var signedToken = tokenProvider.Sign(user);

            UserModel userModel = Mapper.Map<UserModel>(user);

            return Ok(new { signedToken.AccessToken, signedToken.ExpiresIn, User = userModel });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var user = await Mediator.Send(command);

            var signedToken = tokenProvider.Sign(user);

            UserModel userModel = Mapper.Map<UserModel>(user);

            return Ok(new { signedToken.AccessToken, signedToken.ExpiresIn, User = userModel });
        }
    }
}
