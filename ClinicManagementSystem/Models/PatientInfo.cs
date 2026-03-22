using System;

namespace ClinicManagementSystem.Models
{
    public class PatientInfo
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime LoginTime { get; set; } = DateTime.Now;

        public string DisplayInfo
        {
            get
            {
                var info = FullName;
                if (!string.IsNullOrEmpty(Phone)) info += $", Phone: {Phone}";
                if (!string.IsNullOrEmpty(Email)) info += $", Email: {Email}";
                return info;
            }
        }
    }
}