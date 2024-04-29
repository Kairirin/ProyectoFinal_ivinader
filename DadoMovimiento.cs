using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class DadoMovimiento: Dado
    {
        public DadoMovimiento() { }
        public override short Lanzar()
        {
            return (short)r.Next(1, 6);
        }
    }
}
