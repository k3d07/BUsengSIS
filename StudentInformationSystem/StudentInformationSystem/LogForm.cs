using System;
using System.Windows.Forms;

namespace BUsengSIS
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
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
