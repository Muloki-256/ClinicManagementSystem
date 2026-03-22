using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class StatusUpdateForm : Form
    {
        public string SelectedStatus { get; private set; }
        public string Notes { get; private set; }

        public StatusUpdateForm(string currentStatus)
        {
            InitializeComponent();
            cmbStatus.SelectedItem = currentStatus;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a status.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SelectedStatus = cmbStatus.SelectedItem.ToString();
            Notes = txtNotes.Text;
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