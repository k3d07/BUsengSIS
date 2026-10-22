using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;

namespace StudentInformationSystem.Data
{
    public class ActivityLogRepository
    {
        public static void Log(string username, string action, string studentId)
        {
            using var conn = DbHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(
                @"INSERT INTO activity_log (username, action, student_id)
                  VALUES (@username, @action, @studentId)", conn);

            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@action", action);
            cmd.Parameters.AddWithValue("@studentId",
                string.IsNullOrWhiteSpace(studentId) ? DBNull.Value : studentId);

            cmd.ExecuteNonQuery();
        }

        // Newest first. Columns: logged_at, username, action, student_id
        public static DataTable GetRecent()
        {
            using var conn = DbHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(
                @"SELECT logged_at, username, action, student_id
                  FROM activity_log
                  ORDER BY logged_at DESC, log_id DESC
                  LIMIT 200", conn);
            using var adapter = new MySqlDataAdapter(cmd);

            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}