using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class DadoEvento: Dado
    {
        private List<IEsEvento> eventos;
        public DadoEvento()
        { 
            //eventos = new List<IEsEvento>();
            eventos = CargarEventos();
        }
        public DadoEvento(List<IEsEvento> eventos)
        {
        this.eventos = eventos;
        }
        public override short Lanzar()
        {
            return (short)r.Next(1, eventos.Count);
        }
        public void ObtenerEvento(Jugador j, Tablero tablero)
        {
            IEsEvento eventoElegido = eventos[Lanzar()];

            switch (eventoElegido.GetType().Name)
            {
                case "Objeto":
                    int probabilidad = r.Next(1, 50);
                    if (probabilidad % 2 == 0)
                        DarObjeto(j);
                    break;
                case "Habitacion":
                    string letra = ((Habitacion)eventoElegido).LetraAsociada;
                    BloquearPuertas(letra, tablero);
                    break;
            }

            //return eventos[Lanzar()];
        }
        public void DarObjeto(Jugador j)
        {
            List<IEsEvento> objetos = eventos.FindAll(o => o is Objeto);

            j.ListaObjetos.Add((Objeto)objetos[r.Next(1, objetos.Count)]);
        }
        public void BloquearPuertas(string letra, Tablero tablero)
        {
            eventos.ForEach(h =>
            {
                if (h is Habitacion && ((Habitacion)h).LetraAsociada == letra)
                {
                    ((Habitacion)h).Puertas.ForEach(p => p.BloquearDesbloquear(letra));
                    tablero.GestionPuertas(letra);
                }
            });
        }
        private List<IEsEvento> CargarEventos()
        {
            List<Objeto> objetos = CargaFichero.DeserializarXML("eventos.xml");
            eventos = new List<IEsEvento>();
            eventos.AddRange(objetos);

            /*string fichero = "habitaciones.json";
            string jsonString = File.ReadAllText(fichero);
            List<Habitacion> habs = JsonSerializer.Deserialize<List<Habitacion>>(jsonString);*/

            List<Habitacion> habs = new List<Habitacion>(); //Anotar en informe que no funciona la deserialización json
            habs.Add(new Habitacion("Cocina", "K"));
            habs.Add(new Habitacion("Comedor", "C"));
            habs.Add(new Habitacion("Sala de estar", "S"));
            habs.Add(new Habitacion("Habitación de invitados", "I"));
            habs.Add(new Habitacion("Habitación principal", "H"));
            habs.Add(new Habitacion("Baño", "B"));
            habs.Add(new Habitacion("Estudio", "E"));

            eventos.AddRange(habs);
            //pistas.AddRange(habs);

            return eventos;
        }
    }
}
