namespace Aereo
{
    class VoloAereo
    {
        private int postiOccupati;
        const int MAXPOSTI = 150;
        public int CodiceVolo { get; set; }
        public int PostiOccupati { get { return postiOccupati; } }
        public int PostiLiberi { get { return MAXPOSTI - postiOccupati; } }

        public bool EffettuaPrenotazione(int posti)
        {
            Console.WriteLine($"Posti da prenotare: {posti}");
            if (posti <= 0 || posti > MAXPOSTI - postiOccupati)
            {
                return false; // Prenotazione non valida
            }
            else
            {
                postiOccupati += posti;
                return true; // Prenotazione riuscita
            }

        }



        public void AnnullaPrenotazione(int posti)
        {
            if (posti > 0 && posti <= postiOccupati)
            {
                postiOccupati -= posti;
            }
        }

        public void VisualizzaStato()
        {
            Console.WriteLine($"Volo {CodiceVolo}: Posti Occupati: {PostiOccupati}, Posti Liberi: {PostiLiberi}");
        }
        
    }
}