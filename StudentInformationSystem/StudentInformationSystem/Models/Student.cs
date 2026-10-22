using System;
using System.Collections.Generic;
using System.Text;

namespace StudentInformationSystem.Models
{
    public class User
    {
        public string StudentId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string YearLevel { get; set; } = string.Empty;
        public string Program { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhotoPath { get; set; }
    }
}