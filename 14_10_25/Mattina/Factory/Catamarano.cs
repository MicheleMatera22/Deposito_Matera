namespace Factory;

public class Catamarano : INavi
{
    public void Naviga()
    {
        Console.WriteLine("Il catamarano sta navigando.");
    }

    public void MostraTipo()
    {
        Console.WriteLine("Tipo di nave: Catamarano");
    }
}