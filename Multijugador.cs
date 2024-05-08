using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Multijugador: Juego
    {
        public Multijugador(List<Jugador> jugadoresLista) : base(jugadoresLista)
        {
            jugadores.Sort((j1,j2) => j1.Turno.CompareTo(j2.Turno));
        }
        public void LanzarJuego()
        {
            int turnos = jugadores.Count;
            int contador = 0;
            bool resuelto = false;

            while(!resuelto)
            {
                foreach (Jugador j in jugadores)
                {
                    if (j.Activo)
                        resuelto = JugarTurno(j);
                }

                if(!resuelto)
                {
                    if (contador < turnos - 1)
                        contador++;
                    else
                        contador = 0;

                    jugadores[contador].Activo = true;
                }
            }

            /*if(resuelto)
            {
                Console.Clear();
                Console.SetCursorPosition(Console.WindowWidth / 3, Console.WindowHeight / 2);
                Console.WriteLine("Enhorabuena! Has resuelto el asesinato");
            }*/
        }
        public bool JugarTurno(Jugador j)
        {
            //Implementar juego por turnos en multi
            int contadorPasos = 0, pasos;
            bool resuelto = false;

            pasos = dadoM.Lanzar();
            dadoE.ObtenerEvento(j, tablero);

            while (contadorPasos < pasos)
            {
                Console.Clear();
                tablero.MostrarTablero();

                j.MostrarDatos(pasos);
                j.MostrarObjetos(0, tablero.Alto + 3);
                j.MostrarPistas(Console.WindowWidth / 2, tablero.Alto + 3);

                //if (contadorPasos % 8 == 0)
                  //  OcurreEvento(j);

                foreach(Jugador sprites in jugadores)
                {
                    sprites.Icono.Dibujar();
                }

                //j.Icono.Dibujar(); -> En el modo multijugador este método ha sido sustituido por el foreach que permite que todos los sprites salgan por pantalla todo el rato
                j.Icono.Mover(tablero, j);
                contadorPasos++;

                if (tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "1" && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "P" && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "p" && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != " " && tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) != "R")
                {
                    DarPista(j, tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y));
                    if (j.ListaObjetos.Contains(new Objeto("Trofeo", "")))
                    {
                        DarPista(j, tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y));
                        j.ListaObjetos.Remove(new Objeto("Trofeo", ""));
                    }
                }
                else
                {
                    if (tablero.ComprobarEspacio(j.Icono.X, j.Icono.Y) == "R")
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

            if (contadorPasos == pasos)
                j.Activo = false;

            return resuelto;
        }
    }
}
