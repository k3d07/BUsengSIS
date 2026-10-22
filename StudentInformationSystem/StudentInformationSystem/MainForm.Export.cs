using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using StudentInformationSystem.Data;
using StudentInformationSystem.Helpers;
using StudentInformationSystem.Security;

namespace StudentInformationSystem
{
    partial class MainForm
    {
        private void btnExport_Click(object? sender, EventArgs e)
        {
            try
            {
                // Export exactly what the grid shows (search and filters included).
                if (dgvStudents.DataSource is not DataTable table || table.DefaultView.Count == 0)
                {
                    MessageBox.Show("There are no students to export.",
                        "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using var dialog = new SaveFileDialog
                {
                    Title = "Export students to CSV",
                    Filter = "CSV file (*.csv)|*.csv",
                    FileName = $"students_{DateTime.Now:yyyyMMdd_HHmm}.csv",
                    OverwritePrompt = true
                };

                if (dialog.ShowDialog() != DialogResult.OK) return;

                int count = table.DefaultView.Count;
                CsvExporter.Export(table.DefaultView, dialog.FileName);

                // The file is already saved; a log failure must not turn this into an error.
                try
                {
                    ActivityLogRepository.Log(Session.CurrentUsername, "EXPORT", "");
                }
                catch (Exception)
                {
                    // ignore: the export itself worked
                }

                MessageBox.Show($"Exported {count} student(s) successfully.",
                    "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException)
            {
                MessageBox.Show("Could not save the file. It may be open in another program (like Excel). Close it and try again.",
                    "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("You don't have permission to save in that folder. Please choose another folder.",
                    "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong while exporting. Please try again.",
                    "Export CSV", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}