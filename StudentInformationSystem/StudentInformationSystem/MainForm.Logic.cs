using System.Data;
using StudentInformationSystem.Data;
using StudentInformationSystem.Models;

namespace StudentInformationSystem
{
    partial class MainForm
    {
        private readonly StudentRepository _repo = new StudentRepository();
        private string _selectedStudentId = "";

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
            txtStudentId.ReadOnly = false;
            txtStudentId.TabStop = true;

            // Buttons (handlers are in MainForm.Crud.cs)
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;

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

        // Used by search/filter events: never throws, never pops a message box
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
                Email = txtEmail.Text
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
        }

        private static string Str(object value)
        {
            return value == null || value == DBNull.Value ? "" : value.ToString() ?? "";
        }
    }
}