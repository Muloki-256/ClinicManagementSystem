using System;
using System.Drawing;
using System.Windows.Forms;
using ClinicManagementSystem.Models;

namespace ClinicManagementSystem
{
    public partial class PatientLoginForm : Form
    {
        public PatientLoginForm()
        {
            InitializeComponent();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // Validate required fields
            if (string.IsNullOrEmpty(txtFullName.Text.Trim()))
            {
                MessageBox.Show("Please enter your full name", "Information Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            // Create patient info
            var patientInfo = new PatientInfo
            {
                FullName = txtFullName.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim()
            };

            // Open patient portal as a dialog to keep it open
            using (PatientPortalForm portal = new PatientPortalForm(patientInfo))
            {
                this.Hide(); // Hide the login form
                portal.ShowDialog(); // Show portal as modal dialog
                this.Show(); // Show login form again when portal closes
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Go back to main login form
            this.Close();
        }

        private void PatientLoginForm_Load(object sender, EventArgs e)
        {
            txtFullName.Focus();
        }

        private void PatientLoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If this is the main form, show login form again
            if (Application.OpenForms["LoginForm"] != null)
            {
                Application.OpenForms["LoginForm"].Show();
            }
        }
    }
}