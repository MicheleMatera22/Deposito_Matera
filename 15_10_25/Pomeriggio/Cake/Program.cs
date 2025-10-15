#region INTERFACE

public interface ITorta
{
    string Descrizione();
}
#endregion

#region CLASSI CONCRETE
public class TortaCioccolato : ITorta
{
    public string Descrizione()
    {
        return "Torta al Cioccolato";
    }
}

public class TortaVaniglia : ITorta
{
    public string Descrizione()
    {
        return "Torta alla Vaniglia";
    }
}

public class TortaFrutta : ITorta
{
    public string Descrizione()
    {
        return "Torta alla Frutta";
    }
}
#endregion

#region DECORATOR
public abstract class TortaDecorator : ITorta
{
    protected ITorta _torta;

    protected TortaDecorator(ITorta torta)
    {
        _torta = torta;
    }

    public virtual string Descrizione()
    {
        return _torta.Descrizione();
    }
}
#endregion

#region DECORATOR CONCRETE
public class ConPanna : TortaDecorator
{
    public ConPanna(ITorta torta) : base(torta) { }

    public override string Descrizione()
    {
        return base.Descrizione() + " con Panna";
    }
}

public class ConFragole : TortaDecorator
{
    public ConFragole(ITorta torta) : base(torta) { }

    public override string Descrizione()
    {
        return base.Descrizione() + " con Fragole";
    }
}

public class ConGlassa : TortaDecorator
{
    public ConGlassa(ITorta torta) : base(torta) { }

    public override string Descrizione()
    {
        return base.Descrizione() + " con Glassa";
    }
}

#endregion

#region FACTORY CLASS
public static class TortaFactory
{
    public static ITorta CreaTorta(string tipo)
    {
        return tipo.ToLower() switch
        {
            "cioccolato" => new TortaCioccolato(),
            "vaniglia" => new TortaVaniglia(),
            "frutta" => new TortaFrutta(),
            _ => throw new ArgumentException("Tipo di torta non riconosciuto"),
        };
    }
}

#endregion

#region SINGLETON
public sealed class NegozioTorte
{
    private static NegozioTorte? _istanza;
    private List<ITorta> torte = new List<ITorta>();

    private NegozioTorte() { }

    public static NegozioTorte GetIstance()
    {
        if (_istanza == null)
        {
            _istanza = new NegozioTorte();
        }
        return _istanza;
    }

    public void AggiungiTorta(ITorta torta)
    {
        torte.Add(torta);
    }
}
#endregion

#region PROGRAM
class Program
{
    static void Main()
    {
        Console.WriteLine("🍰 Benvenuto nel Negozio di Torte!");
        Console.WriteLine("Scegli la torta base:");
        Console.WriteLine("1. Cioccolato");
        Console.WriteLine("2. Vaniglia");
        Console.WriteLine("3. Frutta");
        Console.Write("Scelta: ");
        ITorta tortaBase;
        switch (Console.ReadLine())
        {
            case "1":
                tortaBase = TortaFactory.CreaTorta("cioccolato");
                break;
            case "2":
                tortaBase = TortaFactory.CreaTorta("vaniglia");
                break;
            case "3":
                tortaBase = TortaFactory.CreaTorta("frutta");
                break;
            default:
                Console.WriteLine("Scelta non valida, si predefinisce Cioccolato.");
                tortaBase = TortaFactory.CreaTorta("cioccolato");
                break;
        }
        
        ITorta tortaFinale = tortaBase;

        bool aggiungiAltro = true;

        while (aggiungiAltro)
        {
            Console.WriteLine("\nVuoi aggiungere un ingrediente extra?");
            Console.WriteLine("1. Panna");
            Console.WriteLine("2. Fragole");
            Console.WriteLine("3. Glassa");
            Console.WriteLine("0. Nessun altro ingrediente");
            Console.Write("Scelta: ");
            string sceltaExtra = Console.ReadLine() ?? "";

            switch (sceltaExtra)
            {
                case "1":
                    tortaFinale = new ConPanna(tortaFinale);
                    break;
                case "2":
                    tortaFinale = new ConFragole(tortaFinale);
                    break;
                case "3":
                    tortaFinale = new ConGlassa(tortaFinale);
                    break;
                case "0":
                    aggiungiAltro = false;
                    continue;
                default:
                    Console.WriteLine("Scelta non valida, riprova.");
                    continue;
            }

            Console.WriteLine("Ingrediente aggiunto!");
        }

        var negozio = NegozioTorte.GetIstance();
        negozio.AggiungiTorta(tortaFinale);

        Console.WriteLine("\nTorta completata!");
        Console.WriteLine($"Descrizione: {tortaFinale.Descrizione()}");

        Console.WriteLine("\nPremi un tasto per uscire...");
        Console.ReadKey();
    }
}
#endregion