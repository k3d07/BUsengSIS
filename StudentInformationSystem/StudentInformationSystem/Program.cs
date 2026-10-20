using System;
using System.Windows.Forms;

namespace StudentInformationSystem
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Login -> Main window -> (Logout returns to Login, closing the window exits)
            while (true)
            {
                using (var login = new LoginForm())
                {
                    if (login.ShowDialog() != DialogResult.OK) break;
                                                                                        //Uncomment lines below depending on preview
                    using (var main = new MainForm(login.Username, login.IsAdmin))    //Admin preview | with Log Panel
                    //using (var main = new MainForm("student", false))                 //Normal User preview | Without Log panel
                    {
                        main.ShowDialog();
                        if (!main.LoggedOut) break;
                    }
                }
            }
        }
    }
}
