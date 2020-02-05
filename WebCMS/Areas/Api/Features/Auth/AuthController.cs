using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Areas.Api.Models;
using System.Threading.Tasks;
using Application.Interfaces.Providers;
using Application.MediatR.Auth.Commands.Login;
using Application.MediatR.Auth.Commands.Register;
using System;

namespace WebCMS.Areas.Api.Features.Auth
{
    [Route("api/[controller]")]
    public class AuthController : ApiBaseController
    {
        private readonly ITokenProvider tokenProvider;

        public AuthController(ITokenProvider tokenProvider)
        {
            this.tokenProvider = tokenProvider;
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
