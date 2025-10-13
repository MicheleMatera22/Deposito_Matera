namespace Logger;

    public sealed class Misuratore
    {
        private static Misuratore? istanza;
        private List<double> valori = new List<double>();
        private Misuratore() { }
        public static Misuratore GetIstanza()
        {
            if (istanza == null)
                istanza = new Misuratore();
            return istanza;
        }

        public void Aggiungi(double numero)
        {
            valori.Add(numero);
        }

        public void Stampa()
        {
            foreach (var val in valori)
                Console.WriteLine(val);
        }
    }
