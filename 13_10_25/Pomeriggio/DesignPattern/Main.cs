using System;
using DesignPattern;
class Program
{
    public static void Main()
    {
        Logger logger1 = Logger.GetIstanza();
        logger1.ScriviMessaggio("Avvio del programma.");

        Logger logger2 = Logger.GetIstanza();
        logger2.ScriviMessaggio("Esecuzione di un'altra operazione.");

        if (object.ReferenceEquals(logger1, logger2))
        {
            Console.WriteLine("Entrambi i logger fanno riferimento alla stessa istanza (Singleton).");
        }
        else
        {
            Console.WriteLine("Le istanze sono diverse (errore nel pattern Singleton).");
        }

        Utente utente1 = new Utente("Giorgio");
        Utente utente2 = new Utente("Michele");
        utente1.EseguiAzione("Login");
        utente2.EseguiAzione("Visualizza Documento");
    }
}
