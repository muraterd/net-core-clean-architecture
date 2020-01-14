﻿using System;
using Application.Services.User;
using Application.Services.User.Commands;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Areas.Admin.Features.Users.Requests;
using WebCMS.Controllers;
using WebCMS.Data;

namespace WebCMS.Areas.Admin.Features.Users
{
    [Authorize]
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class UsersController : Controller
    {
        private readonly UserService userService;

        public UsersController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpPatch("{id}")]
        public IActionResult Update(long id, [FromBody] UpdateUserRequest request)
        {
            var command = Mapper.Map<UpdateUserCommand>(request);
            userService.UpdateUser(id, command);

            return Ok();
        }
    }
}
