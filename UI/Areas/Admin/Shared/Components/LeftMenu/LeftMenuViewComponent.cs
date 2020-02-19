using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Views.Components.PopularPosts
{
    public class LeftMenuViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
