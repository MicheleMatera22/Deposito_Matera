using System;

namespace Operatori
{
    /// <summary>
    /// Classe derivata da Operatore che rappresenta un operatore logistico.
    /// Aggiunge la proprietà NumeroConsegne e ridefinisce il metodo EseguiCompito.
    /// </summary>
    class OperatoreLogistico : Operatore
    {
        // Campo privato per memorizzare il numero di consegne da coordinare
        private int numeroConsegne;

        /// <summary>
        /// Proprietà per accedere o impostare il numero di consegne.
        /// Accetta solo valori maggiori di zero.
        /// </summary>
        public int NumeroConsegne
        {
            get { return numeroConsegne; }
            set
            {
                // Ciclo finché l'utente non inserisce un valore maggiore di zero
                while (value <= 0)
                {
                    Console.WriteLine("Valore non ammesso! \nRiprova: ");
                    string? v = Console.ReadLine();

                    // Conversione della stringa inserita in intero
                    value = int.Parse(v);
                }
                numeroConsegne = value;
            }
        }

        /// <summary>
        /// Override del metodo EseguiCompito della classe base.
        /// Mostra il messaggio generico e il numero di consegne coordinate dall'operatore.
        /// </summary>
        /// <param name="a">Operatore su cui eseguire il compito (qui non utilizzato specificamente).</param>
        public override void EseguiCompito(Operatore a)
        {
            // Chiama il metodo della classe base
            base.EseguiCompito(a);

            // Mostra il numero di consegne coordinate
            Console.WriteLine($"Coordinamento di {numeroConsegne} consegne.");
        }
    }
}
