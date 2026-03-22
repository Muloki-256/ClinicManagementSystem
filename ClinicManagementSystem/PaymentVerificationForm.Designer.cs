using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    partial class PaymentVerificationForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridViewPayments;
        private Button btnVerify;
        private Button btnReject;
        private Button btnPrintReceipt;
        private Button btnRefresh;
        private Button btnClose;
        private Label lblStatus;
        private Label lblTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Text = "Payment Verification";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Title Label
            this.lblTitle = new Label();
            this.lblTitle.Text = "Payment Verification";
            this.lblTitle.Font = new Font("Arial", 16, FontStyle.Bold);
            this.lblTitle.Location = new Point(20, 20);
            this.lblTitle.Size = new Size(300, 30);
            this.lblTitle.ForeColor = Color.DarkBlue;

            // Status Label
            this.lblStatus = new Label();
            this.lblStatus.Text = "Loading payments...";
            this.lblStatus.Location = new Point(20, 60);
            this.lblStatus.Size = new Size(300, 20);

            // Data Grid View
            this.dataGridViewPayments = new DataGridView();
            this.dataGridViewPayments.Location = new Point(20, 90);
            this.dataGridViewPayments.Size = new Size(860, 350);
            this.dataGridViewPayments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPayments.ReadOnly = true;
            this.dataGridViewPayments.AllowUserToAddRows = false;
            this.dataGridViewPayments.AllowUserToDeleteRows = false;

            // Verify Button
            this.btnVerify = new Button();
            this.btnVerify.Text = "Verify Payment";
            this.btnVerify.Location = new Point(20, 460);
            this.btnVerify.Size = new Size(120, 35);
            this.btnVerify.BackColor = Color.LightGreen;
            this.btnVerify.Click += new EventHandler(btnVerify_Click);

            // Reject Button
            this.btnReject = new Button();
            this.btnReject.Text = "Reject Payment";
            this.btnReject.Location = new Point(150, 460);
            this.btnReject.Size = new Size(120, 35);
            this.btnReject.BackColor = Color.LightCoral;
            this.btnReject.Click += new EventHandler(btnReject_Click);

            // Print Receipt Button
            this.btnPrintReceipt = new Button();
            this.btnPrintReceipt.Text = "Print Receipt";
            this.btnPrintReceipt.Location = new Point(280, 460);
            this.btnPrintReceipt.Size = new Size(120, 35);
            this.btnPrintReceipt.BackColor = Color.LightBlue;
            this.btnPrintReceipt.Click += new EventHandler(btnPrintReceipt_Click);

            // Refresh Button
            this.btnRefresh = new Button();
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Location = new Point(410, 460);
            this.btnRefresh.Size = new Size(100, 35);
            this.btnRefresh.Click += new EventHandler(btnRefresh_Click);

            // Close Button
            this.btnClose = new Button();
            this.btnClose.Text = "Close";
            this.btnClose.Location = new Point(780, 460);
            this.btnClose.Size = new Size(100, 35);
            this.btnClose.BackColor = Color.LightGray;
            this.btnClose.Click += new EventHandler(btnClose_Click);

            // Add controls to form
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.dataGridViewPayments);
            this.Controls.Add(this.btnVerify);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.btnPrintReceipt);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClose);
        }
    }
}