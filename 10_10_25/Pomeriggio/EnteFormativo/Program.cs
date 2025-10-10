namespace EnteFormativo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Docente> docenti = new List<Docente>();
            while(true)
            {   
                Console.WriteLine("=== Menù ===");
                Console.WriteLine("1 - Inserisci docente");
                Console.WriteLine("2 - Rimuovi docente");
                Console.WriteLine("3 - Assegna corso a docente");
                Console.WriteLine("4 - Visualizza docenti e corsi");
                Console.WriteLine("5 - Esci");
                Console.Write("Seleziona un'opzione: ");
                string? scelta = Console.ReadLine();

                switch (scelta)
                {
                    case "1":
                        Docente nuovoDocente = new Docente();
                        Console.Write("Inserisci il nome del docente: ");
                        nuovoDocente.Nome = Console.ReadLine();
                        Console.Write("Inserisci la materia del docente: ");
                        nuovoDocente.Materia = Console.ReadLine();
                        docenti.Add(nuovoDocente);
                        Console.WriteLine("Docente inserito con successo.");
                        break;
                    case "2":
                        Console.Write("Inserisci il nome del docente da rimuovere: ");
                        string? nomeDocente = Console.ReadLine();
                        for (int i = 0; i < docenti.Count; i++)
                        {
                            if (docenti[i].Nome == nomeDocente)
                            {
                                docenti.RemoveAt(i);
                                Console.WriteLine("Docente rimosso con successo.");
                                break;
                            }
                        }
                        break;
                    case "3":
                        Console.Write("Inserisci il nome del docente a cui assegnare un corso: ");
                        string? nomeDocenteAssegna = Console.ReadLine();
                        foreach (var docente in docenti)
                        {
                            if (docente.Nome == nomeDocenteAssegna)
                            {
                                Console.WriteLine("Seleziona il tipo di corso da assegnare:");
                                Console.WriteLine("1 - Corso in Presenza");
                                Console.WriteLine("2 - Corso Online");
                                string? tipoCorso = Console.ReadLine();
                                //Corso corsoDaAssegnare = null;
                                if (tipoCorso == "1")
                                {
                                    CorsoInPresenza corsoPresenza = new CorsoInPresenza();
                                    Console.Write("Inserisci il titolo del corso: ");
                                    corsoPresenza.Titolo = Console.ReadLine();
                                    Console.Write("Inserisci la durata del corso (in ore): ");
                                    corsoPresenza.Durata = int.Parse(Console.ReadLine() ?? "0");
                                    Console.Write("Inserisci il numero dell'aula: ");
                                    corsoPresenza.Aula = int.Parse(Console.ReadLine() ?? "0");
                                    Console.Write("Inserisci il numero di posti disponibili: ");
                                    corsoPresenza.NumeroPosti = int.Parse(Console.ReadLine() ?? "0");
                                    docente.AssegnaCorso(corsoPresenza);
                                }
                                else if (tipoCorso == "2")
                                {
                                    CorsoOnline corsoOnline = new CorsoOnline();
                                    Console.Write("Inserisci il titolo del corso: ");
                                    corsoOnline.Titolo = Console.ReadLine();
                                    Console.Write("Inserisci la durata del corso (in ore): ");
                                    corsoOnline.Durata = int.Parse(Console.ReadLine() ?? "0");
                                    Console.Write("Inserisci la piattaforma: ");
                                    corsoOnline.Piattaforma = Console.ReadLine();
                                    Console.Write("Inserisci il link di accesso: ");
                                    corsoOnline.LinkAccesso = Console.ReadLine();
                                    docente.AssegnaCorso(corsoOnline);
                                }
                            }

                        }
                        break;
                    case "4":
                        foreach (var docente in docenti)
                        {
                            docente.StampaCorsiAssegnati();
                            Console.WriteLine();
                        }
                        break;
                    case "5":
                        Console.WriteLine("Uscita dal programma.");
                        return;
                    default:
                        Console.WriteLine("Opzione non valida. Riprova.");
                        break;
                }
            }


        }
    }
}