namespace ClinicManagementSystem
{
    partial class OrderManagementForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpNewOrder;
        private System.Windows.Forms.ComboBox cmbPatients;
        private System.Windows.Forms.Label lblPatient;
        private System.Windows.Forms.GroupBox grpOrderItems;
        private System.Windows.Forms.ComboBox cmbTablets;
        private System.Windows.Forms.Label lblTablet;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.DataGridView dgvOrderItems;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.TextBox txtOrderNotes;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Label lblOrderTotal;
        private System.Windows.Forms.GroupBox grpAvailableTablets;
        private System.Windows.Forms.DataGridView dgvAvailableTablets;
        private System.Windows.Forms.GroupBox grpOrderHistory;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Button btnLoadPatientOrders;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.TextBox txtOrderDetails;
        private System.Windows.Forms.Label lblOrderDetails;

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
            this.grpNewOrder = new System.Windows.Forms.GroupBox();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.lblOrderTotal = new System.Windows.Forms.Label();
            this.txtOrderNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.grpOrderItems = new System.Windows.Forms.GroupBox();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.dgvOrderItems = new System.Windows.Forms.DataGridView();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.cmbTablets = new System.Windows.Forms.ComboBox();
            this.lblTablet = new System.Windows.Forms.Label();
            this.cmbPatients = new System.Windows.Forms.ComboBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.grpAvailableTablets = new System.Windows.Forms.GroupBox();
            this.dgvAvailableTablets = new System.Windows.Forms.DataGridView();
            this.grpOrderHistory = new System.Windows.Forms.GroupBox();
            this.txtOrderDetails = new System.Windows.Forms.TextBox();
            this.lblOrderDetails = new System.Windows.Forms.Label();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnLoadPatientOrders = new System.Windows.Forms.Button();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.grpNewOrder.SuspendLayout();
            this.grpOrderItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.grpAvailableTablets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableTablets)).BeginInit();
            this.grpOrderHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(162, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Order Management";
            // 
            // grpNewOrder
            // 
            this.grpNewOrder.Controls.Add(this.btnCreateOrder);
            this.grpNewOrder.Controls.Add(this.lblOrderTotal);
            this.grpNewOrder.Controls.Add(this.txtOrderNotes);
            this.grpNewOrder.Controls.Add(this.lblNotes);
            this.grpNewOrder.Controls.Add(this.grpOrderItems);
            this.grpNewOrder.Controls.Add(this.cmbPatients);
            this.grpNewOrder.Controls.Add(this.lblPatient);
            this.grpNewOrder.Location = new System.Drawing.Point(12, 40);
            this.grpNewOrder.Name = "grpNewOrder";
            this.grpNewOrder.Size = new System.Drawing.Size(500, 400);
            this.grpNewOrder.TabIndex = 1;
            this.grpNewOrder.TabStop = false;
            this.grpNewOrder.Text = "New Order";
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.Location = new System.Drawing.Point(350, 360);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(120, 30);
            this.btnCreateOrder.TabIndex = 6;
            this.btnCreateOrder.Text = "Create Order";
            this.btnCreateOrder.UseVisualStyleBackColor = true;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // lblOrderTotal
            // 
            this.lblOrderTotal.AutoSize = true;
            this.lblOrderTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderTotal.Location = new System.Drawing.Point(20, 365);
            this.lblOrderTotal.Name = "lblOrderTotal";
            this.lblOrderTotal.Size = new System.Drawing.Size(116, 17);
            this.lblOrderTotal.TabIndex = 5;
            this.lblOrderTotal.Text = "Order Total: $0";
            // 
            // txtOrderNotes
            // 
            this.txtOrderNotes.Location = new System.Drawing.Point(20, 320);
            this.txtOrderNotes.Multiline = true;
            this.txtOrderNotes.Name = "txtOrderNotes";
            this.txtOrderNotes.Size = new System.Drawing.Size(450, 35);
            this.txtOrderNotes.TabIndex = 4;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Location = new System.Drawing.Point(20, 300);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(38, 13);
            this.lblNotes.TabIndex = 3;
            this.lblNotes.Text = "Notes:";
            // 
            // grpOrderItems
            // 
            this.grpOrderItems.Controls.Add(this.btnRemoveItem);
            this.grpOrderItems.Controls.Add(this.dgvOrderItems);
            this.grpOrderItems.Controls.Add(this.btnAddItem);
            this.grpOrderItems.Controls.Add(this.nudQuantity);
            this.grpOrderItems.Controls.Add(this.lblQuantity);
            this.grpOrderItems.Controls.Add(this.cmbTablets);
            this.grpOrderItems.Controls.Add(this.lblTablet);
            this.grpOrderItems.Location = new System.Drawing.Point(20, 50);
            this.grpOrderItems.Name = "grpOrderItems";
            this.grpOrderItems.Size = new System.Drawing.Size(450, 240);
            this.grpOrderItems.TabIndex = 2;
            this.grpOrderItems.TabStop = false;
            this.grpOrderItems.Text = "Order Items";
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Location = new System.Drawing.Point(350, 200);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(90, 30);
            this.btnRemoveItem.TabIndex = 6;
            this.btnRemoveItem.Text = "Remove Item";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // dgvOrderItems
            // 
            this.dgvOrderItems.AllowUserToAddRows = false;
            this.dgvOrderItems.AllowUserToDeleteRows = false;
            this.dgvOrderItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderItems.Location = new System.Drawing.Point(20, 80);
            this.dgvOrderItems.Name = "dgvOrderItems";
            this.dgvOrderItems.ReadOnly = true;
            this.dgvOrderItems.Size = new System.Drawing.Size(420, 110);
            this.dgvOrderItems.TabIndex = 5;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(350, 45);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(90, 25);
            this.btnAddItem.TabIndex = 4;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // nudQuantity
            // 
            this.nudQuantity.Location = new System.Drawing.Point(250, 20);
            this.nudQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(60, 20);
            this.nudQuantity.TabIndex = 3;
            this.nudQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(200, 23);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(49, 13);
            this.lblQuantity.TabIndex = 2;
            this.lblQuantity.Text = "Quantity:";
            // 
            // cmbTablets
            // 
            this.cmbTablets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTablets.FormattingEnabled = true;
            this.cmbTablets.Location = new System.Drawing.Point(60, 20);
            this.cmbTablets.Name = "cmbTablets";
            this.cmbTablets.Size = new System.Drawing.Size(130, 21);
            this.cmbTablets.TabIndex = 1;
            // 
            // lblTablet
            // 
            this.lblTablet.AutoSize = true;
            this.lblTablet.Location = new System.Drawing.Point(20, 23);
            this.lblTablet.Name = "lblTablet";
            this.lblTablet.Size = new System.Drawing.Size(39, 13);
            this.lblTablet.TabIndex = 0;
            this.lblTablet.Text = "Tablet:";
            // 
            // cmbPatients
            // 
            this.cmbPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatients.FormattingEnabled = true;
            this.cmbPatients.Location = new System.Drawing.Point(70, 20);
            this.cmbPatients.Name = "cmbPatients";
            this.cmbPatients.Size = new System.Drawing.Size(200, 21);
            this.cmbPatients.TabIndex = 1;
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Location = new System.Drawing.Point(20, 23);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(44, 13);
            this.lblPatient.TabIndex = 0;
            this.lblPatient.Text = "Patient:";
            // 
            // grpAvailableTablets
            // 
            this.grpAvailableTablets.Controls.Add(this.dgvAvailableTablets);
            this.grpAvailableTablets.Location = new System.Drawing.Point(12, 450);
            this.grpAvailableTablets.Name = "grpAvailableTablets";
            this.grpAvailableTablets.Size = new System.Drawing.Size(500, 200);
            this.grpAvailableTablets.TabIndex = 2;
            this.grpAvailableTablets.TabStop = false;
            this.grpAvailableTablets.Text = "Available Tablets";
            // 
            // dgvAvailableTablets
            // 
            this.dgvAvailableTablets.AllowUserToAddRows = false;
            this.dgvAvailableTablets.AllowUserToDeleteRows = false;
            this.dgvAvailableTablets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAvailableTablets.Location = new System.Drawing.Point(20, 20);
            this.dgvAvailableTablets.Name = "dgvAvailableTablets";
            this.dgvAvailableTablets.ReadOnly = true;
            this.dgvAvailableTablets.Size = new System.Drawing.Size(460, 170);
            this.dgvAvailableTablets.TabIndex = 0;
            // 
            // grpOrderHistory
            // 
            this.grpOrderHistory.Controls.Add(this.txtOrderDetails);
            this.grpOrderHistory.Controls.Add(this.lblOrderDetails);
            this.grpOrderHistory.Controls.Add(this.btnUpdateStatus);
            this.grpOrderHistory.Controls.Add(this.btnRefresh);
            this.grpOrderHistory.Controls.Add(this.btnLoadPatientOrders);
            this.grpOrderHistory.Controls.Add(this.dgvOrders);
            this.grpOrderHistory.Location = new System.Drawing.Point(530, 40);
            this.grpOrderHistory.Name = "grpOrderHistory";
            this.grpOrderHistory.Size = new System.Drawing.Size(550, 610);
            this.grpOrderHistory.TabIndex = 3;
            this.grpOrderHistory.TabStop = false;
            this.grpOrderHistory.Text = "Order History";
            // 
            // txtOrderDetails
            // 
            this.txtOrderDetails.Location = new System.Drawing.Point(20, 420);
            this.txtOrderDetails.Multiline = true;
            this.txtOrderDetails.Name = "txtOrderDetails";
            this.txtOrderDetails.ReadOnly = true;
            this.txtOrderDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOrderDetails.Size = new System.Drawing.Size(510, 150);
            this.txtOrderDetails.TabIndex = 5;
            // 
            // lblOrderDetails
            // 
            this.lblOrderDetails.AutoSize = true;
            this.lblOrderDetails.Location = new System.Drawing.Point(20, 400);
            this.lblOrderDetails.Name = "lblOrderDetails";
            this.lblOrderDetails.Size = new System.Drawing.Size(72, 13);
            this.lblOrderDetails.TabIndex = 4;
            this.lblOrderDetails.Text = "Order Details:";
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.Location = new System.Drawing.Point(440, 370);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(90, 30);
            this.btnUpdateStatus.TabIndex = 3;
            this.btnUpdateStatus.Text = "Update Status";
            this.btnUpdateStatus.UseVisualStyleBackColor = true;
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(340, 370);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 30);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh All";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnLoadPatientOrders
            // 
            this.btnLoadPatientOrders.Location = new System.Drawing.Point(20, 370);
            this.btnLoadPatientOrders.Name = "btnLoadPatientOrders";
            this.btnLoadPatientOrders.Size = new System.Drawing.Size(120, 30);
            this.btnLoadPatientOrders.TabIndex = 1;
            this.btnLoadPatientOrders.Text = "Load Patient Orders";
            this.btnLoadPatientOrders.UseVisualStyleBackColor = true;
            this.btnLoadPatientOrders.Click += new System.EventHandler(this.btnLoadPatientOrders_Click);
            // 
            // dgvOrders
            // 
            this.dgvOrders.AllowUserToAddRows = false;
            this.dgvOrders.AllowUserToDeleteRows = false;
            this.dgvOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrders.Location = new System.Drawing.Point(20, 20);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.ReadOnly = true;
            this.dgvOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrders.Size = new System.Drawing.Size(510, 340);
            this.dgvOrders.TabIndex = 0;
            this.dgvOrders.SelectionChanged += new System.EventHandler(this.dgvOrders_SelectionChanged);
            // 
            // OrderManagementForm
            // 
            this.ClientSize = new System.Drawing.Size(1100, 670);
            this.Controls.Add(this.grpOrderHistory);
            this.Controls.Add(this.grpAvailableTablets);
            this.Controls.Add(this.grpNewOrder);
            this.Controls.Add(this.lblTitle);
            this.Name = "OrderManagementForm";
            this.Text = "Order Management";
            this.grpNewOrder.ResumeLayout(false);
            this.grpNewOrder.PerformLayout();
            this.grpOrderItems.ResumeLayout(false);
            this.grpOrderItems.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.grpAvailableTablets.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableTablets)).EndInit();
            this.grpOrderHistory.ResumeLayout(false);
            this.grpOrderHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}