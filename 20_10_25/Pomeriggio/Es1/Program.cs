using System;

#region Interface
public interface IGreeter
{
    void Greet(string name);
}

public interface ILogger
{
    void Log(string message);
}
#endregion

#region Classes

public class Logger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
    }
}

public class ConsoleGreeter : IGreeter
{
    public void Greet(string name)
    {
        Console.WriteLine($"Benvenuto {name}");
    }
}

public class GreetingService
{
    private IGreeter _greeter;
    private ILogger _logger;

    public GreetingService(IGreeter greeter, ILogger logger)
    {
        _greeter = greeter;
        _logger = logger;

    }

    public void Stampa(string name, string messaggio)
    {
        _greeter.Greet(name);
        _logger.Log(messaggio);
    }
}

#endregion

public class Program
{
    public static void Main()
    {
        IGreeter greeter = new ConsoleGreeter();
        ILogger logger = new Logger();
        Random random = new Random();
        
        var app = new GreetingService(greeter, logger);
        while (true)
        {
            Console.WriteLine("Inserisci il tuo nome(exit per uscire): ");
            string? name = Console.ReadLine();
            if (name.Trim().ToLower() == "exit")
            {
                break;
            }
            app.Stampa(name,random.Next(100).ToString());
        }

    }
}