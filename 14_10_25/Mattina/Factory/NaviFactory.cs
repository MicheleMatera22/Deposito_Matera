namespace Factory;

public abstract class CreatorNave
{
    public abstract INavi CreaNave(string tipo);
}

public class ConcreteCreatorNaveDaCrociera : CreatorNave
{
    public override INavi CreaNave(string tipo)
    {
        return new NaveDaCrociera();
    }
}
public class ConcreteCreatorPeschereccio : CreatorNave
{
    public override INavi CreaNave(string tipo)
    {
        return new Peschereccio();
    }
}
public class ConcreteCreatorCatamarano : CreatorNave
{
    public override INavi CreaNave(string tipo)
    {
        return new Catamarano();
    }
}
