using System.Data;
using StudentInformationSystem.Data;
using StudentInformationSystem.Models;

namespace StudentInformationSystem
{
    // TEMPORARY SCRATCH FORM. NEVER COMMIT. Delete when Sebastian's MainForm arrives.
    public partial class MainForm : Form
    {
        private readonly StudentRepository _repo = new StudentRepository();
        private string _selectedStudentId = "";

        private readonly TextBox txtStudentId = new TextBox();
        private readonly TextBox txtName = new TextBox();
        private readonly ComboBox cmbGender = new ComboBox();
        private readonly ComboBox cmbYearLevel = new ComboBox();
        private readonly ComboBox cmbProgram = new ComboBox();
        private readonly TextBox txtContact = new TextBox();
        private readonly TextBox txtEmail = new TextBox();
        private readonly DataGridView dgvStudents = new DataGridView();
        private readonly Button btnAdd = new Button { Text = "Add" };
        private readonly Button btnUpdate = new Button { Text = "Update" };
        private readonly Button btnDelete = new Button { Text = "Delete" };
        private readonly Button btnClear = new Button { Text = "Clear" };

        public MainForm()
        {
            Text = "SCRATCH TEST FORM (do not commit)";
            Width = 950;
            Height = 560;

            foreach (var c in new[] { cmbGender, cmbYearLevel, cmbProgram })
                c.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
            cmbYearLevel.Items.AddRange(new object[]
                { "1st Year", "2nd Year", "3rd Year", "4th Year" });
            cmbProgram.Items.AddRange(new object[]
            {
                "BS Computer Science", "BS Information Technology",
                "BS Information Systems", "BS Computer Engineering"
            });

            int y = 10;
            AddRow("Student ID", txtStudentId, ref y);
            AddRow("Name", txtName, ref y);
            AddRow("Gender", cmbGender, ref y);
            AddRow("Year Level", cmbYearLevel, ref y);
            AddRow("Program", cmbProgram, ref y);
            AddRow("Contact", txtContact, ref y);
            AddRow("Email", txtEmail, ref y);

            btnAdd.SetBounds(10, y + 10, 80, 30);
            btnUpdate.SetBounds(95, y + 10, 80, 30);
            btnDelete.SetBounds(180, y + 10, 80, 30);
            btnClear.SetBounds(265, y + 10, 80, 30);
            Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete, btnClear });

            dgvStudents.SetBounds(370, 10, 550, 490);
            dgvStudents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                               | AnchorStyles.Left | AnchorStyles.Right;
            dgvStudents.ReadOnly = true;
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.MultiSelect = false;
            dgvStudents.CellClick += dgvStudents_CellClick;
            Controls.Add(dgvStudents);

            btnClear.Click += (s, e) => ClearInputs();
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;

            Load += (s, e) =>
            {
                try { RefreshGrid(); }
                catch (Exception) { MessageBox.Show("Could not load students (is MySQL running?)."); }
            };
        }

        private void AddRow(string label, Control c, ref int y)
        {
            Controls.Add(new Label { Text = label, Left = 10, Top = y + 4, Width = 110 });
            c.SetBounds(125, y, 220, 24);
            Controls.Add(c);
            y += 32;
        }

        private Student ReadInputs()
        {
            return new Student
            {
                StudentId = txtStudentId.Text,
                Name = txtName.Text,
                Gender = cmbGender.SelectedItem?.ToString() ?? "",
                YearLevel = cmbYearLevel.SelectedItem?.ToString() ?? "",
                Program = cmbProgram.SelectedItem?.ToString() ?? "",
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
            cmbGender.SelectedIndex = -1;
            cmbYearLevel.SelectedIndex = -1;
            cmbProgram.SelectedIndex = -1;
            _selectedStudentId = "";
            txtStudentId.ReadOnly = false;
            dgvStudents.ClearSelection();
        }

        private void RefreshGrid()
        {
            dgvStudents.DataSource = _repo.GetAllStudents();
        }

        private void dgvStudents_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvStudents.Rows[e.RowIndex];

            _selectedStudentId = row.Cells["student_id"].Value?.ToString() ?? "";
            txtStudentId.Text = _selectedStudentId;
            txtStudentId.ReadOnly = true;
            txtName.Text = row.Cells["name"].Value?.ToString() ?? "";
            cmbGender.SelectedItem = row.Cells["gender"].Value?.ToString();
            cmbYearLevel.SelectedItem = row.Cells["year_level"].Value?.ToString();
            cmbProgram.SelectedItem = row.Cells["program"].Value?.ToString();
            txtContact.Text = row.Cells["contact_number"].Value?.ToString() ?? "";
            txtEmail.Text = row.Cells["email"].Value?.ToString() ?? "";
        }
    }
}