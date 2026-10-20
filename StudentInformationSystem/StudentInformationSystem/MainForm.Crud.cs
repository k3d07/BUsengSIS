using MySqlConnector;
using StudentInformationSystem.Helpers;
using StudentInformationSystem.Models;

namespace StudentInformationSystem
{
    partial class MainForm
    {
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Student s = ReadInputs();

                if (!Validator.IsValidStudent(s, out string error))
                {
                    MessageBox.Show(error, "Please check your input",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_repo.StudentExists(s.StudentId))
                {
                    MessageBox.Show($"A student with ID {s.StudentId} already exists.",
                        "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_repo.AddStudent(s))
                {
                    // TODO (Keith): ActivityLogRepository.Log(<current user>, "ADD", s.StudentId);
                    ClearInputs();
                    RefreshGrid();
                    MessageBox.Show("Student added.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("The student could not be added. Please try again.",
                        "Add failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062)
            {
                MessageBox.Show("A student with that ID already exists.",
                    "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (MySqlException)
            {
                MessageBox.Show(
                    "Could not reach the database. Please check that MySQL is running and try again.",
                    "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong. Please try again.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}