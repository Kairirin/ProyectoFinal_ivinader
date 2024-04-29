using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Habitacion: Pista, IEsEvento
    {
        private int x;
        private int y;
        private List<Puerta> puertas;
        public Habitacion(string nombre, int x, int y): base(nombre)
        {
            this.x = x;
            this.y = y;
            puertas = new List<Puerta>();
            puertas.Add(new Puerta(this, false));
            puertas.Add(new Puerta(this, true));
        }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public List<Puerta> Puertas { get => puertas; set => puertas = value; }
        public override string ToString()
        {
            return "Habitación: " + base.ToString();
        }
    }
}
