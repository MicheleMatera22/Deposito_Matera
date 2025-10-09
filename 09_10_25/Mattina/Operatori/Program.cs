using System;
using System.Collections.Generic;

namespace Operatori
{
    /// <summary>
    /// Classe principale del programma che gestisce operatori di diversi tipi tramite menu testuale.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Punto di ingresso dell'applicazione.
        /// Gestisce il menu principale e chiama i metodi corrispondenti alle scelte dell'utente.
        /// </summary>
        static void Main()
        {
            // Lista per memorizzare tutti gli operatori creati
            List<Operatore> operatori = new List<Operatore>();
            int scelta;

            do
            {
                // Stampa il menu principale
                Console.WriteLine("\n--- Menu ---");
                Console.WriteLine("1 - Aggiungi nuovo operatore");
                Console.WriteLine("2 - Stampa tutti gli operatori");
                Console.WriteLine("3 - Esegui compiti di un operatore");
                Console.WriteLine("4 - Esci");
                Console.Write("Scelta: ");

                // Legge la scelta dell'utente
                scelta = int.Parse(Console.ReadLine());

                // Esegue l'azione corrispondente alla scelta
                switch (scelta)
                {
                    case 1:
                        AggiungiOperatore(operatori);
                        break;
                    case 2:
                        StampaOperatori(operatori);
                        break;
                    case 3:
                        EseguiCompiti(operatori);
                        break;
                    case 4:
                        Console.WriteLine("Uscita dal programma.");
                        break;
                    default:
                        Console.WriteLine("Scelta non valida!");
                        break;
                }

            } while (scelta != 4); // Continua finché l'utente non sceglie di uscire
        }

        /// <summary>
        /// Metodo per aggiungere un nuovo operatore alla lista.
        /// Chiede all'utente il tipo di operatore, il nome, il turno e le informazioni specifiche del tipo.
        /// </summary>
        /// <param name="operatori">Lista degli operatori in cui aggiungere il nuovo operatore.</param>
        static void AggiungiOperatore(List<Operatore> operatori)
        {
            Console.WriteLine("\nSeleziona il tipo di operatore da aggiungere:");
            Console.WriteLine("1 - Operatore Emergenza");
            Console.WriteLine("2 - Operatore Sicurezza");
            Console.WriteLine("3 - Operatore Logistico");
            Console.Write("Scelta: ");
            int tipo = int.Parse(Console.ReadLine());

            Operatore op;

            // Creazione dell'operatore in base alla scelta
            if (tipo == 1)
            {
                op = new OperatoreEmergenza();
            }
            else if (tipo == 2)
            {
                op = new OperatoreSicurezza();
            }
            else if (tipo == 3)
            {
                op = new OperatoreLogistico();
            }
            else
            {
                op = null;
            }

            if (op == null)
            {
                Console.WriteLine("Tipo non valido.");
                return;
            }

            // Inserimento nome e turno
            Console.WriteLine("Inserisci il nome:");
            op.Nome = Console.ReadLine();

            Console.WriteLine("Inserisci il turno (giorno/notte):");
            op.Turno = Console.ReadLine();

            // Inserimento dati specifici in base al tipo
            if (op is OperatoreEmergenza oe)
            {
                Console.WriteLine("Inserisci il livello di urgenza (1-5):");
                oe.LivelloUrgenza = int.Parse(Console.ReadLine());
            }
            else if (op is OperatoreSicurezza os)
            {
                Console.WriteLine("Inserisci l'area sorvegliata:");
                os.AreaSorvegliata = Console.ReadLine();
            }
            else if (op is OperatoreLogistico ol)
            {
                Console.WriteLine("Inserisci il numero di consegne:");
                ol.NumeroConsegne = int.Parse(Console.ReadLine());
            }

            operatori.Add(op);
            Console.WriteLine("Operatore aggiunto con successo!");
        }

        /// <summary>
        /// Metodo per stampare tutti gli operatori presenti nella lista con informazioni principali.
        /// </summary>
        /// <param name="operatori">Lista degli operatori da stampare.</param>
        static void StampaOperatori(List<Operatore> operatori)
        {
            Console.WriteLine("\n--- Lista Operatori ---");
            foreach (var op in operatori)
            {
                Console.WriteLine($"Nome: {op.Nome}, Tipo: {op.GetType().Name}, Turno: {op.Turno}");
            }
        }

        /// <summary>
        /// Metodo per eseguire il compito di un singolo operatore scelto dall'utente.
        /// Mostra la lista degli operatori e permette di selezionare uno per l'esecuzione.
        /// </summary>
        /// <param name="operatori">Lista degli operatori disponibili.</param>
        static void EseguiCompiti(List<Operatore> operatori)
        {
            if (operatori.Count == 0)
            {
                Console.WriteLine("Non ci sono operatori nella lista.");
                return;
            }

            Console.WriteLine("\n--- Seleziona l'operatore per eseguire il compito ---");

            // Mostra la lista con indice
            for (int i = 0; i < operatori.Count; i++)
            {
                var op = operatori[i];
                Console.WriteLine($"{i + 1} - {op.Nome} ({op.GetType().Name}, Turno: {op.Turno})");
            }

            // Lettura della scelta dell'utente
            Console.Write("Inserisci il numero dell'operatore: ");
            if (int.TryParse(Console.ReadLine(), out int scelta) && scelta >= 1 && scelta <= operatori.Count)
            {
                Operatore selezionato = operatori[scelta - 1];
                Console.WriteLine($"\nEsecuzione del compito per {selezionato.Nome}:");
                selezionato.EseguiCompito(selezionato); // Polimorfismo
            }
            else
            {
                Console.WriteLine("Scelta non valida.");
            }
        }
    }
}
