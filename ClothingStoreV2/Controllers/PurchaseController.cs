using System.Security.Claims;
using ClothingStoreV2.Interfaces;
using ClothingStoreV2.Models;
using ClothingStoreV2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace ClothingStoreV2.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUserDatumRepository _userDatumRepository;
        public PurchaseController(UserManager<IdentityUser> userManager,IPurchaseRepository purchaseRepository,IUserDatumRepository userDatumRepository)
        {
            _userManager = userManager;
            _purchaseRepository = purchaseRepository;
            _userDatumRepository = userDatumRepository;
        }
        [Authorize]
        public async Task<IActionResult> Add(int id, short quantity)
        {
            var userIdentityId=User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!_userDatumRepository.userFilled(userIdentityId))
            {
                return RedirectToAction("FillData", "UserDatum");
            }

            int userId = _purchaseRepository.GetUserId(userIdentityId);
            bool haspurchase = await _purchaseRepository.HasPurchase(userIdentityId);
            int purchaseId=-1;
            if (!haspurchase)
            {
                Purchase purchase = new Purchase
                {
                    PurchaseDate =DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    UserId = userId,
                    TotalPrice = 0,
                    InProgress = true
                };
                purchase = await _purchaseRepository.Add(purchase);
                purchaseId = purchase.Id;
            }
            if(purchaseId==-1){
             purchaseId = await _purchaseRepository.getPurchaseId(userId);
            }
            _purchaseRepository.addItemsToPurchase(purchaseId,id,quantity);
            return RedirectToAction("MyPurchases", "Purchase");
        }
        public async Task<ViewResult> MyPurchases()
        {
            var userIdentityId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = _purchaseRepository.GetUserId(userIdentityId);
            bool haspurchase = await _purchaseRepository.HasPurchase(userIdentityId);
            if (!haspurchase)
            {
                Purchase purchase = new Purchase
                {
                    PurchaseDate = DateTime.Now,
                    UserId = userId,
                    TotalPrice = 0,
                    InProgress = true
                };
                _purchaseRepository.Add(purchase);
            }
            int purchaseId = await _purchaseRepository.getPurchaseId(userId);
            List<PurchaseItem> purchaseItems = _purchaseRepository.getPurchaseItems(purchaseId);
            List<PurchaseViewModel> purchaseViewModelList = new List<PurchaseViewModel>();
            foreach (PurchaseItem purchaseItem in purchaseItems)
            {
                PurchaseViewModel purchaseViewModel = new PurchaseViewModel
                {
                    itemId = purchaseItem.ItemId,
                    photoPath = _purchaseRepository.getItemPhotoPath(purchaseItem.ItemId),
                    price = _purchaseRepository.getItemPrice(purchaseItem.ItemId),
                    quantity = purchaseItem.Quantity ?? 0,
                    itemLabel = _purchaseRepository.getItemLabel(purchaseItem.ItemId)
                };
                purchaseViewModelList.Add(purchaseViewModel);
            }
            return View(purchaseViewModelList);
        }
        public async Task<IActionResult> increaseQuantity(int id)
        {
            var userIdentityId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = _purchaseRepository.GetUserId(userIdentityId);
            int purchaseId = await _purchaseRepository.getPurchaseId(userId);
            _purchaseRepository.changeCount(purchaseId ,id, true);
            return RedirectToAction("MyPurchases");
        }
        public async Task<IActionResult> decreaseQuantity(int id)
        {
            var userIdentityId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = _purchaseRepository.GetUserId(userIdentityId);
            int purchaseId = await _purchaseRepository.getPurchaseId(userId);
            _purchaseRepository.changeCount(purchaseId ,id, false);
            return RedirectToAction("MyPurchases");
        }
        public async Task<IActionResult> CheckOut()
        {
            var userIdentityId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = _purchaseRepository.GetUserId(userIdentityId);
            int purchaseId = await _purchaseRepository.getPurchaseId(userId);
            if (_purchaseRepository.isEmpty(purchaseId))
            {
                return RedirectToAction("EmptyCart", "Error");
            }
            _purchaseRepository.checkOut(purchaseId);
            return View();
        }
    }
}
