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
            int pasos;
            bool resuelto = false;

            pasos = dadoM.Lanzar();
            Console.Clear();
            tablero.MostrarTablero();
            j.MostrarDatos(pasos);
            dadoE.ObtenerEvento(j, ref pasos, tablero);
            

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


                if (tablero.EstaEnHabitacion(j.Icono.PosicionSprite))
                {
                    DarPista(j, tablero.DevolverLetra(j.Icono.PosicionSprite));
                    if (j.ListaObjetos.Contains(new Objeto("Trofeo", "")))
                    {
                        DarPista(j, tablero.DevolverLetra(j.Icono.PosicionSprite));
                        j.ListaObjetos.Remove(new Objeto("Trofeo", ""));
                    }
                    j.MostrarPistas(54, 11);
                    pasos = 0;
                }
                else
                {
                    if (tablero.DevolverLetra(j.Icono.PosicionSprite) == "R")
                    {
                        if (Resolver())
                        {
                            DibujarCuadro();
                            Console.SetCursorPosition(Console.WindowWidth / 4 + 1, 8);
                            Console.WriteLine("Enhorabuena! Has resuelto el asesinato");
                            Console.ReadLine();
                            resuelto = true;
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
