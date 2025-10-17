// Programma di esempio che implementa vari design pattern per la gestione di ordini di prodotti.
// Utilizza Singleton per il contesto dell'app, Factory Method per creare prodotti,
// Decorator per aggiungere funzionalità ai prodotti, Strategy per calcolare i prezzi,
// e Observer per notificare cambiamenti nell'ordine.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


    #region Singleton
    // Classe Singleton per gestire il contesto globale dell'applicazione.
    // Fornisce impostazioni condivise come simbolo valuta, aliquota IVA, e logging.
    public sealed class AppContext
    {
        private static AppContext? istanza;

        // Proprietà per ottenere l'istanza unica (lazy initialization thread-safe).
        public static AppContext GetInstance
        {
            get
            {
                if (istanza == null)
                {
                    if (istanza == null)
                    {
                        istanza = new AppContext();
                    }
                }
                return istanza;
            }
        }
        // Simbolo della valuta utilizzata nell'app.
        public string CurrencySymbol { get; set; } = "€";
        // Aliquota IVA applicata ai prezzi.
        public decimal VatRate { get; set; } = 0.40m;
        // Evento per notificare cambiamenti negli ordini (non utilizzato nel codice attuale).
        public Action<Order> OnOrderChanged { get; set; }
        // Costruttore privato per impedire istanziazioni esterne.
        private AppContext(){}
        // Metodo per loggare messaggi con timestamp.
        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {DateTime.Now:G}: {message}");
        }
    }
    #endregion

    #region INTERFACCIA PRODOTTI E PRODOTTI CONCRETI
    public interface IProduct
    {
        string GetDescription();
        decimal GetPrice();
    }
    public class TShirt : IProduct
    {
        public string GetDescription() => "Maglietta";
        public decimal GetPrice() => 20.00m;
    }

    public class Mug : IProduct
    {
        public string GetDescription() => "Tazza";
        public decimal GetPrice() => 5.00m;
    }

    public class Skin : IProduct
    {
        public string GetDescription() => "Skin";
        public decimal GetPrice() => 9.99m;
    }
    #endregion

    #region Factory Method
    public static class ProductFactory
    {
        public static IProduct CreateProduct(string productCode)
        {
            return productCode.ToUpper() switch
            {
                "TSHIRT" => new TShirt(),
                "MUG" => new Mug(),
                "SKIN" => new Skin(),
                _ => throw new ArgumentException($"Codice prodotto non valido: {productCode}"),
            };
        }
    }
    #endregion

    #region Decorator
    public abstract class ProductDecorator : IProduct
    {
        protected IProduct _product;

        protected ProductDecorator(IProduct product)
        {
            _product = product;
        }

        public virtual string GetDescription() => _product.GetDescription();
        public virtual decimal GetPrice() => _product.GetPrice();
    }
    public class FrontPrintDecorator : ProductDecorator
    {
        public FrontPrintDecorator(IProduct product) : base(product) { }
        public override string GetDescription() => base.GetDescription() + ", con Stampa Frontale";
        public override decimal GetPrice() => base.GetPrice() + 5.00m;
    }

    public class EngravingDecorator : ProductDecorator
    {
        public EngravingDecorator(IProduct product) : base(product) { }
        public override string GetDescription() => base.GetDescription() + ", con Incisione";
        public override decimal GetPrice() => base.GetPrice() + 8.50m;
    }

    public class GiftWrapDecorator : ProductDecorator
    {
        public GiftWrapDecorator(IProduct product) : base(product) { }
        public override string GetDescription() => base.GetDescription() + ", in Confezione Regalo";
        public override decimal GetPrice() => base.GetPrice() + 3.00m;
    }
    public class ExtendedWarrantyDecorator : ProductDecorator
    {
        public ExtendedWarrantyDecorator(IProduct product) : base(product) { }
        public override string GetDescription() => base.GetDescription() + ", con Garanzia Estesa";
        public override decimal GetPrice() => base.GetPrice() + 12.00m;
    }
    #endregion

    #region Strategy
    public interface IPricingStrategy
    {
        decimal CalculateTotal(List<IProduct> items, decimal vatRate);
        string Name { get; }
    }
    public class StandardPricingStrategy : IPricingStrategy
    {
        public string Name => "Standard";
        public decimal CalculateTotal(List<IProduct> items, decimal vatRate)
        {
            decimal total = 0;
            foreach(var v in items)
            {
                total += v.GetPrice();
            }
            return total * (1 + vatRate);
        }
    }
    public class PromoPricingStrategy : IPricingStrategy
    {
        public string Name => "Promozione (10% di sconto)";
        private const decimal Discount = 0.10m;

        public decimal CalculateTotal(List<IProduct> items, decimal vatRate)
        {
            decimal total = 0;
            foreach(var v in items)
            {
                total += v.GetPrice();
            }
            decimal discountedSubtotal = total * (1 - Discount);
            return discountedSubtotal * (1 + vatRate);
        }
    }

