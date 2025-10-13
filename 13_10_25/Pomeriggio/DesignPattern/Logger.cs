namespace DesignPattern
{
    using System;

    public sealed class Logger
    {
        private static Logger? istanza;
        private Logger() { }
        public static Logger GetIstanza()
        {
            if (istanza == null)
            {
                istanza = new Logger();
            }
                
            
            return istanza;
        }

        public void ScriviMessaggio(string messaggio)
        {
            Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {messaggio}");
        }
    }
}