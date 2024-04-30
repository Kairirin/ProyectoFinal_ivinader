using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    [Serializable]
    public class Objeto: IEsEvento
    {
        private string nombre;
        private string descripcion;
        public Objeto() { }
        public Objeto(string nombre, string descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public override bool Equals(object? obj)
        {
            return obj is Objeto objeto &&
                   nombre == objeto.nombre;
        }
        public override string ToString()
        {
            return $"{nombre}\n {descripcion}";
        }
    }
}
