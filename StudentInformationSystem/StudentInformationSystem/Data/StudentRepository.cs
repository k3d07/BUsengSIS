using StudentInformationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySqlConnector;

namespace StudentInformationSystem.Data
{
    internal class UserRepository
    {
        private const string SelectColumns =
            @"SELECT student_id, name, gender, year_level, program,
                     contact_number, email, photo_path
              FROM students";
        public bool AddStudent(Student s)
        {
            using var conn = DbHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(
                @"INSERT INTO students
          (student_id, name, gender, year_level, program, contact_number, email, photo_path)
          VALUES
          (@id, @name, @gender, @year, @program, @contact, @email, @photo)", conn);

            cmd.Parameters.AddWithValue("@id", s.StudentId);
            cmd.Parameters.AddWithValue("@name", s.Name);
            cmd.Parameters.AddWithValue("@gender", s.Gender);
            cmd.Parameters.AddWithValue("@year", s.YearLevel);
            cmd.Parameters.AddWithValue("@program", s.Program);
            cmd.Parameters.AddWithValue("@contact", s.ContactNumber);
            cmd.Parameters.AddWithValue("@email",
                string.IsNullOrWhiteSpace(s.Email) ? DBNull.Value : (object)s.Email);
            cmd.Parameters.AddWithValue("@photo",
                string.IsNullOrWhiteSpace(s.PhotoPath) ? DBNull.Value : (object)s.PhotoPath);

            return cmd.ExecuteNonQuery() > 0;
        }
        public DataTable GetAllStudents()
        {
            using var conn = DbHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(
                SelectColumns + " ORDER BY student_id", conn);
            using var adapter = new MySqlDataAdapter(cmd);

            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable SearchStudents(string keyword)
        {
            keyword = (keyword ?? string.Empty).Trim();

            if (keyword.Length == 0)
                return GetAllStudents();

            using var conn = DbHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(
                SelectColumns +
                " WHERE name LIKE @kw OR student_id LIKE @kw ORDER BY student_id", conn);
            cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
            using var adapter = new MySqlDataAdapter(cmd);

            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public DataTable FilterStudents(string year, string program, string keyword)
        {
            year = (year ?? string.Empty).Trim();
            program = (program ?? string.Empty).Trim();
            keyword = (keyword ?? string.Empty).Trim();

            var sql = new StringBuilder(SelectColumns + " WHERE 1=1");

            using var conn = DbHelper.GetConnection();
            conn.Open();
            using var cmd = new MySqlCommand { Connection = conn };

            if (year.Length > 0 && !year.StartsWith("All", StringComparison.OrdinalIgnoreCase))
            {
                sql.Append(" AND year_level = @year");
                cmd.Parameters.AddWithValue("@year", year);
            }

            if (program.Length > 0 && !program.StartsWith("All", StringComparison.OrdinalIgnoreCase))
            {
                sql.Append(" AND program = @program");
                cmd.Parameters.AddWithValue("@program", program);
            }

            if (keyword.Length > 0)
            {
                sql.Append(" AND (name LIKE @kw OR student_id LIKE @kw)");
                cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
            }

            sql.Append(" ORDER BY student_id");
            cmd.CommandText = sql.ToString();

            using var adapter = new MySqlDataAdapter(cmd);
            var table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public Dictionary<string, int> GetStudentCounts()
        {
            var counts = new Dictionary<string, int>
            {
                ["Total"] = 0,
                ["1st Year"] = 0,
                ["2nd Year"] = 0,
                ["3rd Year"] = 0,
                ["4th Year"] = 0
            };

            using var conn = DbHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(
                "SELECT year_level, COUNT(*) FROM students GROUP BY year_level", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string year = reader.GetString(0);
                int n = Convert.ToInt32(reader.GetValue(1));

                counts["Total"] += n;
                if (counts.ContainsKey(year))
                    counts[year] = n;
            }

            return counts;
        }
        public bool UpdateStudent(Student s)
        {
            using var conn = DbHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(
                @"UPDATE students
          SET name = @name,
              gender = @gender,
              year_level = @year,
              program = @program,
              contact_number = @contact,
              email = @email,
              photo_path = @photo
          WHERE student_id = @id", conn);

            cmd.Parameters.AddWithValue("@id", s.StudentId);
            cmd.Parameters.AddWithValue("@name", s.Name);
            cmd.Parameters.AddWithValue("@gender", s.Gender);
            cmd.Parameters.AddWithValue("@year", s.YearLevel);
            cmd.Parameters.AddWithValue("@program", s.Program);
            cmd.Parameters.AddWithValue("@contact", s.ContactNumber);
            cmd.Parameters.AddWithValue("@email",
                string.IsNullOrWhiteSpace(s.Email) ? DBNull.Value : (object)s.Email);
            cmd.Parameters.AddWithValue("@photo",
                string.IsNullOrWhiteSpace(s.PhotoPath) ? DBNull.Value : (object)s.PhotoPath);

            return cmd.ExecuteNonQuery() > 0;
        }
        public bool DeleteStudent(string studentId)
        {
            using var conn = DbHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(
                "DELETE FROM students WHERE student_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", studentId);

            return cmd.ExecuteNonQuery() > 0;
        }
        public bool StudentExists(string studentId)
        {
            using var conn = DbHelper.GetConnection();
            conn.Open();

            using var cmd = new MySqlCommand(
                "SELECT COUNT(1) FROM students WHERE student_id = @id", conn);
            cmd.Parameters.AddWithValue("@id", studentId);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
    }
}