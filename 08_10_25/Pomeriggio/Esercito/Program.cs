namespace Esercito
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Soldato> esercito = new List<Soldato>();

            int scelta = 0;

            do
            {
                Console.WriteLine("1 - Aggiungi Fante");
                Console.WriteLine("2 - Aggiungi Artigliere");
                Console.WriteLine("3 - Mostra Soldati");
                Console.WriteLine("4 - Esci");
                Console.WriteLine("Inserisci la tua scelta:");
                scelta = int.Parse(Console.ReadLine());
                switch (scelta)
                {
                    case 1:
                        Fante fante = new Fante();
                        Console.WriteLine("Inserisci il nome del fante:");
                        fante.Nome = Console.ReadLine();
                        Console.WriteLine("Inserisci il grado del fante:");
                        fante.Grado = Console.ReadLine();
                        Console.WriteLine("Inserisci gli anni di servizio del fante:");
                        fante.AnniDiServizio = int.Parse(Console.ReadLine());
                        Console.WriteLine("Inserisci l'arma del fante:");
                        fante.Arma = Console.ReadLine();
                        esercito.Add(fante);
                        break;
                    case 2:
                        Artigliere artigliere = new Artigliere();
                        Console.WriteLine("Inserisci il nome dell'artigliere:");
                        artigliere.Nome = Console.ReadLine();
                        Console.WriteLine("Inserisci il grado dell'artigliere:");
                        artigliere.Grado = Console.ReadLine();
                        Console.WriteLine("Inserisci gli anni di servizio dell'artigliere:");
                        artigliere.AnniDiServizio = int.Parse(Console.ReadLine());
                        Console.WriteLine("Inserisci il calibro dell'artigliere (in mm):");
                        artigliere.Calibro = int.Parse(Console.ReadLine());
                        esercito.Add(artigliere);
                        break;
                    case 3:
                        foreach (var soldato in esercito)
                        {
                            soldato.Descrizione();
                            Console.WriteLine();
                        }
                        break;
                    case 4:
                        Console.WriteLine("Uscita dal programma.");
                        break;
                    default:
                        Console.WriteLine("Scelta non valida. Riprova.");
                        break;
                }
            }
            while (scelta != 4);
            
        }
    }
}
