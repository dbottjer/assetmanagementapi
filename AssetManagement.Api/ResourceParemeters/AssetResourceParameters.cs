using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetManagement.Api.ResourceParemeters
{
    public class AssetResourceParameters
    {
        const int maxPageSize = 20;
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string AssetTag { get; set; }
        public string SerialNumber { get; set; }
        public string PurchasedFrom { get; set; }
        public int? YearManufactured { get; set; }

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }

        public AssetResourceParameters()
        {

        }
    }
}
