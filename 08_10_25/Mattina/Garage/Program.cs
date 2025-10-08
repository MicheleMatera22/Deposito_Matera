namespace Garage
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lista che memorizza tutti i veicoli presenti nel garage
            List<Veicolo> garage = new List<Veicolo>();
            bool esci = false;

            // Ciclo principale del menu
            while (!esci)
            {
                Console.WriteLine("\n===== MENU GARAGE =====");
                Console.WriteLine("[1] Inserisci un nuovo veicolo (Auto o Moto)");
                Console.WriteLine("[2] Visualizza tutti i veicoli");
                Console.WriteLine("[0] Esci");
                Console.Write("Scelta: ");

                string? scelta = Console.ReadLine();
                Console.WriteLine();

                switch (scelta)
                {
                    case "1":
                        // Inserimento di un nuovo veicolo
                        Console.Write("Inserisci tipo veicolo (A=Auto, M=Moto): ");
                        string? tipo = Console.ReadLine().ToUpper();

                        Console.Write("Marca: ");
                        string? marca = Console.ReadLine();
                        Console.Write("Modello: ");
                        string? modello = Console.ReadLine();

                        if (tipo == "A")
                        {
                            // Creazione di un oggetto Auto
                            Console.Write("Numero porte: ");
                            int porte = int.Parse(Console.ReadLine());

                            Auto auto = new Auto
                            {
                                Marca = marca,
                                Modello = modello,
                                NumeroPorte = porte
                            };

                            garage.Add(auto);
                            Console.WriteLine("Auto aggiunta al garage!");
                        }
                        else if (tipo == "M")
                        {
                            // Creazione di un oggetto Moto
                            Console.Write("Tipo manubrio: ");
                            string manubrio = Console.ReadLine();

                            Moto moto = new Moto
                            {
                                Marca = marca,
                                Modello = modello,
                                TipoManubrio = manubrio
                            };

                            garage.Add(moto);
                            Console.WriteLine("Moto aggiunta al garage!");
                        }
                        else
                        {
                            Console.WriteLine("Tipo non valido!");
                        }
                        break;

                    case "2":
                        // Visualizzazione dei veicoli presenti nel garage
                        if (garage.Count == 0)
                        {
                            Console.WriteLine("Il garage è vuoto.");
                        }
                        else
                        {
                            Console.WriteLine("=== VEICOLI PRESENTI NEL GARAGE ===");
                            foreach (var v in garage)
                            {
                                v.StampaInfo();
                            }
                        }
                        break;

                    case "0":
                        // Uscita dal programma
                        esci = true;
                        Console.WriteLine("Uscita dal programma...");
                        break;

                    default:
                        Console.WriteLine("Scelta non valida!");
                        break;
                }
            }
        }
    }

}
