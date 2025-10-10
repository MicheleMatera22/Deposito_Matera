namespace Pagamenti
{
    public class PagamentoCarta : IPagamento
    {
        private string? _circuito;

        public string? Circuito
        {
            get => _circuito;
            set => _circuito = value;
        }

        public void EseguiPagamento(decimal importo)
        {
            Console.WriteLine($"Pagamento di {importo}€ effettuato con carta {_circuito}.");
        }

        public void MostroMetodo()
        {
            Console.WriteLine("Metodo di pagamento: Carta");
        }
    }
}