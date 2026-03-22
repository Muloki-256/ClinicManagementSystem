namespace ClinicManagementSystem.Models
{
    public class Doctor
    {
        public int DoctorId { get; set; }
        public int PersonId { get; set; }
        public string Specialization { get; set; }
        public string LicenseNumber { get; set; }
        public string Qualifications { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal ConsultationFee { get; set; }

        // Navigation properties
        public virtual Person PersonInfo { get; set; }

        // Computed properties
        public string FullName => PersonInfo?.FullName ?? "";
        public string Phone => PersonInfo?.Phone ?? "";
        public string Email => PersonInfo?.Email ?? "";
        public string Gender => PersonInfo?.Gender ?? "";
        public string Address => PersonInfo?.Address ?? "";
    }
}