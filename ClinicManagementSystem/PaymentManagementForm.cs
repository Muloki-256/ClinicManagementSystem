using ClinicManagementSystem.Managers;
using ClinicManagementSystem.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class PaymentManagementForm : Form
    {
        private PaymentManager paymentManager;
        private DataTable pendingPayments;
        private int currentUserId;

        public PaymentManagementForm(int userId)
        {
            InitializeComponent();
            paymentManager = new PaymentManager();
            currentUserId = userId;
        }

        private void PaymentManagementForm_Load(object sender, EventArgs e)
        {
            LoadPendingPayments();
            LoadPaymentStats();
            dtpReportStart.Value = DateTime.Now.AddDays(-30);
            dtpReportEnd.Value = DateTime.Now;
        }

        private void LoadPendingPayments()
        {
            try
            {
                pendingPayments = paymentManager.GetPendingPayments();
                dgvPendingPayments.DataSource = pendingPayments;

                if (pendingPayments.Rows.Count > 0)
                {
                    lblTotalPending.Text = $"Pending: {pendingPayments.Rows.Count}";
                }
                else
                {
                    lblTotalPending.Text = "Pending: 0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading pending payments: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPaymentStats()
        {
            try
            {
                // Get today's collection
                decimal dailyCollection = paymentManager.GetDailyCollection(DateTime.Now);
                lblDailyCollection.Text = $"Today's Collection: {dailyCollection:C}";

                // Get verified payments count for today
                var verifiedCount = paymentManager.GetVerifiedPaymentsCount();
                lblTotalVerified.Text = $"Verified Today: {verifiedCount}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading payment stats: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVerifyPayment_Click(object sender, EventArgs e)
        {
            if (dgvPendingPayments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a payment to verify.", "Information",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvPendingPayments.SelectedRows[0];
            int paymentId = Convert.ToInt32(selectedRow.Cells["PaymentId"].Value);
            string patientName = selectedRow.Cells["PatientName"].Value.ToString();
            decimal amount = Convert.ToDecimal(selectedRow.Cells["Amount"].Value);

            var result = MessageBox.Show($"Verify payment of {amount:C} for {patientName}?",
                                       "Confirm Verification",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var operationResult = paymentManager.VerifyPayment(paymentId, currentUserId, "Verified via Payment Management");

                    if (operationResult.Success)
                    {
                        MessageBox.Show("Payment verified successfully!", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPendingPayments();
                        LoadPaymentStats();
                    }
                    else
                    {
                        MessageBox.Show(operationResult.Message, "Verification Failed",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error verifying payment: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRejectPayment_Click(object sender, EventArgs e)
        {
            if (dgvPendingPayments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a payment to reject.", "Information",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvPendingPayments.SelectedRows[0];
            int paymentId = Convert.ToInt32(selectedRow.Cells["PaymentId"].Value);
            string patientName = selectedRow.Cells["PatientName"].Value.ToString();
            decimal amount = Convert.ToDecimal(selectedRow.Cells["Amount"].Value);

            string reason = ShowInputDialog(
                $"Enter rejection reason for {patientName}'s payment of {amount:C}:",
                "Rejection Reason");

            if (!string.IsNullOrEmpty(reason))
            {
                try
                {
                    var operationResult = paymentManager.RejectPayment(paymentId, currentUserId, reason);

                    if (operationResult.Success)
                    {
                        MessageBox.Show("Payment rejected successfully!", "Success",
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPendingPayments();
                    }
                    else
                    {
                        MessageBox.Show(operationResult.Message, "Rejection Failed",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error rejecting payment: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnViewReceipt_Click(object sender, EventArgs e)
        {
            if (dgvPendingPayments.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a payment to view receipt.", "Information",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dgvPendingPayments.SelectedRows[0];
            string receiptNumber = selectedRow.Cells["ReceiptNumber"].Value.ToString();

            // Use your existing ReceiptForm
            var receiptForm = new ReceiptForm(receiptNumber);
            receiptForm.ShowDialog();
        }

        private void btnSearchReceipt_Click(object sender, EventArgs e)
        {
            string receiptNumber = txtReceiptNumber.Text.Trim();

            if (string.IsNullOrEmpty(receiptNumber))
            {
                MessageBox.Show("Please enter a receipt number.", "Information",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Use your existing ReceiptForm
                var receiptForm = new ReceiptForm(receiptNumber);
                receiptForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving receipt: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = dtpReportStart.Value;
                DateTime endDate = dtpReportEnd.Value;

                var reportData = paymentManager.GetPaymentReport(startDate, endDate);
                dgvPaymentReport.DataSource = reportData;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPendingPayments();
            LoadPaymentStats();
        }

        private string ShowInputDialog(string text, string caption, string defaultValue = "")
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 360 };
            TextBox textBox = new TextBox() { Left = 20, Top = 45, Width = 360, Text = defaultValue };
            Button confirmation = new Button() { Text = "OK", Left = 280, Width = 100, Top = 75, DialogResult = DialogResult.OK };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}