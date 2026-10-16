using StudentInformationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySqlConnector;

namespace StudentInformationSystem.Data
{
    internal class StudentRepository
    {
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
        public DataTable GetAllStudents() { return new DataTable(); }
        public DataTable SearchStudents(string keyword) { return new DataTable(); }
        public bool UpdateStudent(Student s) { return false; }
        public bool DeleteStudent(string studentId) { return false; }
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
