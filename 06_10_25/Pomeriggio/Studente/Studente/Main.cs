namespace Studente
{
    class Program {
        static void Main(string[] args)
        {
            Persona p1 = new Persona("Matera", "Michele", 2002);
            Persona p2 = new Persona("Rossi", "Mario", 1978);
            p1.stampaPersona();
            p2.stampaPersona();

            Studente s1 = new Studente("Giorgio", 1, 8.5);
            Studente s2 = new Studente("Samuele", 2, 6.4);
            s1.stampaStudente();
            s2.stampaStudente();

            List<Studente> storico = new List<Studente>();
            storico.Add(s1);
            storico.Add(s2);

            Console.WriteLine("Di quale studente vuoi informazioni?");
            string? scelta = Console.ReadLine();
            int s = Convert.ToInt32(scelta);
            storico[s-1].stampaStudente();
        }
    }
}