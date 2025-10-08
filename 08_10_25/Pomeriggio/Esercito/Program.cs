namespace Esercito
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Esercito> eserciti = new List<Esercito>();

            int scelta = 0;

            do
            {
                Console.WriteLine("=== GESTIONE ESERCITI ===");
                Console.WriteLine("1 - Crea nuovo esercito");
                Console.WriteLine("2 - Gestisci esercito esistente");
                Console.WriteLine("3 - Mostra tutti gli eserciti");
                Console.WriteLine("4 - Esci");
                Console.Write("Inserisci la tua scelta: ");

                if (!int.TryParse(Console.ReadLine(), out scelta))
                {
                    Console.WriteLine("Input non valido.\n");
                    continue;
                }

                switch (scelta)
                {
                    case 1:
                        // Creazione di un nuovo esercito
                        Console.Write("Inserisci il nome del nuovo esercito: ");
                        string? nomeEsercito = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(nomeEsercito)) nomeEsercito = "Esercito Sconosciuto";
                        Esercito nuovoEsercito = new Esercito(nomeEsercito);
                        eserciti.Add(nuovoEsercito);
                        Console.WriteLine($"Esercito '{nuovoEsercito.Nome}' creato.\n");
                        break;

                    case 2:
                        if (eserciti.Count == 0)
                        {
                            Console.WriteLine("Nessun esercito disponibile.\n");
                            break;
                        }

                        // Mostro gli eserciti con indice
                        Console.WriteLine("Seleziona l'esercito da gestire:");
                        for (int i = 0; i < eserciti.Count; i++)
                        {
                            Console.WriteLine($"{i + 1} - {eserciti[i].Nome}");
                        }

                        if (!int.TryParse(Console.ReadLine(), out int indice) || indice < 1 || indice > eserciti.Count)
                        {
                            Console.WriteLine("Scelta non valida.\n");
                            break;
                        }

                        GestisciEsercito(eserciti[indice - 1]);
                        break;

                    case 3:
                        if (eserciti.Count == 0)
                        {
                            Console.WriteLine("Nessun esercito creato.\n");
                        }
                        else
                        {
                            Console.WriteLine("\n--- Elenco eserciti ---");
                            foreach (var e in eserciti)
                            {
                                Console.WriteLine($"- {e.Nome} (Totale soldati: {e.TotaleSoldati})");
                            }
                            Console.WriteLine();
                        }
                        break;

                    case 4:
                        Console.WriteLine("Uscita dal programma.");
                        break;

                    default:
                        Console.WriteLine("Scelta non valida.\n");
                        break;
                }

            } while (scelta != 4);
        }

        // Metodo per gestire un singolo esercito
        static void GestisciEsercito(Esercito esercito)
        {
            int scelta = 0;

            do
            {
                Console.WriteLine($"\n=== Gestione Esercito '{esercito.Nome}' ===");
                Console.WriteLine("1 - Aggiungi Fante");
                Console.WriteLine("2 - Aggiungi Artigliere");
                Console.WriteLine("3 - Mostra Soldati");
                Console.WriteLine("4 - Torna al menu principale");
                Console.Write("Inserisci la tua scelta: ");

                if (!int.TryParse(Console.ReadLine(), out scelta))
                {
                    Console.WriteLine("Input non valido.\n");
                    continue;
                }

                switch (scelta)
                {
                    case 1:
                        Fante fante = new Fante();
                        Console.Write("Nome: "); fante.Nome = Console.ReadLine();
                        Console.Write("Grado: "); fante.Grado = Console.ReadLine();
                        Console.Write("Anni di servizio: ");
                        if (!int.TryParse(Console.ReadLine(), out int anniFante)) anniFante = 0;
                        fante.AnniDiServizio = anniFante;
                        Console.Write("Arma: "); fante.Arma = Console.ReadLine();
                        esercito.AggiungiSoldato(fante);
                        break;

                    case 2:
                        Artigliere art = new Artigliere();
                        Console.Write("Nome: "); art.Nome = Console.ReadLine();
                        Console.Write("Grado: "); art.Grado = Console.ReadLine();
                        Console.Write("Anni di servizio: ");
                        if (!int.TryParse(Console.ReadLine(), out int anniArt)) anniArt = 0;
                        art.AnniDiServizio = anniArt;
                        Console.Write("Calibro (mm): "); if (!int.TryParse(Console.ReadLine(), out int cal)) cal = 7;
                        art.Calibro = cal;
                        esercito.AggiungiSoldato(art);
                        break;

                    case 3:
                        esercito.MostraSoldati();
                        break;

                    case 4:
                        // Torna al menu principale
                        break;

                    default:
                        Console.WriteLine("Scelta non valida.\n");
                        break;
                }

            } while (scelta != 4);
        }
    }
}
