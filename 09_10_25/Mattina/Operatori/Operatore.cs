using System;

namespace Operatori
{
    /// <summary>
    /// Classe base che rappresenta un operatore generico.
    /// Contiene le proprietà Nome e Turno e un metodo virtuale per eseguire un compito.
    /// </summary>
    public class Operatore
    {
        // Campo privato per memorizzare il nome dell'operatore
        private string? nome;

        // Campo privato per memorizzare il turno dell'operatore ("giorno" o "notte")
        private string? turno;

        /// <summary>
        /// Proprietà per accedere o impostare il nome dell'operatore.
        /// Non consente valori nulli o stringhe vuote.
        /// </summary>
        public string? Nome
        {
            get => nome;
            set
            {
                // Ciclo finché l'utente non inserisce un valore valido
                while (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Valore nullo non ammesso!\nRiprova: ");
                    value = Console.ReadLine();
                }
                nome = value;
            }
        }

        /// <summary>
        /// Proprietà per accedere o impostare il turno dell'operatore.
        /// Accetta solo "giorno" o "notte" (case insensitive).
        /// </summary>
        public string? Turno
        {
            get => turno;
            set
            {
                // Ciclo finché l'utente non inserisce "giorno" o "notte"
                while ((string.IsNullOrWhiteSpace(value)) || ((value.ToLower() != "giorno") && (value.ToLower() != "notte")))
                {
                    Console.WriteLine("Valore non ammesso! \nRiprova: ");
                    value = Console.ReadLine();
                }
                turno = value;
            }
        }

        /// <summary>
        /// Metodo virtuale che rappresenta l'esecuzione di un compito da parte dell'operatore.
        /// Può essere sovrascritto nelle classi derivate per comportamenti specifici.
        /// </summary>
        /// <param name="a">Operatore su cui eseguire il compito (di default stesso operatore).</param>
        public virtual void EseguiCompito(Operatore a)
        {
            Console.WriteLine("Operatore generico in servizio");
        }
    }
}
