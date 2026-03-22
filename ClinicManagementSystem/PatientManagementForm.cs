using ClinicManagementSystem.Managers;
using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class PatientManagementForm : Form
    {
        private PatientManager patientManager;
        private List<Patient> patients;
        private Patient selectedPatient;

        public PatientManagementForm()
        {
            InitializeComponent();
            patientManager = new PatientManager();
            LoadPatients();
        }

        private void LoadPatients()
        {
            try
            {
                patients = patientManager.GetAllPatients();
                RefreshPatientList();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patients: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshPatientList()
        {
            listViewPatients.Items.Clear();
            foreach (var patient in patients)
            {
                var item = new ListViewItem(patient.FullName);
                item.SubItems.Add(patient.Phone);
                item.SubItems.Add(patient.Email);
                item.SubItems.Add(patient.Gender);
                item.SubItems.Add(patient.BloodType ?? "N/A");
                item.Tag = patient.PatientId;
                listViewPatients.Items.Add(item);
            }
        }

        private void listViewPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewPatients.SelectedItems.Count > 0)
            {
                int patientId = (int)listViewPatients.SelectedItems[0].Tag;
                selectedPatient = patientManager.GetPatientById(patientId);
                if (selectedPatient != null)
                {
                    LoadPatientData(selectedPatient);
                }
            }
        }

        private void LoadPatientData(Patient patient)
        {
            txtFirstName.Text = patient.PersonInfo.FirstName;
            txtLastName.Text = patient.PersonInfo.LastName;
            dtpDateOfBirth.Value = patient.PersonInfo.DateOfBirth;
            cmbGender.Text = patient.PersonInfo.Gender;
            txtPhone.Text = patient.PersonInfo.Phone;
            txtEmail.Text = patient.PersonInfo.Email;
            txtAddress.Text = patient.PersonInfo.Address;
            txtEmergencyContact.Text = patient.PersonInfo.EmergencyContact;
            cmbBloodType.Text = patient.BloodType;
            txtAllergies.Text = patient.Allergies;
            txtMedicalHistory.Text = patient.MedicalHistory;
            txtInsuranceProvider.Text = patient.InsuranceProvider;
            txtInsurancePolicy.Text = patient.InsurancePolicyNumber;
            chkActive.Checked = patient.IsActive;

            btnSave.Text = "Update Patient";
            btnDelete.Enabled = true;
        }

        private void ClearForm()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            dtpDateOfBirth.Value = DateTime.Now.AddYears(-30);
            cmbGender.SelectedIndex = -1;
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtEmergencyContact.Clear();
            cmbBloodType.SelectedIndex = -1;
            txtAllergies.Clear();
            txtMedicalHistory.Clear();
            txtInsuranceProvider.Clear();
            txtInsurancePolicy.Clear();
            chkActive.Checked = true;

            btnSave.Text = "Add Patient";
            btnDelete.Enabled = false;
            selectedPatient = null;
            listViewPatients.SelectedItems.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                var patient = new Patient
                {
                    PersonInfo = new Person
                    {
                        FirstName = txtFirstName.Text.Trim(),
                        LastName = txtLastName.Text.Trim(),
                        DateOfBirth = dtpDateOfBirth.Value,
                        Gender = cmbGender.Text,
                        Phone = txtPhone.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        EmergencyContact = txtEmergencyContact.Text.Trim()
                    },
                    BloodType = cmbBloodType.Text,
                    Allergies = txtAllergies.Text.Trim(),
                    MedicalHistory = txtMedicalHistory.Text.Trim(),
                    InsuranceProvider = txtInsuranceProvider.Text.Trim(),
                    InsurancePolicyNumber = txtInsurancePolicy.Text.Trim(),
                    IsActive = chkActive.Checked
                };

                OperationResult result;

                if (selectedPatient == null)
                {
                    // Add new patient
                    result = patientManager.CreatePatient(patient);
                    if (result.Success)
                    {
                        MessageBox.Show("Patient added successfully!", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    // Update existing patient
                    patient.PatientId = selectedPatient.PatientId;
                    patient.PersonId = selectedPatient.PersonId;
                    patient.PersonInfo.PersonId = selectedPatient.PersonId;
                    result = patientManager.UpdatePatient(patient);
                    if (result.Success)
                    {
                        MessageBox.Show("Patient updated successfully!", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                if (result.Success)
                {
                    LoadPatients();
                }
                else
                {
                    MessageBox.Show(result.Message, "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving patient: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedPatient == null)
            {
                MessageBox.Show("Please select a patient to delete.", "Information",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Are you sure you want to delete {selectedPatient.FullName}?",
                                       "Confirm Delete",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var deleteResult = patientManager.DeletePatient(selectedPatient.PatientId);
                    if (deleteResult.Success)
                    {
                        MessageBox.Show("Patient deleted successfully!", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPatients();
                    }
                    else
                    {
                        MessageBox.Show(deleteResult.Message, "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting patient: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPatients();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Please enter first name.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                MessageBox.Show("Please enter last name.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Please enter phone number.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            if (dtpDateOfBirth.Value > DateTime.Now)
            {
                MessageBox.Show("Date of birth cannot be in the future.", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDateOfBirth.Focus();
                return false;
            }

            return true;
        }

        private void PatientManagementForm_Load(object sender, EventArgs e)
        {

        }
    }
}