namespace Studente
{
    class Operazioni
    {

        public int Somma(int a, int b)
        {
            return a + b;
        }

        public int Moltiplica(int a, int b)
        {
            return a * b;
        }

        public void StampaRisultato(string operazione, int risultato)
        {
            if (operazione == "somma")
            {
                Console.WriteLine($"Il risultato dell'operazione somma è {risultato}");
            }
            if (operazione == "moltiplicazione")
            {
                Console.WriteLine($"Il risultato dell'operazione moltiplicazione è {risultato}");
            }
        }
    }
}