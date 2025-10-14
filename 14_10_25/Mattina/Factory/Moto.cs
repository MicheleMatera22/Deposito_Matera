namespace Factory;

class Moto : IVeicolo
{
    public void Avvia()
    {
        Console.WriteLine("La moto è avviata.");
    }

    public void MostraTipo()
    {
        Console.WriteLine("Questo è un veicolo di tipo Moto.");
    }
}