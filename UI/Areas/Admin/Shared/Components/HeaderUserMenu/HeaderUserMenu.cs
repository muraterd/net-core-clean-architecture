using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Shared.Components.HeaderUserMenu
{
    public class HeaderUserMenu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
