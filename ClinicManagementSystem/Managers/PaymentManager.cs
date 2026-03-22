using ClinicManagementSystem.Data;
using ClinicManagementSystem.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClinicManagementSystem.Managers
{
    public class PaymentManager
    {
        private BaseRepository repository;

        public PaymentManager()
        {
            repository = new BaseRepository();
        }

        // Add these missing methods
        public DataTable ExecuteQuery(string query, MySqlParameter[] parameters = null)
        {
            return repository.ExecuteQuery(query, parameters);
        }

        public object ExecuteScalar(string query, MySqlParameter[] parameters = null)
        {
            return repository.ExecuteScalar(query, parameters);
        }

        public decimal GetDailyCollection(DateTime date)
        {
            string query = @"
                SELECT COALESCE(SUM(Amount), 0) as DailyCollection
                FROM Payments 
                WHERE DATE(PaymentDate) = DATE(@Date) 
                    AND Status IN ('Verified', 'Completed')";

            var parameters = new[] { new MySqlParameter("@Date", date) };
            var result = ExecuteScalar(query, parameters);
            return result == DBNull.Value ? 0 : Convert.ToDecimal(result);
        }

        public int GetVerifiedPaymentsCount()
        {
            string query = "SELECT COUNT(*) FROM Payments WHERE DATE(VerifiedDate) = CURDATE() AND Status IN ('Verified', 'Completed')";
            var result = ExecuteScalar(query);
            return result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }

        public OperationResult ProcessPayment(Payment payment)
        {
            try
            {
                // Generate receipt number
                string receiptNumber = GenerateReceiptNumber();

                // Insert payment
                string paymentQuery = @"
                    INSERT INTO Payments (BillId, Amount, PaymentMethod, TransactionId, Status, PaymentDate, Notes, ReceiptNumber) 
                    VALUES (@BillId, @Amount, @PaymentMethod, @TransactionId, @Status, @PaymentDate, @Notes, @ReceiptNumber)";

                var paymentParams = new[]
                {
                    new MySqlParameter("@BillId", payment.BillId),
                    new MySqlParameter("@Amount", payment.Amount),
                    new MySqlParameter("@PaymentMethod", payment.PaymentMethod),
                    new MySqlParameter("@TransactionId", payment.TransactionReference ?? (object)DBNull.Value),
                    new MySqlParameter("@Status", "Pending"),
                    new MySqlParameter("@PaymentDate", DateTime.Now),
                    new MySqlParameter("@Notes", payment.Notes ?? $"Payment via {payment.PaymentMethod}"),
                    new MySqlParameter("@ReceiptNumber", receiptNumber)
                };

                repository.ExecuteNonQuery(paymentQuery, paymentParams);

                // Update bill paid amount
                string billQuery = @"UPDATE Bills 
                                   SET PaidAmount = PaidAmount + @Amount,
                                       LastPaymentDate = @PaymentDate
                                   WHERE BillId = @BillId";

                var billParams = new[]
                {
                    new MySqlParameter("@Amount", payment.Amount),
                    new MySqlParameter("@PaymentDate", DateTime.Now),
                    new MySqlParameter("@BillId", payment.BillId)
                };

                repository.ExecuteNonQuery(billQuery, billParams);

                return OperationResult.SuccessResult("Payment processed successfully. Awaiting verification.", receiptNumber);
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error processing payment: {ex.Message}");
            }
        }

        public OperationResult VerifyPayment(int paymentId, int verifiedBy, string notes = null)
        {
            try
            {
                string query = @"
                    UPDATE Payments 
                    SET Status = 'Verified', 
                        VerifiedDate = @VerifiedDate,
                        VerifiedBy = @VerifiedBy,
                        Notes = CONCAT(COALESCE(Notes, ''), ' | Verified: ', @Notes)
                    WHERE PaymentId = @PaymentId";

                var parameters = new[]
                {
                    new MySqlParameter("@VerifiedDate", DateTime.Now),
                    new MySqlParameter("@VerifiedBy", verifiedBy),
                    new MySqlParameter("@Notes", notes ?? "Payment verified"),
                    new MySqlParameter("@PaymentId", paymentId)
                };

                int rowsAffected = repository.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    CompletePaymentIfFull(paymentId);
                    return OperationResult.SuccessResult("Payment verified successfully.");
                }
                else
                {
                    return OperationResult.ErrorResult("Payment not found or already verified.");
                }
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error verifying payment: {ex.Message}");
            }
        }

        private void CompletePaymentIfFull(int paymentId)
        {
            string checkQuery = @"
                SELECT b.BillId, b.TotalAmount, b.PaidAmount, p.Amount
                FROM Payments p
                INNER JOIN Bills b ON p.BillId = b.BillId
                WHERE p.PaymentId = @PaymentId";

            var checkParams = new[] { new MySqlParameter("@PaymentId", paymentId) };
            var data = repository.ExecuteQuery(checkQuery, checkParams);

            if (data.Rows.Count > 0)
            {
                decimal totalAmount = Convert.ToDecimal(data.Rows[0]["TotalAmount"]);
                decimal paidAmount = Convert.ToDecimal(data.Rows[0]["PaidAmount"]);

                if (paidAmount >= totalAmount)
                {
                    string completeQuery = @"
                        UPDATE Bills 
                        SET Status = 'Paid',
                            PaymentDate = @PaymentDate
                        WHERE BillId = @BillId";

                    var completeParams = new[]
                    {
                        new MySqlParameter("@PaymentDate", DateTime.Now),
                        new MySqlParameter("@BillId", data.Rows[0]["BillId"])
                    };

                    repository.ExecuteNonQuery(completeQuery, completeParams);

                    // Also update payment status to Completed
                    string paymentCompleteQuery = @"
                        UPDATE Payments 
                        SET Status = 'Completed'
                        WHERE PaymentId = @PaymentId";

                    repository.ExecuteNonQuery(paymentCompleteQuery, checkParams);
                }
            }
        }

        public OperationResult RejectPayment(int paymentId, int rejectedBy, string reason)
        {
            try
            {
                string query = @"
                    UPDATE Payments 
                    SET Status = 'Failed',
                        Notes = CONCAT(COALESCE(Notes, ''), ' | Rejected: ', @Reason)
                    WHERE PaymentId = @PaymentId";

                var parameters = new[]
                {
                    new MySqlParameter("@Reason", reason),
                    new MySqlParameter("@PaymentId", paymentId)
                };

                int rowsAffected = repository.ExecuteNonQuery(query, parameters);

                if (rowsAffected > 0)
                {
                    RefundPaymentAmount(paymentId);
                    return OperationResult.SuccessResult("Payment rejected and amount refunded.");
                }
                else
                {
                    return OperationResult.ErrorResult("Payment not found.");
                }
            }
            catch (Exception ex)
            {
                return OperationResult.ErrorResult($"Error rejecting payment: {ex.Message}");
            }
        }

        private void RefundPaymentAmount(int paymentId)
        {
            string refundQuery = @"
                UPDATE Bills b
                INNER JOIN Payments p ON b.BillId = p.BillId
                SET b.PaidAmount = b.PaidAmount - p.Amount
                WHERE p.PaymentId = @PaymentId";

            var refundParams = new[] { new MySqlParameter("@PaymentId", paymentId) };
            repository.ExecuteNonQuery(refundQuery, refundParams);
        }

        public DataTable GetPendingPayments()
        {
            string query = @"
                SELECT 
                    p.PaymentId,
                    p.BillId,
                    p.Amount,
                    p.PaymentMethod,
                    p.TransactionId as TransactionReference,
                    p.Status,
                    p.PaymentDate,
                    p.Notes,
                    p.ReceiptNumber,
                    p.VerifiedDate,
                    p.VerifiedBy,
                    b.PatientId,
                    b.BillType,
                    b.TotalAmount,
                    b.PaidAmount,
                    CONCAT(per.FirstName, ' ', per.LastName) as PatientName
                FROM Payments p
                INNER JOIN Bills b ON p.BillId = b.BillId
                INNER JOIN Patients pat ON b.PatientId = pat.PatientId
                INNER JOIN Persons per ON pat.PersonId = per.PersonId
                WHERE p.Status = 'Pending'
                ORDER BY p.PaymentDate DESC";

            return repository.ExecuteQuery(query);
        }

        public DataTable GetPatientBalance(int patientId)
        {
            string query = @"
                SELECT 
                    b.BillId,
                    b.BillType,
                    b.Description,
                    b.TotalAmount,
                    b.PaidAmount,
                    (b.TotalAmount - b.PaidAmount) as DueAmount,
                    b.BillDate,
                    b.DueDate,
                    CASE 
                        WHEN b.PaidAmount >= b.TotalAmount THEN 'Paid'
                        WHEN b.PaidAmount > 0 THEN 'Partial'
                        ELSE 'Pending'
                    END as PaymentStatus
                FROM Bills b
                WHERE b.PatientId = @PatientId
                ORDER BY b.BillDate DESC";

            var parameters = new[] { new MySqlParameter("@PatientId", patientId) };
            return repository.ExecuteQuery(query, parameters);
        }

        public decimal GetTotalPatientBalance(int patientId)
        {
            string query = @"
                SELECT SUM(TotalAmount - PaidAmount) as TotalBalance
                FROM Bills 
                WHERE PatientId = @PatientId AND (TotalAmount - PaidAmount) > 0";

            var parameters = new[] { new MySqlParameter("@PatientId", patientId) };
            var result = ExecuteScalar(query, parameters);

            return result == DBNull.Value ? 0 : Convert.ToDecimal(result);
        }

        public PaymentReceipt GetReceiptData(string receiptNumber)
        {
            string query = @"
                SELECT 
                    p.*, 
                    per.FirstName, 
                    per.LastName, 
                    per.Phone, 
                    per.Email,
                    b.Description, 
                    b.TotalAmount, 
                    b.PaidAmount,
                    CONCAT(per.FirstName, ' ', per.LastName) as FullName,
                    u.Username as ReceivedByUser
                FROM Payments p
                INNER JOIN Bills b ON p.BillId = b.BillId
                INNER JOIN Patients pat ON b.PatientId = pat.PatientId
                INNER JOIN Persons per ON pat.PersonId = per.PersonId
                LEFT JOIN Users u ON p.VerifiedBy = u.UserId
                WHERE p.ReceiptNumber = @ReceiptNumber";

            var parameters = new[] { new MySqlParameter("@ReceiptNumber", receiptNumber) };
            var data = repository.ExecuteQuery(query, parameters);

            if (data.Rows.Count == 0)
                return null;

            var row = data.Rows[0];

            return new PaymentReceipt
            {
                ReceiptNumber = receiptNumber,
                ReceiptDate = Convert.ToDateTime(row["PaymentDate"]),
                PatientName = row["FullName"].ToString(),
                PatientPhone = row["Phone"]?.ToString() ?? "",
                PatientEmail = row["Email"]?.ToString() ?? "",
                AmountPaid = Convert.ToDecimal(row["Amount"]),
                PaymentMethod = row["PaymentMethod"].ToString(),
                TransactionReference = row["TransactionId"]?.ToString() ?? "",
                BillDescription = row["Description"]?.ToString() ?? "",
                BillTotal = Convert.ToDecimal(row["TotalAmount"]),
                PreviousBalance = Convert.ToDecimal(row["TotalAmount"]) - Convert.ToDecimal(row["Amount"]),
                NewBalance = Convert.ToDecimal(row["TotalAmount"]) - Convert.ToDecimal(row["PaidAmount"]),
                ReceivedBy = row["ReceivedByUser"]?.ToString() ?? "System"
            };
        }

        public DataTable GetPaymentReport(DateTime startDate, DateTime endDate, string status = null)
        {
            string query = @"
                SELECT 
                    p.PaymentDate, 
                    p.ReceiptNumber,
                    pat.PatientId, 
                    per.FirstName, 
                    per.LastName,
                    p.Amount, 
                    p.PaymentMethod, 
                    p.Status,
                    b.BillType, 
                    b.TotalAmount, 
                    b.PaidAmount,
                    (b.TotalAmount - b.PaidAmount) as DueAmount,
                    u.Username as VerifiedByUser
                FROM Payments p
                INNER JOIN Bills b ON p.BillId = b.BillId
                INNER JOIN Patients pat ON b.PatientId = pat.PatientId
                INNER JOIN Persons per ON pat.PersonId = per.PersonId
                LEFT JOIN Users u ON p.VerifiedBy = u.UserId
                WHERE p.PaymentDate BETWEEN @StartDate AND @EndDate";

            var parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@StartDate", startDate),
                new MySqlParameter("@EndDate", endDate.AddDays(1).AddSeconds(-1))
            };

            if (!string.IsNullOrEmpty(status))
            {
                query += " AND p.Status = @Status";
                parameters.Add(new MySqlParameter("@Status", status));
            }

            query += " ORDER BY p.PaymentDate DESC";

            return repository.ExecuteQuery(query, parameters.ToArray());
        }

        public DataTable GetPaymentHistory(int patientId, int months = 12)
        {
            string query = @"
                SELECT 
                    p.PaymentId,
                    p.ReceiptNumber,
                    p.Amount,
                    p.PaymentMethod,
                    p.Status,
                    p.PaymentDate,
                    p.VerifiedDate,
                    b.BillType,
                    b.Description,
                    u.Username as VerifiedByUser,
                    CASE 
                        WHEN p.Status = 'Completed' THEN 'Paid'
                        WHEN p.Status = 'Verified' THEN 'Verified'
                        WHEN p.Status = 'Pending' THEN 'Pending Verification'
                        WHEN p.Status = 'Refunded' THEN 'Refunded'
                        ELSE p.Status
                    END as DisplayStatus
                FROM Payments p
                INNER JOIN Bills b ON p.BillId = b.BillId
                LEFT JOIN Users u ON p.VerifiedBy = u.UserId
                WHERE b.PatientId = @PatientId 
                    AND p.PaymentDate >= @StartDate
                ORDER BY p.PaymentDate DESC";

            var parameters = new[]
            {
                new MySqlParameter("@PatientId", patientId),
                new MySqlParameter("@StartDate", DateTime.Now.AddMonths(-months))
            };

            return repository.ExecuteQuery(query, parameters);
        }

        public DataTable GetOutstandingPayments()
        {
            string query = @"
                SELECT 
                    b.BillId,
                    p.PatientId,
                    per.FirstName,
                    per.LastName,
                    per.Phone,
                    b.TotalAmount,
                    b.PaidAmount,
                    (b.TotalAmount - b.PaidAmount) as DueAmount,
                    b.BillDate,
                    DATEDIFF(CURDATE(), b.BillDate) as DaysOutstanding,
                    b.Description
                FROM Bills b
                INNER JOIN Patients pat ON b.PatientId = pat.PatientId
                INNER JOIN Persons per ON pat.PersonId = per.PersonId
                WHERE b.TotalAmount > b.PaidAmount
                    AND b.BillDate >= DATE_SUB(CURDATE(), INTERVAL 90 DAY)
                ORDER BY (b.TotalAmount - b.PaidAmount) DESC, b.BillDate ASC";

            return repository.ExecuteQuery(query);
        }

        public DataTable GetDailyPaymentSummary(DateTime date)
        {
            string query = @"
                SELECT 
                    PaymentMethod,
                    COUNT(*) as TransactionCount,
                    SUM(Amount) as TotalAmount,
                    Status
                FROM Payments 
                WHERE DATE(PaymentDate) = DATE(@Date)
                GROUP BY PaymentMethod, Status
                ORDER BY PaymentMethod, Status";

            var parameters = new[] { new MySqlParameter("@Date", date) };
            return repository.ExecuteQuery(query, parameters);
        }

        private string GenerateReceiptNumber()
        {
            string prefix = "RCP";
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string random = new Random().Next(1000, 9999).ToString();
            return $"{prefix}-{timestamp}-{random}";
        }
    }
}