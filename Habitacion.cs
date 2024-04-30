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
        private char letra;
        private List<Puerta> puertas;
        public Habitacion() { }
        public Habitacion(string nombre, char letra): base(nombre)
        {
            this.letra = letra;
            puertas = new List<Puerta>();
            puertas.Add(new Puerta(this, false));
            puertas.Add(new Puerta(this, true));
        }
        public List<Puerta> Puertas { get => puertas; set => puertas = value; }
        public char Letra { get => letra; set => letra = value; }
        public override string ToString()
        {
            return "Habitación: " + base.ToString();
        }
    }
}
