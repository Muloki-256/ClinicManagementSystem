using System;
using System.Windows.Forms;

namespace ClinicManagementSystem
{

    partial class PatientLoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtFullName;
        private TextBox txtPhone;
        private TextBox txtEmail;
        private Button btnNext;
        private Button btnBack;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;

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
            this.txtFullName = new TextBox();
            this.txtPhone = new TextBox();
            this.txtEmail = new TextBox();
            this.btnNext = new Button();
            this.btnBack = new Button();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.label4 = new Label();
            this.SuspendLayout();

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Patient Order Portal";

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Full Name:";

            // txtFullName
            this.txtFullName.Location = new System.Drawing.Point(30, 80);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(300, 22);
            this.txtFullName.TabIndex = 1;
            // REMOVED: this.txtFullName.PlaceholderText = "Enter your full name";

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Phone (Optional):";

            // txtPhone
            this.txtPhone.Location = new System.Drawing.Point(30, 140);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(300, 22);
            this.txtPhone.TabIndex = 2;
            // REMOVED: this.txtPhone.PlaceholderText = "Enter your phone number";

            // label4
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 180);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Email (Optional):";

            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(30, 200);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(300, 22);
            this.txtEmail.TabIndex = 3;
            // REMOVED: this.txtEmail.PlaceholderText = "Enter your email address";

            // btnNext
            this.btnNext.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnNext.ForeColor = System.Drawing.Color.White;
            this.btnNext.Location = new System.Drawing.Point(180, 250);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(150, 40);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next →";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new EventHandler(this.btnNext_Click);

            // btnBack
            this.btnBack.Location = new System.Drawing.Point(30, 250);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 40);
            this.btnBack.TabIndex = 5;
            this.btnBack.Text = "← Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            // PatientLoginForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 320);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PatientLoginForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Patient Order Portal";
            this.Load += new EventHandler(this.PatientLoginForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }

}