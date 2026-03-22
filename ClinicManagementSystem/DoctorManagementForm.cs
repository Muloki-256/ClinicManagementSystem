using ClinicManagementSystem.Managers;
using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class DoctorManagementForm : Form
    {
        private DoctorManager doctorManager;
        private List<Doctor> doctors;
        private bool isEditMode = false;
        private int currentDoctorId = 0;

        public DoctorManagementForm()
        {
            InitializeComponent();
            doctorManager = new DoctorManager();
            LoadDoctors();
            ClearForm();
        }

        private void LoadDoctors()
        {
            try
            {
                doctors = doctorManager.GetAllDoctors();
                dgvDoctors.DataSource = doctors;

                // Safe column configuration
                SafeHideColumn("DoctorId");
                SafeHideColumn("PersonId");
                SafeHideColumn("PersonInfo");
                SafeSetHeaderText("FullName", "Doctor Name");
                SafeSetHeaderText("Specialization", "Specialization");
                SafeSetHeaderText("Phone", "Phone");
                SafeSetHeaderText("Email", "Email");
                SafeSetHeaderText("IsActive", "Active");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading doctors: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
            isEditMode = false;
            grpDoctorDetails.Enabled = true;
            lblMode.Text = "Add New Doctor";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                var person = new Person
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    DateOfBirth = dtpDateOfBirth.Value,
                    Gender = cmbGender.SelectedItem?.ToString(),
                    Phone = txtPhone.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    EmergencyContact = txtEmergencyContact.Text.Trim()
                };

                var doctor = new Doctor
                {
                    DoctorId = currentDoctorId,
                    PersonId = currentDoctorId > 0 ? GetCurrentPersonId() : 0,
                    Specialization = txtSpecialization.Text.Trim(),
                    LicenseNumber = txtLicenseNumber.Text.Trim(),
                    Qualifications = txtQualifications.Text.Trim(),
                    Department = txtDepartment.Text.Trim(),
                    IsActive = chkIsActive.Checked,
                    PersonInfo = person
                };

                var result = isEditMode ?
                    doctorManager.UpdateDoctor(doctor) :
                    doctorManager.CreateDoctor(doctor);

                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearForm();
                    LoadDoctors();
                    grpDoctorDetails.Enabled = false;
                }
                else
                {
                    MessageBox.Show(result.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving doctor: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetCurrentPersonId()
        {
            if (currentDoctorId > 0)
            {
                var doctor = doctorManager.GetDoctorById(currentDoctorId);
                return doctor?.PersonId ?? 0;
            }
            return 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
            grpDoctorDetails.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a doctor to edit.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var doctor = (Doctor)dgvDoctors.SelectedRows[0].DataBoundItem;
            LoadDoctorForEdit(doctor);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a doctor to delete.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var doctor = (Doctor)dgvDoctors.SelectedRows[0].DataBoundItem;

            var result = MessageBox.Show($"Are you sure you want to delete Dr. {doctor.FullName}?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var deleteResult = doctorManager.DeleteDoctor(doctor.DoctorId);
                if (deleteResult.Success)
                {
                    MessageBox.Show(deleteResult.Message, "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDoctors();
                }
                else
                {
                    MessageBox.Show(deleteResult.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadDoctorForEdit(Doctor doctor)
        {
            currentDoctorId = doctor.DoctorId;
            isEditMode = true;

            // Load person information
            txtFirstName.Text = doctor.PersonInfo?.FirstName ?? "";
            txtLastName.Text = doctor.PersonInfo?.LastName ?? "";

            if (doctor.PersonInfo?.DateOfBirth != null)
                dtpDateOfBirth.Value = doctor.PersonInfo.DateOfBirth;

            cmbGender.SelectedItem = doctor.PersonInfo?.Gender ?? "";
            txtPhone.Text = doctor.PersonInfo?.Phone ?? "";
            txtEmail.Text = doctor.PersonInfo?.Email ?? "";
            txtAddress.Text = doctor.PersonInfo?.Address ?? "";
            txtEmergencyContact.Text = doctor.PersonInfo?.EmergencyContact ?? "";

            // Load doctor-specific information
            txtSpecialization.Text = doctor.Specialization;
            txtLicenseNumber.Text = doctor.LicenseNumber;
            txtQualifications.Text = doctor.Qualifications;
            txtDepartment.Text = doctor.Department;
            chkIsActive.Checked = doctor.IsActive;

            grpDoctorDetails.Enabled = true;
            lblMode.Text = "Edit Doctor";
            txtFirstName.Focus();
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

            if (string.IsNullOrWhiteSpace(txtSpecialization.Text))
            {
                MessageBox.Show("Please enter specialization.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSpecialization.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLicenseNumber.Text))
            {
                MessageBox.Show("Please enter license number.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLicenseNumber.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Please enter email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Basic email validation
            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void ClearForm()
        {
            currentDoctorId = 0;
            isEditMode = false;

            // Clear personal information
            txtFirstName.Clear();
            txtLastName.Clear();
            dtpDateOfBirth.Value = DateTime.Now.AddYears(-30);
            cmbGender.SelectedIndex = -1;
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            txtEmergencyContact.Clear();

            // Clear doctor-specific information
            txtSpecialization.Clear();
            txtLicenseNumber.Clear();
            txtQualifications.Clear();
            txtDepartment.Clear();
            chkIsActive.Checked = true;

            lblMode.Text = "Add New Doctor";
            txtSummary.Clear();
        }

        private void dgvDoctors_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count > 0)
            {
                var doctor = (Doctor)dgvDoctors.SelectedRows[0].DataBoundItem;
                DisplayDoctorSummary(doctor);
            }
        }

        private void DisplayDoctorSummary(Doctor doctor)
        {
            string summary = $"Name: Dr. {doctor.FullName}\n";
            summary += $"Specialization: {doctor.Specialization}\n";
            summary += $"Department: {doctor.Department}\n";
            summary += $"License: {doctor.LicenseNumber}\n";
            summary += $"Email: {doctor.Email}\n";
            summary += $"Phone: {doctor.Phone}\n";
            summary += $"Status: {(doctor.IsActive ? "Active" : "Inactive")}";

            txtSummary.Text = summary;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            FilterDoctors();
        }

        private void FilterDoctors()
        {
            string searchTerm = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchTerm))
            {
                dgvDoctors.DataSource = doctors;
            }
            else
            {
                var filteredDoctors = doctors.Where(d =>
                    (d.FullName?.ToLower().Contains(searchTerm) ?? false) ||
                    (d.Specialization?.ToLower().Contains(searchTerm) ?? false) ||
                    (d.Department?.ToLower().Contains(searchTerm) ?? false) ||
                    (d.Email?.ToLower().Contains(searchTerm) ?? false) ||
                    (d.Phone?.Contains(searchTerm) ?? false)
                ).ToList();

                dgvDoctors.DataSource = filteredDoctors;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDoctors();
            txtSearch.Clear();
        }

        private void DoctorManagementForm_Load(object sender, EventArgs e)
        {
            // Populate gender combobox
            cmbGender.Items.AddRange(new string[] { "Male", "Female", "Other" });

            // Set default department options
            var departments = new string[]
            {
                "Cardiology", "Dermatology", "Emergency Medicine", "Family Medicine",
                "Internal Medicine", "Neurology", "Obstetrics and Gynecology",
                "Oncology", "Ophthalmology", "Orthopedics", "Pediatrics",
                "Psychiatry", "Radiology", "Surgery", "Urology"
            };

            foreach (var department in departments)
            {
                txtDepartment.AutoCompleteCustomSource.Add(department);
            }
        }

        private void SafeHideColumn(string columnName)
        {
            try
            {
                if (dgvDoctors.Columns.Contains(columnName))
                {
                    dgvDoctors.Columns[columnName].Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Could not hide column '{columnName}': {ex.Message}");
            }
        }

        private void SafeSetHeaderText(string columnName, string headerText)
        {
            try
            {
                if (dgvDoctors.Columns.Contains(columnName))
                {
                    dgvDoctors.Columns[columnName].HeaderText = headerText;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Could not set header for '{columnName}': {ex.Message}");
            }
        }
    }
}