using MySqlConnector;

namespace StudentInformationSystem.Data;

public static class StudentStats
{
    private static readonly HashSet<string> AllowedColumns = new(StringComparer.Ordinal)
    {
        "year_level",
        "program",
        "gender"
    };

    public static Dictionary<string, int> CountBy(string column)
    {
        if (!AllowedColumns.Contains(column))
            throw new ArgumentException("Unsupported student statistics column.", nameof(column));

        var result = new Dictionary<string, int>(StringComparer.Ordinal);
        string sql = $"SELECT {column}, COUNT(*) FROM students GROUP BY {column} ORDER BY {column}";

        using var connection = DbHelper.GetConnection();
        connection.Open();

        using var command = new MySqlCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            string key = reader.IsDBNull(0) ? "Unknown" : reader.GetString(0);
            result[key] = Convert.ToInt32(reader.GetValue(1));
        }

        return result;
    }
}
