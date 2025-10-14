namespace Config
{
    public class Program
    {
        public static void Main()
        {
            Configurazione moduloA = Configurazione.GetInstance();
            Configurazione moduloB = Configurazione.GetInstance();
            Registro registro = Registro.GetInstance();
            bool continua = true;

            while (continua)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Imposta configurazione");
                Console.WriteLine("2. Leggi configurazione");
                Console.WriteLine("3. Stampa tutte le configurazioni");
                Console.WriteLine("4. Esci");
                Console.WriteLine("5. Creazione dispositivi");
                Console.WriteLine("6. Stampa registro dispositivi");
                Console.Write("Scegli un'opzione: ");
                string scelta = Console.ReadLine();

                switch (scelta)
                {
                    case "1":
                        Console.Write("Inserisci la chiave: ");
                        string chiave = Console.ReadLine();
                        Console.Write("Inserisci il valore: ");
                        string valore = Console.ReadLine();
                        moduloA.Imposta(chiave, valore);
                        Console.Write("Configurazione impostata.");
                        Console.Write("Inserisci la chiave: ");
                        string chiave2 = Console.ReadLine();
                        Console.Write("Inserisci il valore: ");
                        string valore2 = Console.ReadLine();
                        moduloB.Imposta(chiave2, valore2);
                        Console.Write("Configurazione impostata.");

                        break;

                    case "2":
                        Console.Write("Inserisci la chiave da leggere: ");
                        chiave = Console.ReadLine();
                        string risultato = moduloB.Leggi(chiave);
                        if (risultato != null)
                            Console.WriteLine($"Valore per '{chiave}': {risultato}");
                        else
                            Console.WriteLine($"Chiave '{chiave}' non trovata.");
                        break;

                    case "3":
                        Console.WriteLine("Tutte le configurazioni con modulo A:");
                        moduloA.Stampa();
                        Console.WriteLine("Tutte le configurazioni con modulo B:");
                        moduloB.Stampa();

                        if (moduloA.GetHashCode() == moduloB.GetHashCode())
                            Console.WriteLine("Le due istanze sono la stessa (singleton).");
                        else
                            Console.WriteLine("Le due istanze sono diverse.");
                        break;

                    case "4":
                        continua = false;
                        break;
                    case "5":
                        Console.Write("Inserisci il tipo di dispositivo (computer/stampante): ");
                        string tipo = Console.ReadLine();
                        IDispositivo dispositivo = DispositivoFactory.CreaDispositivo(tipo);
                        registro.Aggiungi(dispositivo);
                        Console.WriteLine("Dispositivo creato e registrato.");
                        dispositivo.Avvia();
                        dispositivo.MostraTipo();
                        break;
                    case "6":
                        Console.WriteLine("Dispositivi registrati:");
                        registro.Stampa();
                        break;
                    default:
                        Console.WriteLine("Opzione non valida. Riprova.");
                        break;
                }





                Console.WriteLine();
            }

        }
    }
}