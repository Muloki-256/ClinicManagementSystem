namespace ClinicManagementSystem
{
    partial class PaymentManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPending = new System.Windows.Forms.TabPage();
            this.dgvPendingPayments = new System.Windows.Forms.DataGridView();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.dgvPaymentHistory = new System.Windows.Forms.DataGridView();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.dgvPaymentReport = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            this.dtpReportEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpReportStart = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabReceipts = new System.Windows.Forms.TabPage();
            this.btnSearchReceipt = new System.Windows.Forms.Button();
            this.txtReceiptNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panelStats = new System.Windows.Forms.Panel();
            this.lblTotalPending = new System.Windows.Forms.Label();
            this.lblDailyCollection = new System.Windows.Forms.Label();
            this.lblTotalVerified = new System.Windows.Forms.Label();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnViewReceipt = new System.Windows.Forms.Button();
            this.btnRejectPayment = new System.Windows.Forms.Button();
            this.btnVerifyPayment = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPending.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingPayments)).BeginInit();
            this.tabHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentHistory)).BeginInit();
            this.tabReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentReport)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabReceipts.SuspendLayout();
            this.panelStats.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPending);
            this.tabControl1.Controls.Add(this.tabHistory);
            this.tabControl1.Controls.Add(this.tabReports);
            this.tabControl1.Controls.Add(this.tabReceipts);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 120);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(984, 441);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPending
            // 
            this.tabPending.Controls.Add(this.dgvPendingPayments);
            this.tabPending.Location = new System.Drawing.Point(4, 22);
            this.tabPending.Name = "tabPending";
            this.tabPending.Padding = new System.Windows.Forms.Padding(3);
            this.tabPending.Size = new System.Drawing.Size(976, 415);
            this.tabPending.TabIndex = 0;
            this.tabPending.Text = "Pending Payments";
            this.tabPending.UseVisualStyleBackColor = true;
            // 
            // dgvPendingPayments
            // 
            this.dgvPendingPayments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPendingPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPendingPayments.Location = new System.Drawing.Point(3, 3);
            this.dgvPendingPayments.Name = "dgvPendingPayments";
            this.dgvPendingPayments.ReadOnly = true;
            this.dgvPendingPayments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPendingPayments.Size = new System.Drawing.Size(970, 409);
            this.dgvPendingPayments.TabIndex = 0;
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.dgvPaymentHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabHistory.Size = new System.Drawing.Size(976, 415);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "Payment History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // dgvPaymentHistory
            // 
            this.dgvPaymentHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPaymentHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPaymentHistory.Location = new System.Drawing.Point(3, 3);
            this.dgvPaymentHistory.Name = "dgvPaymentHistory";
            this.dgvPaymentHistory.ReadOnly = true;
            this.dgvPaymentHistory.Size = new System.Drawing.Size(970, 409);
            this.dgvPaymentHistory.TabIndex = 0;
            // 
            // tabReports
            // 
            this.tabReports.Controls.Add(this.dgvPaymentReport);
            this.tabReports.Controls.Add(this.groupBox1);
            this.tabReports.Location = new System.Drawing.Point(4, 22);
            this.tabReports.Name = "tabReports";
            this.tabReports.Size = new System.Drawing.Size(976, 415);
            this.tabReports.TabIndex = 2;
            this.tabReports.Text = "Reports";
            this.tabReports.UseVisualStyleBackColor = true;
            // 
            // dgvPaymentReport
            // 
            this.dgvPaymentReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPaymentReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPaymentReport.Location = new System.Drawing.Point(0, 80);
            this.dgvPaymentReport.Name = "dgvPaymentReport";
            this.dgvPaymentReport.ReadOnly = true;
            this.dgvPaymentReport.Size = new System.Drawing.Size(976, 335);
            this.dgvPaymentReport.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGenerateReport);
            this.groupBox1.Controls.Add(this.dtpReportEnd);
            this.groupBox1.Controls.Add(this.dtpReportStart);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(976, 80);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Filters";
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(420, 18);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(120, 23);
            this.btnGenerateReport.TabIndex = 4;
            this.btnGenerateReport.Text = "Generate Report";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // dtpReportEnd
            // 
            this.dtpReportEnd.Location = new System.Drawing.Point(280, 20);
            this.dtpReportEnd.Name = "dtpReportEnd";
            this.dtpReportEnd.Size = new System.Drawing.Size(120, 20);
            this.dtpReportEnd.TabIndex = 3;
            // 
            // dtpReportStart
            // 
            this.dtpReportStart.Location = new System.Drawing.Point(80, 20);
            this.dtpReportStart.Name = "dtpReportStart";
            this.dtpReportStart.Size = new System.Drawing.Size(120, 20);
            this.dtpReportStart.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "End Date:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Start Date:";
            // 
            // tabReceipts
            // 
            this.tabReceipts.Controls.Add(this.btnSearchReceipt);
            this.tabReceipts.Controls.Add(this.txtReceiptNumber);
            this.tabReceipts.Controls.Add(this.label3);
            this.tabReceipts.Location = new System.Drawing.Point(4, 22);
            this.tabReceipts.Name = "tabReceipts";
            this.tabReceipts.Size = new System.Drawing.Size(976, 415);
            this.tabReceipts.TabIndex = 3;
            this.tabReceipts.Text = "Receipts";
            this.tabReceipts.UseVisualStyleBackColor = true;
            // 
            // btnSearchReceipt
            // 
            this.btnSearchReceipt.Location = new System.Drawing.Point(330, 15);
            this.btnSearchReceipt.Name = "btnSearchReceipt";
            this.btnSearchReceipt.Size = new System.Drawing.Size(100, 23);
            this.btnSearchReceipt.TabIndex = 2;
            this.btnSearchReceipt.Text = "Search Receipt";
            this.btnSearchReceipt.UseVisualStyleBackColor = true;
            this.btnSearchReceipt.Click += new System.EventHandler(this.btnSearchReceipt_Click);
            // 
            // txtReceiptNumber
            // 
            this.txtReceiptNumber.Location = new System.Drawing.Point(120, 17);
            this.txtReceiptNumber.Name = "txtReceiptNumber";
            this.txtReceiptNumber.Size = new System.Drawing.Size(200, 20);
            this.txtReceiptNumber.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Receipt Number:";
            // 
            // panelStats
            // 
            this.panelStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStats.Controls.Add(this.lblTotalPending);
            this.panelStats.Controls.Add(this.lblDailyCollection);
            this.panelStats.Controls.Add(this.lblTotalVerified);
            this.panelStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelStats.Location = new System.Drawing.Point(0, 0);
            this.panelStats.Name = "panelStats";
            this.panelStats.Size = new System.Drawing.Size(984, 60);
            this.panelStats.TabIndex = 1;
            // 
            // lblTotalPending
            // 
            this.lblTotalPending.AutoSize = true;
            this.lblTotalPending.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalPending.ForeColor = System.Drawing.Color.Orange;
            this.lblTotalPending.Location = new System.Drawing.Point(10, 20);
            this.lblTotalPending.Name = "lblTotalPending";
            this.lblTotalPending.Size = new System.Drawing.Size(72, 16);
            this.lblTotalPending.TabIndex = 0;
            this.lblTotalPending.Text = "Pending: 0";
            // 
            // lblDailyCollection
            // 
            this.lblDailyCollection.AutoSize = true;
            this.lblDailyCollection.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblDailyCollection.ForeColor = System.Drawing.Color.Blue;
            this.lblDailyCollection.Location = new System.Drawing.Point(280, 20);
            this.lblDailyCollection.Name = "lblDailyCollection";
            this.lblDailyCollection.Size = new System.Drawing.Size(149, 16);
            this.lblDailyCollection.TabIndex = 2;
            this.lblDailyCollection.Text = "Today's Collection: $0.00";
            // 
            // lblTotalVerified
            // 
            this.lblTotalVerified.AutoSize = true;
            this.lblTotalVerified.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalVerified.ForeColor = System.Drawing.Color.Green;
            this.lblTotalVerified.Location = new System.Drawing.Point(150, 20);
            this.lblTotalVerified.Name = "lblTotalVerified";
            this.lblTotalVerified.Size = new System.Drawing.Size(99, 16);
            this.lblTotalVerified.TabIndex = 1;
            this.lblTotalVerified.Text = "Verified: 0";
            // 
            // panelActions
            // 
            this.panelActions.Controls.Add(this.btnRefresh);
            this.panelActions.Controls.Add(this.btnViewReceipt);
            this.panelActions.Controls.Add(this.btnRejectPayment);
            this.panelActions.Controls.Add(this.btnVerifyPayment);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelActions.Location = new System.Drawing.Point(0, 60);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(984, 60);
            this.panelActions.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(340, 15);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 5;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnViewReceipt
            // 
            this.btnViewReceipt.Location = new System.Drawing.Point(230, 15);
            this.btnViewReceipt.Name = "btnViewReceipt";
            this.btnViewReceipt.Size = new System.Drawing.Size(100, 30);
            this.btnViewReceipt.TabIndex = 4;
            this.btnViewReceipt.Text = "View Receipt";
            this.btnViewReceipt.UseVisualStyleBackColor = true;
            this.btnViewReceipt.Click += new System.EventHandler(this.btnViewReceipt_Click);
            // 
            // btnRejectPayment
            // 
            this.btnRejectPayment.Location = new System.Drawing.Point(120, 15);
            this.btnRejectPayment.Name = "btnRejectPayment";
            this.btnRejectPayment.Size = new System.Drawing.Size(100, 30);
            this.btnRejectPayment.TabIndex = 3;
            this.btnRejectPayment.Text = "Reject Payment";
            this.btnRejectPayment.UseVisualStyleBackColor = true;
            this.btnRejectPayment.Click += new System.EventHandler(this.btnRejectPayment_Click);
            // 
            // btnVerifyPayment
            // 
            this.btnVerifyPayment.Location = new System.Drawing.Point(10, 15);
            this.btnVerifyPayment.Name = "btnVerifyPayment";
            this.btnVerifyPayment.Size = new System.Drawing.Size(100, 30);
            this.btnVerifyPayment.TabIndex = 2;
            this.btnVerifyPayment.Text = "Verify Payment";
            this.btnVerifyPayment.UseVisualStyleBackColor = true;
            this.btnVerifyPayment.Click += new System.EventHandler(this.btnVerifyPayment_Click);
            // 
            // PaymentManagementForm
            // 
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelActions);
            this.Controls.Add(this.panelStats);
            this.Name = "PaymentManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payment Management";
            this.Load += new System.EventHandler(this.PaymentManagementForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPending.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPendingPayments)).EndInit();
            this.tabHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentHistory)).EndInit();
            this.tabReports.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentReport)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabReceipts.ResumeLayout(false);
            this.tabReceipts.PerformLayout();
            this.panelStats.ResumeLayout(false);
            this.panelStats.PerformLayout();
            this.panelActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPending;
        private System.Windows.Forms.DataGridView dgvPendingPayments;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.DataGridView dgvPaymentHistory;
        private System.Windows.Forms.TabPage tabReports;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnGenerateReport;
        private System.Windows.Forms.DateTimePicker dtpReportEnd;
        private System.Windows.Forms.DateTimePicker dtpReportStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvPaymentReport;
        private System.Windows.Forms.TabPage tabReceipts;
        private System.Windows.Forms.Button btnSearchReceipt;
        private System.Windows.Forms.TextBox txtReceiptNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelStats;
        private System.Windows.Forms.Label lblTotalPending;
        private System.Windows.Forms.Label lblDailyCollection;
        private System.Windows.Forms.Label lblTotalVerified;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnVerifyPayment;
        private System.Windows.Forms.Button btnRejectPayment;
        private System.Windows.Forms.Button btnViewReceipt;
        private System.Windows.Forms.Button btnRefresh;
    }
}