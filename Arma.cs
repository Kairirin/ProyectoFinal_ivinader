using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    [Serializable]
    public class Arma : Pista
    {
        public Arma() { }
        public Arma(string nombre, string letra) : base(nombre, letra) { }
        public override string ToString()
        {
            return "Arma: " + base.ToString();
        }
    }
}
