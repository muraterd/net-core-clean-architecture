using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Filters;
using WebCMS.Areas.Api.Features.User.Requests;
using WebCMS.Services.User;
using WebCMS.Services.User.Commands;
using WebCMS.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace WebCMS.Areas.Api.Features.User
{
    [Authorize]
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
