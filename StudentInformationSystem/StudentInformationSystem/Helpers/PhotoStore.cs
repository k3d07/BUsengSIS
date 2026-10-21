namespace StudentInformationSystem.Helpers
{
    public static class PhotoStore
    {
        public const long MaxBytes = 2 * 1024 * 1024;   // 2 MB
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png" };

        // Finds the "photos" folder in the repo root by walking up from the running .exe.
        // Falls back to a "photos" folder next to the .exe if it isn't found.
        public static string GetPhotosFolder()
        {
            string? dir = AppContext.BaseDirectory;

            for (int i = 0; i < 6 && !string.IsNullOrEmpty(dir); i++)
            {
                string candidate = Path.Combine(dir, "photos");
                if (Directory.Exists(candidate))
                    return candidate;

                dir = Path.GetDirectoryName(dir.TrimEnd(Path.DirectorySeparatorChar));
            }

            string fallback = Path.Combine(AppContext.BaseDirectory, "photos");
            Directory.CreateDirectory(fallback);
            return fallback;
        }

        public static bool IsAllowed(string path, out string error)
        {
            error = "";

            string ext = Path.GetExtension(path).ToLowerInvariant();
            if (Array.IndexOf(AllowedExtensions, ext) < 0)
            {
                error = "Please choose a JPG or PNG image.";
                return false;
            }

            if (new FileInfo(path).Length > MaxBytes)
            {
                error = "The photo must be 2 MB or smaller.";
                return false;
            }

            return true;
        }

        // Copies the chosen image into the photos folder as <studentId>.<ext>.
        // Returns the file name only (this is what goes in the database).
        public static string Save(string sourcePath, string studentId)
        {
            string ext = Path.GetExtension(sourcePath).ToLowerInvariant();
            if (ext == ".jpeg") ext = ".jpg";

            string fileName = studentId + ext;
            File.Copy(sourcePath, Path.Combine(GetPhotosFolder(), fileName), true);
            return fileName;
        }

        // Loads a saved photo by file name. Returns null if missing or unreadable.
        public static Image? Load(string? fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return null;

            string path = Path.Combine(GetPhotosFolder(), Path.GetFileName(fileName));
            return LoadFile(path);
        }

        // Loads any image file without keeping the file locked.
        public static Image? LoadFile(string path)
        {
            try
            {
                if (!File.Exists(path)) return null;

                using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                using var image = Image.FromStream(stream);
                return new Bitmap(image);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}