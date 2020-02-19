using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebCMS.Areas.Admin.Views.Components.PopularPosts
{
    public class LeftMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
