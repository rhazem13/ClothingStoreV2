using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreV2.Controllers
{
    public class ErrorController : Controller
    {
      

        public IActionResult EmptyCart()
        {
            return View();
        }

        public IActionResult InvalidDelete()
        {
            return View();
        }
    }
}
