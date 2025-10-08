namespace Officina
{
    class Auto : Veicolo
    {
        public override void Ripara()
        {
            Console.WriteLine($"Controllo olio, freni e motore dell'auto");
        }
    }
}