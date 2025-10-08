namespace Officina
{
    class Camion : Veicolo
    {
        public override void Ripara()
        {
            Console.WriteLine($"Controllo sospensioni, freni rinforzati e carico del camion.");
        }
    }
}