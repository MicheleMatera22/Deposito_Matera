namespace Pagamenti
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Utente> utenti = new List<Utente>();
            bool programmaAttivo = true;

            while (programmaAttivo)
            {
                Console.WriteLine("\n--- Menu Principale ---");
                Console.WriteLine("1. Aggiungi un utente");
                Console.WriteLine("2. Elimina un utente");
                Console.WriteLine("3. Visualizza utenti");
                Console.WriteLine("4. Accedi utente");
                Console.WriteLine("0. Esci");
                Console.Write("Scelta: ");

                if (!int.TryParse(Console.ReadLine(), out int scelta))
                {
                    Console.WriteLine("Input non valido!");
                    continue;
                }

                switch (scelta)
                {
                    case 1:
                        AggiungiUtente(utenti);
                        break;

                    case 2:
                        EliminaUtente(utenti);
                        break;

                    case 3:
                        VisualizzaUtenti(utenti);
                        break;

                    case 4:
                        AccediUtente(utenti);
                        break;

                    case 0:
                        Console.WriteLine("Uscita dal programma...");
                        programmaAttivo = false;
                        break;

                    default:
                        Console.WriteLine("Scelta non valida!");
                        break;
                }
            }
        }

        static void AggiungiUtente(List<Utente> utenti)
        {
            Console.Write("Inserisci il nome dell'utente: ");
            string? nome = Console.ReadLine();

            Utente utente = new Utente();
            utente.Nome = nome;

            Console.Write("Inserisci un PIN di 4 cifre: ");
            if (!int.TryParse(Console.ReadLine(), out int pin))
            {
                Console.WriteLine("PIN non valido!");
                return;
            }
            utente.Pin = pin;

            Console.Write("Inserisci il saldo iniziale dell'utente: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal saldo) || saldo < 0)
            {
                Console.WriteLine("Saldo non valido!");
                return;
            }
            utente.Saldo = saldo;

            utenti.Add(utente);
            Console.WriteLine($"Utente {utente.Nome} aggiunto con saldo iniziale: {utente.Saldo}€");
        }

        static void EliminaUtente(List<Utente> utenti)
        {
            if (utenti.Count == 0)
            {
                Console.WriteLine("Non ci sono utenti da eliminare.");
                return;
            }

            VisualizzaUtenti(utenti);
            Console.Write("Inserisci l'indice dell'utente da eliminare: ");
            if (int.TryParse(Console.ReadLine(), out int indice) && indice >= 0 && indice < utenti.Count)
            {
                Console.WriteLine($"Utente {utenti[indice].Nome} eliminato.");
                utenti.RemoveAt(indice);
            }
            else
            {
                Console.WriteLine("Indice non valido!");
            }
        }

        static void VisualizzaUtenti(List<Utente> utenti)
        {
            if (utenti.Count == 0)
            {
                Console.WriteLine("Non ci sono utenti.");
                return;
            }

            Console.WriteLine("\nLista utenti:");
            for (int i = 0; i < utenti.Count; i++)
            {
                Console.WriteLine($"{i}. {utenti[i].Nome} - Saldo: {utenti[i].Saldo}€");
            }
        }

        static void AccediUtente(List<Utente> utenti)
        {
            if (utenti.Count == 0)
            {
                Console.WriteLine("Non ci sono utenti disponibili.");
                return;
            }

            VisualizzaUtenti(utenti);
            Console.Write("Seleziona l'indice dell'utente: ");
            if (!int.TryParse(Console.ReadLine(), out int indice) || indice < 0 || indice >= utenti.Count)
            {
                Console.WriteLine("Indice non valido!");
                return;
            }

            Utente utente = utenti[indice];
            bool gestioneUtente = true;

            while (gestioneUtente)
            {
                Console.WriteLine($"\n--- Menu Utente: {utente.Nome} ---");
                Console.WriteLine("1. Aggiungi metodo di pagamento");
                Console.WriteLine("2. Esegui pagamento");
                Console.WriteLine("3. Rimuovi metodo di pagamento");
                Console.WriteLine("4. Visualizza metodi di pagamento e saldo");
                Console.WriteLine("5. Ricarica saldo");
                Console.WriteLine("0. Torna al menu principale");
                Console.Write("Scelta: ");

                if (!int.TryParse(Console.ReadLine(), out int sceltaUtente))
                {
                    Console.WriteLine("Input non valido!");
                    continue;
                }

                switch (sceltaUtente)
                {
                    case 1:
                        AggiungiMetodoPagamento(utente);
                        break;

                    case 2:
                        EseguiPagamento(utente);
                        break;

                    case 3:
                        RimuoviMetodoPagamento(utente);
                        break;

                    case 4:
                        utente.MostraMetodiPagamento();
                        Console.WriteLine($"Saldo attuale: {utente.Saldo}€");
                        break;

                    case 5:
                        RicaricaSaldo(utente);
                        break;

                    case 0:
                        gestioneUtente = false;
                        break;

                    default:
                        Console.WriteLine("Scelta non valida!");
                        break;
                }
            }
        }

        static void AggiungiMetodoPagamento(Utente utente)
        {
            Console.WriteLine("Seleziona il metodo di pagamento da aggiungere:");
            Console.WriteLine("1. Contanti");
            Console.WriteLine("2. Carta");
            Console.WriteLine("3. PayPal");
            Console.Write("Scelta: ");

            if (!int.TryParse(Console.ReadLine(), out int sceltaMetodo))
            {
                Console.WriteLine("Scelta non valida!");
                return;
            }

            switch (sceltaMetodo)
            {
                case 1:
                    utente.AggiungiMetodoPagamento(new PagamentoContanti());
                    Console.WriteLine("Metodo Contanti aggiunto.");
                    break;

                case 2:
                    var carta = new PagamentoCarta();
                    Console.Write("Inserisci il circuito della carta (es. Visa, MasterCard): ");
                    carta.Circuito = Console.ReadLine();
                    utente.AggiungiMetodoPagamento(carta);
                    Console.WriteLine("Metodo Carta aggiunto.");
                    break;

                case 3:
                    var paypal = new PagamentoPayPal();
                    Console.Write("Inserisci l'email associata a PayPal: ");
                    paypal.Email = Console.ReadLine();
                    utente.AggiungiMetodoPagamento(paypal);
                    Console.WriteLine("Metodo PayPal aggiunto.");
                    break;

                default:
                    Console.WriteLine("Scelta non valida!");
                    break;
            }
        }

        static void EseguiPagamento(Utente utente)
        {
            // Controllo PIN prima del pagamento
            Console.Write("Inserisci il PIN dell'utente per confermare il pagamento: ");
            if (!int.TryParse(Console.ReadLine(), out int pinInserito) || pinInserito != utente.Pin)
            {
                Console.WriteLine("PIN errato. Pagamento annullato.");
                return;
            }

            utente.MostraMetodiPagamento();
            Console.Write("Seleziona l'indice del metodo di pagamento: ");
            if (!int.TryParse(Console.ReadLine(), out int indiceMetodo))
            {
                Console.WriteLine("Indice non valido!");
                return;
            }

            Console.Write("Inserisci l'importo da pagare: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal importo) || importo <= 0)
            {
                Console.WriteLine("Importo non valido!");
                return;
            }

            PagamentoPolimorfico(utente, indiceMetodo, importo);
        }

        static void RimuoviMetodoPagamento(Utente utente)
        {
            utente.MostraMetodiPagamento();
            Console.Write("Inserisci l'indice del metodo da rimuovere: ");
            if (!int.TryParse(Console.ReadLine(), out int indice))
            {
                Console.WriteLine("Indice non valido!");
                return;
            }
            utente.RimuoviMetodoPagamento(indice);
        }

        static void RicaricaSaldo(Utente utente)
        {
            Console.Write("Inserisci l'importo da aggiungere al saldo: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal importo) || importo <= 0)
            {
                Console.WriteLine("Importo non valido!");
                return;
            }

            utente.Saldo += importo;
            Console.WriteLine($"Saldo aggiornato: {utente.Saldo}€");
        }

        static void PagamentoPolimorfico(Utente utente, int indiceMetodo, decimal importo)
        {
            Console.WriteLine($"\nPagamento in corso per l'utente {utente.Nome}:");
            try
            {
                utente.EseguiPagamento(indiceMetodo, importo);
            }
            catch
            {
                Console.WriteLine("Errore: metodo di pagamento non valido.");
            }
        }
    }
}
