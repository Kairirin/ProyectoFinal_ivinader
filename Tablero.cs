using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Tablero
    {
        private int ancho;
        private int alto;
        private string[] tablero;
        public Tablero()
        {
            ancho = 28;
            alto = 14;
            tablero = new string[alto];
            CargarTablero();
        }

        public void CargarTablero()
        {

            if (File.Exists("tableroInferior.txt"))
            {
  
                tablero = File.ReadAllLines("tableroInferior.txt");
            }
            else
            {
                Console.WriteLine("No se encuentra fichero de tablero");
            }
        }
        public void MostrarTablero()
        {
            Console.ResetColor();

            for (int i = 0; i < tablero.Length; i++)
            {
                Console.WriteLine(tablero[i]);
            }
        }
        public int Ancho { get => ancho; set => ancho = value; }
        public int Alto { get => alto; set => alto = value; }
        public string ComprobarEspacio(int x, int y)
        {
            return tablero[y][x].ToString();
        }
    }
}
