namespace News
{
    public class MobileApp : IObserver
    {
        private readonly string nomeApp;
        private string _news;

        public MobileApp(string nome)
        {
            nomeApp = nome;
        }
        public void Update(string message)
        {
            _news = message;
            Console.WriteLine($"{nomeApp}: Notification on mobile: {_news}\n");
        }
    }
}