using Microsoft.AspNetCore.Mvc;
namespace BookStore.WebUI.ViewComponents.UILayout
{
    public class _UILayoutHeaderPartialComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
