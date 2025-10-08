namespace Prenotazioni
{
    class Program
    {
        // Punto di ingresso dell'applicazione
        static void Main(string[] args)
        {
            // Creo un nuovo utente con nome, cognome ed email
            Utente utente = new Utente("Luca", "Rossi", "luca.rossi@email.com");

            // Stampo le informazioni dell'utente creato
            Console.WriteLine($"Utente creato: {utente.Nome} {utente.Cognome}, Email: {utente.Email}\n");

            // Creo due oggetti PrenotazioneViaggio per due destinazioni diverse
            PrenotazioneViaggio viaggio1 = new PrenotazioneViaggio();
            viaggio1.Destinazione = "Roma";

            PrenotazioneViaggio viaggio2 = new PrenotazioneViaggio();
            viaggio2.Destinazione = "Parigi";

            // Aggiungo le prenotazioni all'elenco dell'utente
            utente.AggiungiPrenotazione(viaggio1);
            utente.AggiungiPrenotazione(viaggio2);

            // Prenoto 5 posti per il viaggio a Roma
            viaggio1.PrenotaPosti(5);

            // Stampo lo stato attuale del viaggio a Roma
            Console.WriteLine($"Viaggio a {viaggio1.Destinazione}: {viaggio1.PostiPrenotati} posti prenotati, {viaggio1.PostiDisponibili} posti disponibili");

            // Prenoto 8 posti per il viaggio a Parigi
            viaggio2.PrenotaPosti(8);

            // Stampo lo stato attuale del viaggio a Parigi
            Console.WriteLine($"Viaggio a {viaggio2.Destinazione}: {viaggio2.PostiPrenotati} posti prenotati, {viaggio2.PostiDisponibili} posti disponibili\n");

            // Annulla 2 posti prenotati per il viaggio a Roma
            viaggio1.AnnullaPrenotazione(2);

            // Stampo lo stato aggiornato del viaggio a Roma dopo l'annullamento
            Console.WriteLine($"Dopo annullamento - Viaggio a {viaggio1.Destinazione}: {viaggio1.PostiPrenotati} posti prenotati, {viaggio1.PostiDisponibili} posti disponibili");

            // Annulla 3 posti prenotati per il viaggio a Parigi
            viaggio2.AnnullaPrenotazione(3);

            // Stampo lo stato aggiornato del viaggio a Parigi dopo l'annullamento
            Console.WriteLine($"Dopo annullamento - Viaggio a {viaggio2.Destinazione}: {viaggio2.PostiPrenotati} posti prenotati, {viaggio2.PostiDisponibili} posti disponibili\n");

            // Stampo tutte le prenotazioni dell'utente
            Console.WriteLine($"Prenotazioni di {utente.Nome}:");

            // Ciclo attraverso tutte le prenotazioni dell'utente e stampo dettagli
            foreach (var viaggio in utente.Prenotazioni)
            {
                Console.WriteLine($"- Destinazione: {viaggio.Destinazione}, Posti prenotati: {viaggio.PostiPrenotati}, Posti disponibili: {viaggio.PostiDisponibili}");
            }
        }
    }
}