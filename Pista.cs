using System.Xml.Serialization;

namespace ProyectoFinal_ivinader
{
    [XmlInclude(typeof(Sospechoso))]
    [XmlInclude(typeof(Arma))]
    [XmlInclude(typeof(Habitacion))]
    [Serializable]
    public class Pista
    {
        protected string nombre;
        protected string letraAsociada;
        public Pista() { }
        public Pista(string nombre)
        {
            this.nombre = nombre;
        }
        public Pista(string nombre, string letraAsociada)
        {
            this.nombre = nombre;
            this.letraAsociada = letraAsociada;
        }
        public string Nombre { get => nombre; set => nombre = value; }
        public string LetraAsociada { get => letraAsociada; set => letraAsociada = value; }

        public override bool Equals(object? obj)
        {
            return obj is Pista pista &&
                   nombre == pista.nombre;
        }
        public override string ToString()
        {
            return $"{letraAsociada} - {nombre}";
        }
    }
}
