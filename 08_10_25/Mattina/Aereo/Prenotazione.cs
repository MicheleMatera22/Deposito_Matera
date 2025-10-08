namespace Aereo
{
    class Prenotazione : VoloAereo
    {
        public int CodicePrenotazione { get; set; }
        public string NomeCliente { get; set; }
        public int PostiPrenotati { get; set; }

        public Prenotazione(int codicePrenotazione, string nomeCliente, int postiPrenotati)
        {
            CodicePrenotazione = codicePrenotazione;
            NomeCliente = nomeCliente;
            PostiPrenotati = postiPrenotati;
        }

        public void StampaDettagliPrenotazione()
        {
            Console.WriteLine($"Prenotazione Codice: {CodicePrenotazione}, Cliente: {NomeCliente}, Posti Prenotati: {PostiPrenotati}");
        }

        public bool ConfermaPrenotazione()
        {
            Console.WriteLine($"Tentativo di prenotare {PostiPrenotati} posti per {NomeCliente} (Codice Prenotazione: {CodicePrenotazione})");
            return EffettuaPrenotazione(PostiPrenotati);
        }

    }
}