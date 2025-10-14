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

            Console.WriteLine("\nQuale nave vuoi creare? (NaveDaCrociera/catamarano/peschereccio)");
            string tipoNave = Console.ReadLine().ToLower();
            CreatorNave creatorNave = null;

            switch (tipoNave)
            {
                case "navedacrociera":
                    creatorNave = new ConcreteCreatorNaveDaCrociera();
                    break;
                case "catamarano":
                    creatorNave = new ConcreteCreatorCatamarano();
                    break;
                case "peschereccio":
                    creatorNave = new ConcreteCreatorPeschereccio();
                    break;
                default:
                    Console.WriteLine("Tipo di nave non valido!");
                    return;
            }
            INavi nave = creatorNave.CreaNave(tipoNave);
            nave.Naviga();
            nave.MostraTipo();
        }
    }
}
