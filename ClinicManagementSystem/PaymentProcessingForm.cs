using ClinicManagementSystem.Managers;
using ClinicManagementSystem.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class PaymentProcessingForm : Form
    {
        private PaymentManager paymentManager;
        private DataTable patientBills;

        public PaymentProcessingForm()
        {
            InitializeComponent();
            paymentManager = new PaymentManager();
            LoadPatientBills();
            InitializePaymentMethods();
        }

        private void LoadPatientBills()
        {
            try
            {
                // Load bills with outstanding balances
                patientBills = GetOutstandingBills();
                cmbBills.DataSource = patientBills;
                cmbBills.DisplayMember = "BillInfo";
                cmbBills.ValueMember = "BillId";

                if (patientBills.Rows.Count == 0)
                {
                    MessageBox.Show("No outstanding bills found.", "Information",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading bills: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializePaymentMethods()
        {
            cmbPaymentMethod.Items.AddRange(new[] { "Cash", "Card", "Online", "Insurance" });
            if (cmbPaymentMethod.Items.Count > 0)
                cmbPaymentMethod.SelectedIndex = 0;
        }

        private DataTable GetOutstandingBills()
        {
            string query = @"
                SELECT 
                    b.BillId,
                    CONCAT('Bill #', b.BillId, ' - ', p.FirstName, ' ', p.LastName, ' - $', 
                           (b.TotalAmount - b.PaidAmount), ' due') as BillInfo,
                    b.TotalAmount,
                    b.PaidAmount,
                    (b.TotalAmount - b.PaidAmount) as DueAmount
                FROM Bills b
                INNER JOIN Patients pat ON b.PatientId = pat.PatientId
                INNER JOIN Persons p ON pat.PersonId = p.PersonId
                WHERE b.TotalAmount > b.PaidAmount";

            return paymentManager.ExecuteQuery(query);
        }

        private void cmbBills_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBills.SelectedValue != null && cmbBills.SelectedValue != DBNull.Value)
            {
                try
                {
                    decimal dueAmount = Convert.ToDecimal(
                        ((DataRowView)cmbBills.SelectedItem)["DueAmount"]);
                    numAmount.Maximum = dueAmount;
                    numAmount.Value = dueAmount;
                    lblDueAmount.Text = $"Due: {dueAmount:C}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading bill details: {ex.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnProcessPayment_Click(object sender, EventArgs e)
        {
            if (cmbBills.SelectedValue == null || cmbBills.SelectedValue == DBNull.Value)
            {
                MessageBox.Show("Please select a bill.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbPaymentMethod.SelectedItem == null)
            {
                MessageBox.Show("Please select a payment method.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int billId = Convert.ToInt32(cmbBills.SelectedValue);
            decimal amount = numAmount.Value;
            string paymentMethod = cmbPaymentMethod.SelectedItem.ToString();
            string transactionRef = txtTransactionRef.Text;
            string notes = txtNotes.Text;

            if (amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var payment = new Payment
                {
                    BillId = billId,
                    PatientId = GetPatientIdFromBill(billId),
                    Amount = amount,
                    PaymentMethod = paymentMethod,
                    TransactionReference = string.IsNullOrEmpty(transactionRef) ? null : transactionRef,
                    Notes = string.IsNullOrEmpty(notes) ? $"Payment via {paymentMethod}" : notes,
                    PaymentDate = DateTime.Now,
                    VerifiedBy = 1 // Default admin user for now
                };

                var result = paymentManager.ProcessPayment(payment);

                if (result.Success)
                {
                    MessageBox.Show($"Payment processed successfully!\nReceipt: {result.Data}",
                                  "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Error: {result.Message}", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing payment: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetPatientIdFromBill(int billId)
        {
            string query = "SELECT PatientId FROM Bills WHERE BillId = @BillId";
            var parameters = new[] { new MySql.Data.MySqlClient.MySqlParameter("@BillId", billId) };
            var result = paymentManager.ExecuteScalar(query, parameters);
            return result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}