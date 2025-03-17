using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebUI.ViewComponents.Default
{
    public class _DefaultBillboardPartialComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
           return View();   
        }
    }
}
