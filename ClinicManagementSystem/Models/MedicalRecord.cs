using System;
using System.Collections.Generic;

namespace ClinicManagementSystem.Models
{
    public class MedicalRecord
    {
        public int RecordId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int? AppointmentId { get; set; }
        public string Diagnosis { get; set; }
        public string Symptoms { get; set; }
        public string TreatmentNotes { get; set; }
        public string TestsPerformed { get; set; }
        public string TestResults { get; set; }
        public DateTime RecordDate { get; set; } = DateTime.Now;
        public DateTime? FollowUpDate { get; set; }

        // Navigation properties
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual List<Prescription> Prescriptions { get; set; } = new List<Prescription>();

        // Computed properties for display (READ-ONLY)
        public string PatientName => Patient?.FullName ?? "";
        public string DoctorName => Doctor?.FullName ?? "";
    }
}