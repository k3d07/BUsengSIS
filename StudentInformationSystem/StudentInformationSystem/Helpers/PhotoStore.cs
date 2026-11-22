using System.Drawing;

namespace StudentInformationSystem.Helpers
{
    public static class PhotoStore
    {
        public const long MaxBytes = 5L * 1024 * 1024;
        private static readonly string[] AllowedExtensions = { ".png", ".jpg", ".jpeg", ".bmp" };

        public static string GetPhotosFolder()
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (string.IsNullOrWhiteSpace(localAppData))
                throw new InvalidOperationException("The local application data folder is unavailable.");

            string folder = Path.Combine(localAppData, "BUsengSIS", "Photos");
            Directory.CreateDirectory(folder);
            return folder;
        }

        public static bool IsAllowed(string path, out string error)
        {
            error = string.Empty;
            string extension = Path.GetExtension(path);
            if (!AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                error = "Please choose a PNG, JPG, JPEG, or BMP image.";
                return false;
            }

            if (new FileInfo(path).Length > MaxBytes)
            {
                error = "The photo must be 5 MB or smaller.";
                return false;
            }

            return true;
        }

        public static string Save(string sourcePath, string studentId)
        {
            if (!IsAllowed(sourcePath, out string error))
                throw new InvalidDataException(error);

            string extension = Path.GetExtension(sourcePath);
            char[] invalidCharacters = Path.GetInvalidFileNameChars();
            string safeStudentId = new(studentId.Select(character =>
                invalidCharacters.Contains(character) ? '_' : character).ToArray());
            if (string.IsNullOrWhiteSpace(safeStudentId))
                throw new ArgumentException("The Student ID cannot be used as a photo filename.", nameof(studentId));

            string destinationPath = Path.Combine(GetPhotosFolder(), safeStudentId + extension);
            string fullSourcePath = Path.GetFullPath(sourcePath);
            if (string.Equals(fullSourcePath, destinationPath, StringComparison.OrdinalIgnoreCase))
                return destinationPath;

            File.Copy(fullSourcePath, destinationPath, overwrite: true);
            return destinationPath;
        }

        // Database paths are absolute. Missing or legacy relative paths resolve to no image.
        public static Image? Load(string? path)
        {
            if (string.IsNullOrWhiteSpace(path) || !Path.IsPathRooted(path))
                return null;

            if (!File.Exists(path))
                return null;

            return LoadFile(path);
        }

        // The returned bitmap is detached from the source file, so the file can be replaced.
        public static Image LoadFile(string path)
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var source = Image.FromStream(stream);
            return new Bitmap(source);
        }
    }
}
