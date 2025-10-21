using System;
using System.Collections.Generic;

#region singleton
public sealed class Inventory
{
    private static Inventory? _instance;
    private Dictionary<string, (IBook book, int quantity)> _items = new Dictionary<string, (IBook, int)>();
    private Inventory() { }

    public static Inventory Instance
    {
        get
        {
            if (_instance == null) _instance = new Inventory();
            return _instance;
        }
    }

    public void AddItem(string name, IBook book, int quantity)
    {
        if (_items.ContainsKey(name))
        {
            _items[name] = (_items[name].book, _items[name].quantity + quantity);
        }
        else
        {
            _items.Add(name, (book, quantity));
        }
    }

    public void RemoveItem(string name, int quantity)
    {
        if (_items.ContainsKey(name))
        {
            var current = _items[name];
            if (current.quantity >= quantity)
            {
                _items[name] = (current.book, current.quantity - quantity);
            }
            else
            {
                Console.WriteLine("Quantità non disponibile.");
            }
        }
        else
        {
            Console.WriteLine("Articolo non presente nell'inventario.");
        }
    }

    public bool HasStock(string name, int quantity)
    {
        return _items.ContainsKey(name) && _items[name].quantity >= quantity;
    }

    public Dictionary<string, (IBook book, int quantity)> GetItems()
    {
        return new Dictionary<string, (IBook, int)>(_items);
    }
}
#endregion

#region interface
public interface IInventoryService
{
    bool IsIn(string item, out int num);
}

public interface IPaymentProcessor
{
    void ProcessPayment(double amount, string method);
}

public interface INotificationSender
{
    void SendNotification(string message);
}

public interface IPricingStrategy
{
    double CalculatePrice(int quantity);
}

public interface IBook
{
    void MostraTipo();
    void LeggiIntro();
}
#endregion

#region factory
public class DigitalBook : IBook
{
    public void MostraTipo() => Console.WriteLine("Libro digitale");
    public void LeggiIntro() => Console.WriteLine("Lettura libro digitale");
}

public class PrintBook : IBook
{
    public void MostraTipo() => Console.WriteLine("Libro stampa");
    public void LeggiIntro() => Console.WriteLine("Lettura libro stampa");
}

public abstract class ProductFactory
{
    public abstract IBook CreaLibro(string tipo);
}

public class ConcreteBookFactory : ProductFactory
{
    public override IBook CreaLibro(string tipo)
    {
        return tipo.ToUpper() switch
        {
            "BOOK_DIGITAL" => new DigitalBook(),
            "BOOK_PRINT" => new PrintBook(),
            _ => throw new ArgumentException("Tipo di libro non valido")
        };
    }
}
#endregion

#region implementazione
public class InventoryService : IInventoryService
{
    public bool IsIn(string item, out int num)
    {
        num = 0;
        var inv = Inventory.Instance;
        if (inv.HasStock(item, 1))
        {
            num = 1;
            return true;
        }
        return false;
    }
}

public class PaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(double amount, string method)
    {
        Console.WriteLine($"Pagamento di {amount}€ eseguito tramite {method}.");
    }
}

public class EmailNotificationSender : INotificationSender
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"[EMAIL] {message}");
    }
}

public class DefaultPricingStrategy : IPricingStrategy
{
    public double CalculatePrice(int quantity)
    {
        double pricePerBook = 10.0;
        if (quantity >= 3)
            return pricePerBook * quantity * 0.9;
        return pricePerBook * quantity;
    }
}

public class OrderService
{
    private readonly IInventoryService _inventoryService;
    private readonly IPaymentProcessor _paymentProcessor;

    public INotificationSender? NotificationSender { get; set; }
    public IPricingStrategy? PricingStrategy { get; set; }

    public OrderService(IInventoryService inventoryService, IPaymentProcessor paymentProcessor)
    {
        _inventoryService = inventoryService;
        _paymentProcessor = paymentProcessor;
    }

