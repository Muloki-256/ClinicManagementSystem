using System;

namespace ClinicManagementSystem.Models
{
    public class PaymentReceipt
    {
        public string ReceiptNumber { get; set; }
        public DateTime ReceiptDate { get; set; }
        public string PatientName { get; set; }
        public string PatientPhone { get; set; }
        public string PatientEmail { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionReference { get; set; }
        public string BillDescription { get; set; }
        public decimal BillTotal { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal NewBalance { get; set; }
        public string ReceivedBy { get; set; }
        public string ClinicName { get; set; } = "City Clinic Management System";
        public string ClinicAddress { get; set; } = "123 Medical Center, City, State 12345";
        public string ClinicPhone { get; set; } = "(555) 123-4567";

        // Computed properties for receipt
        public string AmountInWords => ConvertAmountToWords(AmountPaid);
        public string PaymentDateFormatted => ReceiptDate.ToString("MMMM dd, yyyy hh:mm tt");

        private string ConvertAmountToWords(decimal amount)
        {
            // Simple implementation - you can enhance this
            return $"{amount:C} Only";
        }
    }
}