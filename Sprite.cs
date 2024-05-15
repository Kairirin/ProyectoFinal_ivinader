using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Sprite: IEsMovible
    {
        private static Posicion mov_vertical = new Posicion(0, 1);
        private static Posicion mov_horizontal = new Posicion(1, 0);
        private Posicion posicionSprite;
        private string color;
        public Sprite(string color)
        {
            posicionSprite = new Posicion(26, 10);
            this.color = color;
        }
        public string Color { get => color; set => color = value; }
        public Posicion PosicionSprite { get => posicionSprite; set => posicionSprite = value; }

        private bool ComprobarOcupada(List<Jugador> jugadores, Jugador j)
        {
            bool ocupada = false;

            for (int i = 0; i < jugadores.Count; i++)
            {
                if (j != jugadores[i])
                {
                    if (j.Icono.PosicionSprite == jugadores[i].Icono.PosicionSprite)
                        ocupada = true;
                }
            }

            return ocupada;
        }
        public void Mover(Tablero t, List<Jugador> jugadores, Jugador j, ref bool ocupada) //MIRAR BIEN ESTE MÉTODO. MUY LARGO
        {
            Console.CursorVisible = false;

            ConsoleKeyInfo tecla = Console.ReadKey();

            if (tecla.Key == ConsoleKey.UpArrow && posicionSprite.Y > Tablero.Y1_TABLERO1)
            {
                if(t.ComprobarEspacio(posicionSprite - mov_vertical))
                {
                    posicionSprite.Y--;
                    if (ComprobarOcupada(jugadores, j))
                    {
                        posicionSprite.Y++;
                        ocupada = true;
                    }
                }
                else
                {
                    if (t.ComprobarPuerta(posicionSprite - mov_vertical) && j.ListaObjetos.Contains(new Objeto("Ganzúa", "")))
                    {
                        posicionSprite.Y--;
                        j.ListaObjetos.Remove(new Objeto("Ganzúa", ""));
                        t.GestionPuertas(posicionSprite);
                    }
                }
            }
            if (tecla.Key == ConsoleKey.DownArrow && posicionSprite.Y < Tablero.YN_TABLERO1)
            {
                if (t.ComprobarEspacio(posicionSprite + mov_vertical))
                {
                    posicionSprite.Y++;
                    if (ComprobarOcupada(jugadores, j))
                    {
                        posicionSprite.Y--;
                        ocupada = true;
                    }
                }
                else
                {
                    if (t.ComprobarPuerta(posicionSprite + mov_vertical) && j.ListaObjetos.Contains(new Objeto("Ganzúa", "")))
                    {
                        posicionSprite.Y++;
                        j.ListaObjetos.Remove(new Objeto("Ganzúa", ""));
                        t.GestionPuertas(posicionSprite);
                    }
                }
            }
            if (tecla.Key == ConsoleKey.RightArrow && posicionSprite.X < Tablero.XN_TABLERO1)
            {
                if (t.ComprobarEspacio(posicionSprite + mov_horizontal))
                {
                    posicionSprite.X++;
                    if (ComprobarOcupada(jugadores, j))
                    {
                        posicionSprite.X--;
                        ocupada = true;
                    }
                }
                else
                {
                    if (t.ComprobarPuerta(posicionSprite + mov_horizontal) && j.ListaObjetos.Contains(new Objeto("Ganzúa", "")))
                    {
                        posicionSprite.X++;
                        j.ListaObjetos.Remove(new Objeto("Ganzúa", ""));
                        t.GestionPuertas(posicionSprite);
                    }
                }
            }
            if (tecla.Key == ConsoleKey.LeftArrow && posicionSprite.X > Tablero.X1_TABLERO1)
            {
                if (t.ComprobarEspacio(posicionSprite - mov_horizontal))
                {
                    posicionSprite.X--;
                    if (ComprobarOcupada(jugadores, j))
                    {
                        posicionSprite.X++;
                        ocupada = true;
                    }
                }
                else
                {
                    if (t.ComprobarPuerta(posicionSprite - mov_horizontal) && j.ListaObjetos.Contains(new Objeto("Ganzúa", "")))
                    {
                        posicionSprite.X--;
                        j.ListaObjetos.Remove(new Objeto("Ganzúa", ""));
                        t.GestionPuertas(posicionSprite);
                    }
                }
            }
            Console.SetCursorPosition(posicionSprite.X, posicionSprite.Y);
        }

        public void Dibujar()
        {
            AplicarColor();
            Console.SetCursorPosition(posicionSprite.X, posicionSprite.Y);
            Console.Write('■');
        }
        public void AplicarColor()
        {
            switch(color)
            {
                case "Red":
                    Console.ForegroundColor = ConsoleColor.Red; 
                    break;
                case "Blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "Pink":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "Black":
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "Green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }
        }
        
    }
}
