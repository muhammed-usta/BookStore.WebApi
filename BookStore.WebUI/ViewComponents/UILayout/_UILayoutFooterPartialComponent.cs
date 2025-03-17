using Microsoft.AspNetCore.Mvc;
namespace BookStore.WebUI.ViewComponents.UILayout
{
    public class _UILayoutFooterPartialComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