public class WholesalePricingStrategy : IPricingStrategy
{
    public string Name => "Prezzo Ingrosso (No IVA, 25% di sconto)";
    private const decimal Discount = 0.25m;

    public decimal CalculateTotal(List<IProduct> items, decimal vatRate)
    {
        decimal total = 0;
        foreach (var v in items)
        {
            total += v.GetPrice();
        }
        return total * (1 - Discount);
    }
}
    #endregion

    #region Observer
    public class Order
    {
        private static int _nextId = 1;
        private readonly List<IProduct> _items = new List<IProduct>();
        private readonly List<IOrderObserver> _observers = new List<IOrderObserver>();
        private IPricingStrategy _pricingStrategy;

        public int OrderId { get; }
        public IReadOnlyList<IProduct> Items => _items.AsReadOnly();
        public IPricingStrategy PricingStrategy => _pricingStrategy;

        public Order(IPricingStrategy initialStrategy)
        {
            _pricingStrategy = initialStrategy;
            OrderId = _nextId++;
        }

        public void AddObserver(IOrderObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IOrderObserver observer)
        {
            _observers.Remove(observer);
        }

        private void NotifyObservers(string message)
        {
            Console.WriteLine($"\nNotifica agli Observer per: {message}");
            foreach (var observer in _observers)
            {
                observer.Update(this, message);
            }
            Console.WriteLine("\n");
        }


        public void AddItem(IProduct item)
        {
            _items.Add(item);
            NotifyObservers($"Nuovo articolo aggiunto: '{item.GetDescription()}'");
        }

        public void SetPricingStrategy(IPricingStrategy newStrategy)
        {
            _pricingStrategy = newStrategy;
            NotifyObservers($"Strategia di prezzo cambiata in '{newStrategy.Name}'");
        }

        public decimal CalculateTotal()
        {
            return _pricingStrategy.CalculateTotal(_items, AppContext.GetInstance.VatRate);
        }

        public void Checkout()
        {
            NotifyObservers("Checkout dell'ordine avviato.");
            var total = CalculateTotal();
            AppContext.GetInstance.Log($"Ordine {OrderId} concluso. Totale: {AppContext.GetInstance.CurrencySymbol}{total:F2}");
            Console.WriteLine($"\nIl totale finale per l'ordine {OrderId} è {AppContext.GetInstance.CurrencySymbol}{total:F2}");
        }

        public void UpdateItem(int index, IProduct newItem)
        {
            if (index < 0 || index >= _items.Count) return;
            _items[index] = newItem;
            NotifyObservers($"Prodotto aggiornato: {newItem.GetDescription()}");
        }
    }

    public interface IOrderObserver
    {
        void Update(Order order, string message);
    }

    public class UiObserver : IOrderObserver
    {
        public void Update(Order order, string message)
        {
            Console.WriteLine($"[UI Display]: '{message}'. L'ordine {order.OrderId} ha ora {order.Items.Count} articoli.");
        }
    }

    public class LogObserver : IOrderObserver
    {
        public void Update(Order order, string message)
        {
            AppContext.GetInstance.Log($"Aggiornamento Ordine. ID: {order.OrderId}, Evento: {message}");
        }
    }

    public class EmailSmsMockObserver : IOrderObserver
    {
        public void Update(Order order, string message)
        {
            if (message.ToLower().Contains("checkout"))
            {
                Console.WriteLine($"[Mock Email/SMS] Invio conferma per l'ordine {order.OrderId}.");
            }
        }
    }
    #endregion

    #region MAIN
    public class Program
    {
        public static void Main(string[] args)
        {
            var appContext = AppContext.GetInstance;

            var order = new Order(new StandardPricingStrategy());

            var ui = new UiObserver();
            var logger = new LogObserver();
            var notifier = new EmailSmsMockObserver();
            order.AddObserver(ui);
            order.AddObserver(logger);
            order.AddObserver(notifier);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- MENU ORDINE ---");
                Console.WriteLine("1. Aggiungi prodotto");
                Console.WriteLine("2. Aggiungi decoratore a un prodotto");
                Console.WriteLine("3. Cambia strategia di prezzo");
                Console.WriteLine("4. Mostra stato ordine");
                Console.WriteLine("5. Checkout");
                Console.WriteLine("6. Esci");
                Console.Write("Scelta: ");

                string choice = Console.ReadLine() ?? "";
                switch (choice)
                {
                    case "1":
                        Console.Write("Inserisci codice prodotto (TSHIRT, MUG, SKIN): ");
                        string code = Console.ReadLine()?.ToUpper() ?? "";
                        IProduct product = null;
                        if (code == "TSHIRT" || code == "MUG" || code == "SKIN")
                        {
                            product = ProductFactory.CreateProduct(code);
                            order.AddItem(product);
                            Console.WriteLine($"Prodotto '{product.GetDescription()}' aggiunto!");
                        }
                        else
                        {
                            Console.WriteLine("Codice prodotto non valido!");
                        }
                        break;

                    case "2":
                        if (order.Items.Count == 0)
                        {
                            Console.WriteLine("Non ci sono prodotti nell'ordine.");
                            break;
                        }
                        Console.WriteLine("Seleziona il prodotto da decorare:");
                        for (int i = 0; i < order.Items.Count; i++)
                            Console.WriteLine($"{i + 1}. {order.Items[i].GetDescription()}");

                        if (int.TryParse(Console.ReadLine(), out int prodIndex) &&
                            prodIndex > 0 && prodIndex <= order.Items.Count)
                        {
                            IProduct selected = order.Items[prodIndex - 1];
                            Console.WriteLine("Scegli decoratore: 1) Front Print 2) Incisione 3) Regalo 4) Garanzia");
                            string decorChoice = Console.ReadLine() ?? "";
                            IProduct decorated;

                            switch (decorChoice)
                            {
                                case "1":
                                    decorated = new FrontPrintDecorator(selected);
                                    break;
                                case "2":
                                    decorated = new EngravingDecorator(selected);
                                    break;
                                case "3":
                                    decorated = new GiftWrapDecorator(selected);
                                    break;
                                case "4":
                                    decorated = new ExtendedWarrantyDecorator(selected);
                                    break;
                                default:
                                    Console.WriteLine("Decoratore non valido!");
                                    continue;
                            }

                            order.UpdateItem(prodIndex - 1, decorated);
                        }
                        else
                        {
                            Console.WriteLine("Scelta non valida.");
                        }
                        break;

                    case "3":
                        Console.WriteLine("Scegli strategia: 1) Standard 2) Promozione 3) Ingrosso");
                        string strat = Console.ReadLine() ?? "";
                        if (strat == "1") order.SetPricingStrategy(new StandardPricingStrategy());
                        else if (strat == "2") order.SetPricingStrategy(new PromoPricingStrategy());
                        else if (strat == "3") order.SetPricingStrategy(new WholesalePricingStrategy());
                        else Console.WriteLine("Strategia non valida.");
                        break;

                    case "4":
                        Console.WriteLine("\n--- Stato Attuale dell'Ordine ---");
                        foreach (var item in order.Items)
                            Console.WriteLine($"- {item.GetDescription()}, Prezzo: {appContext.CurrencySymbol}{item.GetPrice():F2}");
                        Console.WriteLine($"Totale ({order.PricingStrategy.Name}): {appContext.CurrencySymbol}{order.CalculateTotal():F2}");
                        break;

                    case "5":
                        order.Checkout();
                        break;

                    case "6":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Scelta non valida.");
                        break;
                }
            }

            Console.WriteLine("\n--- Sessione Terminata ---");
        }
    }


    #endregion


