namespace ThreeRules
{
    class Cerchio : Figura
{
    public double Raggio { get; set; }

    public override double CalcolaArea()
    {
        return Math.PI * Raggio * Raggio;
    }

    public override void Disegna(Figura f)
    {
        Console.WriteLine($"Disegno un cerchio di colore {Colore} con area {CalcolaArea()}");
    }
}
}