using System;
using System.Collections.Generic;

namespace Veicoli
{
    public sealed class RegistroVeicoli
    {
        private static RegistroVeicoli _instance;
        private List<IVeicolo> _veicoli;

        private RegistroVeicoli()
        {
            _veicoli = new List<IVeicolo>();
        }

        public static RegistroVeicoli GetInstance()
        {
            if (_instance == null)
                _instance = new RegistroVeicoli();

            return _instance;
        }

        public void Registra(IVeicolo veicolo)
        {
            _veicoli.Add(veicolo);
        }

        public void Stampa()
        {
            foreach (var veicolo in _veicoli)
            {
                veicolo.MostraTipo();
            }
        }
    }
}
