using ClinicManagementSystem.Managers;
using ClinicManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class OrderManagementForm : Form
    {
        private OrderManager orderManager;
        private PatientManager patientManager;
        private List<OrderItem> currentOrderItems;
        private List<Patient> patients;
        private DataTable availableTablets;

        public OrderManagementForm()
        {
            InitializeComponent();
            orderManager = new OrderManager();
            patientManager = new PatientManager();
            currentOrderItems = new List<OrderItem>();
            LoadComboBoxData();
            LoadAvailableTablets();
            RefreshOrdersGrid();
        }

        private void LoadComboBoxData()
        {
            patients = patientManager.GetAllPatients();
            cmbPatients.DataSource = patients;
            cmbPatients.DisplayMember = "FullName";
            cmbPatients.ValueMember = "PatientId";
        }

        private void LoadAvailableTablets()
        {
            availableTablets = orderManager.GetAvailableTablets();
            cmbTablets.DataSource = availableTablets;
            cmbTablets.DisplayMember = "TabletName";
            cmbTablets.ValueMember = "TabletId";

            // Configure data grid view for available tablets
            dgvAvailableTablets.DataSource = availableTablets;
            SafeHideColumn(dgvAvailableTablets, "TabletId");
            SafeSetHeaderText(dgvAvailableTablets, "TabletName", "Tablet Name");
            SafeSetHeaderText(dgvAvailableTablets, "Description", "Description");
            SafeSetHeaderText(dgvAvailableTablets, "CostPerUnit", "Price");
            SafeSetHeaderText(dgvAvailableTablets, "StockQuantity", "In Stock");
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (!ValidateOrderItem())
                return;

            try
            {
                var selectedTablet = (DataRowView)cmbTablets.SelectedItem;
                var orderItem = new OrderItem
                {
                    TabletId = Convert.ToInt32(selectedTablet["TabletId"]),
                    Tablet = new Tablet
                    {
                        TabletName = selectedTablet["TabletName"].ToString(),
                        CostPerUnit = Convert.ToDecimal(selectedTablet["CostPerUnit"])
                    },
                    Quantity = (int)nudQuantity.Value,
                    UnitPrice = Convert.ToDecimal(selectedTablet["CostPerUnit"])
                };

                currentOrderItems.Add(orderItem);
                RefreshOrderItemsGrid();
                CalculateOrderTotal();
                ClearItemForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding item: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvOrderItems.SelectedRows.Count > 0)
            {
                var item = (OrderItem)dgvOrderItems.SelectedRows[0].DataBoundItem;
                currentOrderItems.Remove(item);
                RefreshOrderItemsGrid();
                CalculateOrderTotal();
            }
        }

        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            if (!ValidateOrder())
                return;

            try
            {
                var order = new Order
                {
                    PatientId = (int)cmbPatients.SelectedValue,
                    OrderDate = DateTime.Now,
                    Status = "Pending",
                    TotalAmount = orderManager.CalculateOrderTotal(currentOrderItems),
                    Notes = txtOrderNotes.Text,
                    CreatedBy = LoginForm.CurrentUser?.UserId ?? 1,
                    OrderItems = currentOrderItems
                };

                var result = orderManager.CreateOrder(order);
                if (result.Success)
                {
                    MessageBox.Show(result.Message, "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearOrderForm();
                    RefreshOrdersGrid();
                }
                else
                {
                    MessageBox.Show(result.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating order: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoadPatientOrders_Click(object sender, EventArgs e)
        {
            if (cmbPatients.SelectedValue == null)
            {
                MessageBox.Show("Please select a patient.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int patientId = (int)cmbPatients.SelectedValue;
            LoadPatientOrders(patientId);
        }

        private void LoadPatientOrders(int patientId)
        {
            var orders = orderManager.GetOrdersByPatient(patientId);
            dgvOrders.DataSource = orders;

            // Configure columns
            SafeHideColumn(dgvOrders, "OrderId");
            SafeHideColumn(dgvOrders, "PatientId");
            SafeHideColumn(dgvOrders, "CreatedBy");
            SafeHideColumn(dgvOrders, "Patient");
            SafeHideColumn(dgvOrders, "OrderItems");
            SafeSetHeaderText(dgvOrders, "OrderDate", "Order Date");
            SafeSetHeaderText(dgvOrders, "TotalAmount", "Total Amount");
            SafeSetHeaderText(dgvOrders, "Status", "Status");
            SafeSetHeaderText(dgvOrders, "TotalItems", "Items Count");
        }

        private void RefreshOrdersGrid()
        {
            var orders = orderManager.GetAllOrders();
            dgvOrders.DataSource = orders;

            // Configure columns
            SafeHideColumn(dgvOrders, "OrderId");
            SafeHideColumn(dgvOrders, "PatientId");
            SafeHideColumn(dgvOrders, "CreatedBy");
            SafeHideColumn(dgvOrders, "Patient");
            SafeHideColumn(dgvOrders, "OrderItems");
            SafeSetHeaderText(dgvOrders, "PatientName", "Patient Name");
            SafeSetHeaderText(dgvOrders, "OrderDate", "Order Date");
            SafeSetHeaderText(dgvOrders, "TotalAmount", "Total Amount");
            SafeSetHeaderText(dgvOrders, "Status", "Status");
            SafeSetHeaderText(dgvOrders, "TotalItems", "Items Count");
        }

        private void RefreshOrderItemsGrid()
        {
            dgvOrderItems.DataSource = null;
            dgvOrderItems.DataSource = currentOrderItems;

            // Configure columns
            SafeHideColumn(dgvOrderItems, "OrderItemId");
            SafeHideColumn(dgvOrderItems, "OrderId");
            SafeHideColumn(dgvOrderItems, "TabletId");
            SafeHideColumn(dgvOrderItems, "Order");
            SafeHideColumn(dgvOrderItems, "Tablet");
            SafeSetHeaderText(dgvOrderItems, "TabletName", "Tablet Name");
            SafeSetHeaderText(dgvOrderItems, "Quantity", "Quantity");
            SafeSetHeaderText(dgvOrderItems, "UnitPrice", "Unit Price");
            SafeSetHeaderText(dgvOrderItems, "TotalPrice", "Total Price");
        }

        private void CalculateOrderTotal()
        {
            decimal total = currentOrderItems.Sum(item => item.TotalPrice);
            lblOrderTotal.Text = $"Order Total: {total:C}";
        }

        private bool ValidateOrder()
        {
            if (cmbPatients.SelectedValue == null)
            {
                MessageBox.Show("Please select a patient.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (currentOrderItems.Count == 0)
            {
                MessageBox.Show("Please add at least one item to the order.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool ValidateOrderItem()
        {
            if (cmbTablets.SelectedValue == null)
            {
                MessageBox.Show("Please select a tablet.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nudQuantity.Value <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Check stock availability
            var selectedTablet = (DataRowView)cmbTablets.SelectedItem;
            int stockQuantity = Convert.ToInt32(selectedTablet["StockQuantity"]);
            if (nudQuantity.Value > stockQuantity)
            {
                MessageBox.Show($"Insufficient stock. Only {stockQuantity} available.", "Stock Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void ClearOrderForm()
        {
            currentOrderItems.Clear();
            RefreshOrderItemsGrid();
            CalculateOrderTotal();
            txtOrderNotes.Clear();
            lblOrderTotal.Text = "Order Total: $0.00";
        }

        private void ClearItemForm()
        {
            nudQuantity.Value = 1;
            if (cmbTablets.Items.Count > 0)
                cmbTablets.SelectedIndex = 0;
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count > 0)
            {
                var order = (Order)dgvOrders.SelectedRows[0].DataBoundItem;
                DisplayOrderDetails(order);
            }
        }

        private void DisplayOrderDetails(Order order)
        {
            txtOrderDetails.Text = $"Order #: {order.OrderId}\r\n";
            txtOrderDetails.Text += $"Patient: {order.PatientName}\r\n";
            txtOrderDetails.Text += $"Date: {order.OrderDate:MM/dd/yyyy HH:mm}\r\n";
            txtOrderDetails.Text += $"Status: {order.Status}\r\n";
            txtOrderDetails.Text += $"Total: {order.TotalAmount:C}\r\n";
            txtOrderDetails.Text += $"Items:\r\n";

            foreach (var item in order.OrderItems)
            {
                txtOrderDetails.Text += $"  • {item.TabletName} - {item.Quantity} x {item.UnitPrice:C} = {item.TotalPrice:C}\r\n";
            }

            if (!string.IsNullOrEmpty(order.Notes))
            {
                txtOrderDetails.Text += $"\r\nNotes: {order.Notes}";
            }
        }

        private void btnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvOrders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order to update status.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var order = (Order)dgvOrders.SelectedRows[0].DataBoundItem;

            using (var statusForm = new StatusUpdateForm(order.Status))
            {
                if (statusForm.ShowDialog() == DialogResult.OK)
                {
                    var result = orderManager.UpdateOrderStatus(
                        order.OrderId,
                        statusForm.SelectedStatus,
                        LoginForm.CurrentUser?.UserId ?? 1,
                        statusForm.Notes
                    );

                    if (result.Success)
                    {
                        MessageBox.Show(result.Message, "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshOrdersGrid();
                    }
                    else
                    {
                        MessageBox.Show(result.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshOrdersGrid();
            LoadAvailableTablets();
        }

        private void SafeHideColumn(DataGridView dgv, string columnName)
        {
            try
            {
                if (dgv.Columns.Contains(columnName))
                {
                    dgv.Columns[columnName].Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Could not hide column '{columnName}': {ex.Message}");
            }
        }

        private void SafeSetHeaderText(DataGridView dgv, string columnName, string headerText)
        {
            try
            {
                if (dgv.Columns.Contains(columnName))
                {
                    dgv.Columns[columnName].HeaderText = headerText;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Could not set header for '{columnName}': {ex.Message}");
            }
        }
    }
}