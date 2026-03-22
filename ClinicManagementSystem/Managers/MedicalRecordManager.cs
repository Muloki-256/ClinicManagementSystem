using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ClinicManagementSystem.Managers
{
    public class MedicalRecordManager
    {
        private BaseRepository repository;

        public MedicalRecordManager()
        {
            repository = new BaseRepository();
        }

        public List<MedicalRecord> GetMedicalRecordsByPatient(int patientId)
        {
            var records = new List<MedicalRecord>();

            string query = @"
                SELECT mr.*, 
                       p_pat.FirstName as PatientFirstName, p_pat.LastName as PatientLastName,
                       p_doc.FirstName as DoctorFirstName, p_doc.LastName as DoctorLastName
                FROM MedicalRecords mr
                INNER JOIN Patients pat ON mr.PatientId = pat.PatientId
                INNER JOIN Persons p_pat ON pat.PersonId = p_pat.PersonId
                INNER JOIN Doctors doc ON mr.DoctorId = doc.DoctorId
                INNER JOIN Persons p_doc ON doc.PersonId = p_doc.PersonId
                WHERE mr.PatientId = @PatientId
                ORDER BY mr.RecordDate DESC";

            var parameters = new[] { new MySqlParameter("@PatientId", patientId) };
            var dataTable = repository.ExecuteQuery(query, parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                var record = new MedicalRecord
                {
                    RecordId = Convert.ToInt32(row["RecordId"]),
                    PatientId = Convert.ToInt32(row["PatientId"]),
                    DoctorId = Convert.ToInt32(row["DoctorId"]),
                    AppointmentId = row["AppointmentId"] == DBNull.Value ? (int?)null : Convert.ToInt32(row["AppointmentId"]),
                    Diagnosis = row["Diagnosis"]?.ToString(),
                    Symptoms = row["Symptoms"]?.ToString(),
                    TreatmentNotes = row["TreatmentNotes"]?.ToString(),
                    TestsPerformed = row["TestsPerformed"]?.ToString(),
                    TestResults = row["TestResults"]?.ToString(),
                    RecordDate = Convert.ToDateTime(row["RecordDate"]),
                    FollowUpDate = row["FollowUpDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["FollowUpDate"])
                };

                // Set navigation properties instead of trying to set computed properties
                record.Patient = new Patient
                {
                    PersonInfo = new Person
                    {
                        FirstName = row["PatientFirstName"]?.ToString(),
                        LastName = row["PatientLastName"]?.ToString()
                    }
                };

                record.Doctor = new Doctor
                {
                    PersonInfo = new Person
                    {
                        FirstName = row["DoctorFirstName"]?.ToString(),
                        LastName = row["DoctorLastName"]?.ToString()
                    }
                };

                records.Add(record);
            }

            return records;
        }

        public OperationResult CreateMedicalRecord(MedicalRecord record)
        {
            try
            {
                string query = @"
                    INSERT INTO MedicalRecords (PatientId, DoctorId, AppointmentId, Diagnosis, Symptoms, 
                                              TreatmentNotes, TestsPerformed, TestResults, RecordDate, FollowUpDate)
                    VALUES (@PatientId, @DoctorId, @AppointmentId, @Diagnosis, @Symptoms, @TreatmentNotes, 
                            @TestsPerformed, @TestResults, @RecordDate, @FollowUpDate)";

                var parameters = new[]
                {
                    new MySqlParameter("@PatientId", record.PatientId),
                    new MySqlParameter("@DoctorId", record.DoctorId),
                    new MySqlParameter("@AppointmentId", record.AppointmentId ?? (object)DBNull.Value),
                    new MySqlParameter("@Diagnosis", record.Diagnosis ?? (object)DBNull.Value),
                    new MySqlParameter("@Symptoms", record.Symptoms ?? (object)DBNull.Value),
                    new MySqlParameter("@TreatmentNotes", record.TreatmentNotes ?? (object)DBNull.Value),
                    new MySqlParameter("@TestsPerformed", record.TestsPerformed ?? (object)DBNull.Value),
                    new MySqlParameter("@TestResults", record.TestResults ?? (object)DBNull.Value),
                    new MySqlParameter("@RecordDate", record.RecordDate),
                    new MySqlParameter("@FollowUpDate", record.FollowUpDate ?? (object)DBNull.Value)
                };

                repository.ExecuteNonQuery(query, parameters);

                return OperationResult.SuccessResult("Medical record created successfully.");
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error creating medical record: {ex.Message}");
            }
        }
    }
}