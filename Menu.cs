using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Menu
    {
        private SortedList<string, string> personajes;
        public Menu()
        {
            personajes = new SortedList<string, string>();
            CargarPersonajes();
        }
        private void CargarPersonajes()
        {
            StreamReader ficheroPJ = null;
            string linea = "";

            try
            {
                ficheroPJ = new StreamReader("personajesJugables.txt");
                linea = ficheroPJ.ReadLine();

                while (linea != null)
                {
                    string[] trozos = linea.Split(';');

                    if (linea != null)
                    {
                        personajes.Add(trozos[0], trozos[1]);
                    }

                    linea = ficheroPJ.ReadLine();
                }

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("- Fichero no encontrado -");
            }
            finally
            {
                if (personajes != null)
                {
                    ficheroPJ.Close();
                }
            }
        }
        public void MostrarMenu()
        {
            bool salir = false;
            int linea = 0;

            while (!salir)
            {
                //Cargar botones en pantalla
                Console.Clear();
                Console.WriteLine("- Jugar solo" +
                    "\n- Multijugador" +
                    "\n- Instrucciones" +
                    "\n- Salir");

                Console.SetCursorPosition(0, linea);

                if (!Console.KeyAvailable)
                {
                    ConsoleKeyInfo tecla = Console.ReadKey();

                    if (tecla.Key == ConsoleKey.UpArrow && linea > 0)
                    {
                        linea--;
                    }
                    if (tecla.Key == ConsoleKey.DownArrow && linea < 3)
                    {
                        linea++;
                    }
                    if (tecla.Key == ConsoleKey.Enter)
                    {
                        switch (linea)
                        {
                            case 0:
                                ModoUnJugador();
                                break;
                            case 1:
                                ModoMultijugador();
                                break;
                            case 2:
                                MostrarInstrucciones();
                                break;
                            case 3:
                                salir = true;
                                break;
                        }
                    }
                }
            }
        }
        private void MostrarPersonajes(int indice)
        {
            Console.WriteLine("## Elige personaje ##");

            for (int i = 0; i < personajes.Count; i++)
            {
                if (indice == i)
                    Console.ForegroundColor = ConsoleColor.Green;
                else
                    Console.ResetColor();

                Console.WriteLine(personajes.Keys[i]);
            }
        }
        private Jugador SeleccionPersonaje()
        {
            Jugador j1 = null;
            int indice = 0;
            bool select = false;

            while (!select)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.ResetColor();

                MostrarPersonajes(indice);

                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (!Console.KeyAvailable)
                {
                    if (tecla.Key == ConsoleKey.UpArrow && indice > 0)
                        indice--;
                    if (tecla.Key == ConsoleKey.DownArrow && indice < personajes.Count)
                        indice++;
                    if (tecla.Key == ConsoleKey.Enter)
                    {
                        j1 = new Jugador(personajes.Keys[indice], personajes.Values[indice]);
                        select = true;
                    }
                }
            }

            return j1;
        }
        public void ModoUnJugador()
        {
            Jugador j1 = SeleccionPersonaje();
            //Crear Lista con Personaje
            List<Jugador> jugadores = new List<Jugador>();
            jugadores.Add(j1);

            Juego cluedo = new Juego(jugadores);
            cluedo.LanzarJuego(j1); //Método que sustituye a jugar turno en el modo de un jugador
        }
        public static void ModoMultijugador()
        {
            int numJugadores;

            Console.Write("Número de jugadores: ");
            numJugadores = Convert.ToInt32(Console.ReadLine()); // Botones de 2, 3 o 4.

            //Selección de personajes
            //Cargar fichero jugadores asociado a ficheros binarios de imágenes

            //Crear Lista con Personaje
            //LanzarJuego(); //Pasar personaje como parámetro.
        }
        public static void MostrarInstrucciones()
        {
            Console.Clear();
            //Cargar imagen con instrucciones???
        }
    }
}
