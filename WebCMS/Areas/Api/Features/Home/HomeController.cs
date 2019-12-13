using System;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Areas.Api.Filters;

namespace WebCMS.Areas.Api.Features.Home
{
    [ApiAuthorize]
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
