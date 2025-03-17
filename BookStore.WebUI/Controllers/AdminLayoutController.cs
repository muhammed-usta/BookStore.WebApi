using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Layout()
        {
            return View();
        }
    }
}
