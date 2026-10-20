using System;
using System.Windows.Forms;
using MySqlConnector;
using StudentInformationSystem.Data;
using StudentInformationSystem.Security;

namespace StudentInformationSystem
{
    public partial class LoginForm : Form
    {
        public string Username { get; private set; } = "";
        public bool IsAdmin { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            AcceptButton = btnLogin;
            txtPassword.Clear();   // the designer pre-fills a demo password; don't show it
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text;

            if (user.Length == 0 || pass.Length == 0)
            {
                MessageBox.Show("Please enter your username and password.", "BUseng SIS",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                if (UserRepository.ValidateLogin(user, pass))
                {
                    Username = user;
                    IsAdmin = true;               // the only account is the admin
                    Session.SetUser(Username);

                    try { ActivityLogRepository.Log(Username, "LOGIN", ""); }
                    catch (Exception) { /* logging must never block a login */ }

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
            catch (MySqlException)
            {
                MessageBox.Show(
                    "Could not reach the database. Please check that MySQL is running and try again.",
                    "Database error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Something went wrong. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}