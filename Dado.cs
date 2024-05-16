namespace ProyectoFinal_ivinader
{
    internal abstract class Dado
    {
        protected Random r;
        public Dado()
        {
            r = new Random();
        }
        public abstract short Lanzar();
    }
}
