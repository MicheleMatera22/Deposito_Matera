namespace ThreeRules
{
    class Rettangolo : Figura
    {
        public double Base { get; set; }
        public double Altezza { get; set; }

        public override double CalcolaArea()
        {
            return Base * Altezza;
        }

        public override void Disegna(Figura f)
        {
            Console.WriteLine($"Disegno un rettangolo di colore {Colore} con area {CalcolaArea()}");
        }
    }
}