using StudentInformationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace StudentInformationSystem.Data
{
    internal class StudentRepository
    {
        public bool AddStudent(Student s) { return false; }
        public DataTable GetAllStudents() { return new DataTable(); }
        public DataTable SearchStudents(string keyword) { return new DataTable(); }
        public bool UpdateStudent(Student s) { return false; }
        public bool DeleteStudent(string studentId) { return false; }
        public bool StudentExists(string studentId) { return false; }
    }
}
