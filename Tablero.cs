using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    public class Tablero
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

        private void CargarTablero()
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
        public void GestionPuertas(string letra)
        {
            string aux1, aux2, resultado;

            for (int i = 0; i < tablero.Length; i++)
            {
                for(int j = 0; j < tablero[i].Length; j++)
                {
                    if (tablero[i][j] == 'P' && (tablero[i - 1][j] == Convert.ToChar(letra) || tablero[i + 1][j] == Convert.ToChar(letra) || tablero[i][j+1] == Convert.ToChar(letra) || tablero[i][j-1] == Convert.ToChar(letra)))
                    {
                        aux1 = tablero[i].Substring(0, j);
                        aux2 = tablero[i].Substring(j + 1);
                        resultado = aux1 + 'p' + aux2;
                        tablero[i] = resultado;
                    }
                    else
                    {
                        if (tablero[i][j] == 'p' && (tablero[i - 1][j] == Convert.ToChar(letra) || tablero[i + 1][j] == Convert.ToChar(letra) || tablero[i][j + 1] == Convert.ToChar(letra) || tablero[i][j - 1] == Convert.ToChar(letra)))
                        {
                            aux1 = tablero[i].Substring(0, j);
                            aux2 = tablero[i].Substring(j + 1);
                            resultado = aux1 + 'P' + aux2;
                            tablero[i] = resultado;
                        }
                    }
                }
            }
        }
        public void GestionPuertas(int x, int y)
        {
            string aux1, aux2, resultado;

            aux1 = tablero[y].Substring(0, x);
            aux2 = tablero[y].Substring(x + 1);
            resultado = aux1 + 'p' + aux2;
            tablero[y] = resultado;
        }
        public int Ancho { get => ancho; set => ancho = value; }
        public int Alto { get => alto; set => alto = value; }
        public string ComprobarEspacio(int x, int y)
        {
            return tablero[y][x].ToString();
        }
    }
}
