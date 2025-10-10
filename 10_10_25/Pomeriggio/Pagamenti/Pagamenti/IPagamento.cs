namespace Pagamenti
{
    public interface IPagamento
    {
        void EseguiPagamento(decimal importo);
        void MostroMetodo();
    }
}