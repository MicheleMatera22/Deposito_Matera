namespace Officina
{
    class Veicolo
    {
        private string? _targa { get; set; }

        public string Targa
        {
            get => _targa;
            set
            {
                if (value == null)
                {
                    Console.WriteLine("La targa non può essere nulla.");
                }
                else
                {
                    _targa = value;
                }
            }
        }

        public virtual void Ripara()
        {
            Console.WriteLine($"Il veicolo viene controllato.");
        }
    }
}