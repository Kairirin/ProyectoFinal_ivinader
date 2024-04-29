using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Jugador
    {
        private string nombre;
        private Sprite icono;
        private int turno;
        private bool activo;
        private List<Pista> listaPistas;
        private List<Objeto> listaObjetos;

        public Jugador(string nombre, string color)
        {
            this.nombre = nombre;
            icono = new Sprite(color);
            turno = 1;
            activo = false;
            listaPistas = new List<Pista>();
            listaObjetos = new List<Objeto>();
        }
        public void AddPista(Pista p)
        {
            listaPistas.Add(p);
        }
        public void AddObjeto(Objeto obj)
        {
            listaObjetos.Add(obj);  
        }

        public string Nombre { get => nombre; set => nombre = value; }
        public int Turno { get => turno; set => turno = value; }
        public bool Activo { get => activo; set => activo = value; }
        public List<Pista> ListaPistas { get => listaPistas; set => listaPistas = value; }
        public List<Objeto> ListaObjetos { get => listaObjetos; set => listaObjetos = value; }
        public Sprite Icono { get => icono; set => icono = value; }

        public override string ToString()
        {
            return nombre; //Ampliar
        }
    }
}
