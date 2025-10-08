namespace Esercito
{
    // La classe 'Artigliere' eredita da 'Soldato'
    // Rappresenta un tipo di soldato specializzato nell'uso di armi pesanti e artiglieria
    class Artigliere : Soldato
    {
        // Campo che memorizza il calibro dell’arma (in millimetri)
        private int _calibro;

        // Proprietà per accedere e modificare il calibro dell'artiglieria
        public int Calibro
        {
            get
            {
                return _calibro;
            }
            set
            {
                // Il calibro deve essere positivo per essere valido
                if (value > 0)
                {
                    _calibro = value;
                }
                else
                {
                    // Se il valore non è valido, assegna un calibro di default (7 mm)
                    _calibro = 7;
                }
            }
        }

        // Ridefinisce (override) il metodo Descrizione della classe base Soldato
        public override void Descrizione()
        {
            // Richiama il metodo della classe base per mostrare nome, grado e anni di servizio
            base.Descrizione();

            // Aggiunge le informazioni specifiche dell’artigliere
            Console.WriteLine($"Calibro: {_calibro} mm");
        }
    }
}
