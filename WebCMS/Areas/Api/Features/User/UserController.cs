using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Areas.Api.Filters;
using WebCMS.Areas.Api.Features.User.Requests;
using WebCMS.Services.User;
using WebCMS.Services.User.Commands;
using WebCMS.Controllers;

namespace WebCMS.Areas.Api.Features.User
{
    [ApiAuthorize]
    [Route("api/[controller]")]
    public class UserController : AuthorizedController
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpPatch]
        public IActionResult Update([FromBody] UpdateUserRequest request)
        {
            var command = Mapper.Map<UpdateUserCommand>(request);
            userService.UpdateUser(currentUser.Id, command);

            return Ok();
        }
    }
}
