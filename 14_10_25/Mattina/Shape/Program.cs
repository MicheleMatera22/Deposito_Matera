using System;
namespace Shape;

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Quale forma vuoi disegnare? (Circle/Square)");
            string? tipo = Console.ReadLine().ToLower();

            Creator creator = null;

            switch (tipo)
            {
                case "circle":
                    creator = new ConcreteShapeCircle();
                    break;
                case "square":
                    creator = new ConcreteShapeSquare();
                    break;
                default:
                    Console.WriteLine("Tipo di forma non valido!");
                    return;
            }

            IShape shape = creator.CreateShape(tipo);
            shape.Draw();

            Console.WriteLine("\nPremi un tasto per uscire...");
            Console.ReadKey();
        }
    }

