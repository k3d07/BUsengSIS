using System;
using System.Collections.Generic;
using MySqlConnector;

namespace StudentInformationSystem.Data
{
    public class ActivityLogRepository
    {
        public static void Log(string username, string action, string studentId)
        {
            using (var connection = DbHelper.GetConnection())
            {
                connection.Open();

                string sql = @"INSERT INTO activity_log
                               (username, action, student_id)
                               VALUES (@username, @action, @studentId)";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@action", action);
                    command.Parameters.AddWithValue("@studentId",
                        string.IsNullOrWhiteSpace(studentId)
                            ? (object)DBNull.Value
                            : studentId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<string> GetRecent()
        {
            var logs = new List<string>();

            using (var connection = DbHelper.GetConnection())
            {
                connection.Open();

                string sql = @"SELECT username, action, student_id, logged_at
                               FROM activity_log
                               ORDER BY logged_at DESC
                               LIMIT 50";

                using (var command = new MySqlCommand(sql, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string studentPart = reader.IsDBNull(reader.GetOrdinal("student_id"))
                            ? ""
                            : $" | Student ID: {reader.GetString("student_id")}";

                        logs.Add($"{reader.GetDateTime("logged_at")} | " +
                                 $"{reader.GetString("username")} | " +
                                 $"{reader.GetString("action")}{studentPart}");
                    }
                }
            }

            return logs;
        }
    }
}