    public void PlaceOrder(string item, int quantity, string method)
    {
        Console.WriteLine($"Ordine ricevuto: {item} x {quantity}");

        if (!_inventoryService.IsIn(item, out _))
        {
            Console.WriteLine("Articolo non disponibile a magazzino.");
            return;
        }

        double totalPrice = PricingStrategy?.CalculatePrice(quantity) ?? 10.0 * quantity;
        _paymentProcessor.ProcessPayment(totalPrice, method);

        NotificationSender?.SendNotification($"Ordine per '{item}' completato. Totale: {totalPrice}€");
    }
}
#endregion

#region program
class Program
{
    static void Main()
    {
        IInventoryService inventoryService = new InventoryService();
        IPaymentProcessor paymentProcessor = new PaymentProcessor();

        var orderService = new OrderService(inventoryService, paymentProcessor)
        {
            NotificationSender = new EmailNotificationSender(),
            PricingStrategy = new DefaultPricingStrategy()
        };

        ProductFactory factory = new ConcreteBookFactory();

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1. Mostra inventario");
            Console.WriteLine("2. Crea libro e aggiungi all'inventario");
            Console.WriteLine("3. Rimuovi articolo");
            Console.WriteLine("4. Piazza ordine");
            Console.WriteLine("0. Esci");
            Console.Write("Scelta: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1": ShowInventory(); break;
                case "2": CreateBookAndAdd(factory); break;
                case "3": RemoveItemMenu(); break;
                case "4": PlaceOrderMenu(orderService); break;
                case "0": exit = true; break;
                default: Console.WriteLine("Scelta non valida."); break;
            }
        }
    }

    static void ShowInventory()
    {
        Console.WriteLine("\nInventario:");
        var items = Inventory.Instance.GetItems();
        if (items.Count == 0)
        {
            Console.WriteLine("Inventario vuoto.");
            return;
        }
        foreach (var kvp in items)
        {
            Console.Write($"{kvp.Key}: {kvp.Value.quantity} copie - Tipo: ");
            kvp.Value.book.MostraTipo();
        }
    }

    static void CreateBookAndAdd(ProductFactory factory)
    {
        Console.WriteLine("Scegli il tipo di libro da creare:");
        Console.WriteLine("1. Libro digitale");
        Console.WriteLine("2. Libro stampa");
        Console.Write("Scelta: ");
        string? scelta = Console.ReadLine();

        string tipo = scelta switch
        {
            "1" => "BOOK_DIGITAL",
            "2" => "BOOK_PRINT",
            _ => ""
        };

        if (string.IsNullOrEmpty(tipo))
        {
            Console.WriteLine("Scelta non valida.");
            return;
        }
            IBook libro = factory.CreaLibro(tipo);
            libro.MostraTipo();
            libro.LeggiIntro();

            Console.Write("Nome del libro: ");
            string? nome = Console.ReadLine();
            Console.Write("Quantità: ");
            if (int.TryParse(Console.ReadLine(), out int qty))
            {
                Inventory.Instance.AddItem(nome!, libro, qty);
                Console.WriteLine($"Aggiunto {qty} copie di '{nome}' all'inventario.");
            }
            else
            {
                Console.WriteLine("Quantità non valida.");
            }
    }

    static void RemoveItemMenu()
    {
        Console.Write("Nome articolo da rimuovere: ");
        string? item = Console.ReadLine();
        Console.Write("Quantità: ");
        if (int.TryParse(Console.ReadLine(), out int quantity))
        {
            Inventory.Instance.RemoveItem(item!, quantity);
        }
        else
        {
            Console.WriteLine("Quantità non valida.");
        }
    }

    static void PlaceOrderMenu(OrderService orderService)
    {
        Console.Write("Articolo da ordinare: ");
        string? item = Console.ReadLine();
        Console.Write("Quantità: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Quantità non valida.");
            return;
        }
        Console.Write("Metodo di pagamento: ");
        string? method = Console.ReadLine();
        orderService.PlaceOrder(item!, quantity, method!);
    }
}
#endregion
