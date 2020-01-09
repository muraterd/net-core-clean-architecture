using System;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Filters;

namespace WebCMS.Areas.Api.Features.Home
{
    [JwtAuthorize]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
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
