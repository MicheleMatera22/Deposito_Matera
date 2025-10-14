namespace Factory;

class Camion : IVeicolo
{
    public void Avvia()
    {
        Console.WriteLine("Il camion è avviato.");
    }

    public void MostraTipo()
    {
        Console.WriteLine("Questo è un veicolo di tipo Camion.");
    }
}