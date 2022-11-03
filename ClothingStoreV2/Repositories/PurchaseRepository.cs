using ClothingStoreV2.Interfaces;
using ClothingStoreV2.Models;
using Microsoft.EntityFrameworkCore;
namespace ClothingStoreV2.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ClothingStoreContext _context;
        public PurchaseRepository(ClothingStoreContext context)
        {
            _context = context;
        }
        public async Task<bool> HasPurchase(string userIdentityId)
        {
            var userId = _context.UserData.FirstOrDefault(u => u.IdentityId == userIdentityId).Id;
            Purchase? purchase =
                await _context.Purchases.FirstOrDefaultAsync(p => ((p.UserId == userId) && (p.InProgress == true)));
            return purchase != null;
        }
        public async Task<Purchase> Add(Purchase purchase)
        {
            await _context.Purchases.AddAsync(purchase);
            _context.SaveChanges();
            return purchase;
        }
        public int GetUserId(string userIdentityId)
        {
            //returns 248
            var user = _context.UserData.FirstOrDefault(u => u.IdentityId == userIdentityId);
            return user.Id;
        }
        public async Task<int> getPurchaseId(int userId)
        {
            Purchase purchase =
                _context.Purchases.FirstOrDefault(p => ((p.UserId == userId) && (p.InProgress == true)));
            return purchase.Id;
        }
        public async Task<int> addItemsToPurchase(int purchaseId, int itemId, short quantity)
        {
            if (_context.PurchaseItems.FirstOrDefault(p => (p.PurchaseId == purchaseId) && (p.ItemId == itemId)) !=
                null)
            {
                var purchaaseitem =
                    _context.PurchaseItems.FirstOrDefault(p => (p.PurchaseId == purchaseId) && (p.ItemId == itemId));
                purchaaseitem.Quantity += quantity;
                _context.PurchaseItems.Update(purchaaseitem);
                _context.SaveChanges();
                return purchaseId;
            }
            PurchaseItem purchaseItem = new PurchaseItem
            {
                PurchaseId = purchaseId,
                ItemId = itemId,
                Quantity = quantity
            };
            _context.PurchaseItems.AddAsync(purchaseItem);
            _context.SaveChanges();
            return purchaseId;
        }
        public List<PurchaseItem> getPurchaseItems(int purchaseId)
        {
            List<PurchaseItem> purchaseItems = _context.PurchaseItems.Where(p => p.PurchaseId == purchaseId).ToList();
            return purchaseItems;
        }
        public string getItemPhotoPath(int id)
        {
            string photoPath = _context.Items.Find(id).PhotoPath;
            return photoPath;
        }
        public decimal? getItemPrice(int id)
        {
            decimal? price = _context.Items.Find(id).Price;
            return price;
        }
        public string getItemLabel(int id)
        {
            string itemlabel = _context.Items.Find(id).ItemLabel;
            return itemlabel;
        }
        public void changeCount(int purchaseId, int id, bool increase)
        {
            PurchaseItem item =
                _context.PurchaseItems.FirstOrDefault(i => ((i.ItemId == id) && (i.PurchaseId == purchaseId)));
            if (increase)
            {
                item.Quantity += 1;
            }
            else
            {
                item.Quantity -= 1;
            }
            _context.PurchaseItems.Update(item);
            _context.SaveChanges();
        }
        public void checkOut(int purchaseId)
        {
            Purchase purchase = _context.Purchases.Find(purchaseId);
            purchase.InProgress = false;
            _context.Purchases.Update(purchase);
            _context.SaveChanges();
        }
        public bool isEmpty(int purchaseId)
        {
            if (_context.PurchaseItems.Where(pi => pi.PurchaseId == purchaseId).Count() == 0)
            {
                return true;
            }
            return false;
        }
    }
}
