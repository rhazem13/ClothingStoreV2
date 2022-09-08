using System;
using System.Collections.Generic;

namespace ClothingStoreV2.Models
{
    public partial class UserDatum
    {
        public UserDatum()
        {
            Purchases = new HashSet<Purchase>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Email { get; set; }
        public string? ZipCode { get; set; }
        public string? City { get; set; }
        public int? Age { get; set; }
        public bool? Gender { get; set; }
        public string? Phone { get; set; }
        public string? IdentityId { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
