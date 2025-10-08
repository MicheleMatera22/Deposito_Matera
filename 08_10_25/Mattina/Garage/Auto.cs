namespace Garage
{
class Auto : Veicolo
    {
        // Proprietà specifica per le auto
        public int NumeroPorte { get; set; }

        // Override del metodo StampaInfo per includere le informazioni aggiuntive
        public override void StampaInfo()
        {
            Console.WriteLine($"[AUTO] Marca: {Marca}, Modello: {Modello}, Numero porte: {NumeroPorte}");
        }
    }
}