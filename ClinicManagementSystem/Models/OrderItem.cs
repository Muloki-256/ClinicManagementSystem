namespace ClinicManagementSystem.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int TabletId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => Quantity * UnitPrice;

        // Navigation properties
        public virtual Order Order { get; set; }
        public virtual Tablet Tablet { get; set; }

        // Computed properties
        public string TabletName => Tablet?.TabletName ?? "";
        public string TabletDescription => Tablet?.Description ?? "";
    }
}