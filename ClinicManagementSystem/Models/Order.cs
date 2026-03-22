using System;
using System.Collections.Generic;

namespace ClinicManagementSystem.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int PatientId { get; set; } // 0 for guest orders
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
        public decimal TotalAmount { get; set; }
        public string Notes { get; set; }
        public int CreatedBy { get; set; }

        // New properties for guest orders
        public bool IsGuestOrder { get; set; }
        public string GuestInfo { get; set; }

        // Navigation properties
        public virtual Patient Patient { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Computed properties
        public string PatientName
        {
            get
            {
                if (IsGuestOrder)
                    return string.IsNullOrEmpty(GuestInfo) ? "Guest Customer" : GuestInfo;
                else
                    return Patient?.FullName ?? "";
            }
        }

        public int TotalItems => OrderItems?.Count ?? 0;
        public string DisplayStatus => Status ?? "Pending";
    }
}