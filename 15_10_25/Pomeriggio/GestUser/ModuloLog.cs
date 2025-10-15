namespace Observer
{
    public class ModuloLog : IObserver
    {
        public void NotificaCreazione(string message)
        {
            Console.WriteLine($"[LOG] {message}");
        }
    }
}