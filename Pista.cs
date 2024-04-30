using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProyectoFinal_ivinader
{
    [XmlInclude(typeof(Sospechoso))]
    [XmlInclude(typeof(Arma))]
    [XmlInclude(typeof(Habitacion))]
    [Serializable]
    public class Pista
    {
        protected string nombre;
        public Pista() { }
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
