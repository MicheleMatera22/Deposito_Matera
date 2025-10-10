namespace EnteFormativo
{
    class CorsoInPresenza : Corso
    {
        private int _aula;
        private int _numeroPosti;

        public int Aula
        {
            get { return _aula; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Il numero dell'aula deve essere un numero positivo.");
                }
                else
                {
                    _aula = value;
                }
            }
        }
        public int NumeroPosti
        {
            get { return _numeroPosti; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Il numero di posti deve essere un numero positivo.");
                }
                else
                {
                    _numeroPosti = value;
                }
            }
        }

        public override void ErogaCorso()
        {
            Console.WriteLine($"Il corso in presenza '{Titolo}' di durata {Durata} ore si terrà nell'aula {Aula} con {NumeroPosti} posti disponibili.");
        }

        public override void StampaDettagli()
        {
            Console.WriteLine($"Corso in Presenza: {Titolo}, Durata: {Durata} ore, Aula: {Aula}, Posti: {NumeroPosti}");
        }
    }
}