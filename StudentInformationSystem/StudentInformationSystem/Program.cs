using System;
using System.Windows.Forms;
using StudentInformationSystem.Data;

namespace StudentInformationSystem
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1. Database check
            if (!DbHelper.TestConnection(out _))
            {
                MessageBox.Show(
                    "Could not connect to the database.\n\n" +
                    "Please check that MySQL is running and that App.config has your MySQL login.",
                    "BUseng SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. First run: make sure the admin account exists
            try
            {
                UserRepository.EnsureAdminExists();
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "The user table could not be prepared. Please run database/schema.sql and try again.",
                    "BUseng SIS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Login -> Main window -> (Logout returns to Login, closing the window exits)
            while (true)
            {
                using (var login = new LoginForm())
                {
                    if (login.ShowDialog() != DialogResult.OK) break;

                    using (var main = new MainForm(login.Username, login.IsAdmin))
                    {
                        main.ShowDialog();
                        if (!main.LoggedOut) break;
                    }
                }
            }
        }
    }
}