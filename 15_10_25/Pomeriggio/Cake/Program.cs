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
        return base.Descrizione() + ", Panna";
    }
}

public class ConFragole : TortaDecorator
{
    public ConFragole(ITorta torta) : base(torta) { }

    public override string Descrizione()
    {
        return base.Descrizione() + ", Fragole";
    }
}

public class ConGlassa : TortaDecorator
{
    public ConGlassa(ITorta torta) : base(torta) { }

    public override string Descrizione()
    {
        return base.Descrizione() + ", Glassa";
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

    public void MostraTorte()
    {
        if (torte.Count == 0)
        {
            Console.WriteLine("Nessuna torta disponibile.");
        }
        else
        {
            Console.WriteLine("Torte disponibili:");
            foreach (var torta in torte)
            {
                Console.WriteLine(torta.Descrizione());
            } 
        }

    }
}
#endregion

#region PROGRAM
class Program
{
    static void Main()
    {
        var negozio = NegozioTorte.GetIstance();
        bool continua = true;

        while (continua)
        {
            Console.Clear();
            Console.WriteLine("=== NEGOZIO DI TORTE ===");
            Console.WriteLine("1. Crea nuove torte");
            Console.WriteLine("2. Mostra torte nel negozio");
            Console.WriteLine("0. Esci");
            Console.Write("Scelta: ");

            string scelta = Console.ReadLine() ?? "";

            switch (scelta)
            {
                case "1":
                    bool creaAltre = true;
                    while (creaAltre)
                    {
                        CreaTorta(negozio);
                        Console.Write("\nVuoi creare un'altra torta? (s/n): ");
                        string risposta = Console.ReadLine() ?? "";
                        if (risposta.ToLower() != "s")
                            creaAltre = false;
                    }
                    break;

                case "2":
                    negozio.MostraTorte();
                    Console.WriteLine("\nPremi un tasto per continuare...");
                    Console.ReadKey();
                    break;

                case "0":
                    continua = false;
                    Console.WriteLine("Uscita in corso...");
                    break;

                default:
                    Console.WriteLine("Scelta non valida!");
                    Console.WriteLine("Premi un tasto per continuare...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void CreaTorta(NegozioTorte negozio)
    {
        Console.Clear();
        Console.WriteLine("Scegli la torta base:");
        Console.WriteLine("Cioccolato");
        Console.WriteLine("Vaniglia");
        Console.WriteLine("Frutta");
        Console.Write("Scelta: ");

        string sceltaBase = Console.ReadLine() ?? "";
        ITorta tortaBase;

        switch (sceltaBase.ToLower())
        {
            case "cioccolato":
                tortaBase = TortaFactory.CreaTorta("cioccolato");
                break;
            case "vaniglia":
                tortaBase = TortaFactory.CreaTorta("vaniglia");
                break;
            case "frutta":
                tortaBase = TortaFactory.CreaTorta("frutta");
                break;
            default:
                Console.WriteLine("Scelta non valida! Impostato torta al cioccolato come default.");
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
                    Console.WriteLine("Scelta non valida!");
                    continue;
            }

            Console.WriteLine("Ingrediente aggiunto!");
        }

        negozio.AggiungiTorta(tortaFinale);
        Console.WriteLine($"\nTorta completata: {tortaFinale.Descrizione()}");
        Console.WriteLine("Torta aggiunta al negozio!");
    }
}
#endregion