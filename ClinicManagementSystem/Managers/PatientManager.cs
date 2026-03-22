using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ClinicManagementSystem.Managers
{
    public class PatientManager
    {
        private BaseRepository repository;

        public PatientManager()
        {
            repository = new BaseRepository();
        }

        public List<Patient> GetAllPatients()
        {
            var patients = new List<Patient>();

            string query = @"
                SELECT p.*, per.* 
                FROM Patients p 
                INNER JOIN Persons per ON p.PersonId = per.PersonId 
                ORDER BY per.FirstName, per.LastName";

            var dataTable = repository.ExecuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                patients.Add(MapPatientFromDataRow(row));
            }

            return patients;
        }

        public Patient GetPatientById(int patientId)
        {
            string query = @"
                SELECT p.*, per.* 
                FROM Patients p 
                INNER JOIN Persons per ON p.PersonId = per.PersonId 
                WHERE p.PatientId = @PatientId";

            var parameters = new[] { new MySqlParameter("@PatientId", patientId) };
            var dataTable = repository.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count == 0)
                return null;

            return MapPatientFromDataRow(dataTable.Rows[0]);
        }

        public OperationResult CreatePatient(Patient patient)
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
                    new MySqlParameter("@FirstName", patient.PersonInfo.FirstName),
                    new MySqlParameter("@LastName", patient.PersonInfo.LastName),
                    new MySqlParameter("@DateOfBirth", patient.PersonInfo.DateOfBirth),
                    new MySqlParameter("@Gender", patient.PersonInfo.Gender ?? (object)DBNull.Value),
                    new MySqlParameter("@Phone", patient.PersonInfo.Phone ?? (object)DBNull.Value),
                    new MySqlParameter("@Email", patient.PersonInfo.Email ?? (object)DBNull.Value),
                    new MySqlParameter("@Address", patient.PersonInfo.Address ?? (object)DBNull.Value),
                    new MySqlParameter("@EmergencyContact", patient.PersonInfo.EmergencyContact ?? (object)DBNull.Value),
                    new MySqlParameter("@CreatedDate", DateTime.Now)
                };

                var personId = Convert.ToInt32(repository.ExecuteScalar(personQuery, personParameters));

                // Then create the patient record
                string patientQuery = @"
                    INSERT INTO Patients (PersonId, BloodType, Allergies, MedicalHistory, InsuranceProvider, InsurancePolicyNumber, IsActive)
                    VALUES (@PersonId, @BloodType, @Allergies, @MedicalHistory, @InsuranceProvider, @InsurancePolicyNumber, @IsActive)";

                var patientParameters = new[]
                {
                    new MySqlParameter("@PersonId", personId),
                    new MySqlParameter("@BloodType", patient.BloodType ?? (object)DBNull.Value),
                    new MySqlParameter("@Allergies", patient.Allergies ?? (object)DBNull.Value),
                    new MySqlParameter("@MedicalHistory", patient.MedicalHistory ?? (object)DBNull.Value),
                    new MySqlParameter("@InsuranceProvider", patient.InsuranceProvider ?? (object)DBNull.Value),
                    new MySqlParameter("@InsurancePolicyNumber", patient.InsurancePolicyNumber ?? (object)DBNull.Value),
                    new MySqlParameter("@IsActive", patient.IsActive)
                };

                repository.ExecuteNonQuery(patientQuery, patientParameters);

                return OperationResult.SuccessResult("Patient created successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error creating patient: {ex.Message}");
            }
        }

        public OperationResult UpdatePatient(Patient patient)
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
                    new MySqlParameter("@FirstName", patient.PersonInfo.FirstName),
                    new MySqlParameter("@LastName", patient.PersonInfo.LastName),
                    new MySqlParameter("@DateOfBirth", patient.PersonInfo.DateOfBirth),
                    new MySqlParameter("@Gender", patient.PersonInfo.Gender ?? (object)DBNull.Value),
                    new MySqlParameter("@Phone", patient.PersonInfo.Phone ?? (object)DBNull.Value),
                    new MySqlParameter("@Email", patient.PersonInfo.Email ?? (object)DBNull.Value),
                    new MySqlParameter("@Address", patient.PersonInfo.Address ?? (object)DBNull.Value),
                    new MySqlParameter("@EmergencyContact", patient.PersonInfo.EmergencyContact ?? (object)DBNull.Value),
                    new MySqlParameter("@PersonId", patient.PersonId)
                };

                repository.ExecuteNonQuery(personQuery, personParameters);

                // Update patient record
                string patientQuery = @"
                    UPDATE Patients 
                    SET BloodType = @BloodType, Allergies = @Allergies, MedicalHistory = @MedicalHistory,
                        InsuranceProvider = @InsuranceProvider, InsurancePolicyNumber = @InsurancePolicyNumber, 
                        IsActive = @IsActive
                    WHERE PatientId = @PatientId";

                var patientParameters = new[]
                {
                    new MySqlParameter("@BloodType", patient.BloodType ?? (object)DBNull.Value),
                    new MySqlParameter("@Allergies", patient.Allergies ?? (object)DBNull.Value),
                    new MySqlParameter("@MedicalHistory", patient.MedicalHistory ?? (object)DBNull.Value),
                    new MySqlParameter("@InsuranceProvider", patient.InsuranceProvider ?? (object)DBNull.Value),
                    new MySqlParameter("@InsurancePolicyNumber", patient.InsurancePolicyNumber ?? (object)DBNull.Value),
                    new MySqlParameter("@IsActive", patient.IsActive),
                    new MySqlParameter("@PatientId", patient.PatientId)
                };

                repository.ExecuteNonQuery(patientQuery, patientParameters);

                return OperationResult.SuccessResult("Patient updated successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error updating patient: {ex.Message}");
            }
        }

        public OperationResult DeletePatient(int patientId)
        {
            try
            {
                // First get the person ID
                string getPersonQuery = "SELECT PersonId FROM Patients WHERE PatientId = @PatientId";
                var getPersonParams = new[] { new MySqlParameter("@PatientId", patientId) };
                var personId = Convert.ToInt32(repository.ExecuteScalar(getPersonQuery, getPersonParams));

                // Delete the patient record
                string patientQuery = "DELETE FROM Patients WHERE PatientId = @PatientId";
                var patientParams = new[] { new MySqlParameter("@PatientId", patientId) };
                repository.ExecuteNonQuery(patientQuery, patientParams);

                // Delete the person record
                string personQuery = "DELETE FROM Persons WHERE PersonId = @PersonId";
                var personParams = new[] { new MySqlParameter("@PersonId", personId) };
                repository.ExecuteNonQuery(personQuery, personParams);

                return OperationResult.SuccessResult("Patient deleted successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error deleting patient: {ex.Message}");
            }
        }

        private Patient MapPatientFromDataRow(DataRow row)
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

            return new Patient
            {
                PatientId = Convert.ToInt32(row["PatientId"]),
                PersonId = Convert.ToInt32(row["PersonId"]),
                BloodType = row["BloodType"]?.ToString(),
                Allergies = row["Allergies"]?.ToString(),
                MedicalHistory = row["MedicalHistory"]?.ToString(),
                InsuranceProvider = row["InsuranceProvider"]?.ToString(),
                InsurancePolicyNumber = row["InsurancePolicyNumber"]?.ToString(),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                PersonInfo = person
            };
        }
    }
}