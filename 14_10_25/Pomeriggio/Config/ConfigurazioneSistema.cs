namespace Config
{
    public sealed class Configurazione
    {
        private static Configurazione instance = null;
        private Dictionary<string, string> settings = new Dictionary<string, string>();

        private Configurazione()
        {
            
        }

        public static Configurazione GetInstance()
        {
            if (instance == null)
            {
                instance = new Configurazione();
            }
            return instance;
        }

        public void Imposta(string chiave, string valore)
        {
            settings[chiave] = valore;
        }

        public string Leggi(string chiave)
        {
            return settings.ContainsKey(chiave) ? settings[chiave] : null;
        }

        public void Stampa()
        {
            foreach (var set in settings)
            {
                Console.WriteLine($"{set.Key}: {set.Value}");
            }
        }
    }
}