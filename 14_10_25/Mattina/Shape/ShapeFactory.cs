namespace Shape;

public abstract class Creator
{
    public abstract IShape CreateShape(string tipo);
}

public class ConcreteShapeCircle : Creator
{
    public override IShape CreateShape(string tipo)
    {
        return new Circle();
    }
}

public class ConcreteShapeSquare : Creator
{
    public override IShape CreateShape(string tipo)
    {
        return new Square();
    }
}