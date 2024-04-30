using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProyectoFinal_ivinader
{
    internal class Juego
    {
        private List<Jugador> jugadores;
        private Tablero tablero;
        private Dado[] dados;
        private Solucion solucionCaso;
        private List<Pista> pistas;
        private List<IEsEvento> eventos;
        private Random r;

        public Juego(List<Jugador> jugadores)
        {
            this.jugadores = jugadores;
            tablero = new Tablero();
            r = new Random();
            pistas = CargarPistas();
            eventos = CargarEventos();
            solucionCaso = CargarSolucion();

            if (jugadores.Count > 1)
            {
                dados = new Dado[2];
                dados[0] = new DadoMovimiento();
                dados[1] = new DadoEvento();
            }
            else
            {
                dados = new Dado[1];
                dados[0] = new DadoEvento();
            }
        }
        private List<Pista> CargarPistas()
        {
            List<Pista> pistas = new List<Pista>();
            XmlSerializer formatter = new XmlSerializer(pistas.GetType());
            FileStream stream = new FileStream("pistas.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
            pistas = (List<Pista>)formatter.Deserialize(stream);

            stream.Close();

            return pistas;
        }
        private List<IEsEvento> CargarEventos()
        {
            eventos = new List<IEsEvento>();

            string fichero = "habitaciones.json";
            string jsonString = File.ReadAllText(fichero);
            List<Habitacion> habs = JsonSerializer.Deserialize<List<Habitacion>>(jsonString);

            eventos.Add(new Objeto("Aspirina", "Evita perder un turno"));
            eventos.Add(new Objeto("Ganzúa", "Abre una puerta cerrada"));
            eventos.Add(new Objeto("Trofeo", "Obtienes una pista extra al llegar a una ubicación"));
            eventos.AddRange(habs);
            pistas.AddRange(habs);

            return eventos;
        }

        private Solucion CargarSolucion()
        {
            List<Pista> listaSospechososAuxiliar = pistas.FindAll(s => s is Sospechoso);
            List<Pista> listaArmasAuxiliar = pistas.FindAll(s => s is Arma);
            List<IEsEvento> listaHabsAuxiliar = eventos.FindAll(s => s is Habitacion);

            Sospechoso culpable = (Sospechoso)listaSospechososAuxiliar[r.Next(0, listaSospechososAuxiliar.Count() - 1)];
            Arma arma = (Arma)listaArmasAuxiliar[r.Next(0, listaArmasAuxiliar.Count() - 1)];
            Habitacion escenaCrimen = (Habitacion)listaHabsAuxiliar[r.Next(0, listaHabsAuxiliar.Count() - 1)];

            pistas.Remove(culpable);
            pistas.Remove(arma);
            pistas.Remove(escenaCrimen);

            return new Solucion(culpable, arma, escenaCrimen);
        }
        public List<Jugador> GetJugadores()
        {
            return jugadores;
        }
        public Solucion GetSolucion()
        {
            return solucionCaso;
        }
        public List<Pista> GetPistas()
        {
            return pistas;
        }
        public List<IEsEvento> GetEventos()
        {
            return eventos;
        }
        public void JugarTurno(Jugador j)
        {
            //Implementar juego por turnos
            //Lanzar dados

            while(true)
            {
                Console.Clear();
                tablero.MostrarTablero();
                j.Icono.Dibujar();
                j.Icono.Mover(tablero);


            }
        }
        public void DarObjeto(Jugador j)
        {
            List<IEsEvento> objetos = eventos.FindAll(o => o is Objeto);

            j.ListaObjetos.Add((Objeto)objetos[r.Next(1, objetos.Count)]);
        }
        public void DarPista(Jugador j)
        {
            bool salir = false;

            while (!salir)
            {
                Pista pistaProvisional = pistas[r.Next(1, pistas.Count)];

                if (!j.ListaPistas.Contains(pistaProvisional))
                {
                    j.ListaPistas.Add(pistaProvisional);
                    salir = true;
                }
            }
        }
        //Método PonerTrampa por implementar
        public void BloquearPuertas(Habitacion hab)
        {
            hab.Puertas.ForEach(p => p.BloquearDesbloquear());
        }
        public void Resolver(Solucion sol)
        {
            //Implementar
        }
        private void CargarHabitacionesEnFichero()
        {
            //Método empleado para crear el .json que cargará las habitaciones. No ha vuelto a ser utilizado
            List<Habitacion> habs = new List<Habitacion>();
            habs.Add(new Habitacion("Cocina", 'K'));
            habs.Add(new Habitacion("Comedor", 'C'));
            habs.Add(new Habitacion("Sala de estar", 'S'));
            habs.Add(new Habitacion("Habitación de invitados", 'I'));
            habs.Add(new Habitacion("Habitación principal", 'H'));
            habs.Add(new Habitacion("Baño", 'B'));
            habs.Add(new Habitacion("Estudio", 'E'));
            habs.Add(new Habitacion("Puesto de policía", 'R'));

            string fichero = "habitaciones.json";

            JsonSerializerOptions opciones = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve, WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(habs, opciones);
            File.WriteAllText(fichero, jsonString);
        }
    }
}
