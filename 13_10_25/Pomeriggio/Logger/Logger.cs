namespace Logger
{
    using System;

    public sealed class Logger
    {
        private static Logger? istanza;
        private List<string> logs = new List<string>();
        private Logger() { }
        public static Logger GetIstanza()
        {
            if (istanza == null)
            {
                istanza = new Logger();
            }
                
            
            return istanza;
        }

        public void Log(string messaggio)
        {
            string tempo = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            logs.Add($"[{tempo}] {messaggio}");
        }

        public void StampaLog()
        {
            foreach (var log in logs)
            {
                Console.WriteLine(log);
            }
        }
    }
}