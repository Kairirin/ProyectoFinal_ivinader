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
        public Sospechoso Culpable { get => culpable; set => culpable = value; }
        public Arma Arma { get => arma; set => arma = value; }
        public Habitacion Habitacion { get => habitacion; set => habitacion = value; }

        public override bool Equals(object? obj)
        {
            return obj is Solucion solucion &&
                   EqualityComparer<Sospechoso>.Default.Equals(culpable, solucion.culpable) &&
                   EqualityComparer<Arma>.Default.Equals(arma, solucion.arma) &&
                   EqualityComparer<Habitacion>.Default.Equals(habitacion, solucion.habitacion);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(culpable, arma, habitacion);
        }

        public override string ToString()
        {
            return $"{culpable} - {arma} - {habitacion}";
        }

    }
}
