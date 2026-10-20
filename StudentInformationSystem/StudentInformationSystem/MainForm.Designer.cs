namespace StudentInformationSystem
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.clockTimer = new System.Windows.Forms.Timer(this.components);
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.logoMain = new StudentInformationSystem.LogoBox();
            this.lblAppTitle = new System.Windows.Forms.Label();
            this.btnViewLog = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.tlpStats = new System.Windows.Forms.TableLayoutPanel();
            this.statTotal = new StudentInformationSystem.StatCard();
            this.stat1st = new StudentInformationSystem.StatCard();
            this.stat2nd = new StudentInformationSystem.StatCard();
            this.stat3rd = new StudentInformationSystem.StatCard();
            this.stat4th = new StudentInformationSystem.StatCard();
            this.pnlStatsLine = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlDetails = new StudentInformationSystem.BorderPanel();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.lblStudentId = new System.Windows.Forms.Label();
            this.txtStudentId = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPhoto = new System.Windows.Forms.Label();
            this.pnlPhoto = new StudentInformationSystem.BorderPanel();
            this.picPhoto = new System.Windows.Forms.PictureBox();
            this.lblNoPhoto = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblGender = new System.Windows.Forms.Label();
            this.cboGender = new System.Windows.Forms.ComboBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.lblProgram = new System.Windows.Forms.Label();
            this.cboProgram = new System.Windows.Forms.ComboBox();
            this.lblContact = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.cboYearFilter = new System.Windows.Forms.ComboBox();
            this.cboProgramFilter = new System.Windows.Forms.ComboBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.pnlGrid = new StudentInformationSystem.BorderPanel();
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.colStudentId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProgram = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.pnlStatus = new System.Windows.Forms.Panel();
            this.pnlStatusLine = new System.Windows.Forms.Panel();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblClock = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.tlpStats.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlDetails.SuspendLayout();
            this.pnlPhoto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).BeginInit();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.pnlStatus.SuspendLayout();
            this.SuspendLayout();
            //
            // clockTimer
            //
            this.clockTimer.Interval = 1000;
            this.clockTimer.Tick += new System.EventHandler(this.clockTimer_Tick);
            //
            // pnlHeader
            //
            this.pnlHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.pnlHeader.Controls.Add(this.logoMain);
            this.pnlHeader.Controls.Add(this.lblAppTitle);
            this.pnlHeader.Controls.Add(this.btnViewLog);
            this.pnlHeader.Controls.Add(this.btnLogout);
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 48);
            //
            // logoMain
            //
            this.logoMain.BorderWidth = 1;
            this.logoMain.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.logoMain.Letter = "B";
            this.logoMain.Location = new System.Drawing.Point(16, 10);
            this.logoMain.Name = "logoMain";
            this.logoMain.Size = new System.Drawing.Size(28, 28);
            this.logoMain.TabStop = false;
            //
            // lblAppTitle
            //
            this.lblAppTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lblAppTitle.ForeColor = System.Drawing.Color.White;
            this.lblAppTitle.Location = new System.Drawing.Point(54, 0);
            this.lblAppTitle.Name = "lblAppTitle";
            this.lblAppTitle.Size = new System.Drawing.Size(400, 48);
            this.lblAppTitle.Text = "BUseng SIS";
            this.lblAppTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnViewLog  (ADMIN ONLY - removed from the form for non-admin logins in MainForm.cs)
            //
            this.btnViewLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewLog.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnViewLog.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(143, 163, 189);
            this.btnViewLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewLog.ForeColor = System.Drawing.Color.White;
            this.btnViewLog.Location = new System.Drawing.Point(1016, 9);
            this.btnViewLog.Name = "btnViewLog";
            this.btnViewLog.Size = new System.Drawing.Size(84, 30);
            this.btnViewLog.TabIndex = 0;
            this.btnViewLog.Text = "View Log";
            this.btnViewLog.UseVisualStyleBackColor = false;
            this.btnViewLog.Click += new System.EventHandler(this.btnViewLog_Click);
            //
            // btnLogout
            //
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnLogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(143, 163, 189);
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(1112, 9);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(72, 30);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            //
            // tlpStats
            //
            this.tlpStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpStats.BackColor = System.Drawing.Color.White;
            this.tlpStats.ColumnCount = 5;
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpStats.Controls.Add(this.statTotal, 0, 0);
            this.tlpStats.Controls.Add(this.stat1st, 1, 0);
            this.tlpStats.Controls.Add(this.stat2nd, 2, 0);
            this.tlpStats.Controls.Add(this.stat3rd, 3, 0);
            this.tlpStats.Controls.Add(this.stat4th, 4, 0);
            this.tlpStats.Location = new System.Drawing.Point(0, 48);
            this.tlpStats.Margin = new System.Windows.Forms.Padding(0);
            this.tlpStats.Name = "tlpStats";
            this.tlpStats.RowCount = 1;
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStats.Size = new System.Drawing.Size(1200, 55);
            //
            // statTotal
            //
            this.statTotal.Caption = "Total students";
            this.statTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statTotal.Margin = new System.Windows.Forms.Padding(0);
            this.statTotal.Name = "statTotal";
            this.statTotal.ShowLeftBorder = false;
            this.statTotal.Value = "0";
            //
            // stat1st
            //
            this.stat1st.Caption = "1st Year";
            this.stat1st.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stat1st.Margin = new System.Windows.Forms.Padding(0);
            this.stat1st.Name = "stat1st";
            this.stat1st.Value = "0";
            //
            // stat2nd
            //
            this.stat2nd.Caption = "2nd Year";
            this.stat2nd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stat2nd.Margin = new System.Windows.Forms.Padding(0);
            this.stat2nd.Name = "stat2nd";
            this.stat2nd.Value = "0";
            //
            // stat3rd
            //
            this.stat3rd.Caption = "3rd Year";
            this.stat3rd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stat3rd.Margin = new System.Windows.Forms.Padding(0);
            this.stat3rd.Name = "stat3rd";
            this.stat3rd.Value = "0";
            //
            // stat4th
            //
            this.stat4th.Caption = "4th Year";
            this.stat4th.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stat4th.Margin = new System.Windows.Forms.Padding(0);
            this.stat4th.Name = "stat4th";
            this.stat4th.Value = "0";
            //
            // pnlStatsLine
            //
            this.pnlStatsLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStatsLine.BackColor = System.Drawing.Color.FromArgb(213, 218, 224);
            this.pnlStatsLine.Location = new System.Drawing.Point(0, 103);
            this.pnlStatsLine.Name = "pnlStatsLine";
            this.pnlStatsLine.Size = new System.Drawing.Size(1200, 1);
            //
            // pnlContent
            //
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.Controls.Add(this.pnlDetails);
            this.pnlContent.Controls.Add(this.txtSearch);
            this.pnlContent.Controls.Add(this.cboYearFilter);
            this.pnlContent.Controls.Add(this.cboProgramFilter);
            this.pnlContent.Controls.Add(this.btnReset);
            this.pnlContent.Controls.Add(this.pnlGrid);
            this.pnlContent.Controls.Add(this.lblCount);
            this.pnlContent.Controls.Add(this.btnExport);
            this.pnlContent.Location = new System.Drawing.Point(0, 104);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1200, 540);
            //
            // pnlDetails
            //
            this.pnlDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlDetails.BackColor = System.Drawing.Color.White;
            this.pnlDetails.Controls.Add(this.lblDetailsTitle);
            this.pnlDetails.Controls.Add(this.lblStudentId);
            this.pnlDetails.Controls.Add(this.txtStudentId);
            this.pnlDetails.Controls.Add(this.lblName);
            this.pnlDetails.Controls.Add(this.txtName);
            this.pnlDetails.Controls.Add(this.lblPhoto);
            this.pnlDetails.Controls.Add(this.pnlPhoto);
            this.pnlDetails.Controls.Add(this.btnBrowse);
            this.pnlDetails.Controls.Add(this.lblGender);
            this.pnlDetails.Controls.Add(this.cboGender);
            this.pnlDetails.Controls.Add(this.lblYear);
            this.pnlDetails.Controls.Add(this.cboYear);
            this.pnlDetails.Controls.Add(this.lblProgram);
            this.pnlDetails.Controls.Add(this.cboProgram);
            this.pnlDetails.Controls.Add(this.lblContact);
            this.pnlDetails.Controls.Add(this.txtContact);
            this.pnlDetails.Controls.Add(this.lblEmail);
            this.pnlDetails.Controls.Add(this.txtEmail);
            this.pnlDetails.Controls.Add(this.btnAdd);
            this.pnlDetails.Controls.Add(this.btnUpdate);
            this.pnlDetails.Controls.Add(this.btnDelete);
            this.pnlDetails.Controls.Add(this.btnClear);
            this.pnlDetails.Location = new System.Drawing.Point(16, 16);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Size = new System.Drawing.Size(320, 508);
            //
            // lblDetailsTitle
            //
            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 10.5F);
            this.lblDetailsTitle.Location = new System.Drawing.Point(16, 14);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Text = "Student Details";
            //
            // lblStudentId
            //
            this.lblStudentId.AutoSize = true;
            this.lblStudentId.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStudentId.ForeColor = System.Drawing.Color.FromArgb(61, 75, 90);
            this.lblStudentId.Location = new System.Drawing.Point(16, 44);
            this.lblStudentId.Name = "lblStudentId";
            this.lblStudentId.Text = "Student ID";
            //
            // txtStudentId
            //
            this.txtStudentId.BackColor = System.Drawing.Color.FromArgb(238, 241, 245);
            this.txtStudentId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStudentId.ForeColor = System.Drawing.Color.FromArgb(91, 102, 115);
            this.txtStudentId.Location = new System.Drawing.Point(16, 62);
            this.txtStudentId.Name = "txtStudentId";
            this.txtStudentId.ReadOnly = true;
            this.txtStudentId.Size = new System.Drawing.Size(212, 25);
            this.txtStudentId.TabStop = false;
            //
            // lblName
            //
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(61, 75, 90);
            this.lblName.Location = new System.Drawing.Point(16, 100);
            this.lblName.Name = "lblName";
            this.lblName.Text = "Name";
            //
            // txtName
            //
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(16, 118);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(212, 25);
            this.txtName.TabIndex = 0;
            //
            // lblPhoto
            //
            this.lblPhoto.AutoSize = true;
            this.lblPhoto.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPhoto.ForeColor = System.Drawing.Color.FromArgb(61, 75, 90);
            this.lblPhoto.Location = new System.Drawing.Point(240, 44);
            this.lblPhoto.Name = "lblPhoto";
            this.lblPhoto.Text = "Photo";
            //
            // pnlPhoto
            //
            this.pnlPhoto.BackColor = System.Drawing.Color.FromArgb(238, 241, 245);
            this.pnlPhoto.BorderColor = System.Drawing.Color.FromArgb(170, 179, 189);
            this.pnlPhoto.Controls.Add(this.picPhoto);
            this.pnlPhoto.Location = new System.Drawing.Point(240, 62);
            this.pnlPhoto.Name = "pnlPhoto";
            this.pnlPhoto.Padding = new System.Windows.Forms.Padding(1);
            this.pnlPhoto.Size = new System.Drawing.Size(64, 64);
            //
            // picPhoto
            //
            this.picPhoto.BackColor = System.Drawing.Color.FromArgb(238, 241, 245);
            this.picPhoto.Controls.Add(this.lblNoPhoto);
            this.picPhoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picPhoto.Name = "picPhoto";
            this.picPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPhoto.TabStop = false;
            //
            // lblNoPhoto
            //
            this.lblNoPhoto.BackColor = System.Drawing.Color.Transparent;
            this.lblNoPhoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoPhoto.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblNoPhoto.ForeColor = System.Drawing.Color.FromArgb(91, 102, 115);
            this.lblNoPhoto.Name = "lblNoPhoto";
            this.lblNoPhoto.Text = "No photo";
            this.lblNoPhoto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // btnBrowse
            //
            this.btnBrowse.BackColor = System.Drawing.Color.White;
            this.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(170, 179, 189);
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnBrowse.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.btnBrowse.Location = new System.Drawing.Point(240, 130);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(64, 26);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            //
            // lblGender
            //
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGender.ForeColor = System.Drawing.Color.FromArgb(61, 75, 90);
            this.lblGender.Location = new System.Drawing.Point(16, 166);
            this.lblGender.Name = "lblGender";
            this.lblGender.Text = "Gender";
            //
            // cboGender
            //
            this.cboGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGender.FormattingEnabled = true;
            this.cboGender.Items.AddRange(new object[] { "Male", "Female", "Other" });
            this.cboGender.Location = new System.Drawing.Point(16, 184);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(138, 25);
            this.cboGender.TabIndex = 2;
            //
            // lblYear
            //
            this.lblYear.AutoSize = true;
            this.lblYear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblYear.ForeColor = System.Drawing.Color.FromArgb(61, 75, 90);
            this.lblYear.Location = new System.Drawing.Point(166, 166);
            this.lblYear.Name = "lblYear";
            this.lblYear.Text = "Year Level";
            //
            // cboYear
            //
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.FormattingEnabled = true;
            this.cboYear.Items.AddRange(new object[] { "1st Year", "2nd Year", "3rd Year", "4th Year" });
            this.cboYear.Location = new System.Drawing.Point(166, 184);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(138, 25);
            this.cboYear.TabIndex = 3;
            //
            // lblProgram
            //
            this.lblProgram.AutoSize = true;
            this.lblProgram.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblProgram.ForeColor = System.Drawing.Color.FromArgb(61, 75, 90);
            this.lblProgram.Location = new System.Drawing.Point(16, 222);
            this.lblProgram.Name = "lblProgram";
            this.lblProgram.Text = "Program";
            //
            // cboProgram
            //
            this.cboProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProgram.FormattingEnabled = true;
            this.cboProgram.Items.AddRange(new object[] {
                "BS Computer Science", "BS Information Technology", "BS Information Systems", "BS Computer Engineering"});
            this.cboProgram.Location = new System.Drawing.Point(16, 240);
            this.cboProgram.Name = "cboProgram";
            this.cboProgram.Size = new System.Drawing.Size(288, 25);
            this.cboProgram.TabIndex = 4;
            //
            // lblContact
            //
            this.lblContact.AutoSize = true;
            this.lblContact.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblContact.ForeColor = System.Drawing.Color.FromArgb(61, 75, 90);
            this.lblContact.Location = new System.Drawing.Point(16, 278);
            this.lblContact.Name = "lblContact";
            this.lblContact.Text = "Contact Number";
            //
            // txtContact
            //
            this.txtContact.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtContact.Location = new System.Drawing.Point(16, 296);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(288, 25);
            this.txtContact.TabIndex = 5;
            //
            // lblEmail
            //
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(61, 75, 90);
            this.lblEmail.Location = new System.Drawing.Point(16, 334);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Text = "Email (optional)";
            //
            // txtEmail
            //
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Location = new System.Drawing.Point(16, 352);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(288, 25);
            this.txtEmail.TabIndex = 6;
            //
            // btnAdd
            //
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(16, 416);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(140, 34);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            //
            // btnUpdate
            //
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnUpdate.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(31, 58, 95);
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(164, 416);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(140, 34);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            //
            // btnDelete
            //
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
            this.btnDelete.Location = new System.Drawing.Point(16, 458);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 34);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            //
            // btnClear
            //
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClear.BackColor = System.Drawing.Color.White;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(170, 179, 189);
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.btnClear.Location = new System.Drawing.Point(164, 458);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(140, 34);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            //
            // txtSearch
            //
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Location = new System.Drawing.Point(352, 16);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(438, 25);
            this.txtSearch.TabIndex = 11;
            //
            // cboYearFilter
            //
            this.cboYearFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboYearFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYearFilter.FormattingEnabled = true;
            this.cboYearFilter.Items.AddRange(new object[] { "All years", "1st Year", "2nd Year", "3rd Year", "4th Year" });
            this.cboYearFilter.Location = new System.Drawing.Point(798, 16);
            this.cboYearFilter.Name = "cboYearFilter";
            this.cboYearFilter.Size = new System.Drawing.Size(120, 25);
            this.cboYearFilter.TabIndex = 12;
            //
            // cboProgramFilter
            //
            this.cboProgramFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboProgramFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProgramFilter.FormattingEnabled = true;
            this.cboProgramFilter.Items.AddRange(new object[] {
                "All programs", "BS Computer Science", "BS Information Technology", "BS Information Systems", "BS Computer Engineering"});
            this.cboProgramFilter.Location = new System.Drawing.Point(926, 16);
            this.cboProgramFilter.Name = "cboProgramFilter";
            this.cboProgramFilter.Size = new System.Drawing.Size(190, 25);
            this.cboProgramFilter.TabIndex = 13;
            //
            // btnReset
            //
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.BackColor = System.Drawing.Color.White;
            this.btnReset.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(170, 179, 189);
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.btnReset.Location = new System.Drawing.Point(1124, 15);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(60, 27);
            this.btnReset.TabIndex = 14;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = false;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            //
            // pnlGrid
            //
            this.pnlGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGrid.BackColor = System.Drawing.Color.White;
            this.pnlGrid.Controls.Add(this.dgvStudents);
            this.pnlGrid.Location = new System.Drawing.Point(352, 56);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(1);
            this.pnlGrid.Size = new System.Drawing.Size(832, 430);
            //
            // dgvStudents
            //
            this.dgvStudents.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colStudentId, this.colName, this.colGender, this.colYear, this.colProgram, this.colContact, this.colEmail});
            this.dgvStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.TabIndex = 15;
            //
            // columns
            //
            this.colStudentId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colStudentId.HeaderText = "Student ID";
            this.colStudentId.Name = "colStudentId";
            this.colStudentId.Width = 90;
            this.colName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.Width = 140;
            this.colGender.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colGender.HeaderText = "Gender";
            this.colGender.Name = "colGender";
            this.colGender.Width = 70;
            this.colYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colYear.HeaderText = "Year";
            this.colYear.Name = "colYear";
            this.colYear.Width = 80;
            this.colProgram.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colProgram.HeaderText = "Program";
            this.colProgram.Name = "colProgram";
            this.colProgram.Width = 190;
            this.colContact.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colContact.HeaderText = "Contact";
            this.colContact.Name = "colContact";
            this.colContact.Width = 104;
            this.colEmail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colEmail.HeaderText = "Email";
            this.colEmail.Name = "colEmail";
            //
            // lblCount
            //
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(91, 102, 115);
            this.lblCount.Location = new System.Drawing.Point(352, 494);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(400, 30);
            this.lblCount.Text = "Showing 0 of 0 students";
            this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // btnExport
            //
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackColor = System.Drawing.Color.White;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(170, 179, 189);
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.btnExport.Location = new System.Drawing.Point(1084, 494);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.TabIndex = 16;
            this.btnExport.Text = "Export CSV";
            this.btnExport.UseVisualStyleBackColor = false;
            //
            // pnlStatus
            //
            this.pnlStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlStatus.BackColor = System.Drawing.Color.FromArgb(233, 237, 241);
            this.pnlStatus.Controls.Add(this.lblUser);
            this.pnlStatus.Controls.Add(this.lblClock);
            this.pnlStatus.Controls.Add(this.pnlStatusLine);
            this.pnlStatus.Location = new System.Drawing.Point(0, 644);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Size = new System.Drawing.Size(1200, 26);
            //
            // pnlStatusLine
            //
            this.pnlStatusLine.BackColor = System.Drawing.Color.FromArgb(207, 214, 221);
            this.pnlStatusLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStatusLine.Name = "pnlStatusLine";
            this.pnlStatusLine.Size = new System.Drawing.Size(1200, 1);
            //
            // lblUser
            //
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUser.Location = new System.Drawing.Point(12, 1);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(400, 25);
            this.lblUser.Text = "Logged in as: ";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblClock
            //
            this.lblClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClock.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblClock.Location = new System.Drawing.Point(928, 1);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(260, 25);
            this.lblClock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(244, 246, 248);
            this.ClientSize = new System.Drawing.Size(1200, 670);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlStatsLine);
            this.Controls.Add(this.tlpStats);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlStatus);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.MinimumSize = new System.Drawing.Size(1016, 640);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BUseng SIS";
            this.pnlHeader.ResumeLayout(false);
            this.tlpStats.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.pnlPhoto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).EndInit();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            this.pnlStatus.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Timer clockTimer;
        private System.Windows.Forms.Panel pnlHeader;
        private StudentInformationSystem.LogoBox logoMain;
        private System.Windows.Forms.Label lblAppTitle;
        private System.Windows.Forms.Button btnViewLog;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.TableLayoutPanel tlpStats;
        private StudentInformationSystem.StatCard statTotal;
        private StudentInformationSystem.StatCard stat1st;
        private StudentInformationSystem.StatCard stat2nd;
        private StudentInformationSystem.StatCard stat3rd;
        private StudentInformationSystem.StatCard stat4th;
        private System.Windows.Forms.Panel pnlStatsLine;
        private System.Windows.Forms.Panel pnlContent;
        private StudentInformationSystem.BorderPanel pnlDetails;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.Label lblStudentId;
        private System.Windows.Forms.TextBox txtStudentId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPhoto;
        private StudentInformationSystem.BorderPanel pnlPhoto;
        private System.Windows.Forms.PictureBox picPhoto;
        private System.Windows.Forms.Label lblNoPhoto;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.ComboBox cboGender;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.ComboBox cboYear;
        private System.Windows.Forms.Label lblProgram;
        private System.Windows.Forms.ComboBox cboProgram;
        private System.Windows.Forms.Label lblContact;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cboYearFilter;
        private System.Windows.Forms.ComboBox cboProgramFilter;
        private System.Windows.Forms.Button btnReset;
        private StudentInformationSystem.BorderPanel pnlGrid;
        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStudentId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProgram;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContact;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel pnlStatus;
        private System.Windows.Forms.Panel pnlStatusLine;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblClock;
    }
}
