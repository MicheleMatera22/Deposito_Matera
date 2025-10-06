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

    public class Program
    {
        public static void Main(string[] args)
        {
            Studente s1 = new Studente("Giorgio", 1, 8.5);
            Studente s2 = new Studente("Samuele", 2, 6.4);
            s1.stampaStudente();
            s2.stampaStudente();
        }
    }
}