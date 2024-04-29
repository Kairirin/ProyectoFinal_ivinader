using System.Runtime.CompilerServices;

namespace ProyectoFinal_ivinader
{
    internal class Program
    {
        public static void InicializarConsola()
        {
            Console.Title = "Se ha programado un crimen";
            //Configurar pantalla completa o centrar - Windows forms
            //Cargar imagen de fondo
        }
        public static void MostrarMenu()
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

                    if(tecla.Key == ConsoleKey.UpArrow && linea > 0)
                    {
                        linea--;
                    }
                    if(tecla.Key == ConsoleKey.DownArrow && linea < 3)
                    {
                        linea++;
                    }
                    if(tecla.Key == ConsoleKey.Enter)
                    {
                        switch(linea)
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
        public static void ModoUnJugador()
        {
            //Selección de personaje
            //Cargar fichero jugadores asociado a ficheros binarios de imágenes

            //Crear Lista con Personaje
            LanzarJuego(); //Pasar personaje como parámetro.
        }
        public static void ModoMultijugador()
        {
            int numJugadores;

            Console.Write("Número de jugadores: ");
            numJugadores = Convert.ToInt32(Console.ReadLine()); // Botones de 2, 3 o 4.

            //Selección de personajes
            //Cargar fichero jugadores asociado a ficheros binarios de imágenes

            //Crear Lista con Personaje
            LanzarJuego(); //Pasar personaje como parámetro.
        }
        public static void MostrarInstrucciones()
        {
            Console.Clear();
           //Cargar imagen con instrucciones???
        }
        static void Main(string[] args)
        {
            InicializarConsola();
            MostrarMenu();
            
        }
    }
}