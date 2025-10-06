using System;

class Program
{
    static string[] gusti = { "Cioccolato", "Vaniglia", "Fragola", "Pistacchio", "Limone" };
    static double[] prezzi = { 1.50, 1.20, 1.00, 1.80, 1.30 };

    static void StampaMenu()
    {
        Console.WriteLine("------ MENU GELATERIA ------");
        for (int i = 0; i < gusti.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {gusti[i]} - {prezzi[i]}€ per pallina");
        }
        Console.WriteLine("-----------------------------");
    }
    static double CalcolaTotale(int gusto, int quantita)
    {
        return prezzi[gusto] * quantita;
    }

    static void Main()
    {
        double totale = 0;
        double sogliaSconto = 10.0;
        double sconto = 0.10;

        List<string> gustiOrdinati = new List<string>();
        List<int> quantitaOrdinata = new List<int>();
        List<double> subtotali = new List<double>();

        bool continua = true;

        

        while (continua)
        {
            StampaMenu();

            Console.WriteLine("Scegli un gusto (1-5): ");
            if (!int.TryParse(Console.ReadLine(), out int scelta) || scelta < 1 || scelta > 5)
            {
                Console.WriteLine("Scelta non valida.\n");
                continue;
            }

            Console.Write("Inserisci la quantità di palline: ");
            if (!int.TryParse(Console.ReadLine(), out int quantita) || quantita <= 0)
            {
                Console.WriteLine("Quantità non valida. Riprova.\n");
                continue;
            }

            double subtotale = CalcolaTotale(scelta - 1, quantita);
            totale += subtotale;

            gustiOrdinati.Add(gusti[scelta - 1]);
            quantitaOrdinata.Add(quantita);
            subtotali.Add(subtotale);

            Console.Write("Vuoi aggiungere un altro gusto? (s/n): ");
            string risposta = Console.ReadLine().Trim().ToLower();
            if (risposta != "s")
                continua = false;

            Console.WriteLine();
        }

        double totaleFinale = totale;
        double valoreSconto = 0;
        if (totale > sogliaSconto)
        {
            valoreSconto = totale * sconto;
            totaleFinale -= valoreSconto;
        }


        Console.WriteLine("\nRiepilogo ordine:");
        for (int i = 0; i < gustiOrdinati.Count; i++)
        {
            Console.WriteLine($"{gustiOrdinati[i]} x {quantitaOrdinata[i]} = {subtotali[i]:0.00}€");
        }
        Console.WriteLine($"Totale parziale: {totale:0.00}€");
        if (valoreSconto > 0)
            Console.WriteLine($"Sconto (10%): -{valoreSconto}€");
        Console.WriteLine($"Totale finale: {totaleFinale}€");
    }
}
