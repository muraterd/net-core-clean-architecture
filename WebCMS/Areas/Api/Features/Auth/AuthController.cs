using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Areas.Api.Models;
using WebCMS.Controllers;
using WebCMS.Filters;
using System.Threading.Tasks;
using Application.Interfaces.Providers;
using MediatR;
using Application.MediatR.Auth.Commands;

namespace WebCMS.Areas.Api.Features.Auth
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IMediator mediator;
        private readonly ITokenProvider tokenProvider;

        public AuthController(IMediator mediator, ITokenProvider tokenProvider)
        {
            this.mediator = mediator;
            this.tokenProvider = tokenProvider;
        }

        [ValidateModel]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var user = await mediator.Send(command);

            var signedToken = tokenProvider.Sign(user);

            UserModel userModel = Mapper.Map<UserModel>(user);

            return Ok(new { signedToken.AccessToken, signedToken.ExpiresIn, User = userModel });
        }

        [ValidateModel]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            var user = await mediator.Send(command);

            var signedToken = tokenProvider.Sign(user);

            UserModel userModel = Mapper.Map<UserModel>(user);

            return Ok(new { signedToken.AccessToken, signedToken.ExpiresIn, User = userModel });
        }
    }
}
