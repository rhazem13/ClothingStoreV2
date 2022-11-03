using System;
using System.Collections.Generic;
namespace ClothingStoreV2.Models
{
    public partial class PurchaseItem
    {
        public int PurchaseId { get; set; }
        public int ItemId { get; set; }
        public short? Quantity { get; set; }
        public decimal? Price { get; set; }
        public virtual Item Item { get; set; } = null!;
        public virtual Purchase Purchase { get; set; } = null!;
    }
}
