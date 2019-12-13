using System;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Data;

namespace WebCMS.Areas.Admin.Features.Home
{
    [Route("admin/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly AppDbContext dbContext;

        public HomeController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Selam admin");
        }

        [HttpGet("createdb")]
        public IActionResult CreateDb()
        {
            var result = dbContext.Database.EnsureCreated();

            var response = new
            {
                data = $"Creating db...",
                dbStatus = result ? "Created" : "Ready"
            };

            return Ok(response);
        }
    }
}
