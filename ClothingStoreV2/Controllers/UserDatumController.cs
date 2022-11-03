using System.Security.Claims;
using ClothingStoreV2.Interfaces;
using ClothingStoreV2.Models;
using ClothingStoreV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStoreV2.Controllers
{
    public class UserDatumController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserDatumRepository _userDatumRepository;
        public UserDatumController(UserManager<IdentityUser> userManager,IUserDatumRepository userDatumRepository)
        {
            _userManager = userManager;
            _userDatumRepository = userDatumRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ViewResult FillData()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FillData(UserDatum userDatum)
        {
            string myid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            userDatum.IdentityId=myid;
            _userDatumRepository.Create(userDatum);
           
            return RedirectToAction("All","Home",1);
        }
    }
}
