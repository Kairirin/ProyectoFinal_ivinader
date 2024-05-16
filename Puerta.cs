namespace ProyectoFinal_ivinader
{
    [Serializable]
    public class Puerta
    {
        private Habitacion estancia;
        private bool bloqueada;
        public Puerta() { }
        public Puerta(Habitacion estancia, bool bloqueada)
        {
            this.estancia = estancia;
            this.bloqueada = bloqueada;
        }
        public bool Bloqueada { get => bloqueada; set => bloqueada = value; }
        public Habitacion Estancia { get => estancia; set => estancia = value; }
        public void BloquearDesbloquear(string letra)
        {
            if(bloqueada)
            {
                bloqueada = false;
            }
            else
            {
                bloqueada = true;
            }
        }
    }
}
