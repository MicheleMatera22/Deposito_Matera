namespace Pagamenti
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IPagamento> metodiPagamento = new List<IPagamento>(); 


            bool selezione = true;
            while (selezione)
            {            
                Console.WriteLine("Seleziona i metodi di pagamento da aggiungere:");
                Console.WriteLine("1. Contanti");
                Console.WriteLine("2. Carta");
                Console.WriteLine("3. PayPal");
                Console.WriteLine("0. Fine selezione");
                Console.Write("Scelta: ");
                if (!int.TryParse(Console.ReadLine(), out int scelta))
                {
                    Console.WriteLine("Input non valido!");
                    continue;
                }

                switch (scelta)
                {
                    case 1:
                        PagamentoContanti pagamentoContanti = new PagamentoContanti();
                        metodiPagamento.Add(pagamentoContanti);

                        break;
                    case 2:
                        PagamentoCarta pagamentoCarta = new PagamentoCarta();
                        Console.Write("Inserisci il circuito della carta (es. Visa, MasterCard): ");
                        pagamentoCarta.Circuito = Console.ReadLine();
                        metodiPagamento.Add(pagamentoCarta);

                        break;
                    case 3:
                        PagamentoPayPal pagamentoPayPal = new PagamentoPayPal();
                        Console.Write("Inserisci l'email associata a PayPal: ");
                        pagamentoPayPal.Email = Console.ReadLine();
                        metodiPagamento.Add(pagamentoPayPal);

                        break;
                    case 0:
                        selezione = false;
                        break;
                    default:
                        Console.WriteLine("Scelta non valida!");
                        break;
                }
            }

                while(true)
                {
                    Console.WriteLine("Seleziona metodo per effettuare il pagamento (0 per terminare): ");
                    if (!int.TryParse(Console.ReadLine(), out int metodoScelto) || metodoScelto < 0 || metodoScelto > metodiPagamento.Count)
                    {
                        Console.WriteLine("Input non valido!");
                        continue;
                    }
                    if (metodoScelto == 0)
                    {
                        break;
                    }
                    Console.Write("Inserisci l'importo da pagare: ");
                    if (!decimal.TryParse(Console.ReadLine(), out decimal importo) || importo <= 0)
                    {
                        Console.WriteLine("Importo non valido!");
                        continue;
                    }
                    PagamentoPolimorfico(metodiPagamento[metodoScelto - 1], importo);

                }                

            }  

            static void PagamentoPolimorfico(IPagamento metodo, decimal importo)
            {
                metodo.MostroMetodo();
                metodo.EseguiPagamento(importo);
            }
        }
}
