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

        public Jugador(string nombre, string color, int turno)
        {
            this.nombre = nombre;
            icono = new Sprite(color);
            this.turno = turno;
            if (turno == 1)
                activo = true;
            else
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

        public void MostrarPistas(int x, int y)
        {
            Console.ResetColor();
            Console.SetCursorPosition(x, y);
            Console.WriteLine("Cuaderno de pistas: ");
            if (listaPistas.Count > 0)
            {
                for(int i = 0; i < listaPistas.Count; i++)
                {
                    Console.SetCursorPosition(x, y + (i + 1));
                    Console.WriteLine(listaPistas[i]);
                }
            }
            else
            {
                Console.SetCursorPosition(x, y + 1);
                Console.WriteLine("Sin pistas");
            }
        }
        public void MostrarObjetos(int x, int y)
        {
            Console.ResetColor();
            Console.SetCursorPosition(x, y);
            Console.WriteLine($"Maletín de {nombre}: ");
            if (listaObjetos.Count > 0)
            {
                listaObjetos.ForEach(p => Console.WriteLine(p));
            }
            else
            {
                Console.WriteLine("Sin objetos");
            }
        }

        public override string ToString()
        {
            return nombre; //Ampliar
        }
        public void MostrarDatos(int pasos)
        {
            Console.ResetColor();
            Console.SetCursorPosition(Console.WindowWidth / 2, 0);
            Console.WriteLine(this);
            Console.SetCursorPosition(Console.WindowWidth / 2, 1);
            Console.WriteLine($"Pasos de este turno: {pasos}");
        }
        public override bool Equals(object? obj)
        {
            return obj is Jugador jugador &&
                   nombre == jugador.nombre;
        }
    }
}
