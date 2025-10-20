using System;

namespace BankingApp
{
    #region Singleton
    public sealed class BankContext : ISubject
    {
        private static BankContext? _instance;
        public static BankContext GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BankContext();
                }
                return _instance;
            }
        }

        public Dictionary<int, Cliente> Clienti { get; private set; }
        public Dictionary<int, Conto> Conti { get; private set; }
        public Dictionary<int, List<Operazione>> Operazioni { get; private set; }

        private readonly List<IObserver> _observers = new List<IObserver>();

        private readonly ContoFactory _contoFactory = new ContoFactory();
        private int _nextClienteId = 1;
        private int _nextContoId = 1001;
        private int _nextOperazioneId = 10001;

        private BankContext()
        {
            Clienti = new Dictionary<int, Cliente>();
            Conti = new Dictionary<int, Conto>();
            Operazioni = new Dictionary<int, List<Operazione>>();
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string messaggio)
        {
            foreach (var observer in _observers)
            {
                observer.Aggiorna(messaggio);
            }
        }

        public Cliente CreaCliente(string nome, string cognome)
        {
            var cliente = new Cliente
            {
                IdCliente = _nextClienteId++,
                Nome = nome,
                Cognome = cognome
            };
            Clienti[cliente.IdCliente] = cliente;

            Notify($"Nuovo cliente creato: {cliente.Nome} {cliente.Cognome} (ID: {cliente.IdCliente})");
            return cliente;
        }

        public Conto ApriConto(int idCliente, string tipoConto, decimal saldoIniziale = 0)
        {
            if (!Clienti.ContainsKey(idCliente))
            {
                Notify($"[ERRORE] Cliente {idCliente} non trovato. Impossibile aprire conto.");
                return null;
            }

            int nuovoIdConto = _nextContoId++;
            Conto nuovoConto = _contoFactory.CreaConto(tipoConto, nuovoIdConto, idCliente);

            if (nuovoConto == null)
            {
                Notify($"[ERRORE] Tipo conto '{tipoConto}' non valido.");
                return null;
            }

            nuovoConto.Deposita(saldoIniziale);
            Conti[nuovoConto.IdConto] = nuovoConto;
            Operazioni[nuovoConto.IdConto] = new List<Operazione>();

            if (saldoIniziale > 0)
            {
                RegistraOperazione(nuovoConto.IdConto, "Deposito Iniziale", saldoIniziale);
            }

            Notify($"Nuovo conto aperto (ID: {nuovoConto.IdConto}) per Cliente {idCliente}. Tipo: {tipoConto}, Saldo: {saldoIniziale:C}");
            return nuovoConto;
        }

        public bool EseguiDeposito(int idConto, decimal importo)
        {
            if (importo <= 0)
            {
                Notify($"[ERRORE] Importo deposito non valido per conto {idConto}.");
                return false;
            }

            if (Conti.TryGetValue(idConto, out Conto conto))
            {
                conto.Deposita(importo);
                RegistraOperazione(idConto, "Deposito", importo);
                Notify($"Deposito di {importo:C} su conto {idConto}. Nuovo saldo: {conto.Saldo:C}");
                return true;
            }

            Notify($"[ERRORE] Conto {idConto} non trovato per deposito.");
            return false;
        }

        public bool EseguiPrelievo(int idConto, decimal importo)
        {
            if (importo <= 0)
            {
                Notify($"[ERRORE] Importo prelievo non valido per conto {idConto}.");
                return false;
            }

            if (Conti.TryGetValue(idConto, out Conto conto))
            {
                if (conto.Preleva(importo))
                {
                    RegistraOperazione(idConto, "Prelievo", importo);
                    Notify($"Prelievo di {importo:C} da conto {idConto}. Nuovo saldo: {conto.Saldo:C}");
                    return true;
                }
                else
                {
                    Notify($"[ERRORE] Fondi insufficienti per prelievo su conto {idConto}.");
                    return false;
                }
            }

            Notify($"[ERRORE] Conto {idConto} non trovato per prelievo.");
            return false;
        }

        public bool EseguiTrasferimento(int idContoPartenza, int idContoDestinazione, decimal importo)
        {
            if (!Conti.ContainsKey(idContoPartenza) || !Conti.ContainsKey(idContoDestinazione))
            {
                Notify($"[ERRORE] Conti di partenza o destinazione non validi per trasferimento.");
                return false;
            }
            if (EseguiPrelievo(idContoPartenza, importo))
            {
                if (EseguiDeposito(idContoDestinazione, importo))
                {
                    Notify($"Trasferimento di {importo:C} da {idContoPartenza} a {idContoDestinazione} completato.");
                    RegistraOperazione(idContoPartenza, $"Trasferimento a {idContoDestinazione}", importo, sovrascriviUltima: true);
                    RegistraOperazione(idContoDestinazione, $"Trasferimento da {idContoPartenza}", importo, sovrascriviUltima: true);
                    return true;
                }
                else
                {
                    EseguiDeposito(idContoPartenza, importo);
                    Notify($"[ERRORE] Fallito deposito destinazione. Trasferimento annullato e fondi rimborsati su {idContoPartenza}.");
                    return false;
                }
            }
            return false;
        }

        private void RegistraOperazione(int idConto, string tipo, decimal importo, bool sovrascriviUltima = false)
        {
            var op = new Operazione
            {
                IdOperazione = _nextOperazioneId++,
                Tipo = tipo,
                Importo = importo,
                Data = DateTime.Now
            };

            if (sovrascriviUltima && Operazioni[idConto].Any())
            {
                Operazioni[idConto].RemoveAt(Operazioni[idConto].Count - 1);
            }
            Operazioni[idConto].Add(op);
        }
    }
    #endregion

    #region Observer
    
    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string messaggio);
    }

    public interface IObserver
    {
        void Aggiorna(string messaggio);
    }

    public class ConsoleLogger : IObserver
    {
        public void Aggiorna(string messaggio)
        {
            Console.WriteLine($"[LOG - {DateTime.Now:T}]: {messaggio}");
        }
    }

    public class DashboardObserver : IObserver
    {
        public void Aggiorna(string messaggio)
        {
            if (!messaggio.StartsWith("[ERRORE]"))
            {
                Console.WriteLine($"[DASHBOARD]: Notifica -> {messaggio}");
            }
        }
    }
    #endregion

    #region Strategy
    public interface ICalcoloInteressi
    {
        decimal Calcola(decimal saldo);
        string Descrizione { get; }
    }
    public class InteresseBase : ICalcoloInteressi
    {
        public string Descrizione => "Interesse Base (1%)";
        public decimal Calcola(decimal saldo)
        {
            return saldo * 0.01m;
        }
    }

    public class InteressePremium : ICalcoloInteressi
    {
        public string Descrizione => "Interesse Premium (3%)";
        public decimal Calcola(decimal saldo)
        {
            return saldo * 0.03m;
        }
    }
    public class CommissioneStudent : ICalcoloInteressi
    {
        public string Descrizione => "Conto Student (0% int, -5€ comm.)";
        public decimal Calcola(decimal saldo)
        {
            return -5.0m;
        }
    }
    #endregion

    #region Factory
    public class ContoFactory
    {
        public Conto CreaConto(string tipoConto, int idConto, int idCliente)
        {
            switch (tipoConto.ToLower())
            {
                case "base":
                    return new ContoBase(idConto, idCliente, new InteresseBase());

                case "premium":
                    return new ContoPremium(idConto, idCliente, new InteressePremium());

                case "student":
                    return new ContoStudent(idConto, idCliente, new CommissioneStudent());

                default:
                    return null;
            }
        }
    }
    #endregion

    #region Clienti, Conti, Operazioni
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string? Nome { get; set; }
        public string? Cognome { get; set; }
    }

    public class Operazione
    {
        public int IdOperazione { get; set; }
        public DateTime Data { get; set; }
        public string? Tipo { get; set; }
        public decimal Importo { get; set; }
    }

    public abstract class Conto
    {
        public int IdConto { get; protected set; }
        public int IdCliente { get; protected set; }
        public decimal Saldo { get; protected set; }

        protected ICalcoloInteressi _strategiaCalcolo;

        public Conto(int idConto, int idCliente, ICalcoloInteressi strategia)
        {
            IdConto = idConto;
            IdCliente = idCliente;
            Saldo = 0;
            _strategiaCalcolo = strategia;
        }
        public void Deposita(decimal importo)
        {
            if (importo > 0)
            {
                Saldo += importo;
            }
        }

        public bool Preleva(decimal importo)
        {
            if (importo > 0 && Saldo >= importo)
            {
                Saldo -= importo;
                return true;
            }
            return false;
        }

        public void ApplicaInteressiOCommissioni()
        {
            decimal importo = _strategiaCalcolo.Calcola(Saldo);
            Saldo += importo;

            string tipoOp = importo >= 0 ? "Accredito Interessi" : "Addebito Commissioni";
            
            BankContext.GetInstance.Notify($"Conto {IdConto}: Applicata strategia '{_strategiaCalcolo.Descrizione}'. Importo: {importo:C}. Nuovo Saldo: {Saldo:C}");
        }

        public void CambiaStrategia(ICalcoloInteressi nuovaStrategia)
        {
            _strategiaCalcolo = nuovaStrategia;
            
            BankContext.GetInstance.Notify($"Conto {IdConto}: Strategia aggiornata a '{nuovaStrategia.Descrizione}'.");
        }
    }

    public class ContoBase : Conto
    {
        public ContoBase(int idConto, int idCliente, ICalcoloInteressi strategia)
            : base(idConto, idCliente, strategia)
        {
        }
    }

    public class ContoPremium : Conto
    {
        public ContoPremium(int idConto, int idCliente, ICalcoloInteressi strategia)
            : base(idConto, idCliente, strategia)
        {
        }
    }
    public class ContoStudent : Conto
    {
        public ContoStudent(int idConto, int idCliente, ICalcoloInteressi strategia)
            : base(idConto, idCliente, strategia)
        {
        }
    }

    #endregion

    #region Program
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = BankContext.GetInstance;
            var logger = new ConsoleLogger();
            var dashboard = new DashboardObserver();
            
            context.Attach(logger);
            context.Attach(dashboard);

            bool inEsecuzione = true;
            while (inEsecuzione)
            {
                Console.WriteLine("\n--- MENU PRINCIPALE ---");
                Console.WriteLine("1. Crea Cliente");
                Console.WriteLine("2. Apri Conto");
                Console.WriteLine("3. Esegui Deposito");
                Console.WriteLine("4. Esegui Prelievo");
                Console.WriteLine("5. Esegui Trasferimento");
                Console.WriteLine("6. Applica Interessi/Commissioni (a tutti i conti)");
                Console.WriteLine("7. Mostra Dettagli Cliente");
                Console.WriteLine("8. Mostra Dettagli Conto (con operazioni)");
                Console.WriteLine("9. Esci");
                Console.Write("Seleziona un'opzione: ");

                string? scelta = Console.ReadLine();
                Console.WriteLine("--------------------------------------");

                switch (scelta)
                {
                    case "1":
                        GestisciCreaCliente(context);
                        break;
                    case "2":
                        GestisciApriConto(context);
                        break;
                    case "3":
                        GestisciDeposito(context);
                        break;
                    case "4":
                        GestisciPrelievo(context);
                        break;
                    case "5":
                        GestisciTrasferimento(context);
                        break;
                    case "6":
                        GestisciApplicaInteressi(context);
                        break;
                    case "7":
                        GestisciDettagliCliente(context);
                        break;
                    case "8":
                        GestisciDettagliConto(context);
                        break;
                    case "9":
                        inEsecuzione = false;
                        break;
                    default:
                        Console.WriteLine("[ERRORE] Scelta non valida. Riprova.");
                        break;
                }
            }

            Console.WriteLine("\n--- Simulazione Terminata ---");
        }

        private static void GestisciCreaCliente(BankContext context)
        {
            Console.WriteLine("--- Creazione Nuovo Cliente ---");
            string nome = LeggiStringa("Nome: ");
            string cognome = LeggiStringa("Cognome: ");

            context.CreaCliente(nome, cognome);
        }

        private static void GestisciApriConto(BankContext context)
        {
            Console.WriteLine("--- Apertura Nuovo Conto ---");
            int idCliente = LeggiInt("ID Cliente: ");
            string tipoConto = LeggiStringa("Tipo Conto (base, premium, student): ").ToLower();
            decimal saldoIniziale = LeggiDecimal("Saldo Iniziale: ");

            context.ApriConto(idCliente, tipoConto, saldoIniziale);
        }

        private static void GestisciDeposito(BankContext context)
        {
            Console.WriteLine("--- Esegui Deposito ---");
            int idConto = LeggiInt("ID Conto: ");
            decimal importo = LeggiDecimal("Importo da depositare: ");

            context.EseguiDeposito(idConto, importo);
        }

        private static void GestisciPrelievo(BankContext context)
        {
            Console.WriteLine("--- Esegui Prelievo ---");
            int idConto = LeggiInt("ID Conto: ");
            decimal importo = LeggiDecimal("Importo da prelevare: ");

            context.EseguiPrelievo(idConto, importo);
        }

        private static void GestisciTrasferimento(BankContext context)
        {
            Console.WriteLine("--- Esegui Trasferimento ---");
            int idPartenza = LeggiInt("ID Conto di Partenza: ");
            int idDestinazione = LeggiInt("ID Conto di Destinazione: ");
            decimal importo = LeggiDecimal("Importo da trasferire: ");

            context.EseguiTrasferimento(idPartenza, idDestinazione, importo);
        }

        private static void GestisciApplicaInteressi(BankContext context)
        {
            Console.WriteLine("--- Applicazione Interessi/Commissioni (Strategy) ---");
            if (!context.Conti.Any())
            {
                Console.WriteLine("Nessun conto presente nel sistema.");
                return;
            }

            foreach (var conto in context.Conti.Values)
            {
                conto.ApplicaInteressiOCommissioni();
            }
        }

        private static void GestisciDettagliCliente(BankContext context)
        {
            Console.WriteLine("--- Dettagli Cliente ---");
            int idCliente = LeggiInt("ID Cliente: ");

            if (context.Clienti.TryGetValue(idCliente, out Cliente? cliente))
            {
                Console.WriteLine($"Cliente: {cliente.Nome} {cliente.Cognome} (ID: {cliente.IdCliente})");
                Console.WriteLine("Conti associati:");

                var contiCliente = context.Conti.Values.Where(c => c.IdCliente == idCliente).ToList();
                if (contiCliente.Any())
                {
                    foreach (var conto in contiCliente)
                    {
                        Console.WriteLine($"  - Conto ID: {conto.IdConto}, Saldo: {conto.Saldo:C}");
                    }
                }
                else
                {
                    Console.WriteLine("  Nessun conto associato.");
                }
            }
            else
            {
                Console.WriteLine("[ERRORE] Cliente non trovato.");
            }
        }

        private static void GestisciDettagliConto(BankContext context)
        {
            Console.WriteLine("--- Dettagli Conto ---");
            int idConto = LeggiInt("ID Conto: ");

            if (context.Conti.TryGetValue(idConto, out Conto? conto))
            {
                Console.WriteLine($"Conto ID: {idConto}");
                Console.WriteLine($"Cliente ID: {conto.IdCliente}");
                Console.WriteLine($"Saldo Attuale: {conto.Saldo:C}");
                Console.WriteLine("--- Storico Operazioni ---");

                if (context.Operazioni.TryGetValue(idConto, out List<Operazione>? operazioni) && operazioni.Any())
                {
                    foreach (var op in operazioni)
                    {
                        Console.WriteLine($"  [{op.Data:g}] {op.Tipo} - {op.Importo:C}");
                    }
                }
                else
                {
                    Console.WriteLine("  Nessuna operazione registrata.");
                }
            }
            else
            {
                Console.WriteLine("[ERRORE] Conto non trovato.");
            }
        }

        private static string LeggiStringa(string prompt)
        {
            string? input;
            do
            {
                Console.Write(prompt);
                input = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }

        private static int LeggiInt(string prompt)
        {
            int valore;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out valore))
                {
                    return valore;
                }
                Console.WriteLine("[ERRORE] Inserisci un numero intero valido.");
            }
        }

        private static decimal LeggiDecimal(string prompt)
        {
            decimal valore;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out valore) && valore >= 0)
                {
                    return valore;
                }
                Console.WriteLine("[ERRORE] Inserisci un importo numerico valido (es. 100,50).");
            }
        }
    }
    #endregion
}