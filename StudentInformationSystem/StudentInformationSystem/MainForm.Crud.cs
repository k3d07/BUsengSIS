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

                s.PhotoPath = SaveSelectedPhoto(s.StudentId);

                if (_repo.AddStudent(s))
                {
                    TryLog("ADD", s.StudentId);
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
            catch (IOException)
            {
                MessageBox.Show("The photo could not be saved. Please try another image.",
                    "Photo error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong. Please try again.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_selectedStudentId))
                {
                    MessageBox.Show("Please select a student from the list first.",
                        "No student selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Student s = ReadInputs();
                s.StudentId = _selectedStudentId;   // the ID never changes on update

                if (!Validator.IsValidStudent(s, out string error))
                {
                    MessageBox.Show(error, "Please check your input",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                s.PhotoPath = SaveSelectedPhoto(s.StudentId);

                if (_repo.UpdateStudent(s))
                {
                    TryLog("EDIT", s.StudentId);
                    ClearInputs();
                    RefreshGrid();
                    MessageBox.Show("Student updated.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "No changes were saved. The student may have been removed.",
                        "Update failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RefreshGrid();
                }
            }
            catch (MySqlException)
            {
                MessageBox.Show(
                    "Could not reach the database. Please check that MySQL is running and try again.",
                    "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException)
            {
                MessageBox.Show("The photo could not be saved. Please try another image.",
                    "Photo error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong. Please try again.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_selectedStudentId))
                {
                    MessageBox.Show("Please select a student from the list first.",
                        "No student selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DialogResult answer = MessageBox.Show(
                    $"Are you sure you want to delete student {_selectedStudentId}?",
                    "Confirm delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);   // default is No

                if (answer != DialogResult.Yes)
                    return;

                string id = _selectedStudentId;

                if (_repo.DeleteStudent(id))
                {
                    TryLog("DELETE", id);
                    ClearInputs();
                    RefreshGrid();
                    MessageBox.Show("Student deleted.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(
                        "The student could not be deleted. They may already have been removed.",
                        "Delete failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    RefreshGrid();
                }
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