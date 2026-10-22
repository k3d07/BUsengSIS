using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace StudentInformationSystem.Helpers
{
    // Writes the rows currently shown in the grid to a CSV file.
    public static class CsvExporter
    {
        // (database column name, header text in the file)
        private static readonly (string Column, string Header)[] Columns =
        {
            ("student_id", "Student ID"),
            ("name", "Name"),
            ("gender", "Gender"),
            ("year_level", "Year Level"),
            ("program", "Program"),
            ("contact_number", "Contact Number"),
            ("email", "Email")
        };

        // Uses a DataView so the file keeps the grid's current sort order.
        public static void Export(DataView view, string path)
        {
            var sb = new StringBuilder();

            sb.AppendLine(string.Join(",", Columns.Select(c => Escape(c.Header))));

            foreach (DataRowView row in view)
            {
                sb.AppendLine(string.Join(",", Columns.Select(c => Escape(row[c.Column]))));
            }

            // UTF-8 with BOM so Excel reads the text correctly.
            File.WriteAllText(path, sb.ToString(), new UTF8Encoding(true));
        }

        // Wraps a value in quotes if it has a comma, quote, or line break.
        // Quotes inside the value are doubled.
        private static string Escape(object? value)
        {
            string text = value == null || value == DBNull.Value
                ? ""
                : value.ToString() ?? "";

            if (text.IndexOfAny(new[] { ',', '"', '\r', '\n' }) >= 0)
            {
                text = "\"" + text.Replace("\"", "\"\"") + "\"";
            }

            return text;
        }
    }
}
