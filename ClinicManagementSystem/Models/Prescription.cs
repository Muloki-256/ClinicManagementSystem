using System;

namespace ClinicManagementSystem.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }
        public int RecordId { get; set; }
        public int TabletId { get; set; }
        public string Dosage { get; set; }
        public string Duration { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
        public DateTime PrescribedDate { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual MedicalRecord MedicalRecord { get; set; }
        public virtual Tablet Tablet { get; set; }

        // Computed properties
        public string TabletName => Tablet?.TabletName ?? "";
        public decimal TotalCost => Tablet?.CostPerUnit * Quantity ?? 0;
    }
}