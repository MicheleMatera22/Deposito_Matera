namespace Esercito
{
    // La classe 'Fante' eredita dalla classe base 'Soldato'
    // Rappresenta un tipo specifico di soldato che combatte a terra
    class Fante : Soldato
    {
        // Arma utilizzata dal fante (es. fucile, mitra, pistola)
        private string? _arma;

        // Proprietà per accedere e modificare l’arma del fante
        public string? Arma
        {
            get
            {
                return _arma;
            }
            set
            {
                // Se l'arma è specificata, viene assegnata normalmente
                if (value != null)
                {
                    _arma = value;
                }
                else
                {
                    // Se non viene fornito alcun valore, si imposta un’arma predefinita
                    _arma = "Pistola";
                }
            }
        }

        // Il metodo 'Descrizione' ridefinisce (override) quello della classe base 'Soldato'
        public override void Descrizione()
        {
            // Chiama il metodo Descrizione() della classe Soldato
            base.Descrizione();

            // Aggiunge informazioni specifiche del Fante
            Console.WriteLine($"Arma: {_arma}");
        }
    }
}
