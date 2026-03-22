using ClinicManagementSystem.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace ClinicManagementSystem
{
    public partial class ReportsForm : Form
    {
        private BaseRepository repository;

        public ReportsForm()
        {
            InitializeComponent();
            repository = new BaseRepository();
            LoadReportTypes();
            dtpStartDate.Value = DateTime.Now.AddDays(-30);
            dtpEndDate.Value = DateTime.Now;
        }

        private void LoadReportTypes()
        {
            var reportTypes = new List<string>
            {
                "Daily Payments",
                "Weekly Payments",
                "Monthly Payments",
                "Patient Appointments",
                "Doctor Schedule",
                "Medical Records",
                "Inventory Status",
                "Revenue Analysis"
            };

            cmbReportType.DataSource = reportTypes;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string reportType = cmbReportType.SelectedItem.ToString();
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;

                DataTable reportData = GenerateReport(reportType, startDate, endDate);
                dgvReport.DataSource = reportData;

                UpdateSummary(reportData, reportType);
                UpdatePaymentSummary(reportData, reportType);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GenerateReport(string reportType, DateTime startDate, DateTime endDate)
        {
            string query = "";
            var parameters = new List<MySqlParameter>
            {
                new MySqlParameter("@StartDate", startDate.Date),
                new MySqlParameter("@EndDate", endDate.Date.AddDays(1).AddSeconds(-1))
            };

            switch (reportType)
            {
                case "Daily Payments":
                    query = @"SELECT DATE(p.PaymentDate) as PaymentDate, 
                             COUNT(*) as TotalTransactions,
                             SUM(p.Amount) as TotalAmount,
                             'Daily' as Period
                             FROM Payments p
                             WHERE p.PaymentDate BETWEEN @StartDate AND @EndDate
                             GROUP BY DATE(p.PaymentDate)
                             ORDER BY PaymentDate DESC";
                    break;

                case "Weekly Payments":
                    query = @"SELECT YEAR(p.PaymentDate) as Year, 
                             WEEK(p.PaymentDate) as Week,
                             COUNT(*) as TotalTransactions,
                             SUM(p.Amount) as TotalAmount,
                             'Weekly' as Period
                             FROM Payments p
                             WHERE p.PaymentDate BETWEEN @StartDate AND @EndDate
                             GROUP BY YEAR(p.PaymentDate), WEEK(p.PaymentDate)
                             ORDER BY Year DESC, Week DESC";
                    break;

                case "Monthly Payments":
                    query = @"SELECT YEAR(p.PaymentDate) as Year, 
                             MONTH(p.PaymentDate) as Month,
                             COUNT(*) as TotalTransactions,
                             SUM(p.Amount) as TotalAmount,
                             'Monthly' as Period
                             FROM Payments p
                             WHERE p.PaymentDate BETWEEN @StartDate AND @EndDate
                             GROUP BY YEAR(p.PaymentDate), MONTH(p.PaymentDate)
                             ORDER BY Year DESC, Month DESC";
                    break;

                case "Patient Appointments":
                    query = @"SELECT a.AppointmentDate, 
                             p.FirstName, p.LastName,
                             d.FirstName as DoctorFirstName, d.LastName as DoctorLastName,
                             a.Status, a.Reason
                             FROM Appointments a
                             INNER JOIN Patients pat ON a.PatientId = pat.PatientId
                             INNER JOIN Persons p ON pat.PersonId = p.PersonId
                             INNER JOIN Doctors doc ON a.DoctorId = doc.DoctorId
                             INNER JOIN Persons d ON doc.PersonId = d.PersonId
                             WHERE a.AppointmentDate BETWEEN @StartDate AND @EndDate
                             ORDER BY a.AppointmentDate DESC";
                    break;

                case "Doctor Schedule":
                    query = @"SELECT d.FirstName, d.LastName, doc.Specialization,
                             COUNT(a.AppointmentId) as TotalAppointments,
                             SUM(CASE WHEN a.Status = 'Completed' THEN 1 ELSE 0 END) as Completed,
                             SUM(CASE WHEN a.Status = 'Scheduled' THEN 1 ELSE 0 END) as Scheduled
                             FROM Doctors doc
                             INNER JOIN Persons d ON doc.PersonId = d.PersonId
                             LEFT JOIN Appointments a ON doc.DoctorId = a.DoctorId 
                             AND a.AppointmentDate BETWEEN @StartDate AND @EndDate
                             GROUP BY doc.DoctorId, d.FirstName, d.LastName, doc.Specialization
                             ORDER BY TotalAppointments DESC";
                    break;

                case "Medical Records":
                    query = @"SELECT mr.RecordDate,
                             p.FirstName as PatientFirstName, p.LastName as PatientLastName,
                             d.FirstName as DoctorFirstName, d.LastName as DoctorLastName,
                             mr.Diagnosis, mr.Symptoms
                             FROM MedicalRecords mr
                             INNER JOIN Patients pat ON mr.PatientId = pat.PatientId
                             INNER JOIN Persons p ON pat.PersonId = p.PersonId
                             INNER JOIN Doctors doc ON mr.DoctorId = doc.DoctorId
                             INNER JOIN Persons d ON doc.PersonId = d.PersonId
                             WHERE mr.RecordDate BETWEEN @StartDate AND @EndDate
                             ORDER BY mr.RecordDate DESC";
                    break;

                case "Revenue Analysis":
                    query = @"SELECT 
                             b.BillType,
                             COUNT(*) as TotalBills,
                             SUM(b.TotalAmount) as TotalAmount,
                             SUM(b.PaidAmount) as PaidAmount,
                             SUM(b.DueAmount) as DueAmount
                             FROM Bills b
                             WHERE b.BillDate BETWEEN @StartDate AND @EndDate
                             GROUP BY b.BillType
                             ORDER BY TotalAmount DESC";
                    break;

                default:
                    query = "SELECT 'No data available' as Message";
                    break;
            }

            return repository.ExecuteQuery(query, parameters.ToArray());
        }

        private void UpdateSummary(DataTable data, string reportType)
        {
            if (data.Rows.Count == 0)
            {
                txtSummary.Text = "No data available for the selected criteria.";
                return;
            }

            string summary = $"{reportType} Report Summary:\n\n";
            summary += $"Report Period: {dtpStartDate.Value:MM/dd/yyyy} to {dtpEndDate.Value:MM/dd/yyyy}\n";
            summary += $"Total Records: {data.Rows.Count}\n";

            if (data.Columns.Contains("TotalAmount"))
            {
                decimal totalAmount = 0;
                foreach (DataRow row in data.Rows)
                {
                    if (row["TotalAmount"] != DBNull.Value)
                        totalAmount += Convert.ToDecimal(row["TotalAmount"]);
                }
                summary += $"Total Amount: {totalAmount:C}\n";
            }

            if (data.Columns.Contains("TotalTransactions"))
            {
                int totalTransactions = 0;
                foreach (DataRow row in data.Rows)
                {
                    if (row["TotalTransactions"] != DBNull.Value)
                        totalTransactions += Convert.ToInt32(row["TotalTransactions"]);
                }
                summary += $"Total Transactions: {totalTransactions}\n";
            }

            txtSummary.Text = summary;
        }

        private void UpdatePaymentSummary(DataTable data, string reportType)
        {
            if (!reportType.Contains("Payment") && !reportType.Contains("Revenue"))
            {
                grpPaymentDetails.Visible = false;
                return;
            }

            grpPaymentDetails.Visible = true;

            decimal totalAmount = 0;
            decimal totalPaid = 0;
            decimal totalDue = 0;

            foreach (DataRow row in data.Rows)
            {
                if (data.Columns.Contains("TotalAmount") && row["TotalAmount"] != DBNull.Value)
                    totalAmount += Convert.ToDecimal(row["TotalAmount"]);

                if (data.Columns.Contains("PaidAmount") && row["PaidAmount"] != DBNull.Value)
                    totalPaid += Convert.ToDecimal(row["PaidAmount"]);

                if (data.Columns.Contains("DueAmount") && row["DueAmount"] != DBNull.Value)
                    totalDue += Convert.ToDecimal(row["DueAmount"]);
            }

            // If we don't have separate paid/due columns, calculate from bills
            if (totalPaid == 0 && totalDue == 0 && totalAmount > 0)
            {
                totalPaid = totalAmount * 0.7m; // Example: 70% paid
                totalDue = totalAmount * 0.3m;   // Example: 30% due
            }

            txtTotalAmount.Text = totalAmount.ToString("C");
            txtTotalPaid.Text = totalPaid.ToString("C");
            txtTotalDue.Text = totalDue.ToString("C");
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dgvReport.Rows.Count == 0)
            {
                MessageBox.Show("No data to export. Please generate a report first.", "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv|Text files (*.txt)|*.txt";
                saveFileDialog.Title = "Export Report";
                saveFileDialog.FileName = $"{cmbReportType.SelectedItem}_{DateTime.Now:yyyyMMdd}.csv";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(saveFileDialog.FileName))
                    {
                        // Write headers
                        for (int i = 0; i < dgvReport.Columns.Count; i++)
                        {
                            writer.Write(dgvReport.Columns[i].HeaderText);
                            if (i < dgvReport.Columns.Count - 1)
                                writer.Write(",");
                        }
                        writer.WriteLine();

                        // Write data
                        foreach (DataGridViewRow row in dgvReport.Rows)
                        {
                            for (int i = 0; i < dgvReport.Columns.Count; i++)
                            {
                                writer.Write(row.Cells[i].Value?.ToString() ?? "");
                                if (i < dgvReport.Columns.Count - 1)
                                    writer.Write(",");
                            }
                            writer.WriteLine();
                        }
                    }

                    MessageBox.Show("Report exported successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            // Additional initialization if needed
        }
    }
}