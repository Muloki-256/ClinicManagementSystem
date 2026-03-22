using ClinicManagementSystem.Managers;
using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class AppointmentForm : Form
    {
        private AppointmentManager appointmentManager;
        private PatientManager patientManager;
        private DoctorManager doctorManager;
        private List<Patient> patients;
        private List<Doctor> doctors;
        private int currentAppointmentId = 0;

        public AppointmentForm()
        {
            InitializeComponent();
            appointmentManager = new AppointmentManager();
            patientManager = new PatientManager();
            doctorManager = new DoctorManager();
            LoadComboBoxData();
            SetupDataGridView();
            LoadAppointments();
        }

        private void LoadComboBoxData()
        {
            try
            {
                // Load patients
                patients = patientManager.GetAllPatients();
                cmbPatient.DataSource = patients;
                cmbPatient.DisplayMember = "FullName";
                cmbPatient.ValueMember = "PatientId";

                // Load doctors
                doctors = doctorManager.GetAllDoctors();
                cmbDoctor.DataSource = doctors;
                cmbDoctor.DisplayMember = "FullName";
                cmbDoctor.ValueMember = "DoctorId";

                // Load filter doctors (include "All" option)
                var filterDoctors = new List<Doctor> { new Doctor { DoctorId = 0, PersonInfo = new Person { FirstName = "All", LastName = "Doctors" } } };
                filterDoctors.AddRange(doctors);
                cmbFilterDoctor.DataSource = filterDoctors;
                cmbFilterDoctor.DisplayMember = "FullName";
                cmbFilterDoctor.ValueMember = "DoctorId";

                // Load status options
                cmbStatus.Items.AddRange(new string[] { "Scheduled", "Completed", "Cancelled", "NoShow" });
                cmbStatus.SelectedItem = "Scheduled";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dgvAppointments.AutoGenerateColumns = false;
            dgvAppointments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAppointments.MultiSelect = false;

            // Clear existing columns
            dgvAppointments.Columns.Clear();

            // Add columns manually
            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AppointmentId",
                HeaderText = "ID",
                DataPropertyName = "AppointmentId",
                Visible = false
            });

            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PatientName",
                HeaderText = "Patient",
                DataPropertyName = "PatientName",
                Width = 150
            });

            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DoctorName",
                HeaderText = "Doctor",
                DataPropertyName = "DoctorName",
                Width = 150
            });

            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "AppointmentDate",
                HeaderText = "Appointment Date",
                DataPropertyName = "AppointmentDate",
                Width = 150
            });

            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status",
                Width = 100
            });

            dgvAppointments.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Reason",
                HeaderText = "Reason",
                DataPropertyName = "Reason",
                Width = 200
            });
        }

        private void LoadAppointments(DateTime? date = null, int doctorId = 0)
        {
            try
            {
                var currentUser = LoginForm.CurrentUser;
                int selectedDoctorId = doctorId;

                // If user is a doctor, only show their appointments
                if (currentUser?.Role == "Doctor")
                {
                    var doctor = doctors.FirstOrDefault(d => d.PersonId == currentUser.PersonId);
                    if (doctor != null)
                    {
                        selectedDoctorId = doctor.DoctorId;
                        cmbFilterDoctor.SelectedValue = doctor.DoctorId;
                        cmbFilterDoctor.Enabled = false;
                    }
                }

                var appointments = appointmentManager.GetAppointmentsByDoctor(selectedDoctorId, date);
                dgvAppointments.DataSource = appointments;

                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointments: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = dgvAppointments.SelectedRows.Count > 0;
            btnComplete.Enabled = hasSelection;
            btnCancelAppointment.Enabled = hasSelection;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
            grpAppointmentDetails.Enabled = true;
            cmbStatus.SelectedItem = "Scheduled";
            dtpAppointmentDate.Value = DateTime.Now.AddHours(1); // Default to 1 hour from now
            currentAppointmentId = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                var appointment = new Appointment
                {
                    PatientId = (int)cmbPatient.SelectedValue,
                    DoctorId = (int)cmbDoctor.SelectedValue,
                    AppointmentDate = dtpAppointmentDate.Value,
                    Status = cmbStatus.SelectedItem?.ToString(),
                    Reason = txtReason.Text
                };

                var result = appointmentManager.CreateAppointment(appointment);
                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    grpAppointmentDetails.Enabled = false;
                    LoadAppointments();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show(result.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving appointment: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            grpAppointmentDetails.Enabled = false;
            ClearForm();
            currentAppointmentId = 0;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DateTime? filterDate = chkFilterDate.Checked ? dtpFilterDate.Value.Date : (DateTime?)null;
            int doctorId = (int)cmbFilterDoctor.SelectedValue;
            LoadAppointments(filterDate, doctorId);
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            dtpFilterDate.Value = DateTime.Now;
            chkFilterDate.Checked = false;
            cmbFilterDoctor.SelectedIndex = 0;
            LoadAppointments();
        }

        private void btnComplete_Click(object sender, EventArgs e)
        {
            UpdateAppointmentStatus("Completed");
        }

        private void btnCancelAppointment_Click(object sender, EventArgs e)
        {
            UpdateAppointmentStatus("Cancelled");
        }

        private void UpdateAppointmentStatus(string status)
        {
            if (dgvAppointments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an appointment.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentId"].Value);

                string message = $"Are you sure you want to mark this appointment as '{status}'?";
                var result = MessageBox.Show(message, "Confirm Status Change",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                var updateResult = appointmentManager.UpdateAppointmentStatus(appointmentId, status);

                if (updateResult.Success)
                {
                    MessageBox.Show(updateResult.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAppointments();
                }
                else
                {
                    MessageBox.Show(updateResult.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error updating appointment status to '{status}':\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nInner Exception:\n{ex.InnerException.Message}";
                }

                MessageBox.Show(errorMessage, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvAppointments_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAppointments.SelectedRows.Count > 0)
            {
                try
                {
                    var appointmentId = Convert.ToInt32(dgvAppointments.SelectedRows[0].Cells["AppointmentId"].Value);
                    var appointment = appointmentManager.GetAppointmentById(appointmentId);

                    if (appointment != null)
                    {
                        DisplayAppointmentDetails(appointment);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading appointment details: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            UpdateButtonStates();
        }

        private void DisplayAppointmentDetails(Appointment appointment)
        {
            try
            {
                cmbPatient.SelectedValue = appointment.PatientId;
                cmbDoctor.SelectedValue = appointment.DoctorId;
                dtpAppointmentDate.Value = appointment.AppointmentDate;
                cmbStatus.SelectedItem = appointment.Status;
                txtReason.Text = appointment.Reason ?? "";
                currentAppointmentId = appointment.AppointmentId;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error displaying appointment details: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateForm()
        {
            if (cmbPatient.SelectedValue == null || cmbPatient.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a patient.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPatient.Focus();
                return false;
            }

            if (cmbDoctor.SelectedValue == null || cmbDoctor.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a doctor.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDoctor.Focus();
                return false;
            }

            if (dtpAppointmentDate.Value < DateTime.Now)
            {
                MessageBox.Show("Appointment date cannot be in the past.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpAppointmentDate.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cmbStatus.SelectedItem?.ToString()))
            {
                MessageBox.Show("Please select a status.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbStatus.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            if (cmbPatient.Items.Count > 0) cmbPatient.SelectedIndex = 0;
            if (cmbDoctor.Items.Count > 0) cmbDoctor.SelectedIndex = 0;
            dtpAppointmentDate.Value = DateTime.Now.AddHours(1);
            if (cmbStatus.Items.Count > 0) cmbStatus.SelectedItem = "Scheduled";
            txtReason.Clear();
            currentAppointmentId = 0;
        }

        private void AppointmentForm_Load(object sender, EventArgs e)
        {
            // Set filter date to today by default
            dtpFilterDate.Value = DateTime.Now;
            chkFilterDate.Checked = true;
        }
    }
}