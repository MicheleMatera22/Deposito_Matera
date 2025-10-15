namespace Observer
{
    public interface ISoggetto
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify(string messaggio);
    }
}