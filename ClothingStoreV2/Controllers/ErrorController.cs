using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreV2.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmptyCart()
        {
            return View();
        }
    }
}
