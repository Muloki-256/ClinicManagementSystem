using System;

namespace ClinicManagementSystem.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BillId { get; set; }
        public int PatientId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // Cash, Card, Insurance, Online
        public string TransactionReference { get; set; }
        public string Status { get; set; } // Pending, Verified, Completed, Failed, Refunded
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public DateTime? VerifiedDate { get; set; }
        public int VerifiedBy { get; set; } // UserId who verified
        public string Notes { get; set; }
        public string ReceiptNumber { get; set; }

        // Navigation properties
        public virtual Bill Bill { get; set; }
        public virtual Patient Patient { get; set; }

        // Computed properties
        public string PatientName => Patient?.FullName ?? "";
        public bool IsVerified => Status == "Verified" || Status == "Completed";
        public bool IsPartial => Amount > 0 && Bill?.DueAmount > 0;
        public string PaymentStatus => GetPaymentStatus();

        private string GetPaymentStatus()
        {
            return Status switch
            {
                "Pending" => "Pending Verification",
                "Verified" => "Verified - Ready for Processing",
                "Completed" => "Payment Completed",
                "Failed" => "Payment Failed",
                "Refunded" => "Payment Refunded",
                _ => "Unknown"
            };
        }
    }
}