#region  INTERFACE
using System.Reflection.Metadata.Ecma335;

public interface IPiatto
{
    string Descrizione();
    string Prepara();
}
#endregion

#region CONCRETE STRATEGY

public class Pizza : IPiatto
{
    public string Descrizione()
    {
        return "Pizza Margherita";
    }

    public string Prepara()
    {
        return "Impasto, salsa di pomodoro, mozzarella, basilico, olio d'oliva";
    }
}

public class Hamburger : IPiatto
{
    public string Descrizione()
    {
        return "Hamburger Classico";
    }

    public string Prepara()
    {
        return "Pane, carne di manzo, lattuga, pomodoro, salsa";
    }
}

public class Insalata : IPiatto
{
    public string Descrizione()
    {
        return "Insalata Mista";
    }

    public string Prepara()
    {
        return "Lattuga, pomodori, cetrioli, carote, olio d'oliva, aceto";
    }
}
#endregion

#region DECORATOR

public abstract class PiattoDecorator : IPiatto
{
    protected IPiatto _piatto;

    protected PiattoDecorator(IPiatto piatto)
    {
        _piatto = piatto;
    }

    public virtual string Descrizione()
    {
        return _piatto.Descrizione();
    }

    public virtual string Prepara()
    {
        return _piatto.Prepara();
    }
}

public class ConFormaggio : PiattoDecorator
{
    public ConFormaggio(IPiatto piatto) : base(piatto) { }

    public override string Descrizione()
    {
        return _piatto.Descrizione() + " + Formaggio Extra";
    }

    public override string Prepara()
    {
        return _piatto.Prepara() + ", aggiungi formaggio extra";
    }
}

public class ConBacon : PiattoDecorator
{
    public ConBacon(IPiatto piatto) : base(piatto) { }

    public override string Descrizione()
    {
        return _piatto.Descrizione() + " + Bacon Extra";
    }

    public override string Prepara()
    {
        return _piatto.Prepara() + ", aggiungi bacon croccante extra";
    }
}

public class ConSalsa : PiattoDecorator
{
    public ConSalsa(IPiatto piatto) : base(piatto) { }

    public override string Descrizione()
    {
        return _piatto.Descrizione() + " + Salsa Extra";
    }

    public override string Prepara()
    {
        return _piatto.Prepara() + ", aggiungi salsa extra";
    }
}

#endregion

#region FACTORY

public static class PiattoFactory
{
    public static IPiatto CreaPiatto(string tipo)
    {
        return tipo.ToLower() switch
        {
            "pizza" => new Pizza(),
            "hamburger" => new Hamburger(),
            "insalata" => new Insalata(),
            _ => throw new ArgumentException("Tipo di piatto non riconosciuto.")
        };
    }
}

#endregion

#region STRATEGY

public interface IStrategy
{
    string Prepara(string descrizione);
}

public class Fritto : IStrategy
{
    public string Prepara(string descrizione)
    {
        return $"Fritto: {descrizione} è stato fritto in olio caldo.";
    }
}

public class AlForno : IStrategy
{
    public string Prepara(string descrizione)
    {
        return $"Al Forno: {descrizione} è stato cotto in forno a 200°C per 20 minuti.";
    }
}

public class AllaGriglia : IStrategy
{
    public string Prepara(string descrizione)
    {
        return $"Alla Griglia: {descrizione} è stato grigliato fino a doratura.";
    }
}

#endregion

#region CONTEXT

public class Chef
{
    private IStrategy? _strategia;

    public void ImpostaStrategia(IStrategy strategia)
    {
        _strategia = strategia;
    }

    public void PreparaPiatto(IPiatto piatto)
    {
        if (_strategia == null)
        {
            Console.WriteLine("Nessuna strategia di cottura impostata.");
            return;
        }
        string preparazione = _strategia.Prepara(piatto.Descrizione());
        Console.WriteLine(preparazione);
        Console.WriteLine($"Dettagli preparazione: {piatto.Prepara()}");
    }
}
#endregion

#region PROGRAM

public class Program
{
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("Scegli un piatto (pizza, hamburger, insalata) o 'exit' per uscire:");
            string? sceltaPiatto = Console.ReadLine();
            switch (sceltaPiatto)
            {
                case "pizza":
                    var pizza = PiattoFactory.CreaPiatto("pizza");
                    pizza = AggiungiIngredienti(pizza);
                    pizza = ScegliTipoCottura(pizza);
                    break;
                case "hamburger":
                    var hamburger = PiattoFactory.CreaPiatto("hamburger");
                    hamburger = AggiungiIngredienti(hamburger);
                    hamburger = ScegliTipoCottura(hamburger);
                    break;
                case "insalata":
                    var insalata = PiattoFactory.CreaPiatto("insalata");
                    insalata = AggiungiIngredienti(insalata);
                    //insalata = ScegliTipoCottura(insalata);
                    break;
                case "exit":
                    return;
                default:
                    Console.WriteLine("Piatto non riconosciuto.");
                    break;


            }
        }
    }

    public static IPiatto AggiungiIngredienti(IPiatto piatto)
    {
            while (true)
            {
                Console.WriteLine("Vuoi aggiungere formaggio, bacon o salsa? (scrivi 'no' per terminare)");
                string? ingrediente = Console.ReadLine();
                switch (ingrediente)
                {
                    case "formaggio":
                        piatto = new ConFormaggio(piatto);
                        break;
                    case "bacon":
                        piatto = new ConBacon(piatto);
                        break;
                    case "salsa":
                        piatto = new ConSalsa(piatto);
                        break;
                    case "no":
                        Console.WriteLine($"Piatto: {piatto.Descrizione()}");
                        return piatto;
                    default:
                        Console.WriteLine("Ingrediente non riconosciuto.");
                        break;
                }

            }
    }

    public static IPiatto ScegliTipoCottura(IPiatto piatto)
    {
            var chef = new Chef();
            Console.WriteLine("Scegli il tipo di cottura (fritto, al forno, alla griglia):");
            string? tipoCottura = Console.ReadLine();
            switch (tipoCottura)
            {
                case "fritto":
                    chef.ImpostaStrategia(new Fritto());
                    break;
                case "al forno":
                    chef.ImpostaStrategia(new AlForno());
                    break;
                case "alla griglia":
                    chef.ImpostaStrategia(new AllaGriglia());
                    break;
                default:
                    Console.WriteLine("Tipo di cottura non riconosciuto. Usando 'al forno' come default.");
                    chef.ImpostaStrategia(new AlForno());
                    break;
            }
            chef.PreparaPiatto(piatto);
            return piatto;
    }
    
}

#endregion