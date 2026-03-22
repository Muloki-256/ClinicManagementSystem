using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ClinicManagementSystem.Managers
{
    public class AppointmentManager
    {
        private BaseRepository repository;

        public AppointmentManager()
        {
            repository = new BaseRepository();
        }

        public List<Appointment> GetAppointmentsByDoctor(int doctorId, DateTime? date = null)
        {
            var appointments = new List<Appointment>();

            try
            {
                string query = @"
                    SELECT a.*, 
                           p_pat.FirstName as PatientFirstName, p_pat.LastName as PatientLastName,
                           p_doc.FirstName as DoctorFirstName, p_doc.LastName as DoctorLastName,
                           pat.PatientId, doc.DoctorId
                    FROM Appointments a
                    INNER JOIN Patients pat ON a.PatientId = pat.PatientId
                    INNER JOIN Persons p_pat ON pat.PersonId = p_pat.PersonId
                    INNER JOIN Doctors doc ON a.DoctorId = doc.DoctorId
                    INNER JOIN Persons p_doc ON doc.PersonId = p_doc.PersonId
                    WHERE 1=1";

                var parameters = new List<MySqlParameter>();

                // If doctorId is 0, show all doctors, otherwise filter by specific doctor
                if (doctorId > 0)
                {
                    query += " AND a.DoctorId = @DoctorId";
                    parameters.Add(new MySqlParameter("@DoctorId", doctorId));
                }

                if (date.HasValue)
                {
                    query += " AND DATE(a.AppointmentDate) = DATE(@AppointmentDate)";
                    parameters.Add(new MySqlParameter("@AppointmentDate", date.Value));
                }

                query += " ORDER BY a.AppointmentDate DESC";

                var dataTable = repository.ExecuteQuery(query, parameters.ToArray());

                foreach (DataRow row in dataTable.Rows)
                {
                    var appointment = new Appointment
                    {
                        AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                        PatientId = Convert.ToInt32(row["PatientId"]),
                        DoctorId = Convert.ToInt32(row["DoctorId"]),
                        AppointmentDate = Convert.ToDateTime(row["AppointmentDate"]),
                        Status = row["Status"].ToString(),
                        Reason = row["Reason"]?.ToString(),
                        Notes = row["Notes"]?.ToString(),
                        CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"]) : DateTime.Now
                    };

                    // Create display objects instead of trying to set read-only properties
                    appointment.Patient = new Patient
                    {
                        PatientId = appointment.PatientId,
                        PersonInfo = new Person
                        {
                            FirstName = row["PatientFirstName"]?.ToString(),
                            LastName = row["PatientLastName"]?.ToString()
                        }
                    };

                    appointment.Doctor = new Doctor
                    {
                        DoctorId = appointment.DoctorId,
                        PersonInfo = new Person
                        {
                            FirstName = row["DoctorFirstName"]?.ToString(),
                            LastName = row["DoctorLastName"]?.ToString()
                        }
                    };

                    appointments.Add(appointment);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading appointments: {ex.Message}", ex);
            }

            return appointments;
        }

        public OperationResult CreateAppointment(Appointment appointment)
        {
            try
            {
                // Validate appointment time is not in the past
                if (appointment.AppointmentDate < DateTime.Now)
                {
                    return OperationResult.ErrorResult("Appointment date cannot be in the past.");
                }

                // Check for scheduling conflicts
                string conflictQuery = @"
                    SELECT COUNT(*) FROM Appointments 
                    WHERE DoctorId = @DoctorId 
                    AND AppointmentDate = @AppointmentDate 
                    AND Status NOT IN ('Cancelled', 'NoShow')";

                var conflictParams = new[]
                {
                    new MySqlParameter("@DoctorId", appointment.DoctorId),
                    new MySqlParameter("@AppointmentDate", appointment.AppointmentDate)
                };

                var conflictCount = Convert.ToInt32(repository.ExecuteScalar(conflictQuery, conflictParams));
                if (conflictCount > 0)
                {
                    return OperationResult.ErrorResult("Doctor is not available at the selected time.");
                }

                string query = @"
                    INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Status, Reason, Notes, CreatedDate)
                    VALUES (@PatientId, @DoctorId, @AppointmentDate, @Status, @Reason, @Notes, @CreatedDate)";

                var parameters = new[]
                {
                    new MySqlParameter("@PatientId", appointment.PatientId),
                    new MySqlParameter("@DoctorId", appointment.DoctorId),
                    new MySqlParameter("@AppointmentDate", appointment.AppointmentDate),
                    new MySqlParameter("@Status", appointment.Status ?? "Scheduled"),
                    new MySqlParameter("@Reason", appointment.Reason ?? (object)DBNull.Value),
                    new MySqlParameter("@Notes", appointment.Notes ?? (object)DBNull.Value),
                    new MySqlParameter("@CreatedDate", DateTime.Now)
                };

                int rowsAffected = repository.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    return OperationResult.SuccessResult("Appointment created successfully.");
                }
                else
                {
                    return OperationResult.ErrorResult("Failed to create appointment.");
                }
            }
            catch (MySqlException ex)
            {
                return OperationResult.ErrorResult($"Database error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error creating appointment: {ex.Message}");
            }
        }

        public OperationResult UpdateAppointmentStatus(int appointmentId, string status)
        {
            try
            {
                // Validate status
                string[] validStatuses = { "Scheduled", "Completed", "Cancelled", "NoShow" };
                if (Array.IndexOf(validStatuses, status) == -1)
                {
                    return OperationResult.ErrorResult("Invalid appointment status.");
                }

                string query = "UPDATE Appointments SET Status = @Status, UpdatedDate = @UpdatedDate WHERE AppointmentId = @AppointmentId";

                var parameters = new[]
                {
                    new MySqlParameter("@Status", status),
                    new MySqlParameter("@AppointmentId", appointmentId),
                    new MySqlParameter("@UpdatedDate", DateTime.Now)
                };

                int rowsAffected = repository.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    return OperationResult.SuccessResult($"Appointment status updated to '{status}' successfully.");
                }
                else
                {
                    return OperationResult.ErrorResult("Appointment not found or no changes made.");
                }
            }
            catch (MySqlException ex)
            {
                // Handle specific MySQL errors
                switch (ex.Number)
                {
                    case 0:
                        return OperationResult.ErrorResult("Cannot connect to database. Please check your connection.");
                    case 1045:
                        return OperationResult.ErrorResult("Invalid database credentials.");
                    case 1146:
                        return OperationResult.ErrorResult("Appointments table not found.");
                    default:
                        return OperationResult.ErrorResult($"Database error: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error updating appointment status: {ex.Message}");
            }
        }

        public Appointment GetAppointmentById(int appointmentId)
        {
            try
            {
                string query = @"
                    SELECT a.*, 
                           p_pat.FirstName as PatientFirstName, p_pat.LastName as PatientLastName,
                           p_doc.FirstName as DoctorFirstName, p_doc.LastName as DoctorLastName
                    FROM Appointments a
                    INNER JOIN Patients pat ON a.PatientId = pat.PatientId
                    INNER JOIN Persons p_pat ON pat.PersonId = p_pat.PersonId
                    INNER JOIN Doctors doc ON a.DoctorId = doc.DoctorId
                    INNER JOIN Persons p_doc ON doc.PersonId = p_doc.PersonId
                    WHERE a.AppointmentId = @AppointmentId";

                var parameters = new[] { new MySqlParameter("@AppointmentId", appointmentId) };

                var dataTable = repository.ExecuteQuery(query, parameters);

                if (dataTable.Rows.Count == 0)
                    return null;

                DataRow row = dataTable.Rows[0];

                var appointment = new Appointment
                {
                    AppointmentId = Convert.ToInt32(row["AppointmentId"]),
                    PatientId = Convert.ToInt32(row["PatientId"]),
                    DoctorId = Convert.ToInt32(row["DoctorId"]),
                    AppointmentDate = Convert.ToDateTime(row["AppointmentDate"]),
                    Status = row["Status"].ToString(),
                    Reason = row["Reason"]?.ToString(),
                    Notes = row["Notes"]?.ToString(),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                };

                // Set navigation properties
                appointment.Patient = new Patient
                {
                    PatientId = appointment.PatientId,
                    PersonInfo = new Person
                    {
                        FirstName = row["PatientFirstName"]?.ToString(),
                        LastName = row["PatientLastName"]?.ToString()
                    }
                };

                appointment.Doctor = new Doctor
                {
                    DoctorId = appointment.DoctorId,
                    PersonInfo = new Person
                    {
                        FirstName = row["DoctorFirstName"]?.ToString(),
                        LastName = row["DoctorLastName"]?.ToString()
                    }
                };

                return appointment;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting appointment: {ex.Message}", ex);
            }
        }
    }
}