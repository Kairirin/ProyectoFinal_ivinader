using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Solucion
    {
        private Sospechoso culpable;
        private Arma arma;
        private Habitacion habitacion;
        public Solucion(Sospechoso culpable, Arma arma, Habitacion habitacion)
        {
            this.culpable = culpable;
            this.arma = arma;
            this.habitacion = habitacion;
        }
        public Sospechoso GetCulpable()
        {
            return culpable;
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
                   EqualityComparer<Sospechoso>.Default.Equals(culpable, solucion.culpable) &&
                   EqualityComparer<Arma>.Default.Equals(arma, solucion.arma) &&
                   EqualityComparer<Habitacion>.Default.Equals(habitacion, solucion.habitacion);
        }
        public override string ToString()
        {
            return $"{culpable} - {arma} - {habitacion}";
        }
    }
}
