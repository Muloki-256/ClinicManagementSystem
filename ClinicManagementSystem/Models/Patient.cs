namespace ClinicManagementSystem.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public int PersonId { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
        public string MedicalHistory { get; set; }
        public string InsuranceProvider { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation property
        public virtual Person PersonInfo { get; set; }

        // Computed properties for display
        public string FullName => PersonInfo?.FullName ?? "";
        public string Phone => PersonInfo?.Phone ?? "";
        public string Email => PersonInfo?.Email ?? "";
        public string Gender => PersonInfo?.Gender ?? "";
        public string Address => PersonInfo?.Address ?? "";
    }
}