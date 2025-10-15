namespace Observer
{
    public  class User
    {
        private static User _instance;
        private List<IObserver> _observers = new List<IObserver>();

        private string Username;
        private User(string username)
        {
            Username = username;
        }

        public static User GetInstance(string username)
        {
            if (_instance == null)
            {
                _instance = new User(username);
            }
            return _instance;
        }

        public void RegistraOsservatore(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RimuoviOsservatore(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Stampa()
        {
            foreach (var observer in _observers)
            {
                observer.NotificaCreazione($"User: {Username} created.");
            }
        }
    }
}