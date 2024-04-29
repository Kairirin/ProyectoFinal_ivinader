using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Pista
    {
        protected string nombre;
        public Pista(string nombre)
        {
            this.nombre = nombre;
        }
        public string Nombre { get => nombre; set => nombre = value; }

        public override bool Equals(object? obj)
        {
            return obj is Pista pista &&
                   nombre == pista.nombre;
        }
        public override string ToString()
        {
            return nombre;
        }
    }
}
