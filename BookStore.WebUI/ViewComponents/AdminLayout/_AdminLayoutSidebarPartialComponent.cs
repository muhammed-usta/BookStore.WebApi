using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebUI.ViewComponents.AdminLayout
{
    public class _AdminLayoutSidebarPartialComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
