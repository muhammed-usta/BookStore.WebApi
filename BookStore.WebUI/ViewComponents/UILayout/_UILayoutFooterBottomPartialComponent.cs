using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebUI.ViewComponents.UILayout
{
    public class _UILayoutFooterBottomPartialComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
