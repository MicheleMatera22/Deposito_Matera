namespace Pagamenti
{
    class PagamentoContanti : IPagamento
    {
        public void EseguiPagamento(decimal importo)
        {
            Console.WriteLine($"Pagamento di {importo}€ effettuato in contanti.");
        }

        public void MostroMetodo()
        {
            Console.WriteLine("Metodo di pagamento: Contanti");
        }
    }
}