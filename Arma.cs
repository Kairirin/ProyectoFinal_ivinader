using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Arma : Pista
    {
        public Arma(string nombre) : base(nombre) { }
        public override string ToString()
        {
            return "Arma: " + base.ToString();
        }
    }
}
