namespace Officina
{
    class Moto : Veicolo
    {
        public override void Ripara()
        {
            Console.WriteLine($"Controllo catena, freni e gomme della moto.");
        }
    }
}