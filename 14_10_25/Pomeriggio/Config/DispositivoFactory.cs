namespace Config
{
    public class DispositivoFactory
    {
        public static IDispositivo CreaDispositivo(string tipo)
        {
            return tipo.ToLower() switch
            {
                "computer" => new Computer(),
                "stampante" => new Stampante(),
                _ => throw new ArgumentException("Tipo di dispositivo non riconosciuto.")
            };
        }
    }
}