using System;

namespace Veicoli
{
    public class Camion : IVeicolo
    {
        public void Avvia() => Console.WriteLine("Il camion è stato avviato.");
        public void MostraTipo() => Console.WriteLine("Questo è un veicolo di tipo: Camion.");
    }
}
