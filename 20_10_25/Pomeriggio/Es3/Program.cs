#region interfacce
public interface ILogger
{
    void Log(string message);
}
#endregion

#region classi
public sealed class AppConfig
{
    private static readonly AppConfig _instance = new AppConfig();
    public string NomeApp { get; private set; }
    public string Valuta { get; private set; }
    public double IVA { get; private set; }

    private AppConfig()
    {
        NomeApp = "OrderManager";
        Valuta = "€";
        IVA = 0.22;
    }

    public static AppConfig Instance => _instance;
}

public class LoggerService : ILogger
{
    private AppConfig _config;

    public LoggerService(AppConfig config)
    {
        _config = config;
    }

    public virtual void Log(string message)
    {
        Console.WriteLine($"[{_config.NomeApp}] {message}");
    }
}

public abstract class LoggerDecorator : ILogger
{
    protected ILogger _innerLogger;

    protected LoggerDecorator(ILogger logger)
    {
        _innerLogger = logger;
    }

    public abstract void Log(string message);
}
public class TimestampLoggerDecorator : LoggerDecorator
{
    public TimestampLoggerDecorator(ILogger logger) : base(logger) { }

    public override void Log(string message)
    {
        string timestampedMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
        _innerLogger.Log(timestampedMessage);
    }
}

public class OrderService
{
    private ILogger _logger;

    public OrderService(ILogger logger)
    {
        _logger = logger;
    }

    public void CreaOrdine(string prodotto, double prezzo)
    {
        _logger.Log($"Ordine creato: {prodotto} - Prezzo base: {prezzo}{AppConfig.Instance.Valuta}");
        double totale = prezzo + (prezzo * AppConfig.Instance.IVA);
        _logger.Log($"Totale con IVA ({AppConfig.Instance.IVA * 100}%): {totale}{AppConfig.Instance.Valuta}");
    }
}
#endregion

#region main
public class Program
{
    public static void Main()
    {
        var config = AppConfig.Instance;

        Console.WriteLine("Scegli modalità logger:");
        Console.WriteLine("1. Normale");
        Console.WriteLine("2. Con timestamp");
        Console.Write("Scelta: ");
        string? sceltaLogger = Console.ReadLine();

        ILogger logger = new LoggerService(config);

        if (sceltaLogger == "2")
        {
            // Applichiamo il pattern decorator
            logger = new TimestampLoggerDecorator(logger);
        }

        var orderService = new OrderService(logger);

        while (true)
        {
            Console.WriteLine("\n--- MENU ORDINI ---");
            Console.WriteLine("1. Crea nuovo ordine");
            Console.WriteLine("2. Esci");
            Console.Write("Scelta: ");
            string? scelta = Console.ReadLine();

            if (scelta == "1")
            {
                Console.Write("Inserisci nome prodotto: ");
                string? prodotto = Console.ReadLine();

                Console.Write("Inserisci prezzo prodotto: ");
                if (double.TryParse(Console.ReadLine(), out double prezzo))
                {
                    orderService.CreaOrdine(prodotto, prezzo);
                }
                else
                {
                    Console.WriteLine("Prezzo non valido.");
                }
            }
            else if (scelta == "2")
            {
                Console.WriteLine("Chiusura programma...");
                break;
            }
            else
            {
                Console.WriteLine("Scelta non valida.");
            }
        }
    }
}
#endregion
