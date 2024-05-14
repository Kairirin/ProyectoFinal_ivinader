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
        private const int X1_TABLERO = 11;
        private const int XN_TABLERO = 39;
        private const int Y1_TABLERO = 1;
        private const int YN_TABLERO = 14;

        private string[] tablero;

        public static int X1_TABLERO1 => X1_TABLERO;

        public static int XN_TABLERO1 => XN_TABLERO;

        public static int Y1_TABLERO1 => Y1_TABLERO;

        public static int YN_TABLERO1 => YN_TABLERO;

        public Tablero()
        {
            tablero = CargaFichero.Cargar("tableroJuego.txt");
        }
        public void MostrarTablero()
        {
            Console.ResetColor();

            for (int i = 0; i < tablero.Length; i++)
            {
                Console.WriteLine(tablero[i]);
            }
        }
        public void GestionPuertas(string letra) //Volver con todo el tema de la posiciones
        {
            string aux1, aux2, resultado;

            for (int i = Y1_TABLERO; i < YN_TABLERO; i++)
            {
                for(int j = X1_TABLERO; j < XN_TABLERO; j++)
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
        public string ComprobarEspacio(int x, int y)
        {
            return tablero[y][x].ToString();
        }
    }
}
