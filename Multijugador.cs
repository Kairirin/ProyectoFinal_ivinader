using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Multijugador: Juego
    {
        public Multijugador(List<Jugador> jugadoresLista) : base(jugadoresLista)
        {
            jugadores.Sort((j1,j2) => j1.Turno.CompareTo(j2.Turno));
        }
        public override bool JugarTurno(Jugador j)
        {
            //Implementar juego por turnos en multi
            int pasos;
            bool resuelto = false;

            pasos = dadoM.Lanzar();
            dadoE.ObtenerEvento(j, tablero);

            while (pasos > 0)
            {
                Console.Clear();
                bool ocupada = false;
                tablero.MostrarTablero();

                j.MostrarDatos(pasos);
                j.MostrarObjetos(4, 18);
                j.MostrarPistas(54, 11);

                foreach (Jugador sprites in jugadores)
                {
                    sprites.Icono.Dibujar();
                }

                j.Icono.Mover(tablero, jugadores, j, ref ocupada);
                if (!ocupada)
                    pasos--;


                if (tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "1" && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "P" && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "p" && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != " " && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "R")
                {
                    DarPista(j, tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y));
                    if (j.ListaObjetos.Contains(new Objeto("Trofeo", "")))
                    {
                        DarPista(j, tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y));
                        j.ListaObjetos.Remove(new Objeto("Trofeo", ""));
                    }
                    pasos = 0;
                }
                else
                {
                    if (tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) == "R")
                    {
                        if (Resolver())
                        {
                            DibujarCuadro();
                            Console.SetCursorPosition(Console.WindowWidth / 4 + 1, 8);
                            Console.WriteLine("Enhorabuena! Has resuelto el asesinato");
                        }
                        else
                        {
                            DibujarCuadro();
                            Console.SetCursorPosition(Console.WindowWidth / 4 + 1, 8);
                            Console.WriteLine("Sigue intentándolo");
                            Console.SetCursorPosition(Console.WindowWidth / 4 + 1, 8 + 1);
                            Console.WriteLine(" Pulsa cualquier tecla para continuar");
                            Console.ReadLine();
                        }
                    }
                }
            }

            if (pasos == 0)
                j.Activo = false;

            return resuelto;
        }
    }
}
