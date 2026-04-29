using ConsoleApp1.Classes;
using Serilog;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string template = "{Timestamp:yyyy-MM-dd HH:mm:ss} | [{Level:u3}] | {Message:lj}{NewLine}{Exception}";
            Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate: template)
                .WriteTo.File("logs/file_.txt", outputTemplate: template)
                .CreateLogger();

            Controller.RegistrationUser();
        }
    }
}
