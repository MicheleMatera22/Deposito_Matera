#region INTERFACE

public interface IStrategiaOperazione
{
    double Calcola(double a, double b);
}
#endregion

#region CONCRETE STRATEGY
public class SommaStrategia : IStrategiaOperazione
{
    public double Calcola(double a, double b)
    {
        return a + b;
    }
}

public class SottrazioneStrategia : IStrategiaOperazione
{
    public double Calcola(double a, double b)
    {
        return a - b;
    }
}

public class MoltiplicazioneStrategia : IStrategiaOperazione
{
    public double Calcola(double a, double b)
    {
        return a * b;
    }
}

public class DivisioneStrategia : IStrategiaOperazione
{
    public double Calcola(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("Divisione per zero non permessa.");
        return a / b;
    }
}

#endregion

#region CONTEXT

public class Calcolatrice
{
    private IStrategiaOperazione _strategia;

    public void ImpostaStrategia(IStrategiaOperazione strategia)
    {
        _strategia = strategia;
    }

    public double EseguiOperazione(double a, double b)
    {
        if (_strategia == null)
        {
            Console.WriteLine("Nessuna strategia impostata.");
            return 0;
        }
        double result = _strategia.Calcola(a, b);
        Console.WriteLine($"Risultato dell'operazione: {result}");
        return result;
    }
}
#endregion

#region SINGLETON

public sealed class User
{
    private static User? istanza;
    private List<double> risultati = new List<double>();
    private User() { }
    public static User GetIstanza()
    {
        if (istanza == null)
        {
            istanza = new User();
        }
        return istanza;
    }

    public void AggiungiRisultato(double risultato)
    {
        risultati.Add(risultato);
    }

    public void StampaRisultati()
    {
        foreach (var risultato in risultati)
        {
            Console.WriteLine(risultato);
        }
    }
}
#endregion

#region PROGRAM

public class Program
{
    public static void Main(string[] args)
    {
        var calcolatrice = new Calcolatrice();
        var user = User.GetIstanza();
        double risultato;

        while (true)
        {
            Console.WriteLine("Scegli l'operazione (+, -, *, /) o 'exit' per uscire:");
            string operazione = Console.ReadLine();
            switch (operazione)
            {
                case "+":
                    calcolatrice.ImpostaStrategia(new SommaStrategia());
                    InserisciNumeri(out double a, out double b);
                    risultato = calcolatrice.EseguiOperazione(a, b);
                    user.AggiungiRisultato(risultato);
                    break;
                case "-":
                    calcolatrice.ImpostaStrategia(new SottrazioneStrategia());
                    InserisciNumeri(out a, out b);
                    risultato = calcolatrice.EseguiOperazione(a, b);
                    user.AggiungiRisultato(risultato);
                    break;
                case "*":
                    calcolatrice.ImpostaStrategia(new MoltiplicazioneStrategia());
                    InserisciNumeri(out a, out b);
                    risultato = calcolatrice.EseguiOperazione(a, b);
                    user.AggiungiRisultato(risultato);
                    break;
                case "/":
                    calcolatrice.ImpostaStrategia(new DivisioneStrategia());
                    do
                    {
                        InserisciNumeri(out a, out b);
                        if (a == 0)
                            Console.WriteLine("Il divisore non può essere zero. Inserisci un altro numero:");
                    } while (a == 0);
                    risultato = calcolatrice.EseguiOperazione(a, b);
                    user.AggiungiRisultato(risultato);
                    break;
                case "exit":
                    return;
                default:
                    Console.WriteLine("Operazione non valida.");
                    continue;
            }

        }
        Console.WriteLine("\nStorico risultati:");
        user.StampaRisultati();
    }

    public static void InserisciNumeri(out double a, out double b)
    {
        Console.WriteLine("Inserisci il primo numero:");
        while (!double.TryParse(Console.ReadLine(), out a))
            Console.WriteLine("Valore non valido, inserisci un numero:");

        Console.WriteLine("Inserisci il secondo numero:");
        while (!double.TryParse(Console.ReadLine(), out b))
            Console.WriteLine("Valore non valido, inserisci un numero:");
    }
}
#endregion