using ClinicManagementSystem.Managers;
using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class MedicalRecordsForm : Form
    {
        private MedicalRecordManager medicalRecordManager;
        private PatientManager patientManager;
        private DoctorManager doctorManager;
        private List<Patient> patients;
        private List<Tablet> tablets;
        private List<Prescription> currentPrescriptions;

        public MedicalRecordsForm()
        {
            InitializeComponent();
            medicalRecordManager = new MedicalRecordManager();
            patientManager = new PatientManager();
            doctorManager = new DoctorManager();
            currentPrescriptions = new List<Prescription>();
            LoadComboBoxData();
            LoadTablets();
        }

        private void LoadComboBoxData()
        {
            patients = patientManager.GetAllPatients();
            cmbPatients.DataSource = patients;
            cmbPatients.DisplayMember = "FullName";
            cmbPatients.ValueMember = "PatientId";
        }

        private void LoadTablets()
        {
            // This would typically come from a TabletManager
            // For now, we'll create some sample data
            tablets = new List<Tablet>
            {
                new Tablet { TabletId = 1, TabletName = "Paracetamol", CostPerUnit = 50.00m },
                new Tablet { TabletId = 2, TabletName = "Amoxicillin", CostPerUnit = 200.00m },
                new Tablet { TabletId = 3, TabletName = "Metformin", CostPerUnit = 150.00m },
                new Tablet { TabletId = 4, TabletName = "Lisinopril", CostPerUnit = 180.00m },
                new Tablet { TabletId = 5, TabletName = "Chloroquine", CostPerUnit = 120.00m }
            };

            cmbTablets.DataSource = tablets;
            cmbTablets.DisplayMember = "TabletName";
            cmbTablets.ValueMember = "TabletId";
        }

        private void btnLoadRecords_Click(object sender, EventArgs e)
        {
            if (cmbPatients.SelectedValue == null)
            {
                MessageBox.Show("Please select a patient.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int patientId = (int)cmbPatients.SelectedValue;
            LoadMedicalRecords(patientId);
            EnableRecordCreation();
        }

        private void LoadMedicalRecords(int patientId)
        {
            try
            {
                var records = medicalRecordManager.GetMedicalRecordsByPatient(patientId);
                dgvMedicalRecords.DataSource = records;

                // Safe column configuration with null checks
                if (dgvMedicalRecords.Columns.Count > 0)
                {
                    // List of columns that might exist
                    var columnsToHide = new[] { "RecordId", "PatientId", "DoctorId", "AppointmentId", "Doctor", "Patient", "Prescriptions" };

                    foreach (var columnName in columnsToHide)
                    {
                        if (dgvMedicalRecords.Columns.Contains(columnName))
                        {
                            dgvMedicalRecords.Columns[columnName].Visible = false;
                        }
                    }

                    // Set header texts for visible columns
                    SetColumnHeaderText("Diagnosis", "Diagnosis");
                    SetColumnHeaderText("Symptoms", "Symptoms");
                    SetColumnHeaderText("RecordDate", "Record Date");
                    SetColumnHeaderText("TreatmentNotes", "Treatment Notes");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading medical records: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetColumnHeaderText(string columnName, string headerText)
        {
            if (dgvMedicalRecords.Columns.Contains(columnName))
            {
                dgvMedicalRecords.Columns[columnName].HeaderText = headerText;
            }
        }

        private void EnableRecordCreation()
        {
            grpMedicalRecord.Enabled = true;
            grpPrescriptions.Enabled = true;
            btnNewRecord.Enabled = true;
        }

        private void btnNewRecord_Click(object sender, EventArgs e)
        {
            ClearMedicalRecordForm();
            currentPrescriptions.Clear();
            RefreshPrescriptionsGrid();
            grpPrescriptionDetails.Enabled = true;
        }

        private void btnSaveRecord_Click(object sender, EventArgs e)
        {
            if (!ValidateMedicalRecord())
                return;

            try
            {
                var currentUser = LoginForm.CurrentUser;
                var doctor = doctorManager.GetAllDoctors()
                    .FirstOrDefault(d => d.PersonId == currentUser.PersonId);

                if (doctor == null)
                {
                    MessageBox.Show("Only doctors can create medical records.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var medicalRecord = new MedicalRecord
                {
                    PatientId = (int)cmbPatients.SelectedValue,
                    DoctorId = doctor.DoctorId,
                    Diagnosis = txtDiagnosis.Text,
                    Symptoms = txtSymptoms.Text,
                    TreatmentNotes = txtTreatmentNotes.Text,
                    RecordDate = DateTime.Now,
                    Prescriptions = currentPrescriptions
                };

                var result = medicalRecordManager.CreateMedicalRecord(medicalRecord);
                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearMedicalRecordForm();
                    currentPrescriptions.Clear();
                    RefreshPrescriptionsGrid();
                    grpPrescriptionDetails.Enabled = false;
                    LoadMedicalRecords(medicalRecord.PatientId);
                }
                else
                {
                    MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving medical record: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddPrescription_Click(object sender, EventArgs e)
        {
            grpPrescriptionDetails.Enabled = true;
        }

        private void btnAddToPrescription_Click(object sender, EventArgs e)
        {
            if (!ValidatePrescription())
                return;

            var prescription = new Prescription
            {
                TabletId = (int)cmbTablets.SelectedValue,
                Tablet = (Tablet)cmbTablets.SelectedItem,
                Dosage = txtDosage.Text,
                Duration = txtDuration.Text,
                Quantity = (int)nudQuantity.Value,
                Instructions = txtInstructions.Text
            };

            currentPrescriptions.Add(prescription);
            RefreshPrescriptionsGrid();
            ClearPrescriptionForm();
        }

        private void btnRemovePrescription_Click(object sender, EventArgs e)
        {
            if (dgvPrescriptions.SelectedRows.Count > 0)
            {
                var prescription = (Prescription)dgvPrescriptions.SelectedRows[0].DataBoundItem;
                currentPrescriptions.Remove(prescription);
                RefreshPrescriptionsGrid();
            }
        }

        private void RefreshPrescriptionsGrid()
        {
            dgvPrescriptions.DataSource = null;
            dgvPrescriptions.DataSource = currentPrescriptions;

            if (dgvPrescriptions.Columns.Count > 0)
            {
                dgvPrescriptions.Columns["PrescriptionId"].Visible = false;
                dgvPrescriptions.Columns["RecordId"].Visible = false;
                dgvPrescriptions.Columns["TabletId"].Visible = false;
                dgvPrescriptions.Columns["Tablet"].Visible = false;
            }
        }

        private void dgvMedicalRecords_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMedicalRecords.SelectedRows.Count > 0)
            {
                var record = (MedicalRecord)dgvMedicalRecords.SelectedRows[0].DataBoundItem;
                DisplayMedicalRecordDetails(record);
            }
        }

        private void DisplayMedicalRecordDetails(MedicalRecord record)
        {
            txtDiagnosis.Text = record.Diagnosis;
            txtSymptoms.Text = record.Symptoms;
            txtTreatmentNotes.Text = record.TreatmentNotes;

            // Display prescriptions in a separate grid or read-only section
            currentPrescriptions = record.Prescriptions;
            RefreshPrescriptionsGrid();
        }

        private bool ValidateMedicalRecord()
        {
            if (string.IsNullOrWhiteSpace(txtDiagnosis.Text))
            {
                MessageBox.Show("Please enter a diagnosis.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool ValidatePrescription()
        {
            if (cmbTablets.SelectedValue == null)
            {
                MessageBox.Show("Please select a tablet.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDosage.Text))
            {
                MessageBox.Show("Please enter dosage information.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDuration.Text))
            {
                MessageBox.Show("Please enter duration.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearMedicalRecordForm()
        {
            txtDiagnosis.Clear();
            txtSymptoms.Clear();
            txtTreatmentNotes.Clear();
        }

        private void ClearPrescriptionForm()
        {
            cmbTablets.SelectedIndex = -1;
            txtDosage.Clear();
            txtDuration.Clear();
            nudQuantity.Value = 1;
            txtInstructions.Clear();
        }

        private void MedicalRecordsForm_Load(object sender, EventArgs e)
        {
            // Additional initialization if needed
        }
    }
}