using System.Runtime.CompilerServices;

namespace ProyectoFinal_ivinader
{
    internal class Program
    {
        public static void InicializarConsola()
        {
            Console.Title = "Se ha programado un crimen";
        }
        static void Main(string[] args)
        {
            InicializarConsola();
            Menu menuJuego = new Menu();
            menuJuego.MostrarMenu();
            
        }
    }
}