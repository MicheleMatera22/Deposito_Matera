namespace Garage
{
class Veicolo
    {
        // Proprietà comuni a tutti i veicoli
        public string Marca { get; set; }
        public string Modello { get; set; }

        // Metodo virtuale che può essere ridefinito nelle classi derivate
        public virtual void StampaInfo()
        {
            Console.WriteLine($"Marca: {Marca}, Modello: {Modello}");
        }
    }
}