namespace Logger;

    public sealed class Contatore
    {
        private static Contatore? istanza;
        private List<int> valori = new List<int>();
        private Contatore() { }
        public static Contatore GetIstanza()
        {
            if (istanza == null)
                istanza = new Contatore();
            return istanza;
        }

        public void Aggiungi(int numero)
        {
            valori.Add(numero);
        }

        public void Stampa()
        {
            foreach (var val in valori)
                Console.WriteLine(val);
        }
    }
