using System;
using System.Windows.Forms;

namespace ClinicManagementSystem
{

    partial class PatientPortalForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblWelcome;
        private Label lblContactInfo;
        private DataGridView dgvAvailableTablets;
        private Button btnAddToCart;
        private ListBox lstCart;
        private Button btnRemoveFromCart;
        private Button btnClearCart;
        private Button btnPlaceOrder;
        private Label lblCartTotal;
        private Button btnRefreshTablets;
        private Button btnLogout;
        private Label label1;
        private Label label2;

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
            this.lblWelcome = new Label();
            this.lblContactInfo = new Label();
            this.dgvAvailableTablets = new DataGridView();
            this.btnAddToCart = new Button();
            this.lstCart = new ListBox();
            this.btnRemoveFromCart = new Button();
            this.btnClearCart = new Button();
            this.btnPlaceOrder = new Button();
            this.lblCartTotal = new Label();
            this.btnRefreshTablets = new Button();
            this.btnLogout = new Button();
            this.label1 = new Label();
            this.label2 = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableTablets)).BeginInit();
            this.SuspendLayout();

            // lblWelcome
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Location = new System.Drawing.Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(120, 29);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome";

            // lblContactInfo
            this.lblContactInfo.AutoSize = true;
            this.lblContactInfo.Location = new System.Drawing.Point(22, 55);
            this.lblContactInfo.Name = "lblContactInfo";
            this.lblContactInfo.Size = new System.Drawing.Size(130, 16);
            this.lblContactInfo.TabIndex = 1;
            this.lblContactInfo.Text = "Contact information";

            // label1
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Available Tablets";

            // dgvAvailableTablets
            this.dgvAvailableTablets.AllowUserToAddRows = false;
            this.dgvAvailableTablets.AllowUserToDeleteRows = false;
            this.dgvAvailableTablets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvailableTablets.Location = new System.Drawing.Point(25, 120);
            this.dgvAvailableTablets.Name = "dgvAvailableTablets";
            this.dgvAvailableTablets.ReadOnly = true;
            this.dgvAvailableTablets.RowHeadersWidth = 51;
            this.dgvAvailableTablets.RowTemplate.Height = 24;
            this.dgvAvailableTablets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvAvailableTablets.Size = new System.Drawing.Size(550, 250);
            this.dgvAvailableTablets.TabIndex = 3;

            // btnAddToCart
            this.btnAddToCart.BackColor = System.Drawing.Color.Green;
            this.btnAddToCart.ForeColor = System.Drawing.Color.White;
            this.btnAddToCart.Location = new System.Drawing.Point(25, 385);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(120, 35);
            this.btnAddToCart.TabIndex = 4;
            this.btnAddToCart.Text = "Add to Cart";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            this.btnAddToCart.Click += new EventHandler(this.btnAddToCart_Click);

            // btnRefreshTablets
            this.btnRefreshTablets.Location = new System.Drawing.Point(155, 385);
            this.btnRefreshTablets.Name = "btnRefreshTablets";
            this.btnRefreshTablets.Size = new System.Drawing.Size(120, 35);
            this.btnRefreshTablets.TabIndex = 5;
            this.btnRefreshTablets.Text = "Refresh List";
            this.btnRefreshTablets.UseVisualStyleBackColor = true;
            this.btnRefreshTablets.Click += new EventHandler(this.btnRefreshTablets_Click);

            // label2
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(600, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Shopping Cart";

            // lstCart
            this.lstCart.FormattingEnabled = true;
            this.lstCart.ItemHeight = 16;
            this.lstCart.Location = new System.Drawing.Point(605, 120);
            this.lstCart.Name = "lstCart";
            this.lstCart.Size = new System.Drawing.Size(350, 180);
            this.lstCart.TabIndex = 7;

            // lblCartTotal
            this.lblCartTotal.AutoSize = true;
            this.lblCartTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblCartTotal.Location = new System.Drawing.Point(605, 310);
            this.lblCartTotal.Name = "lblCartTotal";
            this.lblCartTotal.Size = new System.Drawing.Size(65, 20);
            this.lblCartTotal.TabIndex = 8;
            this.lblCartTotal.Text = "Total: $0";

            // btnRemoveFromCart
            this.btnRemoveFromCart.Location = new System.Drawing.Point(605, 340);
            this.btnRemoveFromCart.Name = "btnRemoveFromCart";
            this.btnRemoveFromCart.Size = new System.Drawing.Size(120, 35);
            this.btnRemoveFromCart.TabIndex = 9;
            this.btnRemoveFromCart.Text = "Remove Item";
            this.btnRemoveFromCart.UseVisualStyleBackColor = true;
            this.btnRemoveFromCart.Click += new EventHandler(this.btnRemoveFromCart_Click);

            // btnClearCart
            this.btnClearCart.Location = new System.Drawing.Point(735, 340);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(100, 35);
            this.btnClearCart.TabIndex = 10;
            this.btnClearCart.Text = "Clear Cart";
            this.btnClearCart.UseVisualStyleBackColor = true;
            this.btnClearCart.Click += new EventHandler(this.btnClearCart_Click);

            // btnPlaceOrder
            this.btnPlaceOrder.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnPlaceOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnPlaceOrder.ForeColor = System.Drawing.Color.White;
            this.btnPlaceOrder.Location = new System.Drawing.Point(605, 385);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(350, 45);
            this.btnPlaceOrder.TabIndex = 11;
            this.btnPlaceOrder.Text = "PLACE ORDER";
            this.btnPlaceOrder.UseVisualStyleBackColor = false;
            this.btnPlaceOrder.Click += new EventHandler(this.btnPlaceOrder_Click);

            // btnLogout
            this.btnLogout.Location = new System.Drawing.Point(855, 20);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(100, 35);
            this.btnLogout.TabIndex = 12;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new EventHandler(this.btnLogout_Click);

            // PatientPortalForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 453);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnPlaceOrder);
            this.Controls.Add(this.btnClearCart);
            this.Controls.Add(this.btnRemoveFromCart);
            this.Controls.Add(this.lblCartTotal);
            this.Controls.Add(this.lstCart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRefreshTablets);
            this.Controls.Add(this.btnAddToCart);
            this.Controls.Add(this.dgvAvailableTablets);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblContactInfo);
            this.Controls.Add(this.lblWelcome);
            this.Name = "PatientPortalForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Patient Order Portal";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvailableTablets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }

}