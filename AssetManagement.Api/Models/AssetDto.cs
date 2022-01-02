using System;
namespace AssetManagement.Api.Models
{
    public class AssetDto
    {
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
    }
}
