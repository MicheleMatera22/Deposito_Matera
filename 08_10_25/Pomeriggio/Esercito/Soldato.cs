namespace Esercito
{
    // Classe base 'Soldato' — rappresenta un generico soldato dell’esercito
    public class Soldato
    {

        // Nome del soldato (può essere nullo)
        private string? _nome;

        // Grado del soldato (es. caporale, sergente, ecc.)
        private string? _grado;

        // Numero di anni di servizio
        private int _anniDiServizio;

        // Proprietà per accedere e modificare il nome
        public string? Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                // Se il valore non è nullo, viene assegnato
                if (value != null)
                {
                    _nome = value;
                }
                else
                {
                    // Se è nullo, si imposta un valore di default
                    _nome = "Sconosciuto";
                }
            }
        }

        // Proprietà per accedere e modificare il grado del soldato
        public string? Grado
        {
            get
            {
                return _grado;
            }
            set
            {
                // Se il valore è valido, viene assegnato
                if (value != null)
                {
                    _grado = value;
                }
                else
                {
                    // In caso contrario, il grado viene impostato a “Recluta”
                    _grado = "Recluta";
                }
            }
        }

        // Proprietà per accedere e modificare gli anni di servizio
        public int AnniDiServizio
        {
            get
            {
                return _anniDiServizio;
            }
            set
            {
                // Controlla che il numero di anni sia non negativo
                if (value >= 0)
                {
                    _anniDiServizio = value;
                }
                else
                {
                    // Se il valore è negativo, mostra un messaggio di errore
                    Console.WriteLine("Anni di servizio non validi");
                }
            }
        }


        // Metodo 'Descrizione' — mostra le informazioni principali del soldato
        public virtual void Descrizione()
        {
            Console.WriteLine($"Nome: {_nome}, Grado: {_grado}, Anni di servizio: {_anniDiServizio}");
        }
    }
}
