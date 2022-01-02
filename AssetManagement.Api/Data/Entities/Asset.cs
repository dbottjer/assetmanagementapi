using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AssetManagement.Api.Data.Entities
{
    public partial class Asset
    {
        public Asset()
        {
            AssetAssignments = new HashSet<AssetAssignment>();
        }

        public int AssetId { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string AssetTag { get; set; }
        public string SerialNumber { get; set; }
        public decimal PurchasePrice { get; set; }
        public int ManufacturedYear { get; set; }
        public string PurchasedFrom { get; set; }
        public DateTime WarrantyExpiration { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<AssetAssignment> AssetAssignments { get; set; }
    }
}
