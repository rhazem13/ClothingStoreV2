using ClothingStoreV2.Models;

namespace ClothingStoreV2.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<bool> HasPurchase(string userIdentityId);
        Task<Purchase> Add(Purchase purchase);
        int GetUserId(string userIdentityId);
        Task<int> getPurchaseId(int userId);
        Task<int> addItemsToPurchase(int purchaseId, int itemId, short quantity);

        List<PurchaseItem> getPurchaseItems(int purchaseId);

        string getItemPhotoPath(int id);
        decimal? getItemPrice(int id);
        string getItemLabel(int id);

        void changeCount(int purchaseId,int id, bool increase);

        void checkOut(int purchaseId);

        bool isEmpty(int purchaseId);
    }
}
