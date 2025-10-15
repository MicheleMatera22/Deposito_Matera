namespace Observer
{
    public class ModuloMarketing : IObserver
    {
        public void NotificaCreazione(string message)
        {
            Console.WriteLine($"[MARKETING] {message}");
        }
    }
}