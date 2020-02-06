using System;
using Application.Services.User;
using Application.Services.User.Commands;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Areas.Admin.Features.Base;
using WebCMS.Areas.Admin.Features.Users.Requests;
using WebCMS.Data;

namespace WebCMS.Areas.Admin.Features.Users
{
    [Authorize]
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class UsersController : BaseController
    {
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }
    }
}
