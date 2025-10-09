using System;

namespace Operatori
{
    /// <summary>
    /// Classe derivata da Operatore che rappresenta un operatore di sicurezza.
    /// Aggiunge la proprietà AreaSorvegliata e ridefinisce il metodo EseguiCompito.
    /// </summary>
    class OperatoreSicurezza : Operatore
    {
        // Campo privato per memorizzare l'area sorvegliata dall'operatore
        private string? areaSorvegliata;

        /// <summary>
        /// Proprietà per accedere o impostare l'area sorvegliata.
        /// Non accetta valori nulli o stringhe vuote.
        /// </summary>
        public string? AreaSorvegliata
        {
            get => areaSorvegliata;
            set
            {
                // Ciclo finché l'utente non inserisce un valore valido
                while (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Valore non ammesso! \nRiprova:");
                    value = Console.ReadLine();
                }
                areaSorvegliata = value;
            }
        }

        /// <summary>
        /// Override del metodo EseguiCompito della classe base.
        /// Mostra il messaggio generico e l'area sorvegliata dall'operatore.
        /// </summary>
        /// <param name="a">Operatore su cui eseguire il compito (qui non utilizzato specificamente).</param>
        public override void EseguiCompito(Operatore a)
        {
            // Chiama il metodo della classe base
            base.EseguiCompito(a);

            // Mostra l'area sorvegliata
            Console.WriteLine($"Sorveglianza dell'area {areaSorvegliata}");
        }
    }
}
