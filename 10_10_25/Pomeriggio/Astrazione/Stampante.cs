namespace Astrazione
{
    class Stampante : DispositivoElettronico
    {
        public override void Accendi()
        {
            Console.WriteLine("La stampante si accende...");
        }

        public override void Spegni()
        {
            Console.WriteLine("La stampante si spegne...");
        }

        public override void MostraInfo()
        {
            base.MostraInfo();
            Console.WriteLine("Tipo: Stampante");
        }
    }
}