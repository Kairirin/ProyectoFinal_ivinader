namespace ProyectoFinal_ivinader
{
    public class Posicion 
    {
        private int x;
        private int y;
        public Posicion(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public static Posicion operator +( Posicion a, Posicion b )
        {
            return new Posicion(a.X + b.X, a.Y + b.Y);
        }
        public static Posicion operator -(Posicion a, Posicion b)
        {
            return new Posicion(a.X - b.X, a.Y - b.Y);
        }
        public static bool operator ==(Posicion a, Posicion b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Posicion a, Posicion b)
        {
            return !(a == b);
        }
    }
}
