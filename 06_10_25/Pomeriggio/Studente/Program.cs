namespace Studente
{
    class Studente
    {
        public string Nome;
        public int Matricola;
        public double MediaVoti;

        public Studente(string Nome, int Matricola, double MediaVoti)
        {
            this.Nome = Nome;
            this.Matricola = Matricola;
            this.MediaVoti = MediaVoti;
        }

        public void stampaStudente()
        {
            Console.WriteLine($"Nome: {Nome}, Matricola:{Matricola}, Media voti: {MediaVoti}");
        }

    }
}