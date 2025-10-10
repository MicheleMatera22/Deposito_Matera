namespace Astrazione
{
    class Computer : DispositivoElettronico
    {
        public override void Accendi()
        {
            Console.WriteLine("Il computer si accende...");
        }

        public override void Spegni()
        {
            Console.WriteLine("Il computer si spegne...");
        }

        public override void MostraInfo()
        {
            base.MostraInfo();
            Console.WriteLine("Tipo: Computer");
        }

    }
}