using System.Xml.Serialization;

namespace ProyectoFinal_ivinader
{
    public class CargaFichero
    {
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
        public static List<Objeto> CargarObjetos(string ruta)
        {
            List<Objeto> lista = new List<Objeto>();

            XmlSerializer formatter = new XmlSerializer(lista.GetType());
            FileStream stream = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.Read);
            lista = (List<Objeto>)formatter.Deserialize(stream);

            stream.Close();

            return lista;
        }
        public static List<Trampa> CargarTrampas(string ruta)
        {
            List<Trampa> lista = new List<Trampa>();

            XmlSerializer formatter = new XmlSerializer(lista.GetType());
            FileStream stream = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.Read);
            lista = (List<Trampa>)formatter.Deserialize(stream);

            stream.Close();

            return lista;
        }
        public static List<Pista> CargarPistas(string ruta)
        {
            List<Pista> pistas = new List<Pista>();
            XmlSerializer formatter = new XmlSerializer(pistas.GetType());
            FileStream stream = new FileStream(ruta, FileMode.Open, FileAccess.Read, FileShare.Read);
            pistas = (List<Pista>)formatter.Deserialize(stream);

            stream.Close();

            return pistas;
        }
        public static List<Habitacion> CargarHabitaciones()
        {
            List<Habitacion> habs = new List<Habitacion>(); //Anotar en informe que no funciona la deserialización json
            habs.Add(new Habitacion("Cocina", "K"));
            habs.Add(new Habitacion("Comedor", "C"));
            habs.Add(new Habitacion("Sala de estar", "S"));
            habs.Add(new Habitacion("Habitación de invitados", "I"));
            habs.Add(new Habitacion("Habitación principal", "H"));
            habs.Add(new Habitacion("Baño", "B"));
            habs.Add(new Habitacion("Estudio", "E"));

            return habs;
        }
    }
}
