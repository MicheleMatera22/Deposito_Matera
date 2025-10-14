namespace Factory;

public class NaveDaCrociera : INavi
{
    public void Naviga()
    {
        Console.WriteLine("La nave da crociera sta navigando.");
    }

    public void MostraTipo()
    {
        Console.WriteLine("Tipo di nave: Nave da Crociera");
    }
}