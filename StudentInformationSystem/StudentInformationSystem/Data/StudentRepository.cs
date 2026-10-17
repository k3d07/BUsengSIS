using System;
using MySqlConnector;
using BUsengSIS.Models;

namespace BUsengSIS.Data
{
    public class UserRepository
    {
        public User GetByUsername(string username)
        {
            using var connection = DbHelper.GetConnection();

            string sql = @"SELECT user_id, username, password_hash, role, student_id
                           FROM users
                           WHERE username = @username
                           LIMIT 1;";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@username", username);

            connection.Open();

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return null;

            return new User
            {
                UserId = reader.GetInt32("user_id"),
                Username = reader.GetString("username"),
                PasswordHash = reader.GetString("password_hash"),
                Role = reader.GetString("role"),
                StudentId = reader.IsDBNull(reader.GetOrdinal("student_id"))
                    ? null
                    : reader.GetInt32("student_id")
            };
        }

        public void UpdateLastLogin(int userId)
        {
            using var connection = DbHelper.GetConnection();

            string sql = @"UPDATE users
                           SET last_login = @lastLogin
                           WHERE user_id = @userId;";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@lastLogin", DateTime.Now);
            command.Parameters.AddWithValue("@userId", userId);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}