namespace EnteFormativo
{
    abstract class Corso
    {
        private string? _titolo;
        private int _durata;        
        public string? Titolo
        {
            get { return _titolo; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Il titolo non può essere vuoto.");
                }
                else
                {
                    _titolo = value;
                }
            }
        }

        public int Durata
        {
            get { return _durata; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("La durata deve essere un numero positivo.");
                }
                else
                {
                    _durata = value;
                }
            }
        }


        public abstract void ErogaCorso();
        public abstract void StampaDettagli();
    }
}