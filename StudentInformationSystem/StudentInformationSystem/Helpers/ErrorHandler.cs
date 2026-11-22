using System.Text;
using System.Windows.Forms;

namespace StudentInformationSystem.Helpers
{
    public static class ErrorHandler
    {
        public static void ShowUnexpected(Exception exception, string friendlyMessage, string title = "Error")
        {
            TryAppendToLog(exception);

            MessageBox.Show(
                $"{friendlyMessage}{Environment.NewLine}{Environment.NewLine}Details: {exception.Message}",
                title,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private static void TryAppendToLog(Exception exception)
        {
            try
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                if (string.IsNullOrWhiteSpace(localAppData))
                    return;

                string directory = Path.Combine(localAppData, "BUsengSIS");
                Directory.CreateDirectory(directory);

                var entry = new StringBuilder()
                    .AppendLine($"Timestamp: {DateTimeOffset.Now:O}")
                    .AppendLine($"Exception type: {exception.GetType().FullName}")
                    .AppendLine($"Message: {exception.Message}")
                    .AppendLine("Stack trace:")
                    .AppendLine(exception.StackTrace ?? "(no stack trace available)")
                    .AppendLine("Full exception:")
                    .AppendLine(exception.ToString())
                    .AppendLine(new string('-', 72))
                    .ToString();

                File.AppendAllText(Path.Combine(directory, "error.log"), entry, Encoding.UTF8);
            }
            catch (Exception)
            {
                // Error reporting must never replace the original error or crash the app.
            }
        }
    }
}
