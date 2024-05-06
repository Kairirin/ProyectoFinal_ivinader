using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
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
        public void LanzarJuego(Jugador j)
        {
            //Implementar juego por turnos en multi
            //Lanzar dados en multijugador
            int contadorPasos = 0;
            bool resuelto = false;

            while(!resuelto)
            {
                Console.Clear();
                tablero.MostrarTablero();

                j.MostrarObjetos(0, tablero.Alto + 3);
                j.MostrarPistas(Console.WindowWidth/2, tablero.Alto + 3);

                if (contadorPasos % 8 == 0) 
                    OcurreEvento(j);

                j.Icono.Dibujar();
                j.Icono.Mover(tablero, j);
                contadorPasos++;
                
                if(tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "1" && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "P" && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "p" && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != " " && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "R") 
                {
                    DarPista(j, tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y));
                    if(j.ListaObjetos.Contains(new Objeto("Trofeo","")))
                    {
                        DarPista(j, tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y));
                        j.ListaObjetos.Remove(new Objeto("Trofeo", ""));
                    }
                }
                else
                {
                    if(tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) == "R")
                    {
                        Console.SetCursorPosition(0, ((tablero.Alto + 3) + (j.ListaObjetos.Count + 3)));
                        Console.WriteLine("¿Quieres resolver? (Si/No)");
                        string respuesta = Console.ReadLine();

                        if (respuesta.ToUpper() == "SI")
                        {
                            if (Resolver())
                            {
                                Console.Clear();
                                Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 2);
                                Console.WriteLine("Enhorabuena! Has resuelto el asesinato");
                                Console.ReadLine();
                                resuelto = true;
                            }
                            else
                            {
                                Console.WriteLine("Sigue intentándolo" +
                                        "\nPulsa cualquier tecla para continuar");
                                Console.ReadLine();
                            }  
                        }
                    }
                }

            }
        }
        public void OcurreEvento(Jugador j)
        {
            IEsEvento eventoElegido = eventos[r.Next(0, eventos.Count - 1)];

            switch (eventoElegido.GetType().Name)
            {
                case "Objeto":
                    int probabilidad = r.Next(1, 50);
                    if(probabilidad % 2 == 0)
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

        public bool Resolver() //Tratar de refactorizar. Repite mucho código
        {
            List<Pista> sol = CargarPistas();
            List<Pista> aux = new List<Pista>();
            int indice = 0;
            bool salir = false;
            Sospechoso propuestaCulpable = null;
            Arma propuestaArma = null;
            Habitacion propuestaEstancia = null;

            Console.SetCursorPosition(0, indice);
            Console.CursorVisible = false;

            while (!salir)
            {
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("Elige sospechoso: ");
                aux = sol.Where(s => s is Sospechoso).ToList();
                Mostrar(aux, indice);
                ConsoleKeyInfo tecla = Console.ReadKey(true);
                
                if(!Console.KeyAvailable)
                {
                    if (tecla.Key == ConsoleKey.UpArrow && indice > 0)
                        indice--;
                    if (tecla.Key == ConsoleKey.DownArrow && indice < 6)
                        indice++;
                    if (tecla.Key == ConsoleKey.Enter)
                    {
                        propuestaCulpable = (Sospechoso)aux[indice];
                        salir = true;
                    }
                }
            }

            salir = false;
            indice = 0;
            Console.SetCursorPosition(0, indice);

            while (!salir)
            {
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("Elige arma: ");
                aux = sol.Where(s => s is Arma).ToList();
                Mostrar(aux, indice);
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (!Console.KeyAvailable)
                {
                    if (tecla.Key == ConsoleKey.UpArrow && indice > 0)
                        indice--;
                    if (tecla.Key == ConsoleKey.DownArrow && indice < 6)
                        indice++;
                    if (tecla.Key == ConsoleKey.Enter)
                    {
                        propuestaArma = (Arma)aux[indice];
                        salir = true;
                    }
                }
            }

            salir = false;
            indice = 0;
            Console.SetCursorPosition(0, indice);

            while (!salir)
            {
                Console.Clear();
                Console.ResetColor();
                Console.WriteLine("Elige escena del crimen: ");
                aux = pistas.Where(s => s is Habitacion).ToList();
                Mostrar(aux, indice);
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (!Console.KeyAvailable)
                {
                    if (tecla.Key == ConsoleKey.UpArrow && indice > 0)
                        indice--;
                    if (tecla.Key == ConsoleKey.DownArrow && indice < 6)
                        indice++;
                    if (tecla.Key == ConsoleKey.Enter)
                    {
                        propuestaEstancia = (Habitacion)aux[indice];
                        salir = true;
                    }
                }
            }

            return solucionCaso == new Solucion(propuestaCulpable, propuestaArma, propuestaEstancia);
        }
        private void Mostrar(List<Pista> lista, int indice)
        {
            for(int i = 0; i < lista.Count; i++)
            {
                if (indice == i)
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ResetColor();

                Console.WriteLine(lista[i]);       
            }
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
