﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Sprite: IEsMovible
    {
        private int x;
        private int y;
        private string color;
        //private imagen???? Asociar aquí a las imágenes
        public Sprite(string color)
        {
            x = 14; 
            y = 13;
            this.color = color;
        }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public string Color { get => color; set => color = value; }

        private bool ComprobarColision(List<Jugador> jugadores, Jugador j)
        {
            bool colision = false;

            for (int i = 0; i < jugadores.Count; i++)
            {
                if (j != jugadores[i])
                {
                    if (j.Icono.X == jugadores[i].Icono.X && j.Icono.Y == jugadores[i].Icono.Y)
                        colision = true;
                }
            }

            return colision;
        }
        public void Mover(Tablero t, List<Jugador> jugadores, Jugador j, ref bool colision)
        {
            Console.CursorVisible = false;

            ConsoleKeyInfo tecla = Console.ReadKey();

            if (tecla.Key == ConsoleKey.UpArrow && y > 1)
            {
                if (t.ComprobarEspacio(x, y - 1) != "1" && t.ComprobarEspacio(x,y-1) != "P" && !ComprobarColision(jugadores, j))
                {
                    y--;
                    if (ComprobarColision(jugadores, j))
                    {
                        y++;
                        colision = true;
                    }
                }

                else
                {
                    if (t.ComprobarEspacio(x, y - 1) == "P" && j.ListaObjetos.Contains(new Objeto("Ganzúa", "")))
                    {
                        y--;
                        j.ListaObjetos.Remove(new Objeto("Ganzúa", ""));
                        t.GestionPuertas(x, y);
                    }
                }
            }
            if (tecla.Key == ConsoleKey.DownArrow && y < t.Alto)
            {
                if (t.ComprobarEspacio(x, y + 1) != "1" && t.ComprobarEspacio(x, y + 1) != "P")
                {
                    y++;
                    if (ComprobarColision(jugadores, j))
                    {
                        y--;
                        colision = true;
                    }
                }
                else
                {
                    if (t.ComprobarEspacio(x, y + 1) == "P" && j.ListaObjetos.Contains(new Objeto("Ganzúa", "")))
                    {
                        y++;
                        j.ListaObjetos.Remove(new Objeto("Ganzúa", ""));
                        t.GestionPuertas(x, y);
                    }
                }
            }
            if (tecla.Key == ConsoleKey.RightArrow && x < t.Ancho)
            {
                if (t.ComprobarEspacio(x+1, y) != "1" && t.ComprobarEspacio(x+1, y) != "P")
                {
                    x++;
                    if (ComprobarColision(jugadores, j))
                    {
                        x--;
                        colision = true;
                    }
                }
                else
                {
                    if (t.ComprobarEspacio(x + 1, y) == "P" && j.ListaObjetos.Contains(new Objeto("Ganzúa", "")))
                    {
                        x++;
                        j.ListaObjetos.Remove(new Objeto("Ganzúa", ""));
                        t.GestionPuertas(x, y);
                    }
                }
            }
            if (tecla.Key == ConsoleKey.LeftArrow && x > 0)
            {
                if (t.ComprobarEspacio(x-1, y) != "1" && t.ComprobarEspacio(x - 1, y) != "P")
                {
                    x--;
                    if (ComprobarColision(jugadores, j))
                    {
                        x++;
                        colision = true;
                    }
                }
                else
                {
                    if (t.ComprobarEspacio(x - 1, y) == "P" && j.ListaObjetos.Contains(new Objeto("Ganzúa", "")))
                    {
                        x--;
                        j.ListaObjetos.Remove(new Objeto("Ganzúa", ""));
                        t.GestionPuertas(x, y);
                    }
                }
            }
            Console.SetCursorPosition(x, y);
        }

        public void Dibujar()
        {
            AplicarColor();
            Console.SetCursorPosition(x, y);
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
