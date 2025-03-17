using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebUI.ViewComponents.AdminLayout
{
    public class _AdminLayoutHeadPartialComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
