namespace ClinicManagementSystem
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Button btnProcessPayment;
        private System.Windows.Forms.Button btnPaymentManagement;
        private System.Windows.Forms.Button btnPatientPortal;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnDoctors;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnMedicalRecords;
        private System.Windows.Forms.Button btnAppointments;
        private System.Windows.Forms.Button btnPatients;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnProcessPayment = new System.Windows.Forms.Button();
            this.btnPaymentManagement = new System.Windows.Forms.Button();
            this.btnPatientPortal = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnDoctors = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnMedicalRecords = new System.Windows.Forms.Button();
            this.btnAppointments = new System.Windows.Forms.Button();
            this.btnPatients = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelSidebar.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelSidebar.Controls.Add(this.btnProcessPayment);
            this.panelSidebar.Controls.Add(this.btnPaymentManagement);
            this.panelSidebar.Controls.Add(this.btnPatientPortal);
            this.panelSidebar.Controls.Add(this.btnOrders);
            this.panelSidebar.Controls.Add(this.btnDoctors);
            this.panelSidebar.Controls.Add(this.btnReports);
            this.panelSidebar.Controls.Add(this.btnMedicalRecords);
            this.panelSidebar.Controls.Add(this.btnAppointments);
            this.panelSidebar.Controls.Add(this.btnPatients);
            this.panelSidebar.Controls.Add(this.btnExit);
            this.panelSidebar.Controls.Add(this.btnLogout);
            this.panelSidebar.Controls.Add(this.lblWelcome);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(300, 923);
            this.panelSidebar.TabIndex = 0;
            // 
            // btnProcessPayment
            // 
            this.btnProcessPayment.Location = new System.Drawing.Point(30, 662);
            this.btnProcessPayment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnProcessPayment.Name = "btnProcessPayment";
            this.btnProcessPayment.Size = new System.Drawing.Size(240, 62);
            this.btnProcessPayment.TabIndex = 11;
            this.btnProcessPayment.Text = "Process Payment";
            this.btnProcessPayment.UseVisualStyleBackColor = true;
            this.btnProcessPayment.Click += new System.EventHandler(this.btnProcessPayment_Click);
            // 
            // btnPaymentManagement
            // 
            this.btnPaymentManagement.Location = new System.Drawing.Point(30, 585);
            this.btnPaymentManagement.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPaymentManagement.Name = "btnPaymentManagement";
            this.btnPaymentManagement.Size = new System.Drawing.Size(240, 62);
            this.btnPaymentManagement.TabIndex = 10;
            this.btnPaymentManagement.Text = "Payment Management";
            this.btnPaymentManagement.UseVisualStyleBackColor = true;
            this.btnPaymentManagement.Click += new System.EventHandler(this.btnPaymentManagement_Click);
            // 
            // btnPatientPortal
            // 
            this.btnPatientPortal.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.btnPatientPortal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPatientPortal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPatientPortal.ForeColor = System.Drawing.Color.White;
            this.btnPatientPortal.Location = new System.Drawing.Point(30, 354);
            this.btnPatientPortal.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPatientPortal.Name = "btnPatientPortal";
            this.btnPatientPortal.Size = new System.Drawing.Size(240, 62);
            this.btnPatientPortal.TabIndex = 9;
            this.btnPatientPortal.Text = "Patient Portal";
            this.btnPatientPortal.UseVisualStyleBackColor = false;
            this.btnPatientPortal.Click += new System.EventHandler(this.btnPatientPortal_Click);
            // 
            // btnOrders
            // 
            this.btnOrders.Location = new System.Drawing.Point(30, 508);
            this.btnOrders.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(240, 62);
            this.btnOrders.TabIndex = 8;
            this.btnOrders.Text = "Orders";
            this.btnOrders.UseVisualStyleBackColor = true;
            this.btnOrders.Click += new System.EventHandler(this.btnOrders_Click);
            // 
            // btnDoctors
            // 
            this.btnDoctors.Location = new System.Drawing.Point(30, 431);
            this.btnDoctors.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDoctors.Name = "btnDoctors";
            this.btnDoctors.Size = new System.Drawing.Size(240, 62);
            this.btnDoctors.TabIndex = 7;
            this.btnDoctors.Text = "Manage Doctors";
            this.btnDoctors.UseVisualStyleBackColor = true;
            this.btnDoctors.Click += new System.EventHandler(this.btnDoctors_Click);
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(30, 277);
            this.btnReports.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(240, 62);
            this.btnReports.TabIndex = 6;
            this.btnReports.Text = "Reports";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // btnMedicalRecords
            // 
            this.btnMedicalRecords.Location = new System.Drawing.Point(30, 200);
            this.btnMedicalRecords.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMedicalRecords.Name = "btnMedicalRecords";
            this.btnMedicalRecords.Size = new System.Drawing.Size(240, 62);
            this.btnMedicalRecords.TabIndex = 5;
            this.btnMedicalRecords.Text = "Medical Records";
            this.btnMedicalRecords.UseVisualStyleBackColor = true;
            this.btnMedicalRecords.Click += new System.EventHandler(this.btnMedicalRecords_Click);
            // 
            // btnAppointments
            // 
            this.btnAppointments.Location = new System.Drawing.Point(30, 123);
            this.btnAppointments.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAppointments.Name = "btnAppointments";
            this.btnAppointments.Size = new System.Drawing.Size(240, 62);
            this.btnAppointments.TabIndex = 4;
            this.btnAppointments.Text = "Appointments";
            this.btnAppointments.UseVisualStyleBackColor = true;
            this.btnAppointments.Click += new System.EventHandler(this.btnAppointments_Click);
            // 
            // btnPatients
            // 
            this.btnPatients.Location = new System.Drawing.Point(30, 46);
            this.btnPatients.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPatients.Name = "btnPatients";
            this.btnPatients.Size = new System.Drawing.Size(240, 62);
            this.btnPatients.TabIndex = 3;
            this.btnPatients.Text = "Patients";
            this.btnPatients.UseVisualStyleBackColor = true;
            this.btnPatients.Click += new System.EventHandler(this.btnPatients_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(30, 816);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(240, 62);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(30, 739);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(240, 62);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblWelcome
            // 
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(15, 893);
            this.lblWelcome.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(270, 92);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(300, 891);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1200, 32);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(179, 25);
            this.lblStatus.Text = "toolStripStatusLabel1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1500, 923);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panelSidebar);
            this.DoubleBuffered = true;
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clinic Management System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.panelSidebar.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}