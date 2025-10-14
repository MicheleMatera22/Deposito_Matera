using System;
using System.Collections.Generic;

namespace Singleton
{
    public class ConfigurazioneSistema
    {
        private static ConfigurazioneSistema instance = null;
        private static Dictionary<string, string> info = new Dictionary<string, string>();

        private ConfigurazioneSistema() { }

        public static ConfigurazioneSistema GetInstance()
        {
            if (instance == null)
            {
                instance = new ConfigurazioneSistema();
            }
            return instance;
        }

        public void Imposta(string chiave, string valore)
        {
            info[chiave] = valore;
        }

        public string Leggi(string chiave)
        {
            return info.ContainsKey(chiave) ? info[chiave] : null;
        }

        public void StampaTutte()
        {
            foreach (var kvp in info)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
        }
    }
}
