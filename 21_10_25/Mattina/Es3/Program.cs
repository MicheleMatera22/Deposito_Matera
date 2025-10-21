#region using
using System;
using System.Collections.Generic;
#endregion

#region singleton
/// <summary>
/// Gestisce l'inventario dei libri come Singleton.
/// </summary>
public sealed class Inventory
{
    private static Inventory? _instance;
    private Dictionary<string, (IBook book, int quantity)> _items = new Dictionary<string, (IBook, int)>();

    private Inventory() { }

    /// <summary>
    /// Restituisce l'istanza unica dell'inventario.
    /// </summary>
    public static Inventory Instance
    {
        get
        {
            if (_instance == null) _instance = new Inventory();
            return _instance;
        }
    }

    /// <summary>
    /// Aggiunge un libro all'inventario con nome e quantità.
    /// </summary>
    public void AddItem(string name, IBook book, int quantity)
    {
        if (_items.ContainsKey(name))
            _items[name] = (_items[name].book, _items[name].quantity + quantity);
        else
            _items.Add(name, (book, quantity));
    }

    /// <summary>
    /// Rimuove una quantità di un libro dall'inventario.
    /// </summary>
    public void RemoveItem(string name, int quantity)
    {
        if (_items.ContainsKey(name))
        {
            var current = _items[name];
            if (current.quantity >= quantity)
                _items[name] = (current.book, current.quantity - quantity);
            else
                Console.WriteLine("Quantità non disponibile.");
        }
        else
        {
            Console.WriteLine("Articolo non presente nell'inventario.");
        }
    }

    /// <summary>
    /// Controlla se un libro è presente con quantità sufficiente.
    /// </summary>
    public bool HasStock(string name, int quantity)
    {
        return _items.ContainsKey(name) && _items[name].quantity >= quantity;
    }

    /// <summary>
    /// Restituisce una copia dell'inventario.
    /// </summary>
    public Dictionary<string, (IBook book, int quantity)> GetItems()
    {
        return new Dictionary<string, (IBook, int)>(_items);
    }
}
#endregion

#region interfaces
/// <summary>
/// Servizio per controllare lo stock in inventario.
/// </summary>
public interface IInventoryService
{
    bool IsIn(string item, out int num);
}

/// <summary>
/// Interfaccia per gestire i pagamenti.
/// </summary>
public interface IPaymentProcessor
{
    void ProcessPayment(double amount, string method);
}

/// <summary>
/// Interfaccia per inviare notifiche.
/// </summary>
public interface INotificationSender
{
    void SendNotification(string message);
}

/// <summary>
/// Strategia per calcolare il prezzo totale.
/// </summary>
public interface IPricingStrategy
{
    double CalculatePrice(int quantity);
}

/// <summary>
/// Interfaccia per rappresentare un libro.
/// </summary>
public interface IBook
{
    void MostraTipo();
    void LeggiIntro();
}
#endregion

#region books
/// <summary>
/// Libro digitale.
/// </summary>
public class DigitalBook : IBook
{
    public void MostraTipo() => Console.WriteLine("Libro digitale");
    public void LeggiIntro() => Console.WriteLine("Stai leggendo un libro digitale");
}

/// <summary>
/// Libro stampato.
/// </summary>
public class PrintBook : IBook
{
    public void MostraTipo() => Console.WriteLine("Libro stampa");
    public void LeggiIntro() => Console.WriteLine("Stai leggendo un libro stampa");
}
#endregion

#region product factory
/// <summary>
/// Factory astratta per creare libri.
/// </summary>
public abstract class ProductFactory
{
    public abstract IBook CreaLibro(string tipo);
}

/// <summary>
/// Factory concreta per creare libri digitali o stampati.
/// </summary>
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

#region payment factory
/// <summary>
/// Factory astratta per creare processori di pagamento.
/// </summary>
public abstract class PaymentFactory
{
    public abstract IPaymentProcessor CreaPagamento(string tipo);
}

/// <summary>
/// Factory concreta per creare processori PayPal o Stripe.
/// </summary>
public class ConcretePaymentFactory : PaymentFactory
{
    public override IPaymentProcessor CreaPagamento(string tipo)
    {
        return tipo.ToUpper() switch
        {
            "PAYPAL" => new PaypalPaymentProcessor(),
            "STRIPE" => new StripePaymentProcessor(),
            _ => throw new ArgumentException("Metodo di pagamento non valido")
        };
    }
}

/// <summary>
/// Processore pagamento PayPal.
/// </summary>
public class PaypalPaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(double amount, string method)
    {
        Console.WriteLine($"Pagamento di {amount}€ eseguito tramite PayPal.");
    }
}

