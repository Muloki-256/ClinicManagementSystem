using ClinicManagementSystem.Managers;
using ClinicManagementSystem.Models;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class ReceiptForm : Form
    {
        private PaymentReceipt _receiptData;
        private PrintDocument _printDocument;
        private TextBox txtReceipt;
        private Button btnPrint;
        private Button btnSave;
        private Button btnClose;

        public ReceiptForm(string receiptNumber)
        {
            InitializeComponent();
            LoadReceiptData(receiptNumber);
            SetupPrinting();
            DisplayReceipt();
        }

        private void LoadReceiptData(string receiptNumber)
        {
            var paymentManager = new PaymentManager();
            _receiptData = paymentManager.GetReceiptData(receiptNumber);

            if (_receiptData == null)
            {
                MessageBox.Show("Receipt not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void SetupPrinting()
        {
            _printDocument = new PrintDocument();
            _printDocument.PrintPage += new PrintPageEventHandler(PrintReceipt);
        }

        private void DisplayReceipt()
        {
            if (_receiptData == null) return;

            txtReceipt.Text = GenerateReceiptText();
        }

        private string GenerateReceiptText()
        {
            var sb = new StringBuilder();

            sb.AppendLine("╔════════════════════════════════════════╗");
            sb.AppendLine("║           CITY CLINIC SYSTEM           ║");
            sb.AppendLine("╠════════════════════════════════════════╣");
            sb.AppendLine($"║ Receipt: {_receiptData.ReceiptNumber,-20} ║");
            sb.AppendLine($"║ Date:   {_receiptData.PaymentDateFormatted,-20} ║");
            sb.AppendLine("╠════════════════════════════════════════╣");
            sb.AppendLine($"║ Patient: {_receiptData.PatientName,-27} ║");
            sb.AppendLine($"║ Phone:   {_receiptData.PatientPhone,-27} ║");
            sb.AppendLine("╠════════════════════════════════════════╣");
            sb.AppendLine($"║ Description: {_receiptData.BillDescription,-23} ║");
            sb.AppendLine("╠════════════════════════════════════════╣");
            sb.AppendLine($"║ Amount Paid: {_receiptData.AmountPaid,15:C} ║");
            sb.AppendLine($"║ Payment Method: {_receiptData.PaymentMethod,-19} ║");
            if (!string.IsNullOrEmpty(_receiptData.TransactionReference))
                sb.AppendLine($"║ Transaction: {_receiptData.TransactionReference,-22} ║");
            sb.AppendLine("╠════════════════════════════════════════╣");
            sb.AppendLine($"║ Bill Total:   {_receiptData.BillTotal,15:C} ║");
            sb.AppendLine($"║ Previous Due: {_receiptData.PreviousBalance,15:C} ║");
            sb.AppendLine($"║ Remaining:    {_receiptData.NewBalance,15:C} ║");
            sb.AppendLine("╠════════════════════════════════════════╣");
            sb.AppendLine($"║ {_receiptData.AmountInWords,-34} ║");
            sb.AppendLine("╠════════════════════════════════════════╣");
            sb.AppendLine($"║ Received by: {_receiptData.ReceivedBy,-23} ║");
            sb.AppendLine("╚════════════════════════════════════════╝");
            sb.AppendLine();
            sb.AppendLine("        Thank you for your payment!");
            sb.AppendLine("     Please bring this receipt for any");
            sb.AppendLine("           future correspondence");

            return sb.ToString();
        }

        private void PrintReceipt(object sender, PrintPageEventArgs e)
        {
            Font font = new Font("Courier New", 10);
            e.Graphics.DrawString(txtReceipt.Text, font, Brushes.Black, 10, 10);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = _printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                _printDocument.Print();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text Files|*.txt";
            saveDialog.FileName = $"Receipt_{_receiptData.ReceiptNumber}.txt";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllText(saveDialog.FileName, txtReceipt.Text);
                MessageBox.Show("Receipt saved successfully!", "Success",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}