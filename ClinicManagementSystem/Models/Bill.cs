using System;

namespace ClinicManagementSystem.Models
{
    public class Bill
    {
        public int BillId { get; set; }
        public int PatientId { get; set; }
        public int? AppointmentId { get; set; }
        public string BillType { get; set; } // Consultation, Medicine, Test, Procedure
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount => TotalAmount - PaidAmount;
        public string PaymentStatus => DueAmount <= 0 ? "Paid" : "Pending";
        public DateTime BillDate { get; set; } = DateTime.Now;
        public DateTime? PaymentDate { get; set; }
        public string PaymentMethod { get; set; }

        // Navigation properties
        public virtual Patient Patient { get; set; }
        public virtual Appointment Appointment { get; set; }

        // Computed properties
        public string PatientName => Patient?.FullName ?? "";
    }
}