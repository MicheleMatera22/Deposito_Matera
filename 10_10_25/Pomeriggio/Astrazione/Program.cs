using System;
using System.Collections.Generic;
using Astrazione;

namespace MenuDispositivi
{
    class Program
    {
        static void Main(string[] args)
        {
            List<DispositivoElettronico> dispositivi = new List<DispositivoElettronico>();
            int scelta = -1;
            Console.WriteLine("--- Inserimento Dispositivi ---");
            bool inserimento = true;
            while (inserimento)
            {
                Console.WriteLine("Che tipo di dispositivo vuoi inserire?");
                Console.WriteLine("1. Computer");
                Console.WriteLine("2. Stampante");
                Console.WriteLine("0. Fine inserimento");
                Console.Write("Scelta: ");
                if (!int.TryParse(Console.ReadLine(), out int tipo))
                {
                    Console.WriteLine("Input non valido!");
                    continue;
                }

                switch (tipo)
                {
                    case 1:
                        Console.WriteLine("Inserisci modello del Computer: ");
                        string? modelloPC = Console.ReadLine();
                        dispositivi.Add(new Computer { Modello = modelloPC });
                        break;
                    case 2:
                        Console.WriteLine("Inserisci modello della Stampante: ");
                        string modelloStamp = Console.ReadLine() ?? "";
                        dispositivi.Add(new Stampante { Modello = modelloStamp });
                        break;
                    case 0:
                        inserimento = false;
                        break;
                    default:
                        Console.WriteLine("Scelta non valida!");
                        break;
                }
            }

            
            while (scelta != 0)
            {
                Console.WriteLine("\n--- Menu Dispositivi ---");
                for (int i = 0; i < dispositivi.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {dispositivi[i].Modello}");
                }
                Console.WriteLine("0. Esci");
                Console.Write("Seleziona un dispositivo: ");
                if (!int.TryParse(Console.ReadLine(), out scelta))
                {
                    Console.WriteLine("Input non valido!");
                    continue;
                }

                if (scelta > 0 && scelta <= dispositivi.Count)
                {
                    DispositivoElettronico dispositivo = dispositivi[scelta - 1];

                    Console.WriteLine($"\nHai selezionato: {dispositivo.Modello}");
                    Console.WriteLine("1. Accendi");
                    Console.WriteLine("2. Spegni");
                    Console.WriteLine("3. Mostra info");
                    Console.Write("Scegli un'azione: ");
                    if (!int.TryParse(Console.ReadLine(), out int azione))
                    {
                        Console.WriteLine("Input non valido!");
                        continue;
                    }

                    switch (azione)
                    {
                        case 1:
                            dispositivo.Accendi();
                            break;
                        case 2:
                            dispositivo.Spegni();
                            break;
                        case 3:
                            dispositivo.MostraInfo();
                            break;
                        default:
                            Console.WriteLine("Azione non valida!");
                            break;
                    }
                }
                else if (scelta != 0)
                {
                    Console.WriteLine("Selezione non valida!");
                }
            }

            Console.WriteLine("Programma terminato.");
        }
    }
}
