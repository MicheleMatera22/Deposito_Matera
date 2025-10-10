namespace Pagamenti
{
    class PagamentoPayPal : IPagamento
    {
        private string? _email;

        public string? Email
        {
            get => _email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                {
                    Console.WriteLine("Email non valida.");
                }
                else
                {
                    _email = value;
                }
            }
        }

        public void EseguiPagamento(decimal importo)
        {
            Console.WriteLine($"Pagamento di {importo}€ effettuato con PayPal (email: {_email}).");
        }

        public void MostroMetodo()
        {
            Console.WriteLine("Metodo di pagamento: PayPal");
        }
    }
}