using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Areas.Web.Models.Page;

namespace WebCMS.Areas.Web.Features.Page
{
    [Area("Web")]
    [Route("/[controller]")]
    public class PagesController: Controller
    {
        [HttpGet("{slug}")]
        public IActionResult Index(String slug)
        {
            //var page = pageService.GetPage(slug);
            //var pageModel = Mapper.Map<PageModel>(page);

            //return View(pageModel);

            // TODO: Mediatra taşınacak

            return View();
        }
    }
}
