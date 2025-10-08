namespace Prenotazioni
{
    public class PrenotazioneViaggio
    {
        // Campo privato per memorizzare il numero di posti prenotati
        private int postiPrenotati;

        // Numero massimo fisso di posti disponibili per ogni viaggio
        private const int maxPosti = 20;

        // Proprietà pubblica Destinazione con get e set
        public string? Destinazione { get; set; }

        // Proprietà calcolata PostiDisponibili: restituisce i posti rimasti
        public int PostiDisponibili
        {
            get { return maxPosti - postiPrenotati; }
        }

        // Proprietà sola lettura per ottenere i posti prenotati
        public int PostiPrenotati
        {
            get { return postiPrenotati; }
        }

        // Metodo per prenotare posti
        public bool PrenotaPosti(int numero)
        {
            // Controlla che il numero di posti da prenotare sia positivo
            if (numero <= 0)
            {
                Console.WriteLine("Numero di posti non valido.");
                return false;
            }

            // Verifica se ci sono abbastanza posti disponibili
            if (numero <= PostiDisponibili)
            {
                postiPrenotati += numero; // Aggiunge i posti prenotati
                return true;
            }
            else
            {
                Console.WriteLine("Non ci sono abbastanza posti disponibili.");
                return false;
            }
        }

        // Metodo per annullare prenotazioni
        public bool AnnullaPrenotazione(int numero)
        {
            // Controlla che il numero di posti da annullare sia positivo
            if (numero <= 0)
            {
                Console.WriteLine("Numero di posti non valido.");
                return false;
            }

            // Verifica se il numero da annullare non supera i posti già prenotati
            if (numero <= postiPrenotati)
            {
                postiPrenotati -= numero; // Riduce i posti prenotati
                return true;
            }
            else
            {
                Console.WriteLine("Non puoi annullare più posti di quelli prenotati.");
                return false;
            }
        }
    }

}