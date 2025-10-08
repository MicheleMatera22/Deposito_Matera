namespace Aereo
{
    class Program
    {
        static void Main(string[] args)
        {
            VoloAereo volo = new VoloAereo { CodiceVolo = 12345 };
            Prenotazione prenotazione = new Prenotazione(1, "Mario Rossi", 3);

            if (prenotazione.ConfermaPrenotazione())
            {
                Console.WriteLine("Prenotazione confermata.");
                prenotazione.StampaDettagliPrenotazione();
            }
            else
            {
                Console.WriteLine("Prenotazione non riuscita. Posti insufficienti.");
            }

            volo.VisualizzaStato();

            prenotazione.AnnullaPrenotazione(prenotazione.PostiPrenotati);
            Console.WriteLine("Prenotazione annullata.");

            volo.VisualizzaStato();

            VoloAereo volo2 = new VoloAereo { CodiceVolo = 67890 };
            Prenotazione prenotazione2 = new Prenotazione(2, "Luigi Bianchi", 200);
            if (prenotazione2.ConfermaPrenotazione())
            {
                Console.WriteLine("Prenotazione confermata.");
                prenotazione2.StampaDettagliPrenotazione();
            }
            else
            {
                Console.WriteLine("Prenotazione non riuscita. Posti insufficienti.");
            }
            volo2.VisualizzaStato();

            prenotazione.AnnullaPrenotazione(prenotazione.PostiPrenotati);
            Console.WriteLine("Prenotazione annullata.");

            volo.VisualizzaStato();


        }
    }
}