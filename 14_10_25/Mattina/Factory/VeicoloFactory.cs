namespace Factory;

public abstract class Creator
{
    public abstract IVeicolo CreaVeicolo(string tipo);
}

public class ConcreteCreatorAuto : Creator
{
    public override IVeicolo CreaVeicolo(string tipo)
    {
        return new Auto();
    }
}
public class ConcreteCreatorMoto : Creator
{
    public override IVeicolo CreaVeicolo(string tipo)
    {
        return new Moto();
    }
}
public class ConcreteCreatorCamion : Creator
{
    public override IVeicolo CreaVeicolo(string tipo)
    {
        return new Camion();
    }
}
