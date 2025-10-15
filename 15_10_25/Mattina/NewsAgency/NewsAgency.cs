namespace News
{
    public sealed class NewsAgency : ISubject
    {
        private readonly List<IObserver> obj = new List<IObserver>();
        private string message;

        private static NewsAgency? istanza;
        private NewsAgency() { }

        public static NewsAgency GetIstanza()
        {
            if (istanza == null)
            {
                istanza = new NewsAgency();
            }
            return istanza;
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

        public void Attach(IObserver observer)
        {
                obj.Add(observer);
        }
        

        public void Detach(IObserver observer)
        {
            obj.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in obj)
            {
                observer.Update(message);
            }
        }
    }
}