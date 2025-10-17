using System;

#region MODELS
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    public User(int id, string username, string email)
    {
        Id = id;
        Username = username;
        Email = email;
        IsActive = true;
        CreatedAt = DateTime.Now;
    }
}

public class ActionLog
{
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string? ActionType { get; set; }
    public string? Metadata { get; set; }
}
#endregion

#region OBSERVER
public interface IObserver
{
    void Update(string message);
}

public interface IObservable
{
    void AddObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
}

public class ConsoleLogger : IObserver
{
    public void Update(string message)
    {
        Console.WriteLine($"[LOG] {DateTime.Now:G} - {message}");
    }
}
#endregion

#region SINGLETON DATABASE
public sealed class FakeDatabase : IObservable
{
    private static FakeDatabase? _instance;
    public static FakeDatabase Instance => _instance ??= new FakeDatabase();

    private List<IObserver> _observers = new List<IObserver>();

    public Dictionary<int, User> Users { get; private set; } = new Dictionary<int, User>();
    public Dictionary<int, List<ActionLog>> ActionsByUser { get; private set; } = new Dictionary<int, List<ActionLog>>();

    private int _nextUserId = 1;

    private FakeDatabase() { }

    #region Observer Methods
    public void AddObserver(IObserver observer) => _observers.Add(observer);
    public void RemoveObserver(IObserver observer) => _observers.Remove(observer);
    private void NotifyObservers(string message)
    {
        foreach (var obs in _observers)
            obs.Update(message);
    }
    #endregion

    #region User Methods
    public int GetNextUserId() => _nextUserId++;

    public bool AddUser(User user)
    {
        if (Users.ContainsKey(user.Id))
            return false;

        Users.Add(user.Id, user);
        ActionsByUser.Add(user.Id, new List<ActionLog>());
        NotifyObservers($"Nuovo utente creato: {user.Username}");
        return true;
    }

    public User? GetUserByUsername(string username)
    {
        return Users.Values.FirstOrDefault(u => u.Username == username);
    }
    #endregion

    #region Action Methods
    public bool AddAction(int userId, ActionLog action)
    {
        if (ActionsByUser.TryGetValue(userId, out var actions))
        {
            actions.Add(action);
            NotifyObservers($"Azione registrata per utente ID {userId}: {action.ActionType}");
            return true;
        }
        return false;
    }

    #endregion
}
#endregion

#region MAIN PROGRAM
public class Program
{
    private static FakeDatabase _db = FakeDatabase.Instance;
    private static User? _loggedUser = null;

    public static void Main(string[] args)
    {
        _db.AddObserver(new ConsoleLogger());

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\n=== MENU PRINCIPALE ===");
            Console.WriteLine("1. Crea nuovo utente");
            Console.WriteLine("2. Mostra utenti");
            Console.WriteLine("3. Autentica utente");
            Console.WriteLine("4. Registra azione manuale");
            Console.WriteLine("5. Visualizza storico azioni");
            Console.WriteLine("6. Esci");
            Console.Write("Scelta: ");
            string scelta = Console.ReadLine() ?? "";

            switch (scelta)
            {
                case "1": CreaUtente(); break;
                case "2": MostraUtenti(); break;
                case "3": AutenticaUtente(); break;
                case "4": RegistraAzione(); break;
                case "5": MostraStorico(); break;
                case "6": exit = true; break;
                default: Console.WriteLine("Scelta non valida!"); break;
            }
        }

        Console.WriteLine("Programma terminato.");
    }

    private static void CreaUtente()
    {
        Console.Write("Username: ");
        string username = Console.ReadLine() ?? "";
        if (_db.Users.Values.Any(u => u.Username == username))
        {
            Console.WriteLine("Username già esistente!");
            return;
        }

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        var user = new User(_db.GetNextUserId(), username, email);
        _db.AddUser(user);
        Console.WriteLine("Utente creato con successo!");
    }

    private static void MostraUtenti()
    {
        if (_db.Users.Count == 0)
        {
            Console.WriteLine("Nessun utente registrato.");
            return;
        }

        Console.WriteLine("\n--- Utenti Registrati ---");
        foreach (var user in _db.Users.Values)
        {
            Console.WriteLine($"ID: {user.Id}, Username: {user.Username}, Email: {user.Email}, Attivo: {user.IsActive}, Creato il: {user.CreatedAt:G}");
        }
    }

    private static void AutenticaUtente()
    {
        Console.Write("Inserisci username: ");
        string username = Console.ReadLine() ?? "";
        var user = _db.GetUserByUsername(username);

        if (user == null)
        {
            Console.WriteLine("Utente non trovato!");
            return;
        }

        _loggedUser = user;
        _db.AddAction(user.Id, new ActionLog { ActionType = "LOGIN", Metadata = "Accesso riuscito" });
    }

    private static void RegistraAzione()
    {
        if (_loggedUser == null)
        {
            Console.WriteLine("Effettua prima il login!");
            return;
        }

        Console.Write("Tipo azione (VIEW, CREATE, UPDATE, DELETE, LOGOUT): ");
        string tipo = Console.ReadLine() ?? "";
        Console.Write("Descrizione: ");
        string descrizione = Console.ReadLine() ?? "";

        _db.AddAction(_loggedUser.Id, new ActionLog { ActionType = tipo, Metadata = descrizione });
    }

    private static void MostraStorico()
    {
        Console.Write("Inserisci username da filtrare (vuoto per tutti): ");
        string filtroUser = Console.ReadLine() ?? "";
        Console.Write("Filtra per tipo azione (vuoto per tutti): ");
        string filtroAzione = Console.ReadLine() ?? "";

        Console.WriteLine("\n--- Storico Azioni ---");

    
        foreach (var kvp in _db.ActionsByUser)
        {
            int userId = kvp.Key;
            var user = _db.Users[userId];
            
        
            if (!string.IsNullOrWhiteSpace(filtroUser) && user.Username != filtroUser)
                continue;

        
            foreach (var azione in kvp.Value)
            {
            
                if (!string.IsNullOrWhiteSpace(filtroAzione) && azione.ActionType != filtroAzione)
                    continue;

                
                Console.WriteLine($"{user.Username} - {azione.Timestamp:G} - {azione.ActionType} - {azione.Metadata}");
            }
        }
    }
}
#endregion
