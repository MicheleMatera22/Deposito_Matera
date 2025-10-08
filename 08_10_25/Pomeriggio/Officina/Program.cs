using System.Runtime;

namespace Officina
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Veicolo> veicoli = new List<Veicolo>
            {
                new Auto { Targa = "AB123CD" },
                new Moto { Targa = "EF456GH" },
                new Camion { Targa = "IJ789KL" }
            };
            foreach (var veicolo in veicoli)
            {
                Console.WriteLine($"Riparazione del veicolo con targa: {veicolo.Targa}");
                veicolo.Ripara();
                Console.WriteLine();
            }
        }
    }
}