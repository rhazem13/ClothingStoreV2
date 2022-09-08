using ClothingStoreV2.Models;

namespace ClothingStoreV2.ViewModels
{
    public class ItemEditViewModel : ItemCreateViewModel
    {
        public int Id { get; set; }
        public string? ExistingPhotoPath { get; set; }

        public List<Category>? Categories { get; set; }
    }
}
