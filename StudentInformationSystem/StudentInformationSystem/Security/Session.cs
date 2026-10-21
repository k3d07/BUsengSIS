namespace StudentInformationSystem.Security
{
    public static class Session
    {
        public static string CurrentUsername { get; private set; }

        public static void SetUser(string username)
        {
            CurrentUsername = username;
        }

        public static void Clear()
        {
            CurrentUsername = null;
        }
    }
}