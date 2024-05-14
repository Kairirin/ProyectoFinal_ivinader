using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProyectoFinal_ivinader
{
    public class CargaFichero
    {
        public const int ANCHO = 100;
        public const int ALTO = 28;

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
        public static List<Objeto> DeserializarXML(string ruta)
        {
            List<Objeto> lista = new List<Objeto>();

            XmlSerializer formatter = new XmlSerializer(lista.GetType());
            FileStream stream = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.Read);
            lista = (List<Objeto>)formatter.Deserialize(stream);

            stream.Close();

            return lista;

        }
    }
}
