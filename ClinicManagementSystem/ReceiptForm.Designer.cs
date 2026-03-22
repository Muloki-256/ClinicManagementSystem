using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    partial class ReceiptForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.ClientSize = new System.Drawing.Size(500, 600);
            this.Text = "Payment Receipt";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Receipt Text Box
            this.txtReceipt = new TextBox();
            this.txtReceipt.Location = new Point(20, 20);
            this.txtReceipt.Size = new Size(460, 450);
            this.txtReceipt.Multiline = true;
            this.txtReceipt.ScrollBars = ScrollBars.Vertical;
            this.txtReceipt.Font = new Font("Courier New", 9);
            this.txtReceipt.ReadOnly = true;

            // Print Button
            this.btnPrint = new Button();
            this.btnPrint.Text = "Print Receipt";
            this.btnPrint.Location = new Point(20, 490);
            this.btnPrint.Size = new Size(100, 35);
            this.btnPrint.BackColor = Color.LightBlue;

            // Save Button
            this.btnSave = new Button();
            this.btnSave.Text = "Save as Text";
            this.btnSave.Location = new Point(130, 490);
            this.btnSave.Size = new Size(100, 35);

            // Close Button
            this.btnClose = new Button();
            this.btnClose.Text = "Close";
            this.btnClose.Location = new Point(380, 490);
            this.btnClose.Size = new Size(100, 35);
            this.btnClose.BackColor = Color.LightGray;

            // Add event handlers
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnClose.Click += new EventHandler(btnClose_Click);

            // Add controls to form
            this.Controls.Add(this.txtReceipt);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
        }
    }
}