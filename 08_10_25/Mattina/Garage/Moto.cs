namespace Garage
{
    class Moto : Veicolo
    {
        // Proprietà specifica per le moto
        public string TipoManubrio { get; set; }

        // Override del metodo StampaInfo per includere le informazioni aggiuntive
        public override void StampaInfo()
        {
            Console.WriteLine($"[MOTO] Marca: {Marca}, Modello: {Modello}, Tipo manubrio: {TipoManubrio}");
        }
    }
}