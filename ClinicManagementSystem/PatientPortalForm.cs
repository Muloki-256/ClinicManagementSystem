using ClinicManagementSystem.Managers;
using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class PatientPortalForm : Form
    {
        private PatientInfo _patientInfo;
        private OrderManager _orderManager;
        private List<OrderItem> _cartItems;

        public PatientPortalForm(PatientInfo patientInfo)
        {
            InitializeComponent();
            _patientInfo = patientInfo;
            _orderManager = new OrderManager();
            _cartItems = new List<OrderItem>();

            LoadPatientInfo();
            LoadAvailableTablets();
            UpdateCartDisplay();
        }

        private void LoadPatientInfo()
        {
            lblWelcome.Text = $"Welcome, {_patientInfo.FullName}";
            if (!string.IsNullOrEmpty(_patientInfo.Phone) || !string.IsNullOrEmpty(_patientInfo.Email))
            {
                lblContactInfo.Text = _patientInfo.DisplayInfo;
            }
            else
            {
                lblContactInfo.Text = "Contact information not provided";
            }
        }

        private void LoadAvailableTablets()
        {
            try
            {
                var tablets = _orderManager.GetAvailableTablets();
                dgvAvailableTablets.DataSource = tablets;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tablets: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCartDisplay()
        {
            lstCart.Items.Clear();
            decimal total = 0;

            foreach (var item in _cartItems)
            {
                string itemText = $"{item.Quantity} x {item.TabletName} - ${item.UnitPrice:0.00} each = ${item.TotalPrice:0.00}";
                lstCart.Items.Add(itemText);
                total += item.TotalPrice;
            }

            lblCartTotal.Text = $"Total: ${total:0.00}";
            btnPlaceOrder.Enabled = _cartItems.Count > 0;
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (dgvAvailableTablets.CurrentRow != null && dgvAvailableTablets.CurrentRow.DataBoundItem is DataRowView row)
            {
                try
                {
                    int tabletId = Convert.ToInt32(row["TabletId"]);
                    string tabletName = row["TabletName"].ToString();
                    decimal unitPrice = Convert.ToDecimal(row["CostPerUnit"]);
                    int stockQuantity = Convert.ToInt32(row["StockQuantity"]);

                    // Use custom input dialog instead of Microsoft.VisualBasic.Interaction
                    string input = ShowSimpleInputDialog($"Enter quantity for {tabletName} (Max: {stockQuantity}):", "1");

                    if (int.TryParse(input, out int quantity) && quantity > 0 && quantity <= stockQuantity)
                    {
                        _cartItems.Add(new OrderItem
                        {
                            TabletId = tabletId,
                            Quantity = quantity,
                            UnitPrice = unitPrice,
                            Tablet = new Tablet { TabletName = tabletName }
                        });

                        UpdateCartDisplay();
                        MessageBox.Show($"Added {quantity} {tabletName} to cart", "Cart Updated",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (!string.IsNullOrEmpty(input))
                    {
                        MessageBox.Show("Invalid quantity entered", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding item to cart: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a tablet first", "Selection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Custom input dialog to replace Microsoft.VisualBasic.Interaction
        private string ShowSimpleInputDialog(string text, string defaultValue)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Enter Quantity",
                StartPosition = FormStartPosition.CenterParent
            };

            Label textLabel = new Label() { Left = 20, Top = 20, Text = text, Width = 260 };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 240, Text = defaultValue };
            Button confirmation = new Button() { Text = "OK", Left = 180, Width = 80, Top = 80, DialogResult = DialogResult.OK };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (lstCart.SelectedIndex >= 0)
            {
                _cartItems.RemoveAt(lstCart.SelectedIndex);
                UpdateCartDisplay();
            }
            else
            {
                MessageBox.Show("Please select an item to remove", "Selection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClearCart_Click(object sender, EventArgs e)
        {
            if (_cartItems.Count > 0)
            {
                var result = MessageBox.Show("Clear all items from cart?", "Confirm Clear",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    _cartItems.Clear();
                    UpdateCartDisplay();
                }
            }
        }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cartItems.Count == 0)
                {
                    MessageBox.Show("Your cart is empty", "Cart Empty",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Calculate total
                decimal totalAmount = 0;
                foreach (var item in _cartItems)
                {
                    totalAmount += item.Quantity * item.UnitPrice;
                }

                // Create order for guest patient (PatientId = 0)
                var order = new Order
                {
                    PatientId = 0, // 0 indicates guest patient
                    OrderDate = DateTime.Now,
                    Status = "Pending",
                    TotalAmount = totalAmount,
                    Notes = $"Guest Order - Customer: {_patientInfo.DisplayInfo}",
                    CreatedBy = 1, // System user
                    OrderItems = new List<OrderItem>(_cartItems)
                };

                var result = _orderManager.CreateOrder(order);
                if (result.Success)
                {
                    MessageBox.Show($"Order placed successfully!\n\nOrder ID: {result.Data}\nTotal: ${totalAmount:0.00}\n\nThank you for your order!",
                        "Order Confirmed", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear cart but keep form open for new orders
                    _cartItems.Clear();
                    UpdateCartDisplay();

                    // Ask if user wants to place another order or exit
                    var dialogResult = MessageBox.Show("Would you like to place another order?", "New Order",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dialogResult == DialogResult.No)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show($"Order failed: {result.Message}", "Order Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error placing order: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshTablets_Click(object sender, EventArgs e)
        {
            LoadAvailableTablets();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PatientPortalForm_Load(object sender, EventArgs e)
        {
            // Ensure form is visible and focused
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            this.Focus();
        }
    }
}