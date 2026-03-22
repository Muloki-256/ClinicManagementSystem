using System;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    partial class QuantityInputForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTabletName;
        private NumericUpDown numQuantity;
        private Button btnOK;
        private Button btnCancel;
        private Label lblMaxQuantity;

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
            this.lblTabletName = new Label();
            this.numQuantity = new NumericUpDown();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.lblMaxQuantity = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();

            // lblTabletName
            this.lblTabletName.AutoSize = true;
            this.lblTabletName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTabletName.Location = new System.Drawing.Point(20, 20);
            this.lblTabletName.Name = "lblTabletName";
            this.lblTabletName.Size = new System.Drawing.Size(110, 20);
            this.lblTabletName.TabIndex = 0;
            this.lblTabletName.Text = "Tablet Name";

            // numQuantity
            this.numQuantity.Location = new System.Drawing.Point(24, 70);
            this.numQuantity.Minimum = 1;
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(120, 22);
            this.numQuantity.TabIndex = 1;
            this.numQuantity.Value = 1;

            // lblMaxQuantity
            this.lblMaxQuantity.AutoSize = true;
            this.lblMaxQuantity.Location = new System.Drawing.Point(150, 73);
            this.lblMaxQuantity.Name = "lblMaxQuantity";
            this.lblMaxQuantity.Size = new System.Drawing.Size(80, 16);
            this.lblMaxQuantity.TabIndex = 2;
            this.lblMaxQuantity.Text = "Max: 100";

            // btnOK
            this.btnOK.BackColor = System.Drawing.Color.Green;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(24, 110);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 30);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(130, 110);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // QuantityInputForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 160);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblMaxQuantity);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.lblTabletName);
            this.Name = "QuantityInputForm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Select Quantity";
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}