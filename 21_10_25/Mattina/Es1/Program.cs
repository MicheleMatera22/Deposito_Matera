#region interfaccia
public interface ILogger
{
    void Log(string message);
}

public class LoggerService : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"Log: {message}");
    }
}

public class Printer
{
    public ILogger? Logger { get; set; }

    public void Report(string message)
    {
        if (Logger == null)
        {
            Console.WriteLine("Nessun logger creato.");
        }
        else
        {
            Console.WriteLine("Logger creato.");
            Logger.Log(message);
        }
    }
}


#endregion



public class Program
{
    public static void Main(string[] args)
    {
        var logger = new Printer();
        logger.Logger = new LoggerService();
        logger.Report("Messaggio di prova");

    }
}