/// <summary>
/// Processore pagamento Stripe.
/// </summary>
public class StripePaymentProcessor : IPaymentProcessor
{
    public void ProcessPayment(double amount, string method)
    {
        Console.WriteLine($"Pagamento di {amount}€ eseguito tramite Stripe.");
    }
}
#endregion

#region services
/// <summary>
/// Implementazione del servizio inventario.
/// </summary>
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

/// <summary>
/// Invia notifiche via email.
/// </summary>
public class EmailNotificationSender : INotificationSender
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"[EMAIL] {message}");
    }
}

/// <summary>
/// Strategia di prezzo di default.
/// </summary>
public class DefaultPricingStrategy : IPricingStrategy
{
    public double CalculatePrice(int quantity)
    {
        double pricePerBook = 10.0;
        if (quantity >= 3) return pricePerBook * quantity * 0.9;
        return pricePerBook * quantity;
    }
}

/// <summary>
/// Gestione degli ordini.
/// </summary>
public class OrderService
{
    private readonly IInventoryService _inventoryService;

    public INotificationSender? NotificationSender { get; set; }
    public IPricingStrategy? PricingStrategy { get; set; }

    public OrderService(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    public void PlaceOrder(string item, int quantity, IPaymentProcessor paymentProcessor)
    {
        Console.WriteLine($"Ordine ricevuto: {item} x {quantity}");
        if (!_inventoryService.IsIn(item, out _))
        {
            Console.WriteLine("Articolo non disponibile a magazzino.");
            return;
        }

        double totalPrice = PricingStrategy?.CalculatePrice(quantity) ?? 10.0 * quantity;
        paymentProcessor.ProcessPayment(totalPrice, "");
        Inventory.Instance.RemoveItem(item, quantity);
        NotificationSender?.SendNotification($"Ordine per '{item}' completato. Totale: {totalPrice}€");
    }
}
#endregion

#region main
/// <summary>
/// Classe principale con menu interattivo.
/// </summary>
class Program
{
    static void Main()
    {
        IInventoryService inventoryService = new InventoryService();
        ProductFactory bookFactory = new ConcreteBookFactory();
        PaymentFactory paymentFactory = new ConcretePaymentFactory();
        var orderService = new OrderService(inventoryService)
        {
            NotificationSender = new EmailNotificationSender(),
            PricingStrategy = new DefaultPricingStrategy()
        };

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
                case "2": CreateBookAndAdd(bookFactory); break;
                case "3": RemoveItemMenu(); break;
                case "4": PlaceOrderMenu(orderService, paymentFactory); break;
                case "0": exit = true; break;
                default: Console.WriteLine("Scelta non valida."); break;
            }
        }
    }

    /// <summary>
    /// Mostra l'inventario completo.
    /// </summary>
    static void ShowInventory()
    {
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

    /// <summary>
    /// Crea un libro tramite factory e lo aggiunge all'inventario.
    /// </summary>
    static void CreateBookAndAdd(ProductFactory factory)
    {
        Console.WriteLine("Scegli il tipo di libro da creare:");
        Console.WriteLine("1. Libro digitale");
        Console.WriteLine("2. Libro stampa");
        Console.Write("Scelta: ");
        string? scelta = Console.ReadLine();
        string tipo = scelta switch { "1" => "BOOK_DIGITAL", "2" => "BOOK_PRINT", _ => "" };
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
        else Console.WriteLine("Quantità non valida.");
    }

    /// <summary>
    /// Rimuove un articolo dall'inventario.
    /// </summary>
    static void RemoveItemMenu()
    {
        Console.Write("Nome articolo da rimuovere: ");
        string? item = Console.ReadLine();
        Console.Write("Quantità: ");
        if (int.TryParse(Console.ReadLine(), out int quantity))
        {
            Inventory.Instance.RemoveItem(item!, quantity);
        }
        else Console.WriteLine("Quantità non valida.");
    }

    /// <summary>
    /// Gestisce la piazzatura di un ordine e selezione del metodo di pagamento.
    /// </summary>
    static void PlaceOrderMenu(OrderService orderService, PaymentFactory paymentFactory)
    {
        Console.Write("Articolo da ordinare: ");
        string? item = Console.ReadLine();
        Console.Write("Quantità: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Quantità non valida.");
            return;
        }

        Console.WriteLine("Scegli il metodo di pagamento:");
        Console.WriteLine("1. Paypal");
        Console.WriteLine("2. Stripe");
        Console.Write("Scelta: ");
        string? scelta = Console.ReadLine();
        string tipoPagamento = scelta switch { "1" => "PAYPAL", "2" => "STRIPE", _ => "PAYPAL" };

        IPaymentProcessor paymentProcessor = paymentFactory.CreaPagamento(tipoPagamento);
        orderService.PlaceOrder(item!, quantity, paymentProcessor);
    }
}
#endregion
