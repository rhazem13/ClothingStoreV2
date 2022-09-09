using ClothingStoreV2.Models;

namespace ClothingStoreV2.Interfaces
{
    public interface IItemRepository
    {
        Item Add(Item item);
        IQueryable<Item> GetAll();
        IQueryable<Item> GetByCategory(int catid);
        Item Get(int id);

        Item Update(Item itemChanges);
        List<Category> GetAllCategories();

        bool Delete(int id);
    }
}
