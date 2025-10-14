namespace Factory;

class Auto : IVeicolo
{
    public void Avvia()
    {
        Console.WriteLine("L'auto è avviata.");
    }

    public void MostraTipo()
    {
        Console.WriteLine("Questo è un veicolo di tipo Auto.");
    }
}