namespace LeaveManagementSystem.NewFolder
{
    public sealed class AppLogger
    {
        private static readonly Lazy<AppLogger> _instance = new Lazy<AppLogger>(() => new AppLogger());

        public static AppLogger Instance => _instance.Value;

        private AppLogger()
        {
         
        }
        // implement the Singleton Pattern for centralized logging
        public void Log(string message)
        {
            var logLine = $"{DateTime.UtcNow}: {message}";
            Console.WriteLine(logLine);
        }
    }
}
