namespace ProyectoFinal_ivinader
{
    [Serializable]
    public class Arma : Pista
    {
        public Arma() { }
        public Arma(string nombre, string letra) : base(nombre, letra) { }

        public override bool Equals(object? obj)
        {
            return obj is Arma arma &&
                   base.Equals(obj) &&
                   nombre == arma.nombre;
        }

        public override string ToString()
        {
            return "Arma: " + base.ToString();
        }
    }
}
