namespace ThreeRules
{
    class Program
{
    static void Main()
    {
        Figura f1 = new Rettangolo { Colore = "Rosso", Base = 5, Altezza = 3 };
        Figura f2 = new Cerchio { Colore = "Blu", Raggio = 4 };

            f1.Disegna(f1);
            f2.Disegna(f2);
    }
}
} 
