namespace Observer
{
    public sealed class GestoreCreazioneUtente : ISubject
    {
        private readonly List<IObserver> observers = new List<IObserver>();

        private GestoreCreazioneUtente _instance;

        private GestoreCreazioneUtente() { }
        public GestoreCreazioneUtente GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GestoreCreazioneUtente();
            }
            return _instance;
        
        }
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                Notify(message);
            }
        }

        public void CreateUser(string username)
        {
            var utente = UserFactory.CreateUser(username);
            Notify($"Utente '{utente}' creato con successo.");
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in observers)
            {
                observer.NotificaCreazione(message);
            }
        }
    }
}