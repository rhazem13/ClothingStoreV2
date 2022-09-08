using ClothingStoreV2.Models;

namespace ClothingStoreV2.ViewModels
{
    public class ItemCreateViewModel
    {
        public int? CategoryId { get; set; }
        public decimal? Price { get; set; }
        public string? ItemLabel { get; set; }
        public IFormFile? Photo { get; set; }

        public List<Category>? Categories { get; set; }

    }
}
