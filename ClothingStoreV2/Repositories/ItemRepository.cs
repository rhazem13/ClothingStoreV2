using ClothingStoreV2.Interfaces;
using ClothingStoreV2.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ClothingStoreV2.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ClothingStoreContext context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ItemRepository(ClothingStoreContext context, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public Item Add(Item item)
        {
            context.Items.Add(item);
            context.SaveChanges();
            return item;
        }

        public IQueryable<Item> GetAll()
        {
            return context.Items;
        }

        public IQueryable<Item> GetByCategory(int catid)
        {
            return context.Items.Where(i => i.CategoryId == catid);
        }

        public Item Get(int id)
        {
            return context.Items.Find(id);
        }

        public Item Update(Item itemChanges)
        {
            var item = context.Items.Attach(itemChanges);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return itemChanges;
        }

        public List<Category> GetAllCategories()
        {
            return context.Categories.ToList();
        }

        public bool Delete(int id)
        {
            
            try
            {
                var item = context.Items.Find(id);
                if (context.PurchaseItems.Any(pi => pi.Item == item))
                {
                    return false;
                }
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", item.PhotoPath);
                System.IO.File.Delete(filePath);
                context.Items.Remove(item);
                context.SaveChanges();
                return true;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException)
            {
                return false;
            }
        }
    }
}
