namespace Config
{
    public interface IDispositivo
    {
        void Avvia();
        void MostraTipo();
    }

    public class Computer : IDispositivo
    {
        public void Avvia() => Console.WriteLine("Il computer è stato avviato.");
        public void MostraTipo() => Console.WriteLine("Questo è un dispositivo di tipo: Computer.");
    }

    public class Stampante : IDispositivo
    {
        public void Avvia() => Console.WriteLine("La stampante è stata avviata.");
        public void MostraTipo() => Console.WriteLine("Questo è un dispositivo di tipo: Stampante.");
    }
}