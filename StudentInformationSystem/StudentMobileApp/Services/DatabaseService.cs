using MySqlConnector;
using StudentMobileApp.Models;

namespace StudentMobileApp.Services;

public class DatabaseService
{
    private const string ConnStr =
        "Server=10.0.2.2;Port=3306;Database=student_db;User ID=capstone;Password=capstone_pass;" +
        "SslMode=None;AllowPublicKeyRetrieval=True;Connection Timeout=5;";

    public async Task<(List<Student> Items, string? Error)> GetStudentsAsync()
    {
        var list = new List<Student>();
        try
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            const string sql = @"SELECT student_id, name, gender, year_level, program, contact_number
                                 FROM students ORDER BY name";
            await using var cmd = new MySqlCommand(sql, conn);
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new Student
                {
                    StudentId = reader.GetString(reader.GetOrdinal("student_id")),
                    Name = reader.GetString(reader.GetOrdinal("name")),
                    Gender = reader.GetString(reader.GetOrdinal("gender")),
                    YearLevel = reader.GetString(reader.GetOrdinal("year_level")),
                    Program = reader.GetString(reader.GetOrdinal("program")),
                    ContactNumber = reader.GetString(reader.GetOrdinal("contact_number"))
                });
            }

            return (list, null);
        }
        catch (MySqlException ex)
        {
            return (list, "Cannot reach the database. Check that MySQL is running and try again. (" + ex.Message + ")");
        }
        catch (Exception ex)
        {
            return (list, "Could not load students. Please try again. (" + ex.Message + ")");
        }
    }

    public async Task<string?> AddStudentAsync(Student student)
    {
        try
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            const string sql = @"INSERT INTO students
                                 (student_id, name, gender, year_level, program, contact_number)
                                 VALUES (@id, @name, @gender, @year, @program, @contact)";
            await using var cmd = new MySqlCommand(sql, conn);
            Bind(cmd, student);
            await cmd.ExecuteNonQueryAsync();
            return null;
        }
        catch (MySqlException ex) when (ex.Number == 1062)
        {
            return "That Student ID already exists.";
        }
        catch (Exception ex)
        {
            return "Could not save the student. Please try again. (" + ex.Message + ")";
        }
    }

    public async Task<string?> UpdateStudentAsync(Student student)
    {
        try
        {
            await using var conn = new MySqlConnection(ConnStr);
            await conn.OpenAsync();
            const string sql = @"UPDATE students
                                 SET name = @name, gender = @gender, year_level = @year,
                                     program = @program, contact_number = @contact
                                 WHERE student_id = @id";
            await using var cmd = new MySqlCommand(sql, conn);
            Bind(cmd, student);
            await cmd.ExecuteNonQueryAsync();
            return null;
        }
        catch (Exception ex)
        {
            return "Could not update the student. Please try again. (" + ex.Message + ")";
        }
    }

    private static void Bind(MySqlCommand cmd, Student student)
    {
        cmd.Parameters.AddWithValue("@id", student.StudentId);
        cmd.Parameters.AddWithValue("@name", student.Name);
        cmd.Parameters.AddWithValue("@gender", student.Gender);
        cmd.Parameters.AddWithValue("@year", student.YearLevel);
        cmd.Parameters.AddWithValue("@program", student.Program);
        cmd.Parameters.AddWithValue("@contact", student.ContactNumber);
    }
}
