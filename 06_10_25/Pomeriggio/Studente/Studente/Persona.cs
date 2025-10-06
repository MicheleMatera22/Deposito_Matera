class Persona
{
    public string Nome;
    public string Cognome;
    public int AnnoNascita;

    public Persona(string nome, string cognome, int anno)
    {
        this.Nome = nome;
        this.Cognome = cognome;
        this.AnnoNascita = anno;
    }

    public void stampaPersona()
    {
        Console.WriteLine($"{Nome} {Cognome} è nato nel {AnnoNascita}");
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Persona p1 = new Persona("Matera", "Michele", 2002);
            Persona p2 = new Persona("Rossi", "Mario", 1978);
            p1.stampaPersona();
            p2.stampaPersona();
        }
    }
}