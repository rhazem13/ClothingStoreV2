using System;
using System.Collections.Generic;

namespace ClothingStoreV2.Models
{
    public partial class Item
    {
        public Item()
        {
            PurchaseItems = new HashSet<PurchaseItem>();
        }

        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public decimal? Price { get; set; }
        public string? ItemLabel { get; set; }
        public string? PhotoPath { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}
