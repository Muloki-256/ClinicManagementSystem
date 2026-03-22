using ClinicManagementSystem.Managers;
using ClinicManagementSystem.Models;
using System;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class MainForm : Form
    {
        private User currentUser;

        public MainForm()
        {
            InitializeComponent();
            currentUser = LoginForm.CurrentUser;
            this.IsMdiContainer = true;
            InitializeUserInterface();
        }

        private void InitializeUserInterface()
        {
            lblStatus.Text = $"Logged in as: {currentUser.FullName} ({currentUser.Role})";
            lblWelcome.Text = $"Welcome, {currentUser.FullName} ({currentUser.Role})";
            Text = $"Clinic Management System - {currentUser.FullName} ({currentUser.Role})";

            // Show/hide buttons based on user role
            btnDoctors.Visible = currentUser.Role == "Admin";

            // Patient Portal is visible to all staff users
            btnPatientPortal.Visible = true;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            var loginForm = new LoginForm();
            loginForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            var patientForm = new PatientManagementForm();
            patientForm.MdiParent = this;
            patientForm.Show();
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            var appointmentForm = new AppointmentForm();
            appointmentForm.MdiParent = this;
            appointmentForm.Show();
        }

        private void btnMedicalRecords_Click(object sender, EventArgs e)
        {
            var medicalForm = new MedicalRecordsForm();
            medicalForm.MdiParent = this;
            medicalForm.Show();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            var reportsForm = new ReportsForm();
            reportsForm.MdiParent = this;
            reportsForm.Show();
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            var doctorForm = new DoctorManagementForm();
            doctorForm.MdiParent = this;
            doctorForm.Show();
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            var orderForm = new OrderManagementForm();
            orderForm.MdiParent = this;
            orderForm.Show();
        }

        private void btnPatientPortal_Click(object sender, EventArgs e)
        {
            PatientLoginForm patientLogin = new PatientLoginForm();
            patientLogin.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnPaymentManagement_Click(object sender, EventArgs e)
        {
            try
            {
                var paymentForm = new PaymentManagementForm(currentUser.UserId);
                paymentForm.MdiParent = this;
                paymentForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening payment management: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProcessPayment_Click(object sender, EventArgs e)
        {
            try
            {
                var paymentForm = new PaymentProcessingForm();
                paymentForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening payment processing: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}