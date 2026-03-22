namespace ClinicManagementSystem
{
    partial class AppointmentForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpAppointmentDetails;
        private System.Windows.Forms.DateTimePicker dtpAppointmentDate;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.ComboBox cmbDoctor;
        private System.Windows.Forms.ComboBox cmbPatient;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.Label lblAppointmentDate;
        private System.Windows.Forms.Label lblDoctor;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.DataGridView dgvAppointments;
        private System.Windows.Forms.GroupBox grpFilters;
        private System.Windows.Forms.DateTimePicker dtpFilterDate;
        private System.Windows.Forms.ComboBox cmbFilterDoctor;
        private System.Windows.Forms.Label lblFilterDate;
        private System.Windows.Forms.Label lblFilterDoctor;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Button btnComplete;
        private System.Windows.Forms.Button btnCancelAppointment;
        private System.Windows.Forms.CheckBox chkFilterDate;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpAppointmentDetails = new System.Windows.Forms.GroupBox();
            this.dtpAppointmentDate = new System.Windows.Forms.DateTimePicker();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.cmbDoctor = new System.Windows.Forms.ComboBox();
            this.cmbPatient = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblReason = new System.Windows.Forms.Label();
            this.lblAppointmentDate = new System.Windows.Forms.Label();
            this.lblDoctor = new System.Windows.Forms.Label();
            this.lblPatient = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.grpFilters = new System.Windows.Forms.GroupBox();
            this.chkFilterDate = new System.Windows.Forms.CheckBox();
            this.dtpFilterDate = new System.Windows.Forms.DateTimePicker();
            this.cmbFilterDoctor = new System.Windows.Forms.ComboBox();
            this.lblFilterDate = new System.Windows.Forms.Label();
            this.lblFilterDoctor = new System.Windows.Forms.Label();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.btnComplete = new System.Windows.Forms.Button();
            this.btnCancelAppointment = new System.Windows.Forms.Button();
            this.grpAppointmentDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.grpFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(133, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Appointments";
            // 
            // grpAppointmentDetails
            // 
            this.grpAppointmentDetails.Controls.Add(this.dtpAppointmentDate);
            this.grpAppointmentDetails.Controls.Add(this.cmbStatus);
            this.grpAppointmentDetails.Controls.Add(this.txtReason);
            this.grpAppointmentDetails.Controls.Add(this.cmbDoctor);
            this.grpAppointmentDetails.Controls.Add(this.cmbPatient);
            this.grpAppointmentDetails.Controls.Add(this.lblStatus);
            this.grpAppointmentDetails.Controls.Add(this.lblReason);
            this.grpAppointmentDetails.Controls.Add(this.lblAppointmentDate);
            this.grpAppointmentDetails.Controls.Add(this.lblDoctor);
            this.grpAppointmentDetails.Controls.Add(this.lblPatient);
            this.grpAppointmentDetails.Controls.Add(this.btnSave);
            this.grpAppointmentDetails.Controls.Add(this.btnCancel);
            this.grpAppointmentDetails.Enabled = false;
            this.grpAppointmentDetails.Location = new System.Drawing.Point(12, 40);
            this.grpAppointmentDetails.Name = "grpAppointmentDetails";
            this.grpAppointmentDetails.Size = new System.Drawing.Size(400, 250);
            this.grpAppointmentDetails.TabIndex = 1;
            this.grpAppointmentDetails.TabStop = false;
            this.grpAppointmentDetails.Text = "Appointment Details";
            // 
            // dtpAppointmentDate
            // 
            this.dtpAppointmentDate.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dtpAppointmentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAppointmentDate.Location = new System.Drawing.Point(120, 80);
            this.dtpAppointmentDate.Name = "dtpAppointmentDate";
            this.dtpAppointmentDate.Size = new System.Drawing.Size(200, 20);
            this.dtpAppointmentDate.TabIndex = 3;
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(120, 160);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(150, 21);
            this.cmbStatus.TabIndex = 5;
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(120, 110);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(250, 45);
            this.txtReason.TabIndex = 4;
            // 
            // cmbDoctor
            // 
            this.cmbDoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDoctor.FormattingEnabled = true;
            this.cmbDoctor.Location = new System.Drawing.Point(120, 50);
            this.cmbDoctor.Name = "cmbDoctor";
            this.cmbDoctor.Size = new System.Drawing.Size(250, 21);
            this.cmbDoctor.TabIndex = 2;
            // 
            // cmbPatient
            // 
            this.cmbPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatient.FormattingEnabled = true;
            this.cmbPatient.Location = new System.Drawing.Point(120, 20);
            this.cmbPatient.Name = "cmbPatient";
            this.cmbPatient.Size = new System.Drawing.Size(250, 21);
            this.cmbPatient.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 163);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status:";
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(20, 113);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(47, 13);
            this.lblReason.TabIndex = 4;
            this.lblReason.Text = "Reason:";
            // 
            // lblAppointmentDate
            // 
            this.lblAppointmentDate.AutoSize = true;
            this.lblAppointmentDate.Location = new System.Drawing.Point(20, 83);
            this.lblAppointmentDate.Name = "lblAppointmentDate";
            this.lblAppointmentDate.Size = new System.Drawing.Size(96, 13);
            this.lblAppointmentDate.TabIndex = 3;
            this.lblAppointmentDate.Text = "Appointment Date:";
            // 
            // lblDoctor
            // 
            this.lblDoctor.AutoSize = true;
            this.lblDoctor.Location = new System.Drawing.Point(20, 53);
            this.lblDoctor.Name = "lblDoctor";
            this.lblDoctor.Size = new System.Drawing.Size(42, 13);
            this.lblDoctor.TabIndex = 2;
            this.lblDoctor.Text = "Doctor:";
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Location = new System.Drawing.Point(20, 23);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(44, 13);
            this.lblPatient.TabIndex = 1;
            this.lblPatient.Text = "Patient:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(120, 200);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(205, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(430, 40);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 30);
            this.btnNew.TabIndex = 2;
            this.btnNew.Text = "New Appointment";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // dgvAppointments
            // 
            this.dgvAppointments.AllowUserToAddRows = false;
            this.dgvAppointments.AllowUserToDeleteRows = false;
            this.dgvAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAppointments.Location = new System.Drawing.Point(430, 80);
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.ReadOnly = true;
            this.dgvAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppointments.Size = new System.Drawing.Size(550, 300);
            this.dgvAppointments.TabIndex = 3;
            this.dgvAppointments.SelectionChanged += new System.EventHandler(this.dgvAppointments_SelectionChanged);
            // 
            // grpFilters
            // 
            this.grpFilters.Controls.Add(this.chkFilterDate);
            this.grpFilters.Controls.Add(this.dtpFilterDate);
            this.grpFilters.Controls.Add(this.cmbFilterDoctor);
            this.grpFilters.Controls.Add(this.lblFilterDate);
            this.grpFilters.Controls.Add(this.lblFilterDoctor);
            this.grpFilters.Controls.Add(this.btnFilter);
            this.grpFilters.Controls.Add(this.btnClearFilter);
            this.grpFilters.Location = new System.Drawing.Point(12, 300);
            this.grpFilters.Name = "grpFilters";
            this.grpFilters.Size = new System.Drawing.Size(400, 120);
            this.grpFilters.TabIndex = 4;
            this.grpFilters.TabStop = false;
            this.grpFilters.Text = "Filters";
            // 
            // chkFilterDate
            // 
            this.chkFilterDate.AutoSize = true;
            this.chkFilterDate.Location = new System.Drawing.Point(210, 28);
            this.chkFilterDate.Name = "chkFilterDate";
            this.chkFilterDate.Size = new System.Drawing.Size(15, 14);
            this.chkFilterDate.TabIndex = 12;
            this.chkFilterDate.UseVisualStyleBackColor = true;
            // 
            // dtpFilterDate
            // 
            this.dtpFilterDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFilterDate.Location = new System.Drawing.Point(80, 25);
            this.dtpFilterDate.Name = "dtpFilterDate";
            this.dtpFilterDate.Size = new System.Drawing.Size(120, 20);
            this.dtpFilterDate.TabIndex = 8;
            // 
            // cmbFilterDoctor
            // 
            this.cmbFilterDoctor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilterDoctor.FormattingEnabled = true;
            this.cmbFilterDoctor.Location = new System.Drawing.Point(80, 55);
            this.cmbFilterDoctor.Name = "cmbFilterDoctor";
            this.cmbFilterDoctor.Size = new System.Drawing.Size(200, 21);
            this.cmbFilterDoctor.TabIndex = 9;
            // 
            // lblFilterDate
            // 
            this.lblFilterDate.AutoSize = true;
            this.lblFilterDate.Location = new System.Drawing.Point(20, 28);
            this.lblFilterDate.Name = "lblFilterDate";
            this.lblFilterDate.Size = new System.Drawing.Size(33, 13);
            this.lblFilterDate.TabIndex = 2;
            this.lblFilterDate.Text = "Date:";
            // 
            // lblFilterDoctor
            // 
            this.lblFilterDoctor.AutoSize = true;
            this.lblFilterDoctor.Location = new System.Drawing.Point(20, 58);
            this.lblFilterDoctor.Name = "lblFilterDoctor";
            this.lblFilterDoctor.Size = new System.Drawing.Size(42, 13);
            this.lblFilterDoctor.TabIndex = 3;
            this.lblFilterDoctor.Text = "Doctor:";
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(300, 25);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 10;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Location = new System.Drawing.Point(300, 55);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(75, 23);
            this.btnClearFilter.TabIndex = 11;
            this.btnClearFilter.Text = "Clear";
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // btnComplete
            // 
            this.btnComplete.Enabled = false;
            this.btnComplete.Location = new System.Drawing.Point(430, 390);
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(100, 30);
            this.btnComplete.TabIndex = 5;
            this.btnComplete.Text = "Mark Complete";
            this.btnComplete.UseVisualStyleBackColor = true;
            this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
            // 
            // btnCancelAppointment
            // 
            this.btnCancelAppointment.Enabled = false;
            this.btnCancelAppointment.Location = new System.Drawing.Point(540, 390);
            this.btnCancelAppointment.Name = "btnCancelAppointment";
            this.btnCancelAppointment.Size = new System.Drawing.Size(100, 30);
            this.btnCancelAppointment.TabIndex = 6;
            this.btnCancelAppointment.Text = "Cancel Appointment";
            this.btnCancelAppointment.UseVisualStyleBackColor = true;
            this.btnCancelAppointment.Click += new System.EventHandler(this.btnCancelAppointment_Click);
            // 
            // AppointmentForm
            // 
            this.ClientSize = new System.Drawing.Size(1000, 450);
            this.Controls.Add(this.btnCancelAppointment);
            this.Controls.Add(this.btnComplete);
            this.Controls.Add(this.grpFilters);
            this.Controls.Add(this.dgvAppointments);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.grpAppointmentDetails);
            this.Controls.Add(this.lblTitle);
            this.Name = "AppointmentForm";
            this.Text = "Appointment Management";
            this.Load += new System.EventHandler(this.AppointmentForm_Load);
            this.grpAppointmentDetails.ResumeLayout(false);
            this.grpAppointmentDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.grpFilters.ResumeLayout(false);
            this.grpFilters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}