namespace Esercito
{
    // Classe che rappresenta un esercito
    public class Esercito
    {
        // Nome dell'esercito
        public string Nome { get; set; }

        // Lista privata dei soldati
        private List<Soldato> soldati = new List<Soldato>();

        // Proprietà per ottenere il numero totale di soldati
        public int TotaleSoldati
        {
            get { return soldati.Count; }
        }

        // Costruttore che richiede il nome dell'esercito
        public Esercito(string nome)
        {
            Nome = nome;
        }

        // Metodo per aggiungere un soldato
        public void AggiungiSoldato(Soldato soldato)
        {
            if (soldato != null)
            {
                soldati.Add(soldato);
                Console.WriteLine($"Soldato aggiunto all'esercito '{Nome}': {soldato.Nome}");
            }
        }

        // Metodo per rimuovere un soldato
        public void RimuoviSoldato(Soldato soldato)
        {
            if (soldati.Contains(soldato))
            {
                soldati.Remove(soldato);
                Console.WriteLine($"Soldato rimosso dall'esercito '{Nome}': {soldato.Nome}");
            }
        }

        // Metodo per mostrare tutti i soldati
        public void MostraSoldati()
        {
            Console.WriteLine($"\n--- Soldati dell'esercito '{Nome}' ---");
            if (soldati.Count == 0)
            {
                Console.WriteLine("Nessun soldato presente.");
            }
            else
            {
                foreach (var soldato in soldati)
                {
                    soldato.Descrizione();
                    Console.WriteLine();
                }
            }
        }

        // Metodo per ottenere lista dei soldati in sola lettura
        public List<Soldato> ElencoSoldati()
        {
            return soldati;
        }
    }
}
