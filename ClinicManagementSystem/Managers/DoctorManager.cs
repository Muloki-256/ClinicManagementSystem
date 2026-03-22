using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ClinicManagementSystem.Managers
{
    public class DoctorManager
    {
        private BaseRepository repository;

        public DoctorManager()
        {
            repository = new BaseRepository();
        }

        public List<Doctor> GetAllDoctors()
        {
            var doctors = new List<Doctor>();

            string query = @"
                SELECT d.*, p.* 
                FROM Doctors d 
                INNER JOIN Persons p ON d.PersonId = p.PersonId 
                ORDER BY p.FirstName, p.LastName";

            var dataTable = repository.ExecuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                doctors.Add(MapDoctorFromDataRow(row));
            }

            return doctors;
        }

        public Doctor GetDoctorById(int doctorId)
        {
            string query = @"
                SELECT d.*, p.* 
                FROM Doctors d 
                INNER JOIN Persons p ON d.PersonId = p.PersonId 
                WHERE d.DoctorId = @DoctorId";

            var parameters = new[] { new MySqlParameter("@DoctorId", doctorId) };
            var dataTable = repository.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count == 0)
                return null;

            return MapDoctorFromDataRow(dataTable.Rows[0]);
        }

        public OperationResult CreateDoctor(Doctor doctor)
        {
            try
            {
                // First create the person record
                string personQuery = @"
                    INSERT INTO Persons (FirstName, LastName, DateOfBirth, Gender, Phone, Email, Address, EmergencyContact, CreatedDate)
                    VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @Phone, @Email, @Address, @EmergencyContact, @CreatedDate);
                    SELECT LAST_INSERT_ID();";

                var personParameters = new[]
                {
                    new MySqlParameter("@FirstName", doctor.PersonInfo.FirstName),
                    new MySqlParameter("@LastName", doctor.PersonInfo.LastName),
                    new MySqlParameter("@DateOfBirth", doctor.PersonInfo.DateOfBirth),
                    new MySqlParameter("@Gender", doctor.PersonInfo.Gender ?? (object)DBNull.Value),
                    new MySqlParameter("@Phone", doctor.PersonInfo.Phone ?? (object)DBNull.Value),
                    new MySqlParameter("@Email", doctor.PersonInfo.Email ?? (object)DBNull.Value),
                    new MySqlParameter("@Address", doctor.PersonInfo.Address ?? (object)DBNull.Value),
                    new MySqlParameter("@EmergencyContact", doctor.PersonInfo.EmergencyContact ?? (object)DBNull.Value),
                    new MySqlParameter("@CreatedDate", DateTime.Now)
                };

                var personId = Convert.ToInt32(repository.ExecuteScalar(personQuery, personParameters));

                // Then create the doctor record
                string doctorQuery = @"
                    INSERT INTO Doctors (PersonId, Specialization, LicenseNumber, Qualifications, Department, IsActive, ConsultationFee)
                    VALUES (@PersonId, @Specialization, @LicenseNumber, @Qualifications, @Department, @IsActive, @ConsultationFee)";

                var doctorParameters = new[]
                {
                    new MySqlParameter("@PersonId", personId),
                    new MySqlParameter("@Specialization", doctor.Specialization),
                    new MySqlParameter("@LicenseNumber", doctor.LicenseNumber),
                    new MySqlParameter("@Qualifications", doctor.Qualifications ?? (object)DBNull.Value),
                    new MySqlParameter("@Department", doctor.Department ?? (object)DBNull.Value),
                    new MySqlParameter("@IsActive", doctor.IsActive),
                    new MySqlParameter("@ConsultationFee", doctor.ConsultationFee)
                };

                repository.ExecuteNonQuery(doctorQuery, doctorParameters);

                return OperationResult.SuccessResult("Doctor created successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error creating doctor: {ex.Message}");
            }
        }

        public OperationResult UpdateDoctor(Doctor doctor)
        {
            try
            {
                // Update person record
                string personQuery = @"
                    UPDATE Persons 
                    SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, 
                        Gender = @Gender, Phone = @Phone, Email = @Email, Address = @Address, 
                        EmergencyContact = @EmergencyContact
                    WHERE PersonId = @PersonId";

                var personParameters = new[]
                {
                    new MySqlParameter("@FirstName", doctor.PersonInfo.FirstName),
                    new MySqlParameter("@LastName", doctor.PersonInfo.LastName),
                    new MySqlParameter("@DateOfBirth", doctor.PersonInfo.DateOfBirth),
                    new MySqlParameter("@Gender", doctor.PersonInfo.Gender ?? (object)DBNull.Value),
                    new MySqlParameter("@Phone", doctor.PersonInfo.Phone ?? (object)DBNull.Value),
                    new MySqlParameter("@Email", doctor.PersonInfo.Email ?? (object)DBNull.Value),
                    new MySqlParameter("@Address", doctor.PersonInfo.Address ?? (object)DBNull.Value),
                    new MySqlParameter("@EmergencyContact", doctor.PersonInfo.EmergencyContact ?? (object)DBNull.Value),
                    new MySqlParameter("@PersonId", doctor.PersonId)
                };

                repository.ExecuteNonQuery(personQuery, personParameters);

                // Update doctor record
                string doctorQuery = @"
                    UPDATE Doctors 
                    SET Specialization = @Specialization, LicenseNumber = @LicenseNumber, 
                        Qualifications = @Qualifications, Department = @Department, 
                        IsActive = @IsActive, ConsultationFee = @ConsultationFee
                    WHERE DoctorId = @DoctorId";

                var doctorParameters = new[]
                {
                    new MySqlParameter("@Specialization", doctor.Specialization),
                    new MySqlParameter("@LicenseNumber", doctor.LicenseNumber),
                    new MySqlParameter("@Qualifications", doctor.Qualifications ?? (object)DBNull.Value),
                    new MySqlParameter("@Department", doctor.Department ?? (object)DBNull.Value),
                    new MySqlParameter("@IsActive", doctor.IsActive),
                    new MySqlParameter("@ConsultationFee", doctor.ConsultationFee),
                    new MySqlParameter("@DoctorId", doctor.DoctorId)
                };

                repository.ExecuteNonQuery(doctorQuery, doctorParameters);

                return OperationResult.SuccessResult("Doctor updated successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error updating doctor: {ex.Message}");
            }
        }

        public OperationResult DeleteDoctor(int doctorId)
        {
            try
            {
                // First get the person ID
                string getPersonQuery = "SELECT PersonId FROM Doctors WHERE DoctorId = @DoctorId";
                var getPersonParams = new[] { new MySqlParameter("@DoctorId", doctorId) };
                var personId = Convert.ToInt32(repository.ExecuteScalar(getPersonQuery, getPersonParams));

                // Delete the doctor record
                string doctorQuery = "DELETE FROM Doctors WHERE DoctorId = @DoctorId";
                var doctorParams = new[] { new MySqlParameter("@DoctorId", doctorId) };
                repository.ExecuteNonQuery(doctorQuery, doctorParams);

                // Delete the person record
                string personQuery = "DELETE FROM Persons WHERE PersonId = @PersonId";
                var personParams = new[] { new MySqlParameter("@PersonId", personId) };
                repository.ExecuteNonQuery(personQuery, personParams);

                return OperationResult.SuccessResult("Doctor deleted successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error deleting doctor: {ex.Message}");
            }
        }

        private Doctor MapDoctorFromDataRow(DataRow row)
        {
            var person = new Person
            {
                PersonId = Convert.ToInt32(row["PersonId"]),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                DateOfBirth = Convert.ToDateTime(row["DateOfBirth"]),
                Gender = row["Gender"]?.ToString(),
                Phone = row["Phone"]?.ToString(),
                Email = row["Email"]?.ToString(),
                Address = row["Address"]?.ToString(),
                EmergencyContact = row["EmergencyContact"]?.ToString(),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"])
            };

            return new Doctor
            {
                DoctorId = Convert.ToInt32(row["DoctorId"]),
                PersonId = Convert.ToInt32(row["PersonId"]),
                Specialization = row["Specialization"].ToString(),
                LicenseNumber = row["LicenseNumber"].ToString(),
                Qualifications = row["Qualifications"]?.ToString(),
                Department = row["Department"]?.ToString(),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                ConsultationFee = Convert.ToDecimal(row["ConsultationFee"]),
                PersonInfo = person
            };
        }
    }
}