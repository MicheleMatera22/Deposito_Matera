using System;

namespace Veicoli
{
    public class Moto : IVeicolo
    {
        public void Avvia() => Console.WriteLine("La moto è stata avviata.");
        public void MostraTipo() => Console.WriteLine("Questo è un veicolo di tipo: Moto.");
    }
}
