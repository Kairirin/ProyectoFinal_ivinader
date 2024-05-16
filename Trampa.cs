namespace ProyectoFinal_ivinader
{
    [Serializable]
    public class Trampa: IEsEvento
    {
        private string nombre;
        private string descripcion;
        public Trampa() { }
        public Trampa(string nombre, string descripcion)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
        }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public override string ToString()
        {
            return $"{nombre}: {descripcion}";
        }
    }
}
