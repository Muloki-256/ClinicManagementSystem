namespace ClinicManagementSystem
{
    partial class MedicalRecordsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpPatientSelection;
        private System.Windows.Forms.ComboBox cmbPatients;
        private System.Windows.Forms.Label lblSelectPatient;
        private System.Windows.Forms.Button btnLoadRecords;
        private System.Windows.Forms.GroupBox grpMedicalRecord;
        private System.Windows.Forms.TextBox txtTreatmentNotes;
        private System.Windows.Forms.TextBox txtSymptoms;
        private System.Windows.Forms.TextBox txtDiagnosis;
        private System.Windows.Forms.Label lblTreatmentNotes;
        private System.Windows.Forms.Label lblSymptoms;
        private System.Windows.Forms.Label lblDiagnosis;
        private System.Windows.Forms.Button btnSaveRecord;
        private System.Windows.Forms.Button btnNewRecord;
        private System.Windows.Forms.GroupBox grpPrescriptions;
        private System.Windows.Forms.DataGridView dgvPrescriptions;
        private System.Windows.Forms.Button btnAddPrescription;
        private System.Windows.Forms.Button btnRemovePrescription;
        private System.Windows.Forms.GroupBox grpPrescriptionDetails;
        private System.Windows.Forms.TextBox txtInstructions;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.TextBox txtDosage;
        private System.Windows.Forms.ComboBox cmbTablets;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblDosage;
        private System.Windows.Forms.Label lblTablet;
        private System.Windows.Forms.Button btnAddToPrescription;
        private System.Windows.Forms.DataGridView dgvMedicalRecords;
        private System.Windows.Forms.Label lblMedicalHistory;

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
            this.grpPatientSelection = new System.Windows.Forms.GroupBox();
            this.cmbPatients = new System.Windows.Forms.ComboBox();
            this.lblSelectPatient = new System.Windows.Forms.Label();
            this.btnLoadRecords = new System.Windows.Forms.Button();
            this.grpMedicalRecord = new System.Windows.Forms.GroupBox();
            this.txtTreatmentNotes = new System.Windows.Forms.TextBox();
            this.txtSymptoms = new System.Windows.Forms.TextBox();
            this.txtDiagnosis = new System.Windows.Forms.TextBox();
            this.lblTreatmentNotes = new System.Windows.Forms.Label();
            this.lblSymptoms = new System.Windows.Forms.Label();
            this.lblDiagnosis = new System.Windows.Forms.Label();
            this.btnSaveRecord = new System.Windows.Forms.Button();
            this.btnNewRecord = new System.Windows.Forms.Button();
            this.grpPrescriptions = new System.Windows.Forms.GroupBox();
            this.dgvPrescriptions = new System.Windows.Forms.DataGridView();
            this.btnAddPrescription = new System.Windows.Forms.Button();
            this.btnRemovePrescription = new System.Windows.Forms.Button();
            this.grpPrescriptionDetails = new System.Windows.Forms.GroupBox();
            this.txtInstructions = new System.Windows.Forms.TextBox();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtDosage = new System.Windows.Forms.TextBox();
            this.cmbTablets = new System.Windows.Forms.ComboBox();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblDosage = new System.Windows.Forms.Label();
            this.lblTablet = new System.Windows.Forms.Label();
            this.btnAddToPrescription = new System.Windows.Forms.Button();
            this.dgvMedicalRecords = new System.Windows.Forms.DataGridView();
            this.lblMedicalHistory = new System.Windows.Forms.Label();
            this.grpPatientSelection.SuspendLayout();
            this.grpMedicalRecord.SuspendLayout();
            this.grpPrescriptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrescriptions)).BeginInit();
            this.grpPrescriptionDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicalRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(158, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Medical Records";
            // 
            // grpPatientSelection
            // 
            this.grpPatientSelection.Controls.Add(this.cmbPatients);
            this.grpPatientSelection.Controls.Add(this.lblSelectPatient);
            this.grpPatientSelection.Controls.Add(this.btnLoadRecords);
            this.grpPatientSelection.Location = new System.Drawing.Point(12, 40);
            this.grpPatientSelection.Name = "grpPatientSelection";
            this.grpPatientSelection.Size = new System.Drawing.Size(400, 80);
            this.grpPatientSelection.TabIndex = 1;
            this.grpPatientSelection.TabStop = false;
            this.grpPatientSelection.Text = "Patient Selection";
            // 
            // cmbPatients
            // 
            this.cmbPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatients.FormattingEnabled = true;
            this.cmbPatients.Location = new System.Drawing.Point(80, 25);
            this.cmbPatients.Name = "cmbPatients";
            this.cmbPatients.Size = new System.Drawing.Size(200, 21);
            this.cmbPatients.TabIndex = 1;
            // 
            // lblSelectPatient
            // 
            this.lblSelectPatient.AutoSize = true;
            this.lblSelectPatient.Location = new System.Drawing.Point(20, 28);
            this.lblSelectPatient.Name = "lblSelectPatient";
            this.lblSelectPatient.Size = new System.Drawing.Size(44, 13);
            this.lblSelectPatient.TabIndex = 0;
            this.lblSelectPatient.Text = "Patient:";
            // 
            // btnLoadRecords
            // 
            this.btnLoadRecords.Location = new System.Drawing.Point(300, 23);
            this.btnLoadRecords.Name = "btnLoadRecords";
            this.btnLoadRecords.Size = new System.Drawing.Size(75, 23);
            this.btnLoadRecords.TabIndex = 2;
            this.btnLoadRecords.Text = "Load Records";
            this.btnLoadRecords.UseVisualStyleBackColor = true;
            this.btnLoadRecords.Click += new System.EventHandler(this.btnLoadRecords_Click);
            // 
            // grpMedicalRecord
            // 
            this.grpMedicalRecord.Controls.Add(this.txtTreatmentNotes);
            this.grpMedicalRecord.Controls.Add(this.txtSymptoms);
            this.grpMedicalRecord.Controls.Add(this.txtDiagnosis);
            this.grpMedicalRecord.Controls.Add(this.lblTreatmentNotes);
            this.grpMedicalRecord.Controls.Add(this.lblSymptoms);
            this.grpMedicalRecord.Controls.Add(this.lblDiagnosis);
            this.grpMedicalRecord.Controls.Add(this.btnSaveRecord);
            this.grpMedicalRecord.Controls.Add(this.btnNewRecord);
            this.grpMedicalRecord.Enabled = false;
            this.grpMedicalRecord.Location = new System.Drawing.Point(12, 130);
            this.grpMedicalRecord.Name = "grpMedicalRecord";
            this.grpMedicalRecord.Size = new System.Drawing.Size(400, 300);
            this.grpMedicalRecord.TabIndex = 2;
            this.grpMedicalRecord.TabStop = false;
            this.grpMedicalRecord.Text = "Medical Record";
            // 
            // txtTreatmentNotes
            // 
            this.txtTreatmentNotes.Location = new System.Drawing.Point(120, 110);
            this.txtTreatmentNotes.Multiline = true;
            this.txtTreatmentNotes.Name = "txtTreatmentNotes";
            this.txtTreatmentNotes.Size = new System.Drawing.Size(250, 60);
            this.txtTreatmentNotes.TabIndex = 3;
            // 
            // txtSymptoms
            // 
            this.txtSymptoms.Location = new System.Drawing.Point(120, 60);
            this.txtSymptoms.Multiline = true;
            this.txtSymptoms.Name = "txtSymptoms";
            this.txtSymptoms.Size = new System.Drawing.Size(250, 45);
            this.txtSymptoms.TabIndex = 2;
            // 
            // txtDiagnosis
            // 
            this.txtDiagnosis.Location = new System.Drawing.Point(120, 25);
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.txtDiagnosis.Size = new System.Drawing.Size(250, 20);
            this.txtDiagnosis.TabIndex = 1;
            // 
            // lblTreatmentNotes
            // 
            this.lblTreatmentNotes.AutoSize = true;
            this.lblTreatmentNotes.Location = new System.Drawing.Point(20, 113);
            this.lblTreatmentNotes.Name = "lblTreatmentNotes";
            this.lblTreatmentNotes.Size = new System.Drawing.Size(88, 13);
            this.lblTreatmentNotes.TabIndex = 5;
            this.lblTreatmentNotes.Text = "Treatment Notes:";
            // 
            // lblSymptoms
            // 
            this.lblSymptoms.AutoSize = true;
            this.lblSymptoms.Location = new System.Drawing.Point(20, 63);
            this.lblSymptoms.Name = "lblSymptoms";
            this.lblSymptoms.Size = new System.Drawing.Size(58, 13);
            this.lblSymptoms.TabIndex = 4;
            this.lblSymptoms.Text = "Symptoms:";
            // 
            // lblDiagnosis
            // 
            this.lblDiagnosis.AutoSize = true;
            this.lblDiagnosis.Location = new System.Drawing.Point(20, 28);
            this.lblDiagnosis.Name = "lblDiagnosis";
            this.lblDiagnosis.Size = new System.Drawing.Size(56, 13);
            this.lblDiagnosis.TabIndex = 3;
            this.lblDiagnosis.Text = "Diagnosis:";
            // 
            // btnSaveRecord
            // 
            this.btnSaveRecord.Location = new System.Drawing.Point(120, 250);
            this.btnSaveRecord.Name = "btnSaveRecord";
            this.btnSaveRecord.Size = new System.Drawing.Size(100, 30);
            this.btnSaveRecord.TabIndex = 4;
            this.btnSaveRecord.Text = "Save Record";
            this.btnSaveRecord.UseVisualStyleBackColor = true;
            this.btnSaveRecord.Click += new System.EventHandler(this.btnSaveRecord_Click);
            // 
            // btnNewRecord
            // 
            this.btnNewRecord.Location = new System.Drawing.Point(230, 250);
            this.btnNewRecord.Name = "btnNewRecord";
            this.btnNewRecord.Size = new System.Drawing.Size(100, 30);
            this.btnNewRecord.TabIndex = 5;
            this.btnNewRecord.Text = "New Record";
            this.btnNewRecord.UseVisualStyleBackColor = true;
            this.btnNewRecord.Click += new System.EventHandler(this.btnNewRecord_Click);
            // 
            // grpPrescriptions
            // 
            this.grpPrescriptions.Controls.Add(this.dgvPrescriptions);
            this.grpPrescriptions.Controls.Add(this.btnAddPrescription);
            this.grpPrescriptions.Controls.Add(this.btnRemovePrescription);
            this.grpPrescriptions.Enabled = false;
            this.grpPrescriptions.Location = new System.Drawing.Point(12, 440);
            this.grpPrescriptions.Name = "grpPrescriptions";
            this.grpPrescriptions.Size = new System.Drawing.Size(400, 200);
            this.grpPrescriptions.TabIndex = 3;
            this.grpPrescriptions.TabStop = false;
            this.grpPrescriptions.Text = "Prescriptions";
            // 
            // dgvPrescriptions
            // 
            this.dgvPrescriptions.AllowUserToAddRows = false;
            this.dgvPrescriptions.AllowUserToDeleteRows = false;
            this.dgvPrescriptions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPrescriptions.Location = new System.Drawing.Point(20, 20);
            this.dgvPrescriptions.Name = "dgvPrescriptions";
            this.dgvPrescriptions.ReadOnly = true;
            this.dgvPrescriptions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrescriptions.Size = new System.Drawing.Size(350, 120);
            this.dgvPrescriptions.TabIndex = 0;
            // 
            // btnAddPrescription
            // 
            this.btnAddPrescription.Location = new System.Drawing.Point(20, 150);
            this.btnAddPrescription.Name = "btnAddPrescription";
            this.btnAddPrescription.Size = new System.Drawing.Size(120, 30);
            this.btnAddPrescription.TabIndex = 1;
            this.btnAddPrescription.Text = "Add Prescription";
            this.btnAddPrescription.UseVisualStyleBackColor = true;
            this.btnAddPrescription.Click += new System.EventHandler(this.btnAddPrescription_Click);
            // 
            // btnRemovePrescription
            // 
            this.btnRemovePrescription.Location = new System.Drawing.Point(150, 150);
            this.btnRemovePrescription.Name = "btnRemovePrescription";
            this.btnRemovePrescription.Size = new System.Drawing.Size(120, 30);
            this.btnRemovePrescription.TabIndex = 2;
            this.btnRemovePrescription.Text = "Remove Selected";
            this.btnRemovePrescription.UseVisualStyleBackColor = true;
            this.btnRemovePrescription.Click += new System.EventHandler(this.btnRemovePrescription_Click);
            // 
            // grpPrescriptionDetails
            // 
            this.grpPrescriptionDetails.Controls.Add(this.txtInstructions);
            this.grpPrescriptionDetails.Controls.Add(this.nudQuantity);
            this.grpPrescriptionDetails.Controls.Add(this.txtDuration);
            this.grpPrescriptionDetails.Controls.Add(this.txtDosage);
            this.grpPrescriptionDetails.Controls.Add(this.cmbTablets);
            this.grpPrescriptionDetails.Controls.Add(this.lblInstructions);
            this.grpPrescriptionDetails.Controls.Add(this.lblQuantity);
            this.grpPrescriptionDetails.Controls.Add(this.lblDuration);
            this.grpPrescriptionDetails.Controls.Add(this.lblDosage);
            this.grpPrescriptionDetails.Controls.Add(this.lblTablet);
            this.grpPrescriptionDetails.Controls.Add(this.btnAddToPrescription);
            this.grpPrescriptionDetails.Enabled = false;
            this.grpPrescriptionDetails.Location = new System.Drawing.Point(430, 40);
            this.grpPrescriptionDetails.Name = "grpPrescriptionDetails";
            this.grpPrescriptionDetails.Size = new System.Drawing.Size(350, 250);
            this.grpPrescriptionDetails.TabIndex = 4;
            this.grpPrescriptionDetails.TabStop = false;
            this.grpPrescriptionDetails.Text = "Prescription Details";
            // 
            // txtInstructions
            // 
            this.txtInstructions.Location = new System.Drawing.Point(100, 160);
            this.txtInstructions.Multiline = true;
            this.txtInstructions.Name = "txtInstructions";
            this.txtInstructions.Size = new System.Drawing.Size(200, 45);
            this.txtInstructions.TabIndex = 5;
            // 
            // nudQuantity
            // 
            this.nudQuantity.Location = new System.Drawing.Point(100, 130);
            this.nudQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(60, 20);
            this.nudQuantity.TabIndex = 4;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtDuration
            // 
            this.txtDuration.Location = new System.Drawing.Point(100, 100);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(150, 20);
            this.txtDuration.TabIndex = 3;
            // 
            // txtDosage
            // 
            this.txtDosage.Location = new System.Drawing.Point(100, 70);
            this.txtDosage.Name = "txtDosage";
            this.txtDosage.Size = new System.Drawing.Size(150, 20);
            this.txtDosage.TabIndex = 2;
            // 
            // cmbTablets
            // 
            this.cmbTablets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTablets.FormattingEnabled = true;
            this.cmbTablets.Location = new System.Drawing.Point(100, 30);
            this.cmbTablets.Name = "cmbTablets";
            this.cmbTablets.Size = new System.Drawing.Size(200, 21);
            this.cmbTablets.TabIndex = 1;
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(20, 163);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(63, 13);
            this.lblInstructions.TabIndex = 5;
            this.lblInstructions.Text = "Instructions:";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(20, 133);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(49, 13);
            this.lblQuantity.TabIndex = 4;
            this.lblQuantity.Text = "Quantity:";
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(20, 103);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(50, 13);
            this.lblDuration.TabIndex = 3;
            this.lblDuration.Text = "Duration:";
            // 
            // lblDosage
            // 
            this.lblDosage.AutoSize = true;
            this.lblDosage.Location = new System.Drawing.Point(20, 73);
            this.lblDosage.Name = "lblDosage";
            this.lblDosage.Size = new System.Drawing.Size(47, 13);
            this.lblDosage.TabIndex = 2;
            this.lblDosage.Text = "Dosage:";
            // 
            // lblTablet
            // 
            this.lblTablet.AutoSize = true;
            this.lblTablet.Location = new System.Drawing.Point(20, 33);
            this.lblTablet.Name = "lblTablet";
            this.lblTablet.Size = new System.Drawing.Size(39, 13);
            this.lblTablet.TabIndex = 1;
            this.lblTablet.Text = "Tablet:";
            // 
            // btnAddToPrescription
            // 
            this.btnAddToPrescription.Location = new System.Drawing.Point(100, 210);
            this.btnAddToPrescription.Name = "btnAddToPrescription";
            this.btnAddToPrescription.Size = new System.Drawing.Size(150, 30);
            this.btnAddToPrescription.TabIndex = 6;
            this.btnAddToPrescription.Text = "Add to Prescription List";
            this.btnAddToPrescription.UseVisualStyleBackColor = true;
            this.btnAddToPrescription.Click += new System.EventHandler(this.btnAddToPrescription_Click);
            // 
            // dgvMedicalRecords
            // 
            this.dgvMedicalRecords.AllowUserToAddRows = false;
            this.dgvMedicalRecords.AllowUserToDeleteRows = false;
            this.dgvMedicalRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMedicalRecords.Location = new System.Drawing.Point(430, 300);
            this.dgvMedicalRecords.Name = "dgvMedicalRecords";
            this.dgvMedicalRecords.ReadOnly = true;
            this.dgvMedicalRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMedicalRecords.Size = new System.Drawing.Size(550, 340);
            this.dgvMedicalRecords.TabIndex = 5;
            this.dgvMedicalRecords.SelectionChanged += new System.EventHandler(this.dgvMedicalRecords_SelectionChanged);
            // 
            // lblMedicalHistory
            // 
            this.lblMedicalHistory.AutoSize = true;
            this.lblMedicalHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedicalHistory.Location = new System.Drawing.Point(427, 280);
            this.lblMedicalHistory.Name = "lblMedicalHistory";
            this.lblMedicalHistory.Size = new System.Drawing.Size(117, 17);
            this.lblMedicalHistory.TabIndex = 6;
            this.lblMedicalHistory.Text = "Medical History";
            // 
            // MedicalRecordsForm
            // 
            this.ClientSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.lblMedicalHistory);
            this.Controls.Add(this.dgvMedicalRecords);
            this.Controls.Add(this.grpPrescriptionDetails);
            this.Controls.Add(this.grpPrescriptions);
            this.Controls.Add(this.grpMedicalRecord);
            this.Controls.Add(this.grpPatientSelection);
            this.Controls.Add(this.lblTitle);
            this.Name = "MedicalRecordsForm";
            this.Text = "Medical Records Management";
            this.Load += new System.EventHandler(this.MedicalRecordsForm_Load);
            this.grpPatientSelection.ResumeLayout(false);
            this.grpPatientSelection.PerformLayout();
            this.grpMedicalRecord.ResumeLayout(false);
            this.grpMedicalRecord.PerformLayout();
            this.grpPrescriptions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrescriptions)).EndInit();
            this.grpPrescriptionDetails.ResumeLayout(false);
            this.grpPrescriptionDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicalRecords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}