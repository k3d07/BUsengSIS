using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using StudentInformationSystem.Security;

namespace StudentInformationSystem
{
    public partial class MainForm : Form
    {
        private static readonly Color PrimaryBlue = ColorTranslator.FromHtml("#1F6FEB");
        private readonly bool _isAdmin;
        private readonly Button _btnStudents = new();
        private readonly Button _btnDashboard = new();

        /// <summary>True when the user pressed Logout (so Program shows the login again).</summary>
        public bool LoggedOut { get; private set; }

        public MainForm(string username, bool isAdmin)
        {
            InitializeComponent();
            ApplyDetailsLayout();
            _isAdmin = isAdmin;
            InitializeNavigation();
            StyleMainActions();

            // The View Log button exists ONLY for admins. For anyone else it is
            // removed from the form entirely (not just hidden/disabled).
            if (!_isAdmin)
            {
                pnlHeader.Controls.Remove(btnViewLog);
                btnViewLog.Dispose();
                btnViewLog = null;
            }
            else
            {
                StyleHeaderButton(btnViewLog, selected: false);
                btnViewLog.Text = "Activity Log";
                btnViewLog.Size = new Size(92, 30);
                btnViewLog.Location = new Point(1004, 9);
            }

            lblUser.Text = "Logged in as: " + username;

            UiTheme.StyleGrid(dgvStudents);
            NativeMethods.SetPlaceholder(txtSearch, "Search by name or ID");
            NativeMethods.SetPlaceholder(txtEmail, "name@example.com");

            cboYearFilter.SelectedIndex = 0;
            cboProgramFilter.SelectedIndex = 0;
            InitLogic();
            UpdateClock();
            clockTimer.Start();
        }

        private void InitializeNavigation()
        {
            Color header = PrimaryBlue;
            pnlHeader.BackColor = header;

            _btnStudents.Text = "Students";
            _btnStudents.Location = new Point(808, 9);
            _btnStudents.Size = new Size(88, 30);
            StyleHeaderButton(_btnStudents, selected: true);
            _btnStudents.Click += (_, _) =>
            {
                SafeRefresh();
                SetActiveNavigation(_btnStudents);
            };

            _btnDashboard.Text = "Dashboard";
            _btnDashboard.Location = new Point(904, 9);
            _btnDashboard.Size = new Size(92, 30);
            StyleHeaderButton(_btnDashboard, selected: false);
            _btnDashboard.Click += (_, _) =>
            {
                SetActiveNavigation(_btnDashboard);
                new DashboardForm().Show(this);
            };

            btnLogout.BackColor = header;
            btnLogout.FlatAppearance.BorderColor = Color.FromArgb(170, 205, 255);
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(26, 95, 205);

            pnlHeader.Controls.Add(_btnStudents);
            pnlHeader.Controls.Add(_btnDashboard);
        }

        private void StyleMainActions()
        {
            Color primary = ColorTranslator.FromHtml("#1F6FEB");
            foreach (Button button in new[] { btnAdd, btnUpdate })
            {
                button.BackColor = primary;
                button.FlatAppearance.BorderColor = primary;
                button.ForeColor = Color.White;
                button.UseVisualStyleBackColor = false;
            }
        }

        private static void StyleHeaderButton(Button button, bool selected)
        {
            button.BackColor = selected ? Color.White : PrimaryBlue;
            button.ForeColor = selected ? PrimaryBlue : Color.White;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = selected ? 0 : 1;
            button.FlatAppearance.BorderColor = selected ? Color.White : Color.FromArgb(170, 205, 255);
            button.FlatAppearance.MouseOverBackColor = selected
                ? Color.FromArgb(234, 242, 255)
                : Color.FromArgb(26, 95, 205);
            button.FlatAppearance.MouseDownBackColor = selected
                ? Color.FromArgb(216, 233, 255)
                : Color.FromArgb(19, 74, 158);
            button.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
            button.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }

        private void SetActiveNavigation(Button activeButton)
        {
            StyleHeaderButton(_btnStudents, ReferenceEquals(activeButton, _btnStudents));
            StyleHeaderButton(_btnDashboard, ReferenceEquals(activeButton, _btnDashboard));
            if (_isAdmin)
                StyleHeaderButton(btnViewLog, ReferenceEquals(activeButton, btnViewLog));
        }

        private void UpdateClock()
        {
            lblClock.Text = DateTime.Now.ToString("ddd, MMM d yyyy  h:mm tt", CultureInfo.GetCultureInfo("en-US"));
        }

        private void clockTimer_Tick(object sender, EventArgs e)
        {
            UpdateClock();
        }

        private void btnViewLog_Click(object sender, EventArgs e)
        {
            if (!_isAdmin) return; // guard: non-admins can't reach this even if wired up by mistake
            SetActiveNavigation(btnViewLog);
            using (var log = new LogForm(_isAdmin))
                log.ShowDialog(this);
            SetActiveNavigation(_btnStudents);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            LoggedOut = true;
            Close();
        }

        // Light UI helpers (no database logic)
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboYearFilter.SelectedIndex = 0;
            cboProgramFilter.SelectedIndex = 0;
            SafeRefresh();
        }
    }
}
