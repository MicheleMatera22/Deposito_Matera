using System;
using System.Collections.Generic;

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

        public Dictionary<int, Conto> Conti { get; private set; }
        public Dictionary<int, List<Operazione>> Operazioni { get; private set; }
        
        public Dictionary<int, Utente> Utenti { get; private set; }
        private int _nextUtenteId = 1;

        private readonly List<IObserver> _observers = new List<IObserver>();
        private readonly ContoFactory _contoFactory = new ContoFactory();
        private int _nextContoId = 1001;
        private int _nextOperazioneId = 10001;

        private BankContext()
        {
            Conti = new Dictionary<int, Conto>();
            Operazioni = new Dictionary<int, List<Operazione>>();
            Utenti = new Dictionary<int, Utente>(); 
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
        
        public bool RegistraUtente(string nome, string password)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(password))
            {
                Notify("[ERRORE] Nome utente e password non possono essere vuoti.");
                return false;
            }

            bool utenteEsiste = false;
            foreach (var u in Utenti.Values)
            {
                if (u.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    utenteEsiste = true;
                    break;
                }
            }
            if (utenteEsiste)
            {
                Notify($"[ERRORE] Registrazione fallita: L'utente '{nome}' esiste già.");
                return false;
            }

            var nuovoUtente = new Utente
            {
                IdUtente = _nextUtenteId++,
                Nome = nome,
                Password = password 
            };
            
            Utenti[nuovoUtente.IdUtente] = nuovoUtente;
            Notify($"Nuovo utente registrato con successo: {nome} (ID: {nuovoUtente.IdUtente})");
            return true;
        }

        public Utente? Login(string nome, string password)
        {
            Utente? utenteTrovato = null;
            foreach (var utente in Utenti.Values)
            {
                if (utente.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase))
                {
                    utenteTrovato = utente;
                    break;
                }
            }

            if (utenteTrovato != null && utenteTrovato.Password == password)
            {
                Notify($"Login riuscito. Benvenuto, {utenteTrovato.Nome}.");
                return utenteTrovato;
            }

            Notify("[ERRORE] Login fallito: Nome utente o password errati.");
            return null;
        }

        public Conto ApriConto(int idUtente, string tipoConto, decimal saldoIniziale = 0)
        {
            if (!Utenti.ContainsKey(idUtente))
            {
                Notify($"[ERRORE] Utente {idUtente} non trovato. Impossibile aprire conto.");
                return null;
            }

            int nuovoIdConto = _nextContoId++;
            Conto nuovoConto = _contoFactory.CreaConto(tipoConto, nuovoIdConto, idUtente);

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

            Notify($"Nuovo conto aperto (ID: {nuovoConto.IdConto}) per Utente {idUtente}. Tipo: {tipoConto}, Saldo: {saldoIniziale:C}");
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

            if (sovrascriviUltima && Operazioni[idConto].Count > 0)
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
        public Conto CreaConto(string tipoConto, int idConto, int idUtente)
        {
            switch (tipoConto.ToLower())
            {
                case "base":
                    return new ContoBase(idConto, idUtente, new InteresseBase());

                case "premium":
                    return new ContoPremium(idConto, idUtente, new InteressePremium());

                case "student":
                    return new ContoStudent(idConto, idUtente, new CommissioneStudent());

                default:
                    return null;
            }
        }
    }
    #endregion

    #region Conti, Operazioni, Utenti
    
    public class Utente
    {
        public int IdUtente { get; set; }
        public string? Nome { get; set; }
        public string? Password { get; set; }
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
        public int IdUtente { get; protected set; } 
        public decimal Saldo { get; protected set; }

        protected ICalcoloInteressi _strategiaCalcolo;

        public Conto(int idConto, int idUtente, ICalcoloInteressi strategia)
        {
            IdConto = idConto;
            IdUtente = idUtente; 
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
        public ContoBase(int idConto, int idUtente, ICalcoloInteressi strategia)
            : base(idConto, idUtente, strategia) 
        {
        }
    }

    public class ContoPremium : Conto
    {
        public ContoPremium(int idConto, int idUtente, ICalcoloInteressi strategia)
            : base(idConto, idUtente, strategia) 
        {
        }
    }
    public class ContoStudent : Conto
    {
        public ContoStudent(int idConto, int idUtente, ICalcoloInteressi strategia)
            : base(idConto, idUtente, strategia) 
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

            Console.WriteLine("--- Benvenuto nel Sistema Bancario ---");
            bool appInEsecuzione = true;

            while (appInEsecuzione)
            {
                Console.WriteLine("\n--- AUTENTICAZIONE ---");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Registrati");
                Console.WriteLine("3. Esci dall'applicazione");
                Console.Write("Seleziona un'opzione: ");

                string? sceltaAuth = Console.ReadLine();
                Utente? utenteCorrente = null;

                switch (sceltaAuth)
                {
                    case "1":
                        utenteCorrente = GestisciLogin(context);
                        break;
                    case "2":
                        GestisciRegistrazione(context);
                        break;
                    case "3":
                        appInEsecuzione = false;
                        break;
                    default:
                        Console.WriteLine("[ERRORE] Scelta non valida.");
                        break;
                }

                if (utenteCorrente != null)
                {
                    MostraMenuOperazioni(context, utenteCorrente);
                }
            }

            Console.WriteLine("\n--- Simulazione Terminata ---");
        }

        private static Utente? GestisciLogin(BankContext context)
        {
            Console.WriteLine("--- Login ---");
            string nome = LeggiStringa("Nome Utente: ");
            string password = LeggiStringa("Password: ");
            return context.Login(nome, password);
        }

        private static void GestisciRegistrazione(BankContext context)
        {
            Console.WriteLine("--- Registrazione Nuovo Utente ---");
            string nome = LeggiStringa("Scegli Nome Utente: ");
            string password = LeggiStringa("Scegli Password: ");
            context.RegistraUtente(nome, password);
        }

        private static void MostraMenuOperazioni(BankContext context, Utente utente)
        {
            Console.WriteLine($"\n--- BENVENUTO, {utente.Nome?.ToUpper()} ---");
            bool utenteLoggato = true;

            while (utenteLoggato)
            {
                Console.WriteLine("\n--- MENU PRINCIPALE ---");
                Console.WriteLine("1. Apri Conto");
                Console.WriteLine("2. Esegui Deposito");
                Console.WriteLine("3. Esegui Prelievo");
                Console.WriteLine("4. Esegui Trasferimento");
                Console.WriteLine("5. Applica Interessi/Commissioni (a tutti i conti)");
                Console.WriteLine("6. Mostra Dettagli Utente (e Conti)");
                Console.WriteLine("7. Mostra Dettagli Conto (con operazioni)");
                Console.WriteLine("8. Logout");
                Console.Write("Seleziona un'opzione: ");

                string? scelta = Console.ReadLine();
                Console.WriteLine("--------------------------------------");

                switch (scelta)
                {
                    case "1": 
                        GestisciApriConto(context);
                        break;
                    case "2": 
                        GestisciDeposito(context);
                        break;
                    case "3": 
                        GestisciPrelievo(context);
                        break;
                    case "4": 
                        GestisciTrasferimento(context);
                        break;
                    case "5": 
                        GestisciApplicaInteressi(context);
                        break;
                    case "6": 
                        GestisciDettagliUtente(context); 
                        break;
                    case "7": 
                        GestisciDettagliConto(context);
                        break;
                    case "8": 
                        utenteLoggato = false; 
                        context.Notify($"Logout eseguito dall'utente: {utente.Nome}");
                        break;
                    default:
                        Console.WriteLine("[ERRORE] Scelta non valida. Riprova.");
                        break;
                }
            }
        }

        private static void GestisciApriConto(BankContext context)
        {
            Console.WriteLine("--- Apertura Nuovo Conto ---");
            int idUtente = LeggiInt("ID Utente proprietario: ");
            string tipoConto = LeggiStringa("Tipo Conto (base, premium, student): ").ToLower();
            decimal saldoIniziale = LeggiDecimal("Saldo Iniziale: ");

            context.ApriConto(idUtente, tipoConto, saldoIniziale);
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
            if (context.Conti.Count == 0)
            {
                Console.WriteLine("Nessun conto presente nel sistema.");
                return;
            }

            foreach (var conto in context.Conti.Values)
            {
                conto.ApplicaInteressiOCommissioni();
            }
        }

        private static void GestisciDettagliUtente(BankContext context)
        {
            Console.WriteLine("--- Dettagli Utente ---");
            int idUtente = LeggiInt("ID Utente: ");

            if (context.Utenti.TryGetValue(idUtente, out Utente? utente))
            {
                Console.WriteLine($"Utente: {utente.Nome} (ID: {utente.IdUtente})");
                Console.WriteLine("Conti associati:");
                
                var contiUtente = new List<Conto>();
                foreach (var c in context.Conti.Values)
                {
                    if (c.IdUtente == idUtente)
                    {
                        contiUtente.Add(c);
                    }
                }

                if (contiUtente.Count > 0)
                {
                    foreach (var conto in contiUtente)
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
                Console.WriteLine("[ERRORE] Utente non trovato.");
            }
        }

        private static void GestisciDettagliConto(BankContext context)
        {
            Console.WriteLine("--- Dettagli Conto ---");
            int idConto = LeggiInt("ID Conto: ");

            if (context.Conti.TryGetValue(idConto, out Conto? conto))
            {
                Console.WriteLine($"Conto ID: {idConto}");
                Console.WriteLine($"Utente ID: {conto.IdUtente}"); 
                Console.WriteLine($"Saldo Attuale: {conto.Saldo:C}");
                Console.WriteLine("--- Storico Operazioni ---");

                if (context.Operazioni.TryGetValue(idConto, out List<Operazione>? operazioni) && operazioni.Count > 0)
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