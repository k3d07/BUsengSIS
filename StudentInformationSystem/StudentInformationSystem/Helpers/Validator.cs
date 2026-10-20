using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using StudentInformationSystem.Models;

namespace StudentInformationSystem.Helpers
{
    public static class Validator
    {
        private static readonly string[] Genders =
            { "Male", "Female", "Other" };

        private static readonly string[] YearLevels =
            { "1st Year", "2nd Year", "3rd Year", "4th Year" };

        private static readonly string[] Programs =
        {
            "BS Computer Science",
            "BS Information Technology",
            "BS Information Systems",
            "BS Computer Engineering"
        };

        // Letters, numbers, dashes only.
        private static readonly Regex IdPattern =
            new Regex(@"^[A-Za-z0-9-]+$", RegexOptions.Compiled);

        // Letters (any language, so ñ works), spaces, and . , ' -
        private static readonly Regex NamePattern =
            new Regex(@"^[\p{L} .,'\-]+$", RegexOptions.Compiled);

        // 09XXXXXXXXX or +639XXXXXXXXX
        private static readonly Regex ContactPattern =
            new Regex(@"^(09|\+639)\d{9}$", RegexOptions.Compiled);

        // Simple email check: something@something.something, no spaces.
        private static readonly Regex EmailPattern =
            new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public static bool IsValidStudent(Student s, out string errorMessage)
        {
            errorMessage = "";

            if (s == null)
            {
                errorMessage = "No student data was provided.";
                return false;
            }

            // Trim every text field first. Spaces-only becomes empty.
            s.StudentId = (s.StudentId ?? "").Trim();
            s.Name = (s.Name ?? "").Trim();
            s.Gender = (s.Gender ?? "").Trim();
            s.YearLevel = (s.YearLevel ?? "").Trim();
            s.Program = (s.Program ?? "").Trim();
            s.ContactNumber = (s.ContactNumber ?? "").Trim();

            // Student ID
            if (s.StudentId.Length == 0)
            {
                errorMessage = "Student ID is required.";
                return false;
            }
            if (s.StudentId.Length > 20)
            {
                errorMessage = "Student ID must be 20 characters or fewer.";
                return false;
            }
            if (!IdPattern.IsMatch(s.StudentId))
            {
                errorMessage = "Student ID may only contain letters, numbers, and dashes.";
                return false;
            }

            // Name
            if (s.Name.Length == 0)
            {
                errorMessage = "Name is required.";
                return false;
            }
            if (s.Name.Length > 100)
            {
                errorMessage = "Name must be 100 characters or fewer.";
                return false;
            }
            if (!NamePattern.IsMatch(s.Name))
            {
                errorMessage = "Name may only contain letters, spaces, and . , ' -";
                return false;
            }

            // Gender, Year Level, Program (exact dropdown values, case-sensitive)
            if (s.Gender.Length == 0)
            {
                errorMessage = "Please select a gender.";
                return false;
            }
            if (Array.IndexOf(Genders, s.Gender) < 0)
            {
                errorMessage = "Please choose a gender from the list.";
                return false;
            }

            if (s.YearLevel.Length == 0)
            {
                errorMessage = "Please select a year level.";
                return false;
            }
            if (Array.IndexOf(YearLevels, s.YearLevel) < 0)
            {
                errorMessage = "Please choose a year level from the list.";
                return false;
            }

            if (s.Program.Length == 0)
            {
                errorMessage = "Please select a program.";
                return false;
            }
            if (Array.IndexOf(Programs, s.Program) < 0)
            {
                errorMessage = "Please choose a program from the list.";
                return false;
            }

            // Contact number
            if (s.ContactNumber.Length == 0)
            {
                errorMessage = "Contact number is required.";
                return false;
            }
            if (!ContactPattern.IsMatch(s.ContactNumber))
            {
                errorMessage = "Enter a valid Philippine mobile number, for example 09171234567.";
                return false;
            }

            // Email is optional. Empty or spaces-only becomes null.
            string email = (s.Email ?? "").Trim();
            if (email.Length == 0)
            {
                s.Email = null;
            }
            else
            {
                if (email.Length > 100)
                {
                    errorMessage = "Email must be 100 characters or fewer.";
                    return false;
                }
                if (!EmailPattern.IsMatch(email))
                {
                    errorMessage = "Enter a valid email address, or leave it blank.";
                    return false;
                }
                s.Email = email;
            }

            return true;
        }
    }
}