using System.Data;
using StudentInformationSystem.Data;
using StudentInformationSystem.Helpers;
using StudentInformationSystem.Models;
using StudentInformationSystem.Security;
using System.Drawing.Drawing2D;

namespace StudentInformationSystem
{
    partial class MainForm
    {
        private readonly StudentRepository _repo = new StudentRepository();
        private string _selectedStudentId = "";
        private readonly TextBox _firstName = new();
        private readonly TextBox _middleName = new();
        private readonly TextBox _lastName = new();
        private string _selectedFullName = "";
        private string _selectedNameFromParts = "";

        // Photo state
        private string _currentPhotoFile = "";     // file name already saved for the selected student
        private string _pendingPhotoSource = "";   // full path of a newly browsed image (not saved yet)
        
        // Grid thumbnails
        private readonly Dictionary<string, Image> _thumbCache = new();
        private readonly HashSet<string> _reportedPhotoLoadErrors = new(StringComparer.OrdinalIgnoreCase);
        private Image? _avatarThumb;   // default avatar thumbnail for students without a photo

        private void ApplyDetailsLayout()
        {
            pnlDetails.SuspendLayout();
            pnlDetails.Controls.Clear();
            pnlDetails.Padding = new Padding(20);
            pnlDetails.Width = 440;
            pnlDetails.AutoScroll = true;

            const int detailsWidthIncrease = 120;
            pnlGrid.Left += detailsWidthIncrease;
            pnlGrid.Width -= detailsWidthIncrease;
            txtSearch.Left += detailsWidthIncrease;
            txtSearch.Width -= detailsWidthIncrease;
            lblCount.Left += detailsWidthIncrease;

            ConfigureNameBox(_firstName, "First name", "First name");
            ConfigureNameBox(_middleName, "Middle (optional)", "Middle name (optional)");
            ConfigureNameBox(_lastName, "Last name", "Last name");

            txtName.Dispose();
            lblName.Dispose();
            lblPhoto.Dispose();

            lblDetailsTitle.Text = "Student Details";
            lblStudentId.Text = "Student ID";
            lblGender.Text = "Gender";
            lblYear.Text = "Year Level";
            lblProgram.Text = "Program";
            lblContact.Text = "Contact Number";
            lblEmail.Text = "Email (optional)";

            foreach (Control input in new Control[]
            {
                txtStudentId, _firstName, _middleName, _lastName,
                cboGender, cboYear, cboProgram, txtContact, txtEmail
            })
            {
                input.Font = new Font("Segoe UI", 9.75f);
            }

            foreach (ComboBox combo in new[] { cboGender, cboYear, cboProgram })
            {
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
                combo.IntegralHeight = false;
                combo.Font = txtStudentId.Font;
                combo.Height = 32;
                combo.MinimumSize = new Size(0, 32);
            }

            txtStudentId.AutoSize = false;
            txtContact.AutoSize = false;
            txtEmail.AutoSize = false;
            _firstName.AutoSize = false;
            _middleName.AutoSize = false;
            _lastName.AutoSize = false;
            foreach (TextBox textBox in new[] { txtStudentId, txtContact, txtEmail, _firstName, _middleName, _lastName })
                textBox.Height = 32;

            pnlPhoto.Size = new Size(96, 96);
            pnlPhoto.Margin = new Padding(0, 0, 0, 6);
            picPhoto.Dock = DockStyle.Fill;
            btnBrowse.Size = new Size(96, 30);
            btnBrowse.Dock = DockStyle.Top;
            btnBrowse.Margin = Padding.Empty;

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 7,
                Margin = Padding.Empty,
                Padding = Padding.Empty
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            for (int row = 0; row < root.RowCount; row++)
                root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            root.Controls.Add(lblDetailsTitle, 0, 0);
            lblDetailsTitle.AutoSize = true;
            lblDetailsTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblDetailsTitle.Margin = new Padding(0, 0, 0, 6);

            var photo = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                AutoSize = true,
                Margin = Padding.Empty
            };
            photo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 112));
            photo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            var photoLeft = new TableLayoutPanel
            {
                AutoSize = true,
                ColumnCount = 1,
                RowCount = 2,
                Margin = Padding.Empty
            };
            photoLeft.Controls.Add(pnlPhoto, 0, 0);
            photoLeft.Controls.Add(btnBrowse, 0, 1);

            var photoRight = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                AutoSize = true,
                Margin = Padding.Empty
            };
            photoRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            photoRight.Controls.Add(Field(lblStudentId, txtStudentId), 0, 0);
            photoRight.Controls.Add(Field(lblGender, cboGender), 0, 1);
            photo.Controls.Add(photoLeft, 0, 0);
            photo.Controls.Add(photoRight, 1, 0);
            root.Controls.Add(photo, 0, 1);

            var names = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 3,
                RowCount = 2,
                AutoSize = false,
                Height = 68,
                Margin = new Padding(0, 0, 0, 4)
            };
            names.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            names.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
            names.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));
            names.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36));
            names.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32));

            var firstCaption = NameCaption("First name");
            var middleCaption = NameCaption("Middle name\n(optional)");
            var lastCaption = NameCaption("Last name");
            firstCaption.TextAlign = ContentAlignment.TopLeft;
            middleCaption.TextAlign = ContentAlignment.TopLeft;
            lastCaption.TextAlign = ContentAlignment.TopLeft;

            _firstName.Dock = DockStyle.Fill;
            _middleName.Dock = DockStyle.Fill;
            _lastName.Dock = DockStyle.Fill;
            _firstName.Margin = new Padding(0, 0, 8, 0);
            _middleName.Margin = new Padding(0, 0, 8, 0);
            _lastName.Margin = Padding.Empty;

            names.Controls.Add(firstCaption, 0, 0);
            names.Controls.Add(middleCaption, 1, 0);
            names.Controls.Add(lastCaption, 2, 0);
            names.Controls.Add(_firstName, 0, 1);
            names.Controls.Add(_middleName, 1, 1);
            names.Controls.Add(_lastName, 2, 1);
            root.Controls.Add(names, 0, 2);

            var yearProgram = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                AutoSize = true,
                Margin = Padding.Empty
            };
            yearProgram.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42));
            yearProgram.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58));
            yearProgram.Controls.Add(Field(lblYear, cboYear, 8), 0, 0);
            yearProgram.Controls.Add(Field(lblProgram, cboProgram), 1, 0);
            root.Controls.Add(yearProgram, 0, 3);

            root.Controls.Add(Field(lblContact, txtContact), 0, 4);
            root.Controls.Add(Field(lblEmail, txtEmail), 0, 5);

            var buttons = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                AutoSize = true,
                Margin = Padding.Empty
            };
            buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            foreach (Button button in new[] { btnAdd, btnUpdate, btnDelete, btnClear })
            {
                button.Dock = DockStyle.Fill;
                button.Height = 38;
                button.Margin = new Padding(0, 0, 8, 4);
            }
            buttons.Controls.Add(btnAdd, 0, 0);
            buttons.Controls.Add(btnUpdate, 1, 0);
            buttons.Controls.Add(btnDelete, 0, 1);
            buttons.Controls.Add(btnClear, 1, 1);
            root.Controls.Add(buttons, 0, 6);

            pnlDetails.Controls.Add(root);
            pnlDetails.ResumeLayout(true);
        }

        private static void ConfigureNameBox(TextBox textBox, string placeholder, string accessibleName)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = new Font("Segoe UI", 9.75f);
            textBox.AccessibleName = accessibleName;
            textBox.TabStop = true;
            NativeMethods.SetPlaceholder(textBox, placeholder);
        }

        private static Control Field(string caption, Control input, int rightGap = 0)
        {
            var wrapper = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                AutoSize = true,
                Margin = new Padding(0, 0, rightGap, 4)
            };
            wrapper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            wrapper.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            wrapper.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            wrapper.Controls.Add(new Label
            {
                Text = caption,
                AutoSize = true,
                ForeColor = ColorTranslator.FromHtml("#656D76"),
                Font = new Font("Segoe UI", 9F),
                Margin = new Padding(0, 0, 0, 4)
            }, 0, 0);
            input.Dock = DockStyle.Top;
            input.Margin = Padding.Empty;
            wrapper.Controls.Add(input, 0, 1);
            return wrapper;
        }

        private static Control Field(Label caption, Control input, int rightGap = 0)
        {
            caption.AutoSize = true;
            caption.ForeColor = ColorTranslator.FromHtml("#656D76");
            caption.Font = new Font("Segoe UI", 9F);
            caption.Margin = new Padding(0, 0, 0, 4);

            var wrapper = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                AutoSize = true,
                Margin = new Padding(0, 0, rightGap, 4)
            };
            wrapper.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            wrapper.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            wrapper.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            input.Dock = DockStyle.Top;
            input.Margin = Padding.Empty;
            wrapper.Controls.Add(caption, 0, 0);
            wrapper.Controls.Add(input, 0, 1);
            return wrapper;
        }

        private static Label NameCaption(string text) => new()
        {
            Text = text,
            Dock = DockStyle.Fill,
            AutoSize = false,
            ForeColor = ColorTranslator.FromHtml("#656D76"),
            Font = new Font("Segoe UI", 9F),
            Margin = new Padding(0, 0, 8, 0)
        };

        private void LoadNameParts(string fullName)
        {
            _selectedFullName = fullName;
            string[] parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            _firstName.Text = parts.Length > 0 ? parts[0] : "";
            _middleName.Text = parts.Length > 2 ? string.Join(" ", parts.Skip(1).Take(parts.Length - 2)) : "";
            _lastName.Text = parts.Length > 1 ? parts[^1] : "";
            _selectedNameFromParts = ComposeNameParts();
        }

        private string GetNameForStorage()
        {
            string composed = ComposeNameParts();
            if (_selectedFullName.Length > 0 &&
                string.Equals(composed, _selectedNameFromParts, StringComparison.Ordinal))
                return _selectedFullName;

            return composed;
        }

        private string ComposeNameParts() => string.Join(" ", new[]
        {
            _firstName.Text.Trim(),
            _middleName.Text.Trim(),
            _lastName.Text.Trim()
        }.Where(part => part.Length > 0));

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
            // Photo thumbnail column (added in code, so the Designer files stay untouched)
            var colPhoto = new DataGridViewImageColumn
            {
                Name = "colPhoto",
                HeaderText = "Photo",
                Width = 56,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                ImageLayout = DataGridViewImageCellLayout.Zoom,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Resizable = DataGridViewTriState.False
            };
            dgvStudents.Columns.Insert(0, colPhoto);
            dgvStudents.RowTemplate.Height = 46;
            dgvStudents.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvStudents.CellFormatting += dgvStudents_CellFormatting;

            // The designer marks the ID box read-only; it must be editable when adding a student.
            txtStudentId.ReadOnly = false;
            txtStudentId.TabStop = true;

            // Buttons (handlers are in MainForm.Crud.cs, and btnBrowse_Click below)
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnBrowse.Click += btnBrowse_Click;
            btnExport.Click += btnExport_Click;

            // Grid, search and filters
            dgvStudents.CellClick += dgvStudents_CellClick;
            txtSearch.TextChanged += (s, e) => SafeRefresh();
            cboYearFilter.SelectedIndexChanged += (s, e) => SafeRefresh();
            cboProgramFilter.SelectedIndexChanged += (s, e) => SafeRefresh();

            Load += (s, e) => SafeRefresh();
            ShowPhoto(null);
        }

        // Reloads the grid, respecting the search box and both filters,
        // and updates the counter and dashboard cards.
        private void RefreshGrid()
        {
            _thumbCache.Clear();   // so a replaced photo shows up after Add/Update
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

        // Fills the photo cell for each row. Never throws.
        private void dgvStudents_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || dgvStudents.Columns[e.ColumnIndex].Name != "colPhoto") return;

            try
            {
                _avatarThumb ??= MakeAvatarThumb();
                e.Value = _avatarThumb;
                e.FormattingApplied = true;

                if (dgvStudents.Rows[e.RowIndex].DataBoundItem is not DataRowView row) return;

                string file = row["photo_path"] as string ?? "";
                if (string.IsNullOrWhiteSpace(file)) return;

                if (!_thumbCache.TryGetValue(file, out Image? thumb))
                {
                    using Image? full = PhotoStore.Load(file);
                    if (full == null) return;

                    thumb = MakeThumbnail(full, 40);
                    _thumbCache[file] = thumb;
                }

                e.Value = thumb;
            }
            catch (Exception ex)
            {
                e.Value = _avatarThumb;
                e.FormattingApplied = true;
                string photoPath = dgvStudents.Rows[e.RowIndex].DataBoundItem is DataRowView row
                    ? row["photo_path"] as string ?? ""
                    : "unknown photo";
                if (_reportedPhotoLoadErrors.Add(photoPath))
                    ShowPhotoError($"Could not load a student photo: {ex.Message}");
            }
        }

        // Makes a small square thumbnail (center-cropped) so the grid stays fast.
        private static Image MakeThumbnail(Image source, int size)
        {
            int side = Math.Min(source.Width, source.Height);
            var sourceRect = new Rectangle((source.Width - side) / 2, (source.Height - side) / 2, side, side);

            var thumb = new Bitmap(size, size);
            using var g = Graphics.FromImage(thumb);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(source, new Rectangle(0, 0, size, size), sourceRect, GraphicsUnit.Pixel);
            return thumb;
        }

        private static Image MakeAvatarThumb()
        {
            using Image avatar = DefaultAvatar.Get();
            return MakeThumbnail(avatar, 40);
        }

        private Student ReadInputs()
        {
            return new Student
            {
                StudentId = txtStudentId.Text,
                Name = GetNameForStorage(),
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
            _firstName.Clear();
            _middleName.Clear();
            _lastName.Clear();
            _selectedFullName = "";
            _selectedNameFromParts = "";
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
            LoadNameParts(Str(row["name"]));
            cboGender.SelectedItem = Str(row["gender"]);
            cboYear.SelectedItem = Str(row["year_level"]);
            cboProgram.SelectedItem = Str(row["program"]);
            txtContact.Text = Str(row["contact_number"]);
            txtEmail.Text = Str(row["email"]);

            _pendingPhotoSource = "";
            _currentPhotoFile = Str(row["photo_path"]);
            try
            {
                ShowPhoto(PhotoStore.Load(_currentPhotoFile));
            }
            catch (Exception ex)
            {
                ShowPhoto(null);
                ShowPhotoError($"Could not load the selected student's photo: {ex.Message}");
            }
        }

        // ----- Photo -----

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            Image? preview = null;
            try
            {
                using var dialog = new OpenFileDialog
                {
                    Title = "Choose a student photo",
                    Filter = "Images (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
                };

                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                if (!PhotoStore.IsAllowed(dialog.FileName, out string error))
                {
                    MessageBox.Show(error, "Photo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                preview = PhotoStore.LoadFile(dialog.FileName);
                string studentId = txtStudentId.Text.Trim();
                if (studentId.Length > 0)
                {
                    string copiedPath = PhotoStore.Save(dialog.FileName, studentId);
                    _pendingPhotoSource = copiedPath;
                    _currentPhotoFile = copiedPath;
                }
                else
                {
                    // The file name depends on the Student ID; defer the copy until save if the ID is not entered yet.
                    _pendingPhotoSource = dialog.FileName;
                }

                ShowPhoto(preview);
                preview = null;
            }
            catch (Exception ex)
            {
                preview?.Dispose();
                ShowPhotoError($"Could not prepare the photo: {ex.Message}");
            }
        }

        // Copies a newly browsed photo and returns its absolute path for photo_path.
        private bool TrySaveSelectedPhoto(string studentId, out string? photoPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(_pendingPhotoSource))
                    photoPath = PhotoStore.Save(_pendingPhotoSource, studentId);
                else if (!string.IsNullOrWhiteSpace(_currentPhotoFile) &&
                         Path.IsPathRooted(_currentPhotoFile) && File.Exists(_currentPhotoFile))
                {
                    string currentFolder = Path.GetDirectoryName(Path.GetFullPath(_currentPhotoFile)) ?? string.Empty;
                    string managedFolder = Path.GetFullPath(PhotoStore.GetPhotosFolder());
                    photoPath = string.Equals(currentFolder, managedFolder, StringComparison.OrdinalIgnoreCase)
                        ? _currentPhotoFile
                        : PhotoStore.Save(_currentPhotoFile, studentId);
                }
                else
                    photoPath = null;

                return true;
            }
            catch (Exception ex)
            {
                photoPath = null;
                ShowPhotoError($"Could not save the photo: {ex.Message}");
                return false;
            }
        }

        private void ShowPhoto(Image? image)
        {
            try
            {
                Image next = image ?? DefaultAvatar.Get();
                Image? previous = picPhoto.Image;
                picPhoto.Image = next;
                previous?.Dispose();
                lblNoPhoto.Visible = false;   // the avatar replaces the "No photo" text
            }
            catch (Exception ex)
            {
                image?.Dispose();
                ShowPhotoError($"Could not display the photo: {ex.Message}");
                try
                {
                    picPhoto.Image = DefaultAvatar.Get();
                    lblNoPhoto.Visible = false;
                }
                catch (Exception fallbackException)
                {
                    ShowPhotoError($"Could not display the default photo: {fallbackException.Message}");
                }
            }
        }

        private void ShowPhotoError(string message) =>
            MessageBox.Show(message, "Photo error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
