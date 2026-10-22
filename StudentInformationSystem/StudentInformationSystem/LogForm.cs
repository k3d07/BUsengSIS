using System;
using System.Windows.Forms;
using StudentInformationSystem.Data;

namespace StudentInformationSystem
{
    public partial class LogForm : Form
    {
        public LogForm(bool isAdmin)
        {
            // Second line of defence: non-admins can never open this window.
            if (!isAdmin)
                throw new UnauthorizedAccessException("The activity log is only available to administrators.");

            InitializeComponent();
            UiTheme.StyleGrid(dgvLog);

            dgvLog.AutoGenerateColumns = false;
            colTime.DataPropertyName = "logged_at";
            colTime.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
            colUser.DataPropertyName = "username";
            colAction.DataPropertyName = "action";
            colStudentId.DataPropertyName = "student_id";

            Load += (s, e) => LoadLog();
        }

        private void LoadLog()
        {
            try
            {
                dgvLog.DataSource = ActivityLogRepository.GetRecent();
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "Could not load the activity log. Please check that MySQL is running.",
                    "Activity Log", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}