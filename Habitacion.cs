using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    [Serializable]
    public class Habitacion: Pista, IEsEvento
    {
        private List<Puerta> puertas;
        public Habitacion() { }
        public Habitacion(string nombre) : base(nombre) { }
        public Habitacion(string nombre, string letra): base(nombre, letra)
        {
            puertas = new List<Puerta>();
            puertas.Add(new Puerta(this, false));
            puertas.Add(new Puerta(this, true));
        }
        public List<Puerta> Puertas { get => puertas; set => puertas = value; }
        public override string ToString()
        {
            return "Habitación: " + base.ToString();
        }
    }
}
