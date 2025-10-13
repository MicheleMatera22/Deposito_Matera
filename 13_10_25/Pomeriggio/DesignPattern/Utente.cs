namespace DesignPattern;

    public class Utente
    {
        public string Nome { get; }

        public Utente(string nome)
        {
            Nome = nome;
        }

        public void EseguiAzione(string azione)
        {
            Logger logger = Logger.GetIstanza();
            logger.ScriviMessaggio($"Utente {Nome} esegue azione: {azione}");
        }
    }
