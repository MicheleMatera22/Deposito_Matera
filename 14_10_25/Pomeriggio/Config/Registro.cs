namespace Config
{
    public sealed class Registro
    {
        private static Registro instance = null;
        private List<IDispositivo> log = new List<IDispositivo>();

        private Registro()
        {
        }

        public static Registro GetInstance()
        {
            if (instance == null)
            {
                instance = new Registro();
            }
            return instance;
        }

        public void Aggiungi(IDispositivo dispositivo)
        {
            log.Add(dispositivo);
        }

        public void Stampa()
        {
            foreach (var dispositivo in log)
            {
                dispositivo.MostraTipo();
            }
        }

    }
}