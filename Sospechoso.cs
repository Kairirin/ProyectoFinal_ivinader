using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Sospechoso: Pista
    {
        public Sospechoso(string nombre) : base(nombre) { }
        public override string ToString()
        {
            return "Sospechoso: " + base.ToString();
        }
    }
}
