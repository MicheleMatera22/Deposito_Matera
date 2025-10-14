namespace Factory;

public class Peschereccio : INavi
{
    public void Naviga()
    {
        Console.WriteLine("Il peschereccio sta navigando.");
    }

    public void MostraTipo()
    {
        Console.WriteLine("Tipo di nave: Peschereccio");
    }
}