using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebCMS.Areas.Web.Features.Home
{
    [Route("/")]
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