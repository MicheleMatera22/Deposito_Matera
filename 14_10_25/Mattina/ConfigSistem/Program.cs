using System;

namespace Singleton
{
    class Program
    {
        public static void Main(string[] args)
        {
            var moduloA = ConfigurazioneSistema.GetInstance();
            moduloA.Imposta("Database", "SQLServer");
            moduloA.Imposta("Porta", "1433");
            moduloA.Imposta("Timeout", "30");
            var moduloB = ConfigurazioneSistema.GetInstance();
            moduloB.Imposta("Timer", "60");
            moduloB.Imposta("MaxConnessioni", "100");
            moduloB.Imposta("Porta", "5432"); 

            Console.WriteLine("Configurazioni di sistema (da moduloA):");
            moduloA.StampaTutte();
            Console.WriteLine("\nConfigurazioni di sistema (da moduloB):");
            moduloB.StampaTutte();

            Console.WriteLine("\nLettura altre configurazioni:");
            Console.WriteLine($"Database: {moduloB.Leggi("Database")}");
            Console.WriteLine($"Timeout: {moduloB.Leggi("Timeout")}");
            Console.WriteLine($"MaxConnessioni: {moduloA.Leggi("MaxConnessioni")}");
            Console.WriteLine($"Timer: {moduloA.Leggi("Timer")}");
        }
    }
}
