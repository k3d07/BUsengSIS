using System.Data;
using StudentInformationSystem.Data;
using StudentInformationSystem.Helpers;
using StudentInformationSystem.Models;
using StudentInformationSystem.Security;

namespace StudentInformationSystem
{
    partial class MainForm
    {
        private readonly StudentRepository _repo = new StudentRepository();
        private string _selectedStudentId = "";

        // Photo state
        private string _currentPhotoFile = "";     // file name already saved for the selected student
        private string _pendingPhotoSource = "";   // full path of a newly browsed image (not saved yet)

        // Called once from the MainForm constructor.
        private void InitLogic()
        {
            // The grid columns already exist in the designer: bind them by name.
            dgvStudents.AutoGenerateColumns = false;
            colStudentId.DataPropertyName = "student_id";
            colName.DataPropertyName = "name";
            colGender.DataPropertyName = "gender";
            colYear.DataPropertyName = "year_level";
            colProgram.DataPropertyName = "program";
            colContact.DataPropertyName = "contact_number";
            colEmail.DataPropertyName = "email";

            dgvStudents.ReadOnly = true;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.MultiSelect = false;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // The designer marks the ID box read-only; it must be editable when adding a student.
            txtStudentId.ReadOnly = false;
            txtStudentId.TabStop = true;

            // Buttons (handlers are in MainForm.Crud.cs, and btnBrowse_Click below)
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnBrowse.Click += btnBrowse_Click;

            // Grid, search and filters
            dgvStudents.CellClick += dgvStudents_CellClick;
            txtSearch.TextChanged += (s, e) => SafeRefresh();
            cboYearFilter.SelectedIndexChanged += (s, e) => SafeRefresh();
            cboProgramFilter.SelectedIndexChanged += (s, e) => SafeRefresh();

            Load += (s, e) => SafeRefresh();
        }

        // Reloads the grid, respecting the search box and both filters,
        // and updates the counter and dashboard cards.
        private void RefreshGrid()
        {
            string year = cboYearFilter.SelectedIndex > 0 ? cboYearFilter.Text : "";
            string program = cboProgramFilter.SelectedIndex > 0 ? cboProgramFilter.Text : "";

            DataTable table = _repo.FilterStudents(year, program, txtSearch.Text);
            dgvStudents.DataSource = table;

            var counts = _repo.GetStudentCounts();
            lblCount.Text = $"Showing {table.Rows.Count} of {counts["Total"]} students";

            statTotal.Value = counts["Total"].ToString();
            stat1st.Value = counts["1st Year"].ToString();
            stat2nd.Value = counts["2nd Year"].ToString();
            stat3rd.Value = counts["3rd Year"].ToString();
            stat4th.Value = counts["4th Year"].ToString();
        }

        // Used by search/filter events: never throws and never pops a message box
        // on every keystroke if MySQL is down.
        private void SafeRefresh()
        {
            try
            {
                RefreshGrid();
            }
            catch (Exception)
            {
                lblCount.Text = "Could not load students. Please check that MySQL is running.";
            }
        }

        private Student ReadInputs()
        {
            return new Student
            {
                StudentId = txtStudentId.Text,
                Name = txtName.Text,
                Gender = cboGender.SelectedItem?.ToString() ?? "",
                YearLevel = cboYear.SelectedItem?.ToString() ?? "",
                Program = cboProgram.SelectedItem?.ToString() ?? "",
                ContactNumber = txtContact.Text,
                Email = txtEmail.Text,
                PhotoPath = string.IsNullOrEmpty(_currentPhotoFile) ? null : _currentPhotoFile
            };
        }

        private void ClearInputs()
        {
            txtStudentId.Clear();
            txtName.Clear();
            txtContact.Clear();
            txtEmail.Clear();
            cboGender.SelectedIndex = -1;
            cboYear.SelectedIndex = -1;
            cboProgram.SelectedIndex = -1;

            _selectedStudentId = "";
            ClearPhoto();

            txtStudentId.ReadOnly = false;
            dgvStudents.ClearSelection();
            txtStudentId.Focus();
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvStudents.Rows[e.RowIndex].DataBoundItem is not DataRowView row) return;

            _selectedStudentId = Str(row["student_id"]);
            txtStudentId.Text = _selectedStudentId;
            txtStudentId.ReadOnly = true;          // the ID is locked while editing
            txtName.Text = Str(row["name"]);
            cboGender.SelectedItem = Str(row["gender"]);
            cboYear.SelectedItem = Str(row["year_level"]);
            cboProgram.SelectedItem = Str(row["program"]);
            txtContact.Text = Str(row["contact_number"]);
            txtEmail.Text = Str(row["email"]);

            _pendingPhotoSource = "";
            _currentPhotoFile = Str(row["photo_path"]);
            ShowPhoto(PhotoStore.Load(_currentPhotoFile));
        }

        // ----- Photo -----

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                using var dialog = new OpenFileDialog
                {
                    Title = "Choose a student photo",
                    Filter = "Images (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png"
                };

                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                if (!PhotoStore.IsAllowed(dialog.FileName, out string error))
                {
                    MessageBox.Show(error, "Photo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Image? preview = PhotoStore.LoadFile(dialog.FileName);
                if (preview == null)
                {
                    MessageBox.Show("That image could not be opened. Please try another one.",
                        "Photo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _pendingPhotoSource = dialog.FileName;
                ShowPhoto(preview);
            }
            catch (Exception)
            {
                MessageBox.Show("Could not open the photo. Please try another image.",
                    "Photo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Copies a newly browsed photo into the photos folder. Returns the file name to store,
        // the existing name if no new photo was chosen, or null if the student has no photo.
        private string? SaveSelectedPhoto(string studentId)
        {
            if (!string.IsNullOrEmpty(_pendingPhotoSource))
                return PhotoStore.Save(_pendingPhotoSource, studentId);

            return string.IsNullOrEmpty(_currentPhotoFile) ? null : _currentPhotoFile;
        }

        private void ShowPhoto(Image? image)
        {
            picPhoto.Image?.Dispose();
            picPhoto.Image = image;
            lblNoPhoto.Visible = image == null;
        }

        private void ClearPhoto()
        {
            _currentPhotoFile = "";
            _pendingPhotoSource = "";
            ShowPhoto(null);
        }

        // ----- Helpers -----

        // Logging must never break the app or stop a save from succeeding.
        private static void TryLog(string action, string studentId)
        {
            try
            {
                ActivityLogRepository.Log(Session.CurrentUsername ?? "unknown", action, studentId);
            }
            catch (Exception)
            {
            }
        }

        private static string Str(object value)
        {
            return value == null || value == DBNull.Value ? "" : value.ToString() ?? "";
        }
    }
}