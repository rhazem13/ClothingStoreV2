using System;
using System.Collections.Generic;
namespace ClothingStoreV2.Models
{
    public partial class Purchase
    {
        public Purchase()
        {
            PurchaseItems = new HashSet<PurchaseItem>();
        }
        public int Id { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? UserId { get; set; }
        public bool? InProgress { get; set; }
        public virtual UserDatum? User { get; set; }
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}
