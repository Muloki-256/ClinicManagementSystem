using ClinicManagementSystem.Managers;
using ClinicManagementSystem.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class PaymentVerificationForm : Form
    {
        private PaymentManager _paymentManager;
        private int _currentUserId;

        public PaymentVerificationForm(int currentUserId)
        {
            InitializeComponent();
            _paymentManager = new PaymentManager();
            _currentUserId = currentUserId;
            LoadPendingPayments();
        }

        private void LoadPendingPayments()
        {
            try
            {
                DataTable pendingPayments = _paymentManager.GetPendingPayments();
                dataGridViewPayments.DataSource = pendingPayments;

                // Format grid
                dataGridViewPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridViewPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                lblStatus.Text = $"Found {pendingPayments.Rows.Count} pending payments";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading payments: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a payment to verify.", "Information",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dataGridViewPayments.SelectedRows[0];
            int paymentId = Convert.ToInt32(selectedRow.Cells["PaymentId"].Value);

            var result = _paymentManager.VerifyPayment(paymentId, _currentUserId, "Manually verified");

            if (result.Success)
            {
                MessageBox.Show("Payment verified successfully!", "Success",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPendingPayments();
            }
            else
            {
                MessageBox.Show($"Error: {result.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a payment to reject.", "Information",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Use the simple input method instead of InputDialogForm
            string reason = ShowInputDialog("Enter rejection reason:");

            if (string.IsNullOrEmpty(reason))
                return;

            var selectedRow = dataGridViewPayments.SelectedRows[0];
            int paymentId = Convert.ToInt32(selectedRow.Cells["PaymentId"].Value);

            var result = _paymentManager.RejectPayment(paymentId, _currentUserId, reason);

            if (result.Success)
            {
                MessageBox.Show("Payment rejected successfully!", "Success",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPendingPayments();
            }
            else
            {
                MessageBox.Show($"Error: {result.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a payment to print receipt.", "Information",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dataGridViewPayments.SelectedRows[0];
            string receiptNumber = selectedRow.Cells["ReceiptNumber"].Value?.ToString();

            if (string.IsNullOrEmpty(receiptNumber))
            {
                MessageBox.Show("No receipt number found for this payment.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var receiptForm = new ReceiptForm(receiptNumber);
            receiptForm.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPendingPayments();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string ShowInputDialog(string text)
        {
            // Create a simple input dialog without creating a separate form class
            using (Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Input",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            })
            {
                Label textLabel = new Label()
                {
                    Left = 20,
                    Top = 20,
                    Text = text,
                    Width = 360,
                    Font = new Font("Arial", 9)
                };

                TextBox textBox = new TextBox()
                {
                    Left = 20,
                    Top = 50,
                    Width = 360
                };

                Button okButton = new Button()
                {
                    Text = "OK",
                    Left = 200,
                    Width = 85,
                    Top = 80,
                    DialogResult = DialogResult.OK
                };

                Button cancelButton = new Button()
                {
                    Text = "Cancel",
                    Left = 295,
                    Width = 85,
                    Top = 80,
                    DialogResult = DialogResult.Cancel
                };

                okButton.Click += (sender, e) => { prompt.Close(); };
                cancelButton.Click += (sender, e) => { prompt.Close(); };

                prompt.Controls.Add(textBox);
                prompt.Controls.Add(okButton);
                prompt.Controls.Add(cancelButton);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = okButton;
                prompt.CancelButton = cancelButton;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }
    }
}