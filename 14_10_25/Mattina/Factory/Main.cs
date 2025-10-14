namespace Factory
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Quale veicolo vuoi creare? (auto/moto/camion)");
            string tipo = Console.ReadLine().ToLower();

            Creator creator = null;

            switch (tipo)
            {
                case "auto":
                    creator = new ConcreteCreatorAuto();
                    break;
                case "moto":
                    creator = new ConcreteCreatorMoto();
                    break;
                case "camion":
                    creator = new ConcreteCreatorCamion();
                    break;
                default:
                    Console.WriteLine("Tipo non valido!");
                    return;
            }

            IVeicolo veicolo = creator.CreaVeicolo(tipo);
            veicolo.Avvia();
            veicolo.MostraTipo();
        }
    }
}
