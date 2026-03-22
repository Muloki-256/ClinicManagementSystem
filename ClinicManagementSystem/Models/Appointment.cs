using ClinicManagementSystem.Models;
using System;

public class Appointment
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedDate { get; set; }

    // Navigation properties
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }

    // Computed properties (read-only)
    public string PatientName => Patient?.PersonInfo?.FullName;
    public string DoctorName => Doctor?.PersonInfo?.FullName;
}