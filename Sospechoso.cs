namespace ProyectoFinal_ivinader
{
    [Serializable]
    public class Sospechoso: Pista
    {
        public Sospechoso() { }
        public Sospechoso(string nombre, string letra) : base(nombre, letra) { }

        public override bool Equals(object? obj)
        {
            return obj is Sospechoso sospechoso &&
                   base.Equals(obj) &&
                   nombre == sospechoso.nombre;
        }
        public override string ToString()
        {
            return "Sospechoso: " + base.ToString();
        }
    }
}
