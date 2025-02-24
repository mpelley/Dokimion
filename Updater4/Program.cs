namespace Updater4
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("UpdaterLog.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Application.Run(new Form1());

            Log.CloseAndFlush();
        }
    }
}