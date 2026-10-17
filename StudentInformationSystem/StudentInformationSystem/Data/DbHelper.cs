using MySqlConnector;
using System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace StudentInformationSystem.Data
{
    internal static class DbHelper
    {
        private const string ConnectionName = "StudentDb";

        private static string GetConnectionString()
        {
            var setting = ConfigurationManager.ConnectionStrings[ConnectionName];

            if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
            {
                throw new InvalidOperationException(
                    "The database connection string 'StudentDb' was not found. " +
                    "Copy App.config.example to App.config and enter your MySQL login.");
            }

            return setting.ConnectionString;
        }
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(GetConnectionString());
        }
        public static bool TestConnection(out string error)
        {
            try
            {
                using var conn = GetConnection();
                conn.Open();
                error = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}