using System;
using System.Globalization;
using System.Windows.Forms;

namespace StudentInformationSystem
{
    public partial class MainForm : Form
    {
        private readonly bool _isAdmin;

        /// <summary>True when the user pressed Logout (so Program shows the login again).</summary>
        public bool LoggedOut { get; private set; }

        public MainForm(string username, bool isAdmin)
        {
            InitializeComponent();
            _isAdmin = isAdmin;

            // The View Log button exists ONLY for admins. For anyone else it is
            // removed from the form entirely (not just hidden/disabled).
            if (!_isAdmin)
            {
                pnlHeader.Controls.Remove(btnViewLog);
                btnViewLog.Dispose();
                btnViewLog = null;
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
            using (var log = new LogForm(_isAdmin))
                log.ShowDialog(this);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
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
