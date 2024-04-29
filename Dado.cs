using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal abstract class Dado
    {
        protected Random r;
        public Dado()
        {
            r= new Random();
        }
        public abstract short Lanzar();
    }
}
