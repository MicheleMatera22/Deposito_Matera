namespace EnteFormativo
{
    class Docente
    {
        private List<Corso> _corsiAssegnati = new List<Corso>();
        private string? _nome;
        private string? _materia;

        public string? Nome
        {
            get { return _nome; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Il nome non può essere vuoto.");
                }
                else
                {
                    _nome = value;
                }
            }
        }
        public string? Materia
        {
            get { return _materia; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("La materia non può essere vuota.");
                }
                else
                {
                    _materia = value;
                }
            }
        }

        public void AssegnaCorso(Corso corso)
        {
            if (corso == null)
            {
                Console.WriteLine("Il corso non può essere nullo.");
                return;
            }
            _corsiAssegnati.Add(corso);
        }

        public void StampaCorsiAssegnati()
        {
            Console.WriteLine($"Docente: {Nome}, Materia: {Materia}");
            if (_corsiAssegnati.Count == 0)
            {
                Console.WriteLine("Nessun corso assegnato.");
            }
            else
            {
                Console.WriteLine("Corsi assegnati:");
                foreach (var corso in _corsiAssegnati)
                {
                    corso.StampaDettagli();
                }
            }
        }

        public void RimuoviCorso(Corso corso)
        {
            if (corso == null)
            {
                Console.WriteLine("Il corso non può essere nullo.");
                return;
            }
            if (_corsiAssegnati.Contains(corso))
            {
                _corsiAssegnati.Remove(corso);
            }
            else
            {
                Console.WriteLine("Il corso non è assegnato a questo docente.");
            }
        }

        public void ErogaCorsi()
        {
            foreach (var corso in _corsiAssegnati)
            {
                corso.ErogaCorso();
            }
        }
    }
}