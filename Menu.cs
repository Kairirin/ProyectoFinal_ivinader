using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Menu
    {
        private const int LINEA_PERSONAJES_X = 64;
        private const int LINEA_PERSONAJES_Y = 13;
        private string[] menu;
        private SortedList<string, string> personajes;
        private List<Jugador> jugadores;

        public Menu()
        {
            menu = new string[CargaFichero.ALTO];
            personajes = new SortedList<string, string>();
            jugadores = new List<Jugador>();
            CargarPersonajes();
        }
        private void Dibujar(string[] lineas)
        {
            for (int i = 0; i < lineas.Length; i++)
            {
                Console.WriteLine(lineas[i]);
            }
        }
        public void MostrarMenu()
        {
            bool terminar = false;
            Posicion unJugador = new Posicion(14, 15); //Le he restado 1 a todo, tenerlo en cuenta
            Posicion multijugador = new Posicion(61,15);
            Posicion instrucciones = new Posicion(14,22);
            Posicion salir = new Posicion(61,22);
            menu = CargaFichero.Cargar("menuPrincipal.txt");
            int actual = 1;

            while (!terminar)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                Dibujar(menu);

                switch(actual)
                {
                    case 1:
                        Console.SetCursorPosition(unJugador.X, unJugador.Y);
                        Console.WriteLine("=>");
                        break;
                    case 2:
                        Console.SetCursorPosition(multijugador.X, multijugador.Y);
                        Console.WriteLine("=>");
                        break;
                    case 3:
                        Console.SetCursorPosition(instrucciones.X, instrucciones.Y);
                        Console.WriteLine("=>");
                        break;
                    case 4:
                        Console.SetCursorPosition(salir.X, salir.Y);
                        Console.WriteLine("=>");
                        break;
                }

                if (!Console.KeyAvailable)
                {
                    ConsoleKeyInfo tecla = Console.ReadKey();

                    if(tecla.Key == ConsoleKey.DownArrow && actual != 4)//Console.CursorTop != salir.Y)
                    {
                        if (actual == 1)
                            actual = 3;
                        else
                            actual = 4;
                    }
                    if (tecla.Key == ConsoleKey.UpArrow && actual !=1)//Console.CursorTop != unJugador.Y)
                    {
                        if (actual == 3)
                            actual = 1;
                        else
                            actual = 2;
                    }
                    if (tecla.Key == ConsoleKey.LeftArrow && actual != 1)//Console.CursorLeft != unJugador.X)
                    {
                        if (actual == 2)
                            actual = 1;
                        else
                            actual = 3;
                    }
                    if (tecla.Key == ConsoleKey.RightArrow && actual != 2)//Console.CursorLeft != salir.X)
                    {
                        if (actual == 1)
                            actual = 2;
                        else
                            actual = 4;
                    }

                    if (tecla.Key == ConsoleKey.Enter)
                    {
                        switch(actual)
                        {
                            case 1:
                                ModoUnJugador();
                                break;
                            case 2:
                                ModoMultijugador();
                                break;
                            case 3:
                                MostrarInstrucciones();
                                break;
                            case 4:
                                terminar = true;
                                break;
                        }
                    }
                    if (tecla.Key == ConsoleKey.Escape)
                        terminar = true;
                }
            }
        }
        private void CargarPersonajes()
        {
            string[] personajesFichero = CargaFichero.Cargar("personajesJugables.txt");

            for (int i = 0; i < personajesFichero.Length; i++)
            {
                string[] trozos = personajesFichero[i].Split(';');
                personajes.Add(trozos[0], trozos[1]);
            }
        }
        private void IntroducirEleccionPersonaje()
        {
            string[] pantallaPj = CargaFichero.Cargar("eleccionPersonaje.txt");
            Dibujar(pantallaPj);
        }
        private void MostrarPersonajes(int indice)
        {
            for (int i = 0; i < personajes.Count; i++)
            {
                Console.SetCursorPosition(LINEA_PERSONAJES_X, LINEA_PERSONAJES_Y + i);

                if (indice == i)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ResetColor();

                Console.WriteLine(personajes.Keys[i]);
            }
        }
        private void MostrarYaElegidos()
        {
            Posicion lineaSeleccionados = new Posicion(60, 20);

            for (int i = 0; i < jugadores.Count; i++)
            {
                Console.SetCursorPosition(lineaSeleccionados.X, lineaSeleccionados.Y + i);
                Console.WriteLine($"## Jugador {i + 1} - {jugadores[i].Nombre} ##");
            }
        }
        private Jugador SeleccionPersonaje(int turno)
        {
            Jugador j1 = null;
            int indice = 0;
            bool select = false;

            while (!select)
            {
                Console.CursorVisible = false;
                Console.Clear();
                IntroducirEleccionPersonaje();
                MostrarPersonajes(indice);

                Console.ResetColor();

                if (jugadores.Count > 0)
                {
                    MostrarYaElegidos();
                }

                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (!Console.KeyAvailable)
                {
                    if (tecla.Key == ConsoleKey.UpArrow && indice > 0)
                        indice--;
                    if (tecla.Key == ConsoleKey.DownArrow && indice < personajes.Count - 1)
                        indice++;
                    if (tecla.Key == ConsoleKey.Enter)
                    {
                        j1 = new Jugador(personajes.Keys[indice], personajes.Values[indice], turno);
                        select = true;
                    }
                    if (tecla.Key == ConsoleKey.Escape)
                        MostrarMenu();
                }

            }
            return j1;
        }
        public void ModoUnJugador()
        {
            Console.Clear();
            Jugador j1 = SeleccionPersonaje(1);

            Juego cluedo = new Juego(j1);
            cluedo.LanzarJuego(j1); //Método que sustituye a jugar turno en el modo de un jugador
        }
        private int IntroducirNumJugadores()
        {
            string[] pantalla = CargaFichero.Cargar("eleccionMulti.txt");
            bool terminar = false;
            int numJugadores = 2;
            Posicion cursor = new Posicion(64, 16);

            while (!terminar)
            {
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
                Dibujar(pantalla);

                Console.SetCursorPosition(cursor.X, cursor.Y);
                Console.WriteLine(">");

                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (!Console.KeyAvailable)
                {
                    if (tecla.Key == ConsoleKey.LeftArrow && numJugadores > 2)
                    {
                        numJugadores--;
                        cursor.X-=7;
                    }
                    if (tecla.Key == ConsoleKey.RightArrow && numJugadores < 4)
                    {
                        numJugadores++;
                        cursor.X+=7;
                    }
                    if (tecla.Key == ConsoleKey.Enter)
                    {
                        terminar = true;
                    }
                    if (tecla.Key == ConsoleKey.Escape)
                        MostrarMenu();
                }
            }

            return numJugadores;
        }
        public void ModoMultijugador()
        {
            int numJugadores = IntroducirNumJugadores(), contador = 0;
            Jugador jugadorProvisional;
            bool empezar = false;

            while(!empezar) 
            {
                jugadorProvisional = SeleccionPersonaje(contador + 1);

                if (jugadores.Count == 0)
                {
                    jugadores.Add(jugadorProvisional);
                    personajes.Remove(jugadorProvisional.Nombre);
                    contador++;
                }
                else if (!jugadores.Contains(jugadorProvisional))
                {
                    jugadores.Add(jugadorProvisional);
                    personajes.Remove(jugadorProvisional.Nombre);
                    contador++;
                }
                
                if (jugadores.Count == numJugadores)
                {
                    MostrarYaElegidos();
                    Console.SetCursorPosition(60, 27);
                    Console.WriteLine("Pulsa Enter para empezar...");
                    Console.ReadLine();
                    empezar = true;
                }
            }

            Multijugador cluedo = new Multijugador(jugadores);
            cluedo.LanzarJuego();

            //LanzarJuego(); //Pasar personaje como parámetro.
        }
        public static void MostrarInstrucciones()
        {
            Console.Clear();
            //Cargar imagen con instrucciones???
        }
    }
}
