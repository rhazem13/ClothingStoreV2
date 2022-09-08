using System;
using System.Collections.Generic;

namespace ClothingStoreV2.Models
{
    public partial class Category
    {
        public Category()
        {
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string? CategoryLabel { get; set; }
        public byte? TargetGender { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
