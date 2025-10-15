namespace Observer
{
    public class Program
    {
        public static void Main()
        {
            /*var subject = new CentroMeteo();

            var displayA = new DisplayConsole("Display A");
            var displayB = new DisplayMobile("Display B");

            // Registrazione degli observer
            subject.Attach(displayA);
            subject.Attach(displayB);

            Console.WriteLine("Inserisci il messaggio meteo: ");
            var message = Console.ReadLine();
            subject.Data = message;

            Console.WriteLine("Inserisci un altro messaggio meteo: ");
            message = Console.ReadLine();
            subject.Data = message;
            */

            var subjects = new List<ISoggetto>();
            var observers = new List<IObserver>();

            while (true)
            {
                Console.WriteLine("1. Crea un nuovo soggetto (CentroMeteo)");
                Console.WriteLine("2. Crea un nuovo observer (DisplayConsole o DisplayMobile)");
                Console.WriteLine("3. Registra un observer a un soggetto");
                Console.WriteLine("4. Invia un aggiornamento dal soggetto");
                Console.WriteLine("5. Esci");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var newSubject = new CentroMeteo();
                        subjects.Add(newSubject);
                        Console.WriteLine($"Nuovo soggetto creato con ID {subjects.Count - 1}");
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine("Inserisci il tipo di display (console/mobile): ");
                        var type = Console.ReadLine();
                        Console.WriteLine("Inserisci il nome del display: ");
                        var name = Console.ReadLine();
                        IObserver newObserver = type.ToLower() == "console" ? new DisplayConsole(name) : new DisplayMobile(name);
                        observers.Add(newObserver);
                        Console.WriteLine($"Nuovo observer creato con ID {observers.Count - 1}");
                        Console.WriteLine();
                        break;
                    case "3":
                        DisplayLists(subjects, observers);
                        Console.WriteLine("Inserisci l'ID del soggetto: ");
                        var subjectId = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("Inserisci l'ID dell'observer: ");
                        var observerId = int.Parse(Console.ReadLine() ?? "0");
                        subjects[subjectId].Attach(observers[observerId]);
                        Console.WriteLine($"Observer {observerId} registrato al soggetto {subjectId}");
                        Console.WriteLine();
                        break;
                    case "4":
                        DisplayLists(subjects, observers);
                        Console.WriteLine("Inserisci l'ID del soggetto: ");
                        subjectId = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine("Inserisci il messaggio meteo: ");
                        var message = Console.ReadLine();
                        if (subjects[subjectId] is CentroMeteo centroMeteo)
                        {
                            centroMeteo.Data = message;
                            Console.WriteLine($"Aggiornamento inviato dal soggetto {subjectId}");
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Soggetto non valido.");
                        }
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Scelta non valida.");
                        break;

                }

            }
        }

        public static void DisplayLists(List<ISoggetto> subjects, List<IObserver> observers)
        {
            Console.WriteLine("Soggetti:");
            for (int i = 0; i < subjects.Count; i++)
            {
                Console.WriteLine($"ID {i}: {subjects[i].GetType().Name}");
            }

            Console.WriteLine("Observers:");
            for (int i = 0; i < observers.Count; i++)
            {
                Console.WriteLine($"ID {i}: {observers[i].GetType().Name}");
            }
        }
    }
}