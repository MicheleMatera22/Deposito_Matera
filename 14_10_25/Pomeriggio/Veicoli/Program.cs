using System;

namespace Veicoli
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RegistroVeicoli veicoli = RegistroVeicoli.GetInstance();
            bool continua = true;
            int scelta;

            while(continua)
            {
                Console.WriteLine($"-----Menu-----");
                Console.WriteLine($"1. Inserire quale veicolo creare.");
                Console.WriteLine($"2. Stampa dettagli.");
                Console.WriteLine($"3. Uscire.");
                scelta = int.Parse(Console.ReadLine());

                switch (scelta)
                {
                    case 1:
                    Console.WriteLine($"Che tipo di veicolo vuoi creare?\nAuto\nMoto\nCamion");
                    string? sceltaVeicolo = Console.ReadLine().ToLower();
                    VeicoloFactory.creaVeicolo(sceltaVeicolo);
                    switch (sceltaVeicolo)
                    {
                        case "auto":
                            veicoli.Registra(VeicoloFactory.creaVeicolo(sceltaVeicolo));
                            break;
                            case "moto":
                            veicoli.Registra(VeicoloFactory.creaVeicolo(sceltaVeicolo));
                            break;
                            case "camion":
                            veicoli.Registra(VeicoloFactory.creaVeicolo(sceltaVeicolo));
                            break;
                        }
                        break;
                        case 2:
                        veicoli.Stampa();
                        break;
                        case 3:
                        Console.WriteLine($"Arrivederci!");
                        continua = false;
                        break;
                        default:
                        Console.WriteLine($"Scelta non valida.");
                        break;
                    }
            }
        }
    }
}
