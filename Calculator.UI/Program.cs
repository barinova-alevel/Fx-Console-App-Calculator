using Serilog;
using Microsoft.Extensions.Configuration;
using Calculator.UI;

class Program
{
    static void Main()
    {
        IConfigurationRoot builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder)
            .CreateLogger();
        Log.Logger.Information("start");

        InputOutput inputOutput = new InputOutput();
        inputOutput.Run();

        Console.ReadKey();
    }
}
