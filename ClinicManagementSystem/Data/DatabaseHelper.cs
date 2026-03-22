using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace ClinicManagementSystem.Data
{
    public static class DatabaseHelper
    {
        // Updated connection string with common configurations
        private static string _connectionString = "server=localhost;database=ClinicManagementSystem;uid=root;pwd=;";

        public static string ConnectionString
        {
            get => _connectionString;
            set => _connectionString = value;
        }

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Database connection failed: {ex.Message}");
                return false;
            }
        }
    }
}