namespace ThreeRules
{
    class Figura
    {
        private string? _colore;

        public string? Colore
        {
            get => _colore;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _colore = "Bianco";
                }
                else
                {
                    _colore = value;
                }
            }
        }

        public virtual double CalcolaArea()
        {
            return 0;
        }

        public virtual void Disegna(Figura f)
        {
            Console.WriteLine($"Tipo della figura: {f.GetType().Name}");

            if (f is Rettangolo r)
            {
                Console.WriteLine($"Disegno un rettangolo di colore {r.Colore} con area {r.Base * r.Altezza}");
            }
            else if (f is Cerchio c)
            {
                Console.WriteLine($"Disegno un cerchio di colore {c.Colore} con area {Math.PI * c.Raggio * c.Raggio:F2}");
            }
            else
            {
                Console.WriteLine($"Disegno una figura generica di colore {f.Colore}");
            }
        }
    }
}