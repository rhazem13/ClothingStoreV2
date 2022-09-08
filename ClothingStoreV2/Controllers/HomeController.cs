using ClothingStoreV2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ClothingStoreV2.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ClothingStoreV2.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IItemRepository _itemRepository;

        public HomeController(ILogger<HomeController> logger,RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,IItemRepository itemRepository)
        {
            _logger = logger;
            _roleManager = roleManager;
            _userManager = userManager;
            _itemRepository = itemRepository;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(string? SearchString)
        {
            //this should all happen onc the application start and check first if they already exist if not create them
            //here we create user and admin rules
            //_roleManager.CreateAsync(new IdentityRole("user")).Wait();
            //_roleManager.CreateAsync(new IdentityRole("admin")).Wait();
            //var user = await _userManager.FindByEmailAsync("rhazem13@yahoo.com");
            //await _userManager.AddToRoleAsync(user, "admin");
            List<Item> items = _itemRepository.GetAll().ToList();
           
            return View(items);
        }

        public async Task<IActionResult> All(string? SearchString,int pageNumber=1)
        {
            IQueryable<Item> items = _itemRepository.GetAll();
            if (!String.IsNullOrEmpty(SearchString))
            {
                items = items.Where(i => i.ItemLabel.ToLower().Contains(SearchString.ToLower()));
            }
            return View(await PaginatedList<Item>.CreateAsync(items,pageNumber,12));

        }
        public async Task<IActionResult> BrowseCategory(int id,int pageNumber=1)
        {
            IQueryable<Item> items = _itemRepository.GetByCategory(id);

            return View(await PaginatedList<Item>.CreateAsync(items, pageNumber, 12));
        }
        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}