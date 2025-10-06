namespace Studente
{
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

        
    }
}