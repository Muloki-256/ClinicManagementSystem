using ClinicManagementSystem.Data;
using ClinicManagementSystem.Managers;
using ClinicManagementSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class LoginForm : Form
    {
        public static User CurrentUser { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = @"SELECT u.*, p.FirstName, p.LastName, p.PersonId 
                               FROM Users u 
                               INNER JOIN Persons p ON u.PersonId = p.PersonId 
                               WHERE u.Username = @Username AND u.IsActive = 1";

                var parameters = new[] { new MySqlParameter("@Username", username) };
                var dataTable = new BaseRepository().ExecuteQuery(query, parameters);

                if (dataTable.Rows.Count == 0)
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var row = dataTable.Rows[0];
                string storedHash = row["PasswordHash"].ToString();
                string computedHash = ComputeSha256Hash(password);

                if (storedHash == computedHash)
                {
                    CurrentUser = new User
                    {
                        UserId = Convert.ToInt32(row["UserId"]),
                        Username = row["Username"].ToString(),
                        Role = row["Role"].ToString(),
                        PersonId = Convert.ToInt32(row["PersonId"]),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString()
                    };

                    MessageBox.Show($"Welcome {CurrentUser.FirstName}!", "Login Successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    var mainForm = new MainForm();
                    mainForm.Show();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Set focus to username field when form loads
            txtUsername.Focus();

            // Add Enter key support for login
            txtUsername.KeyDown += TextBox_KeyDown;
            txtPassword.KeyDown += TextBox_KeyDown;
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (sender == txtUsername)
                {
                    txtPassword.Focus();
                }
                else if (sender == txtPassword)
                {
                    btnLogin_Click(sender, e);
                }
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            // Clear any previous error indicators
            ClearErrorIndicators();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Clear any previous error indicators
            ClearErrorIndicators();
        }

        private void ClearErrorIndicators()
        {
            // Reset background colors
            txtUsername.BackColor = SystemColors.Window;
            txtPassword.BackColor = SystemColors.Window;
        }

        private void ValidateInputs()
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsername.BackColor = Color.LightPink;
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                txtPassword.BackColor = Color.LightPink;
                isValid = false;
            }

            if (!isValid)
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // NEW: Patient Portal button click handler
        private void btnPatientPortal_Click(object sender, EventArgs e)
        {
            this.Hide();
            PatientLoginForm patientLogin = new PatientLoginForm();
            patientLogin.Closed += (s, args) => this.Show(); // Show login form again when patient portal closes
            patientLogin.Show();
        }

        private void btnShowPassword_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void btnShowPassword_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }
    }
}