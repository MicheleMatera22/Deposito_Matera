namespace Prenotazioni
{
    public class Utente
    {
        // Campi privati per memorizzare le informazioni dell'utente
        private string nome;
        private string cognome;
        private string email;

        // Proprietà pubblica Nome con get e set, valida solo valori non vuoti o nulli
        public string Nome
        {
            get { return nome; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value)) // Controlla che il valore non sia null, vuoto o solo spazi
                    nome = value;
            }
        }

        // Proprietà pubblica Cognome con get e set, valida solo valori non vuoti o nulli
        public string Cognome
        {
            get { return cognome; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    cognome = value;
            }
        }

        // Proprietà pubblica Email con get e set, valida solo valori non vuoti o nulli
        public string Email
        {
            get { return email; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    email = value;
            }
        }

        // Lista privata per memorizzare le prenotazioni dell'utente
        private List<PrenotazioneViaggio> prenotazioni = new List<PrenotazioneViaggio>();

        // Proprietà pubblica Prenotazioni per accedere alla lista delle prenotazioni
        public List<PrenotazioneViaggio> Prenotazioni
        {
            get { return prenotazioni; }
        }

        // Costruttore della classe Utente che inizializza nome, cognome ed email
        public Utente(string nome, string cognome, string email)
        {
            Nome = nome;
            Cognome = cognome;
            Email = email;
        }

        // Metodo per aggiungere una prenotazione alla lista dell'utente
        public void AggiungiPrenotazione(PrenotazioneViaggio prenotazione)
        {
            if (prenotazione != null) // Controlla che l'oggetto non sia null
                prenotazioni.Add(prenotazione);
        }

        // Metodo per rimuovere una prenotazione dalla lista dell'utente
        public void RimuoviPrenotazione(PrenotazioneViaggio prenotazione)
        {
            if (prenotazione != null) // Controlla che l'oggetto non sia null
                prenotazioni.Remove(prenotazione);
        }
    }

}