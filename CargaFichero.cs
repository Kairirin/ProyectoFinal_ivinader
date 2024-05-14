using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    public class CargaFichero
    {
        public const int ANCHO = 100;
        public const int ALTO = 27;

        public static string[] Cargar(string ruta)
        {
            string[] lineas = new string[ALTO];

            if (File.Exists(ruta))
            {
                lineas = File.ReadAllLines(ruta);
            }
            else
            {
                Console.WriteLine("- Fichero no encontrado -");
            }

            return lineas;
        }
    }
}
