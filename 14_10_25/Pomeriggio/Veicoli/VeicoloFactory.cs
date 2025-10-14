namespace Veicoli
{
    public static class VeicoloFactory
    {
                
        public static IVeicolo creaVeicolo(string tipo)
        {
            switch (tipo.ToLower())
            {
                case "auto":
                    return new Auto();
                case "moto":
                    return new Moto();
                case "camion":
                    return new Camion();
                default:
                    throw new ArgumentException("Tipo di veicolo non riconosciuto.");
            }
        }
    }

}
