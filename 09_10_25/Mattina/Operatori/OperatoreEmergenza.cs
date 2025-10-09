using System;

namespace Operatori
{
    /// <summary>
    /// Classe derivata da Operatore che rappresenta un operatore di emergenza.
    /// Aggiunge la proprietà LivelloUrgenza e ridefinisce il metodo EseguiCompito.
    /// </summary>
    class OperatoreEmergenza : Operatore
    {
        // Campo privato per memorizzare il livello di urgenza (1-5)
        private int livelloUrgenza;

        /// <summary>
        /// Proprietà per accedere o impostare il livello di urgenza dell'operatore.
        /// Valori consentiti: 1 (min) - 5 (max).
        /// </summary>
        public int LivelloUrgenza
        {
            get { return livelloUrgenza; }
            set
            {
                // Ciclo finché il valore non è compreso tra 1 e 5
                while ((value < 1) || (value > 5))
                {
                    Console.WriteLine("Valore non ammesso! \nRiprova:");
                    string? v = Console.ReadLine();

                    // Conversione della stringa inserita in intero
                    value = int.Parse(v);
                }
                livelloUrgenza = value;
            }
        }

        /// <summary>
        /// Override del metodo EseguiCompito della classe base.
        /// Mostra il messaggio generico e il livello di urgenza dell'operatore.
        /// </summary>
        /// <param name="a">Operatore su cui eseguire il compito (qui non utilizzato specificamente).</param>
        public override void EseguiCompito(Operatore a)
        {
            // Chiama il metodo della classe base
            base.EseguiCompito(a);

            // Mostra il livello di urgenza specifico dell'operatore emergenza
            Console.WriteLine($"Livello urgenza: {livelloUrgenza}");
        }
    }
}
