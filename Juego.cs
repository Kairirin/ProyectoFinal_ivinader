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
            //CargarHabitacionesEnFichero();
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
                OcurreEvento(j); //MOdificar luego todo esto para que no sea una locura
                j.Icono.Dibujar();
                j.Icono.Mover(tablero);
                
                if(tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "1" && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "P" && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "p" && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != " " && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "R") //¿Cambiar por expresión regular?
                {
                    DarPista(j, tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y));

                    Console.SetCursorPosition(0, tablero.Alto + 1);//Fragmento de código provisional para comprobar si se dan las pistas. Funciona!!!!!!
                    Console.ResetColor();
                    Console.WriteLine("Lista de pistas del jugador: ");

                    if (j.ListaPistas.Count > 0)
                    {
                        j.ListaPistas.ForEach(p => Console.WriteLine(p));
                    }
                    else
                    {
                        Console.WriteLine("No hay pistas");
                    }
                    Console.WriteLine("Lista de objetos del jugador: ");

                    if (j.ListaObjetos.Count > 0)
                    {
                        j.ListaObjetos.ForEach(p => Console.WriteLine(p));
                    }
                    else
                    {
                        Console.WriteLine("No hay objetos");
                    }
                    Console.WriteLine("Enter para continuar: ");
                    Console.ReadLine(); //Fin del fragmento de código de prueba
                }

            }
        }
        public void OcurreEvento(Jugador j)
        {
            IEsEvento eventoElegido = eventos[r.Next(0, eventos.Count - 1)];// eventos[eventos.Count - 2];Comprobación de puertas

            switch (eventoElegido.GetType().Name)
            {
                case "Objeto":
                    DarObjeto(j);
                    break;
                case "Habitacion":
                    string letra = ((Habitacion)eventoElegido).LetraAsociada;
                    BloquearPuertas(letra);
                    break;
            }
        }
        public void DarObjeto(Jugador j)
        {
            List<IEsEvento> objetos = eventos.FindAll(o => o is Objeto);

            j.ListaObjetos.Add((Objeto)objetos[r.Next(1, objetos.Count)]);
        }
        public void DarPista(Jugador j, string hab)
        {
            bool salir = false;
            int contador = 0;

            List<Pista> pistaProvisional = pistas.FindAll(p => p.LetraAsociada == hab); //pistas[r.Next(1, pistas.Count)]; En un principio existía la posibilidad de dar las pistas aleatoriamente, pero me ha parecido interesante dar las que estén asociadas a la habitación donde está el jugador, de esta manera tiene que recorrerer todo el mapa.

            while (!salir)
            {
                if (pistaProvisional.Count == 0)
                {
                    salir = true;
                }
                else if (j.ListaPistas.Count == 0 || !j.ListaPistas.Contains(pistaProvisional[contador]))
                {
                    j.ListaPistas.Add(pistaProvisional[contador]);
                    salir = true;
                }
                else
                {
                    if(contador != pistaProvisional.Count -1)
                    {
                        contador++;
                    }
                    else
                    {
                        salir = true;
                    }
                }
            }
        }
        //Método PonerTrampa por implementar
        public void BloquearPuertas(string letra)
        {
            eventos.ForEach(h =>
            {
                if(h is Habitacion && ((Habitacion)h).LetraAsociada == letra)
                {
                    ((Habitacion)h).Puertas.ForEach(p => p.BloquearDesbloquear(letra));
                    tablero.GestionPuertas(letra);
                }
            });
        }
        public void Resolver(Solucion sol)
        {
            //Implementar
        }
        private void CargarHabitacionesEnFichero()
        {
            //Método empleado para crear el .json que cargará las habitaciones. No ha vuelto a ser utilizado
            List<Habitacion> habs = new List<Habitacion>();
            habs.Add(new Habitacion("Cocina", "K"));
            habs.Add(new Habitacion("Comedor", "C"));
            habs.Add(new Habitacion("Sala de estar", "S"));
            habs.Add(new Habitacion("Habitación de invitados", "I"));
            habs.Add(new Habitacion("Habitación principal", "H"));
            habs.Add(new Habitacion("Baño", "B"));
            habs.Add(new Habitacion("Estudio", "E"));
            habs.Add(new Habitacion("Puesto de policía", "R"));

            string fichero = "habitaciones.json";

            JsonSerializerOptions opciones = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve, WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(habs, opciones);
            File.WriteAllText(fichero, jsonString);
        }
    }
}
