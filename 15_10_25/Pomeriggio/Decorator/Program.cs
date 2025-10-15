#region INTERFACCE

public interface IBevanda
{
    string Descrizione();
    double Costo();
}
#endregion

#region CLASSI CONCRETE
public class Caffe : IBevanda
{
    public string Descrizione()
    {
        return "Caffè";
    }

    public double Costo()
    {
        return 1.0;
    }
}

public class Te : IBevanda
{
    public string Descrizione()
    {
        return "Tè";
    }

    public double Costo()
    {
        return 0.8;
    }
}

#endregion

#region DECORATOR
public abstract class DecoratorBevanda : IBevanda
{
    protected IBevanda _bevanda;

    protected DecoratorBevanda(IBevanda bevanda)
    {
        _bevanda = bevanda;
    }

    public virtual string Descrizione()
    {
        return _bevanda.Descrizione();
    }

    public virtual double Costo()
    {
        return _bevanda.Costo();
    }
}

#endregion

#region DECORATOR CONCRETI
public class Latte : DecoratorBevanda
{
    public Latte(IBevanda bevanda) : base(bevanda) { }

    public override string Descrizione()
    {

        base.Descrizione();
        return "+ Latte";
    }

    public override double Costo()
    {
        return base.Costo() + 0.5;
    }
}

public class Cioccolato : DecoratorBevanda
{
    public Cioccolato(IBevanda bevanda) : base(bevanda) { }

    public override string Descrizione()
    {
        base.Descrizione();
        return "+ Cioccolato";
    }

    public override double Costo()
    {
        return _bevanda.Costo() + 0.7;
    }
}

public class Panna : DecoratorBevanda
{
    public Panna(IBevanda bevanda) : base(bevanda) { }

    public override string Descrizione()
    {
        return "+ Panna";
    }

    public override double Costo()
    {
        return _bevanda.Costo() + 0.6;
    }
}

#endregion

#region PROGRAM
class Program
{
    static void Main(string[] args)
    {
        IBevanda bevanda = new Caffe();
        double costo = bevanda.Costo();
        Console.WriteLine($"Prodotto: {bevanda.Descrizione()} -  {bevanda.Costo()}€");

        bevanda = new Latte(bevanda);
        costo = bevanda.Costo() - costo;
        Console.WriteLine($"{bevanda.Descrizione()}         -  +{costo:F2}€");
        costo = bevanda.Costo();

        bevanda = new Cioccolato(bevanda);
        costo = bevanda.Costo() - costo;
        Console.WriteLine($"{bevanda.Descrizione()}    -  +{costo:F2}€");
        costo = bevanda.Costo();

        bevanda = new Panna(bevanda);
        costo = bevanda.Costo() - costo;
        Console.WriteLine($"{bevanda.Descrizione()}         -  +{costo:F2}€");
        Console.WriteLine($"\n\n            Totale: {bevanda.Costo():F2}€");
    }
}

#endregion
