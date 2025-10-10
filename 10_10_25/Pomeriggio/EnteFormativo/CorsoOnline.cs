namespace EnteFormativo
{
    class CorsoOnline : Corso
    {
        private string? _piattaforma;
        private string? _linkAccesso;

        public string? Piattaforma
        {
            get { return _piattaforma; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("La piattaforma non può essere vuota.");
                }
                else
                {
                    _piattaforma = value;
                }
            }
        }

        public string? LinkAccesso
        {
            get { return _linkAccesso; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Il link di accesso non può essere vuoto.");
                }
                else
                {
                    _linkAccesso = value;
                }
            }
        }

        public override void ErogaCorso()
        {
            Console.WriteLine($"Il corso online '{Titolo}' di durata {Durata} ore si terrà sulla piattaforma {_piattaforma} con link accesso:  {_linkAccesso}.");
        }

        public override void StampaDettagli()
        {
            Console.WriteLine($"Corso Online: {Titolo}, Durata: {Durata} ore, Piattaforma: {_piattaforma}, link accesso: {_linkAccesso}");
        }
    }
}