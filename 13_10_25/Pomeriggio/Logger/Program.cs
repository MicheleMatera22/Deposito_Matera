using System;
namespace Logger;

class Program
{
    static void Main()
    {
        // Logger
        Logger logger1 = Logger.GetIstanza();
        Logger logger2 = Logger.GetIstanza();

        logger2.Log("Esecuzione operazione 1");
        logger1.Log("Esecuzione operazione 2");

        Console.WriteLine("Contenuto del log:");
        logger1.StampaLog();
        Console.WriteLine("\n");
        logger2.StampaLog();

        // Contatore (interi)
        Contatore contatore1 = Contatore.GetIstanza();
        Contatore contatore2 = Contatore.GetIstanza();

        contatore1.Aggiungi(5);
        contatore2.Aggiungi(10);

        Console.WriteLine("\nContenuto del contatore:");
        contatore1.Stampa();
        Console.WriteLine("\n");
        contatore2.Stampa();

        // Contatore (double)
        Misuratore misuratore1 = Misuratore.GetIstanza();
        Misuratore misuratore2 = Misuratore.GetIstanza();

        misuratore1.Aggiungi(3.14);
        misuratore2.Aggiungi(2.71);

        Console.WriteLine("\nContenuto del misuratore:");
        misuratore1.Stampa();
        Console.WriteLine("\n");
        misuratore2.Stampa();
    }
}
