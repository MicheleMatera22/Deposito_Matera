namespace Observer
{
    public class DisplayMobile : IObserver
    {
        private readonly string _name;
        private string _observerData;

        public DisplayMobile(string name)
        {
            _name = name;
        }

        public void Update(string message)
        {
            _observerData = message;
            Console.WriteLine($"{_name} ha ricevuto aggiornamento, stato = {_observerData}");
        }
    }
}