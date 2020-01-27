﻿using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Filters;

namespace WebCMS.Areas.Api.Features.Home
{
    [JwtAuthorize]
    [Route("api/[controller]")]
    public class HomeController : ApiBaseController
    {
        public HomeController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Home");
        }
    }
}
