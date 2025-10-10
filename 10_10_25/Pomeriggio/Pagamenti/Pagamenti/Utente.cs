namespace Pagamenti
{
    class Utente
    {
        private List<IPagamento> metodiPagamento = new List<IPagamento>();
        private string? _nome;
        private int _pin;
        private decimal _saldo; // saldo privato

        public string? Nome
        {
            get => _nome;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Nome non valido.");
                }
                else
                {
                    _nome = value;
                }
            }
        }

        public int Pin
        {
            get => _pin;
            set
            {
                if (value < 1000 || value > 9999)
                {
                    Console.WriteLine("PIN deve essere un numero di 4 cifre.");
                }
                else
                {
                    _pin = value;
                }
            }
        }

        public decimal Saldo
        {
            get => _saldo;
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Il saldo non può essere negativo.");
                }
                else
                {
                    _saldo = value;
                }
            }
        }

        public void AggiungiMetodoPagamento(IPagamento metodo)
        {
            metodiPagamento.Add(metodo);
        }

        public void MostraMetodiPagamento()
        {
            Console.WriteLine($"\nMetodi di pagamento per {Nome}:");
            for (int i = 0; i < metodiPagamento.Count; i++)
            {
                Console.Write($"{i}. ");
                metodiPagamento[i].MostroMetodo();
            }
        }

        public void EseguiPagamento(int indiceMetodo, decimal importo)
        {
            if (indiceMetodo < 0 || indiceMetodo >= metodiPagamento.Count)
            {
                Console.WriteLine("Metodo di pagamento non valido.");
                return;
            }

            if (importo > Saldo)
            {
                Console.WriteLine("Saldo insufficiente per effettuare il pagamento.");
                return;
            }

            metodiPagamento[indiceMetodo].EseguiPagamento(importo);
            Saldo -= importo;
            Console.WriteLine($"Saldo rimanente: {Saldo}€");
        }

        public void RimuoviMetodoPagamento(int indiceMetodo)
        {
            if (indiceMetodo < 0 || indiceMetodo >= metodiPagamento.Count)
            {
                Console.WriteLine("Metodo di pagamento non valido.");
                return;
            }

            metodiPagamento.RemoveAt(indiceMetodo);
            Console.WriteLine("Metodo di pagamento rimosso.");
        }
    }
}
