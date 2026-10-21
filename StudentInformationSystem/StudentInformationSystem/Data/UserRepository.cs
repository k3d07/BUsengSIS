using MySqlConnector;
using StudentInformationSystem.Security;

namespace StudentInformationSystem.Data
{
    public static class UserRepository
    {
        // True only when the username exists and the password matches its stored hash.
        // Throws MySqlException if the database can't be reached (the caller shows a message).
        public static bool ValidateLogin(string username, string password)
        {
            username = (username ?? "").Trim();
            if (username.Length == 0 || string.IsNullOrEmpty(password))
                return false;

            using var conn = DbHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(
                "SELECT password_hash FROM users WHERE username = @u LIMIT 1", conn);
            cmd.Parameters.AddWithValue("@u", username);

            object? result = cmd.ExecuteScalar();
            if (result == null || result == DBNull.Value)
                return false;

            return PasswordHasher.Verify(password, result.ToString() ?? "");
        }

        // First run: creates the default admin account (admin / admin123) as a salted hash.
        // Does nothing if it already exists, so running it again never creates a duplicate.
        public static void EnsureAdminExists()
        {
            using var conn = DbHelper.GetConnection();
            conn.Open();

            using var check = new MySqlCommand(
                "SELECT COUNT(1) FROM users WHERE username = @u", conn);
            check.Parameters.AddWithValue("@u", "admin");

            if (Convert.ToInt32(check.ExecuteScalar()) > 0)
                return;

            using var insert = new MySqlCommand(
                "INSERT INTO users (username, password_hash) VALUES (@u, @h)", conn);
            insert.Parameters.AddWithValue("@u", "admin");
            insert.Parameters.AddWithValue("@h", PasswordHasher.Hash("admin123"));
            insert.ExecuteNonQuery();
        }
    }
}