using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class DadoEvento: Dado
    {
        private List<IEsEvento> eventos;
        public DadoEvento()
        { 
            eventos = new List<IEsEvento>();
        }
        public DadoEvento(List<IEsEvento> eventos)
        {
        this.eventos = eventos;
        }
        public override short Lanzar()
        {
            return (short)r.Next(1, eventos.Count);
        }
    }
}
