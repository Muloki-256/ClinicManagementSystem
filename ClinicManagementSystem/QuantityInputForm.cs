using System;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class QuantityInputForm : Form
    {
        public int Quantity { get; private set; } = 1;

        public QuantityInputForm(string tabletName, int maxQuantity)
        {
            InitializeComponent();
            lblTabletName.Text = $"Tablet: {tabletName}";
            numQuantity.Maximum = maxQuantity;
            numQuantity.Value = 1;
            lblMaxQuantity.Text = $"Maximum: {maxQuantity}";
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Quantity = (int)numQuantity.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}