using System;

namespace Veicoli
{
    public class Auto : IVeicolo
    {
        public void Avvia() => Console.WriteLine("L'auto è stata avviata.");
        public void MostraTipo() => Console.WriteLine("Questo è un veicolo di tipo: Auto.");
    }
}
