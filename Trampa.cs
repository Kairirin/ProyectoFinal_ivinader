using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    [Serializable]
    public class Trampa: IEsEvento
    {
        private string nombre;
        private string descripcion;
        private Habitacion estancia;
        private bool activa;
        public Trampa() { }
        public Trampa(string nombre, string descripcion, Habitacion estancia, bool activa)
        {
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.estancia = estancia;
            this.activa = activa;
        }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public Habitacion Estancia { get => estancia; set => estancia = value; }
        public bool Activa { get => activa; set => activa = value; }
        public void ActivarDesactivar()
        {
            if(activa)
            {
                activa = false;
            }
            else
            {
                activa = true;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Trampa trampa &&
                   nombre == trampa.nombre &&
                   EqualityComparer<Habitacion>.Default.Equals(estancia, trampa.estancia);
        }
        public override string ToString()
        {
            return $"{nombre}\n {descripcion} - {estancia}";
        }
    }
}
