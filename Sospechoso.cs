using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    [Serializable]
    public class Sospechoso: Pista
    {
        public Sospechoso() { }
        public Sospechoso(string nombre) : base(nombre) { }
        public override string ToString()
        {
            return "Sospechoso: " + base.ToString();
        }
    }
}
