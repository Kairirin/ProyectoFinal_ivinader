using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Solucion
    {
        private Sospechoso sospechoso;
        private Arma arma;
        private Habitacion habitacion;
        public Solucion(Sospechoso sospechoso, Arma arma, Habitacion habitacion)
        {
            this.sospechoso = sospechoso;
            this.arma = arma;
            this.habitacion = habitacion;
        }
        public Sospechoso GetSospechoso()
        {
            return sospechoso;
        }
        public Arma GetArma()
        {
            return arma;
        }
        public Habitacion GetEstancia()
        {
            return habitacion;
        }

        public override bool Equals(object? obj)
        {
            return obj is Solucion solucion &&
                   EqualityComparer<Sospechoso>.Default.Equals(sospechoso, solucion.sospechoso) &&
                   EqualityComparer<Arma>.Default.Equals(arma, solucion.arma) &&
                   EqualityComparer<Habitacion>.Default.Equals(habitacion, solucion.habitacion);
        }
    }
}
