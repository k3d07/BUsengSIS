using System;
using System.Windows.Forms;

namespace BUsengSIS
{
    public partial class LoginForm : Form
    {
        // DEMO ONLY: the single default admin account. Replace with a MySQL lookup later.
        private const string DemoAdminUser = "admin";
        private const string DemoAdminPass = "admin123";

        public string Username { get; private set; }
        public bool IsAdmin { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            AcceptButton = btnLogin;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == DemoAdminUser && txtPassword.Text == DemoAdminPass)
            {
                Username = DemoAdminUser;
                IsAdmin = true;
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "BUseng SIS",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.SelectAll();
                txtPassword.Focus();
            }
        }
    }
}
