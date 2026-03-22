using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ClinicManagementSystem.Managers
{
    public class OrderManager
    {
        private BaseRepository repository;

        public OrderManager()
        {
            repository = new BaseRepository();
        }

        public OperationResult CreateOrder(Order order)
        {
            try
            {
                // Determine if this is a guest order
                bool isGuestOrder = order.PatientId == 0;
                int? patientId = isGuestOrder ? (int?)null : order.PatientId;

                // Create order
                string orderQuery = @"
                    INSERT INTO Orders (PatientId, OrderDate, Status, TotalAmount, Notes, CreatedBy, IsGuestOrder, GuestInfo)
                    VALUES (@PatientId, @OrderDate, @Status, @TotalAmount, @Notes, @CreatedBy, @IsGuestOrder, @GuestInfo);
                    SELECT LAST_INSERT_ID();";

                var orderParams = new[]
                {
                    new MySqlParameter("@PatientId", patientId ?? (object)DBNull.Value),
                    new MySqlParameter("@OrderDate", order.OrderDate),
                    new MySqlParameter("@Status", order.Status ?? "Pending"),
                    new MySqlParameter("@TotalAmount", order.TotalAmount),
                    new MySqlParameter("@Notes", order.Notes ?? (object)DBNull.Value),
                    new MySqlParameter("@CreatedBy", order.CreatedBy),
                    new MySqlParameter("@IsGuestOrder", isGuestOrder),
                    new MySqlParameter("@GuestInfo", order.GuestInfo ?? (object)DBNull.Value)
                };

                var orderId = Convert.ToInt32(repository.ExecuteScalar(orderQuery, orderParams));

                // Create order items
                foreach (var item in order.OrderItems)
                {
                    string itemQuery = @"
                        INSERT INTO OrderItems (OrderId, TabletId, Quantity, UnitPrice)
                        VALUES (@OrderId, @TabletId, @Quantity, @UnitPrice)";

                    var itemParams = new[]
                    {
                        new MySqlParameter("@OrderId", orderId),
                        new MySqlParameter("@TabletId", item.TabletId),
                        new MySqlParameter("@Quantity", item.Quantity),
                        new MySqlParameter("@UnitPrice", item.UnitPrice)
                    };

                    repository.ExecuteNonQuery(itemQuery, itemParams);

                    // Update stock quantity
                    UpdateTabletStock(item.TabletId, -item.Quantity);
                }

                return OperationResult.SuccessResult("Order created successfully.", orderId);
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error creating order: {ex.Message}");
            }
        }

        private void UpdateTabletStock(int tabletId, int quantityChange)
        {
            try
            {
                string query = "UPDATE Tablets SET StockQuantity = StockQuantity + @Quantity WHERE TabletId = @TabletId";
                var parameters = new[]
                {
                    new MySqlParameter("@Quantity", quantityChange),
                    new MySqlParameter("@TabletId", tabletId)
                };

                repository.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating tablet stock: {ex.Message}");
            }
        }

        public List<Order> GetOrdersByPatient(int patientId)
        {
            var orders = new List<Order>();

            string query;
            MySqlParameter[] parameters;

            if (patientId == 0)
            {
                // Get guest orders (PatientId IS NULL)
                query = @"
                    SELECT o.*, COALESCE(o.GuestInfo, 'Guest Customer') as DisplayName
                    FROM Orders o
                    WHERE o.PatientId IS NULL
                    ORDER BY o.OrderDate DESC";
                parameters = new MySqlParameter[0];
            }
            else
            {
                // Get orders for registered patient
                query = @"
                    SELECT o.*, CONCAT(per.FirstName, ' ', per.LastName) as DisplayName
                    FROM Orders o
                    INNER JOIN Patients p ON o.PatientId = p.PatientId
                    INNER JOIN Persons per ON p.PersonId = per.PersonId
                    WHERE o.PatientId = @PatientId
                    ORDER BY o.OrderDate DESC";
                parameters = new[] { new MySqlParameter("@PatientId", patientId) };
            }

            var dataTable = repository.ExecuteQuery(query, parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                var order = MapOrderFromDataRow(row);
                order.OrderItems = GetOrderItems(order.OrderId);
                orders.Add(order);
            }

            return orders;
        }

        public List<Order> GetAllOrders(string status = null)
        {
            var orders = new List<Order>();

            string query = @"
                SELECT o.*, 
                       CASE 
                           WHEN o.PatientId IS NULL THEN COALESCE(o.GuestInfo, 'Guest Customer')
                           ELSE CONCAT(per.FirstName, ' ', per.LastName)
                       END as DisplayName
                FROM Orders o
                LEFT JOIN Patients p ON o.PatientId = p.PatientId
                LEFT JOIN Persons per ON p.PersonId = per.PersonId";

            var parameters = new List<MySqlParameter>();

            if (!string.IsNullOrEmpty(status))
            {
                query += " WHERE o.Status = @Status";
                parameters.Add(new MySqlParameter("@Status", status));
            }

            query += " ORDER BY o.OrderDate DESC";

            var dataTable = repository.ExecuteQuery(query, parameters.ToArray());

            foreach (DataRow row in dataTable.Rows)
            {
                var order = MapOrderFromDataRow(row);
                order.OrderItems = GetOrderItems(order.OrderId);
                orders.Add(order);
            }

            return orders;
        }

        public Order GetOrderById(int orderId)
        {
            string query = @"
                SELECT o.*, 
                       CASE 
                           WHEN o.PatientId IS NULL THEN COALESCE(o.GuestInfo, 'Guest Customer')
                           ELSE CONCAT(per.FirstName, ' ', per.LastName)
                       END as DisplayName
                FROM Orders o
                LEFT JOIN Patients p ON o.PatientId = p.PatientId
                LEFT JOIN Persons per ON p.PersonId = per.PersonId
                WHERE o.OrderId = @OrderId";

            var parameters = new[] { new MySqlParameter("@OrderId", orderId) };
            var dataTable = repository.ExecuteQuery(query, parameters);

            if (dataTable.Rows.Count == 0)
                return null;

            var order = MapOrderFromDataRow(dataTable.Rows[0]);
            order.OrderItems = GetOrderItems(order.OrderId);
            return order;
        }

        private List<OrderItem> GetOrderItems(int orderId)
        {
            var items = new List<OrderItem>();

            string query = @"
                SELECT oi.*, t.TabletName, t.Description
                FROM OrderItems oi
                INNER JOIN Tablets t ON oi.TabletId = t.TabletId
                WHERE oi.OrderId = @OrderId";

            var parameters = new[] { new MySqlParameter("@OrderId", orderId) };
            var dataTable = repository.ExecuteQuery(query, parameters);

            foreach (DataRow row in dataTable.Rows)
            {
                items.Add(new OrderItem
                {
                    OrderItemId = Convert.ToInt32(row["OrderItemId"]),
                    OrderId = Convert.ToInt32(row["OrderId"]),
                    TabletId = Convert.ToInt32(row["TabletId"]),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                    Tablet = new Tablet
                    {
                        TabletName = row["TabletName"]?.ToString(),
                        Description = row["Description"]?.ToString()
                    }
                });
            }

            return items;
        }

        private Order MapOrderFromDataRow(DataRow row)
        {
            return new Order
            {
                OrderId = Convert.ToInt32(row["OrderId"]),
                PatientId = row["PatientId"] == DBNull.Value ? 0 : Convert.ToInt32(row["PatientId"]),
                OrderDate = Convert.ToDateTime(row["OrderDate"]),
                Status = row["Status"]?.ToString() ?? "Pending",
                TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                Notes = row["Notes"]?.ToString(),
                CreatedBy = Convert.ToInt32(row["CreatedBy"]),
                IsGuestOrder = Convert.ToBoolean(row["IsGuestOrder"]),
                GuestInfo = row["GuestInfo"]?.ToString(),
                Patient = new Patient
                {
                    PersonInfo = new Person
                    {
                        FirstName = row["DisplayName"]?.ToString(),
                        LastName = ""
                    }
                }
            };
        }

        public OperationResult UpdateOrderStatus(int orderId, string status, int changedBy, string notes = null)
        {
            try
            {
                string query = "UPDATE Orders SET Status = @Status WHERE OrderId = @OrderId";
                var parameters = new[]
                {
                    new MySqlParameter("@Status", status),
                    new MySqlParameter("@OrderId", orderId)
                };

                repository.ExecuteNonQuery(query, parameters);

                // Add to status history
                string historyQuery = @"
                    INSERT INTO OrderStatusHistory (OrderId, Status, ChangedBy, Notes)
                    VALUES (@OrderId, @Status, @ChangedBy, @Notes)";

                var historyParams = new[]
                {
                    new MySqlParameter("@OrderId", orderId),
                    new MySqlParameter("@Status", status),
                    new MySqlParameter("@ChangedBy", changedBy),
                    new MySqlParameter("@Notes", notes ?? (object)DBNull.Value)
                };

                repository.ExecuteNonQuery(historyQuery, historyParams);

                return OperationResult.SuccessResult($"Order status updated to {status}.");
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error updating order status: {ex.Message}");
            }
        }

        public DataTable GetAvailableTablets()
        {
            string query = @"
                SELECT TabletId, TabletName, Description, CostPerUnit, StockQuantity
                FROM Tablets 
                WHERE IsActive = TRUE AND StockQuantity > 0
                ORDER BY TabletName";

            return repository.ExecuteQuery(query);
        }

        public decimal CalculateOrderTotal(List<OrderItem> items)
        {
            decimal total = 0;
            foreach (var item in items)
            {
                total += item.Quantity * item.UnitPrice;
            }
            return total;
        }

        public OperationResult CancelOrder(int orderId, int changedBy, string reason = null)
        {
            try
            {
                // First, restore stock for all items in the order
                var order = GetOrderById(orderId);
                if (order != null)
                {
                    foreach (var item in order.OrderItems)
                    {
                        UpdateTabletStock(item.TabletId, item.Quantity);
                    }
                }

                // Then update order status to cancelled
                return UpdateOrderStatus(orderId, "Cancelled", changedBy, reason);
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error cancelling order: {ex.Message}");
            }
        }
    }
}