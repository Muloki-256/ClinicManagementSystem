using System;

namespace ClinicManagementSystem.Models
{
    public class Tablet
    {
        public int TabletId { get; set; }
        public string TabletName { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public decimal CostPerUnit { get; set; }
        public int StockQuantity { get; set; }
        public int MinimumStockLevel { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Computed properties (read-only)
        public bool IsLowStock => StockQuantity <= MinimumStockLevel;

        public string StockStatus
        {
            get
            {
                if (StockQuantity == 0) return "Out of Stock";
                if (IsLowStock) return "Low Stock";
                return "In Stock";
            }
        }

        public string DisplayName => $"{TabletName} - {Manufacturer}";

        public decimal TotalValue => CostPerUnit * StockQuantity;
    }
}