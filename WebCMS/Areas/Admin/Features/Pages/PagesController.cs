using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCMS.Areas.Admin.Features.Users.Requests;
using WebCMS.Areas.Admin.Models.Page;
using WebCMS.Controllers;
using WebCMS.Data;
using WebCMS.Services.Page;
using WebCMS.Services.Page.Commands;

namespace WebCMS.Areas.Admin.Features.Users
{
    [Authorize]
    [Area("Admin")]
    [Route("admin/[controller]")]
    public class PagesController : Controller
    {
        private readonly PageService pageService;

        public PagesController(PageService pageService)
        {
            this.pageService = pageService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var pages = pageService.GetPages(1, 10).ToList();

            var model = Mapper.Map<List<BasePageModel>>(pages);

            return View(model);
        }

        [HttpGet("{id}")]
        public IActionResult Update(long id)
        {
            var page = pageService.GetPage(id);
            if(page == null)
            {
                return NotFound();
            }
            var model = Mapper.Map<PageModel>(page);

            return View(model);
        }

        [HttpPost("{id}")]
        public IActionResult Update(long id, PageModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var command = Mapper.Map<UpdatePageCommand>(model);
            pageService.UpdatePage(id, command);

            return View(model);
        }

        [HttpGet("new")]
        public IActionResult Create()
        {
            return View(new PageModel());
        }

        [HttpPost("new")]
        public IActionResult Create(PageModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var command = Mapper.Map<CreatePageCommand>(model);
            pageService.CreatePage(command);

            return RedirectToAction("List");
        }

        [HttpGet("delete/{id}")]
        public IActionResult Delete(long id)
        {
            pageService.DeletePage(id);
            return RedirectToAction("List");
        }
    }
}
