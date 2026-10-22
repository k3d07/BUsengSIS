using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace StudentInformationSystem.Helpers
{
    public static class DefaultAvatar
    {
        private static Bitmap? _source;

        // Returns a NEW copy every time, so callers can dispose it safely.
        public static Bitmap Get()
        {
            _source ??= Load();
            return new Bitmap(_source);
        }

        private static Bitmap Load()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                string? name = assembly.GetManifestResourceNames()
                    .FirstOrDefault(n => n.EndsWith("default_avatar.png", StringComparison.OrdinalIgnoreCase));

                if (name != null)
                {
                    using Stream? stream = assembly.GetManifestResourceStream(name);
                    if (stream != null)
                    {
                        using var image = Image.FromStream(stream);
                        return new Bitmap(image);
                    }
                }
            }
            catch (Exception)
            {
                // Fall through to the plain placeholder below.
            }

            // Fallback so the app never crashes if the image is missing.
            var fallback = new Bitmap(64, 64);
            using var g = Graphics.FromImage(fallback);
            g.Clear(Color.FromArgb(238, 241, 245));
            return fallback;
        }
    }
}