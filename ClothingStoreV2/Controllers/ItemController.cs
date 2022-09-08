using ClothingStoreV2.Interfaces;
using ClothingStoreV2.Models;
using ClothingStoreV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace ClothingStoreV2.Controllers
{
    public class ItemController : Controller
    {
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment;
        private readonly IItemRepository itemRepository;

        public ItemController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment,
            IItemRepository itemRepository)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.itemRepository = itemRepository;
        }
        public IActionResult Index()
        {
            return RedirectToAction("Index","Home");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ViewResult CreateItem()
        {
            ItemCreateViewModel itemCreateViewModel = new ItemCreateViewModel();
            itemCreateViewModel.Categories = itemRepository.GetAllCategories();
            return View(itemCreateViewModel);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult CreateItem(ItemCreateViewModel itemCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(itemCreateViewModel);
                Item item = new Item
                {
                    ItemLabel = itemCreateViewModel.ItemLabel,
                    CategoryId = itemCreateViewModel.CategoryId,
                    Price = itemCreateViewModel.Price,
                    PhotoPath = uniqueFileName
                };
                itemRepository.Add(item);
                return RedirectToAction("Index","Home");

            }

            return View("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ViewResult EditItem(int id)
        {
            Item item = itemRepository.Get(id);
            ItemEditViewModel itemEditViewModel = new ItemEditViewModel
            {
                Id = item.Id,
                ItemLabel = item.ItemLabel,
                CategoryId = item.CategoryId,
                Price = item.Price,
                ExistingPhotoPath = item.PhotoPath,
                Categories = itemRepository.GetAllCategories()
            };
            return View(itemEditViewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EditItem(ItemEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Item item = itemRepository.Get(model.Id);
                item.ItemLabel=model.ItemLabel;
                item.CategoryId=model.CategoryId;
                item.Price=model.Price;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "images",
                            model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    item.PhotoPath = ProcessUploadedFile(model);
                }

                itemRepository.Update(item);
                return RedirectToAction("Index","Home");
            }
            return View(model);
        }

        [HttpGet]
        public ViewResult Buy(int id)
        {
            Item item = itemRepository.Get(id);
            ItemBuyViewModel itemBuyViewModel = new ItemBuyViewModel
            {
                PhotoPath = item.PhotoPath,
                ItemLabel = item.ItemLabel,
                itemPrice = item.Price,
                itemId = item.Id,
                quantity = 0
            };
            return View(itemBuyViewModel);
        }
        private string ProcessUploadedFile(ItemCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {

                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }
        public IActionResult DeleteItem(int id)
        {
            itemRepository.Delete(id);
            return RedirectToAction("Index");
        }
       
    }
}
