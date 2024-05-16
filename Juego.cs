namespace ProyectoFinal_ivinader
{
    internal class Juego
    {
        private const int INICIO_LINEA_ESCRITURA = 8;
        protected List<Jugador> jugadores;
        protected Tablero tablero;
        protected DadoEvento dadoE;
        protected DadoMovimiento dadoM;
        protected Solucion solucionCaso;
        protected List<Pista> pistas;
        protected Random r;

        public Juego()
        {
            tablero = new Tablero();
            r = new Random();
            pistas = CargaFichero.CargarPistas("pistas.xml");
            pistas.AddRange(CargaFichero.CargarHabitaciones());
            solucionCaso = CargarSolucion();
            Console.WriteLine(solucionCaso.ToString());
            Console.ReadLine();
            dadoE = new DadoEvento();
        }
        public Juego(Jugador j1): this()
        {
            jugadores = new List<Jugador>();
            jugadores.Add(j1);
        }
        public Juego(List<Jugador> jugadores): this()
        {
            this.jugadores = jugadores;
            dadoM = new DadoMovimiento();
        }
        private Solucion CargarSolucion()
        {
            List<Pista> listaSospechososAuxiliar = pistas.FindAll(s => s is Sospechoso);
            List<Pista> listaArmasAuxiliar = pistas.FindAll(s => s is Arma);
            List<Pista> listaHabsAuxiliar = pistas.FindAll(s => s is Habitacion);

            Sospechoso culpable = (Sospechoso)listaSospechososAuxiliar[r.Next(0, listaSospechososAuxiliar.Count() - 1)];
            Arma arma = (Arma)listaArmasAuxiliar[r.Next(0, listaArmasAuxiliar.Count() - 1)];
            Habitacion escenaCrimen = (Habitacion)listaHabsAuxiliar[r.Next(0, listaHabsAuxiliar.Count() - 1)];

            pistas.Remove(culpable);
            pistas.Remove(arma);
            pistas.Remove(escenaCrimen);

            return new Solucion(culpable, arma, escenaCrimen);
        }
        public void LanzarJuego()
        {
            bool resuelto = false;
            int turnos = jugadores.Count;
            int contador = 0;

            while (!resuelto)
            {
                if (turnos == 1)
                {
                    resuelto = JugarTurno(jugadores[0]);
                }
                else
                {
                    foreach (Jugador j in jugadores)
                    {
                        if (j.Activo)
                            resuelto = JugarTurno(j);
                    }

                    if (!resuelto)
                    {
                        if (contador < turnos - 1)
                            contador++;
                        else
                            contador = 0;

                        jugadores[contador].Activo = true;
                    }
                }
            }
        }
        public virtual bool JugarTurno(Jugador j)
        {
            int contadorPasos = 0, pasos = 0;
            bool resuelto = false;

            while(!resuelto)
            {
                Console.Clear();
                bool ocupada = false;
                tablero.MostrarTablero();

                j.MostrarDatos();
                j.MostrarObjetos(4, 18);
                j.MostrarPistas(54, 11);

                if (contadorPasos % 8 == 0)
                    dadoE.ObtenerEvento(j, ref pasos, tablero);

                j.Icono.Dibujar();
                j.Icono.Mover(tablero, jugadores, j, ref ocupada);
                contadorPasos++;
                
                if(tablero.EstaEnHabitacion(j.Icono.PosicionSprite))
                {
                    DarPista(j, tablero.DevolverLetra(j.Icono.PosicionSprite));
                    if(j.ListaObjetos.Contains(new Objeto("Trofeo","")))
                    {
                        DarPista(j, tablero.DevolverLetra(j.Icono.PosicionSprite));
                        j.ListaObjetos.Remove(new Objeto("Trofeo", ""));
                    }
                }
                else
                {
                    if(tablero.DevolverLetra(j.Icono.PosicionSprite) == "R")
                    {
                        if (Resolver())
                        {
                            DibujarCuadro();
                            Console.SetCursorPosition(Console.WindowWidth / 4 + 1, INICIO_LINEA_ESCRITURA);
                            Console.WriteLine("Enhorabuena! Has resuelto el asesinato");
                            resuelto = true;
                        }
                        else
                        {
                            DibujarCuadro();
                            Console.SetCursorPosition(Console.WindowWidth / 4 + 1, INICIO_LINEA_ESCRITURA);
                            Console.WriteLine("Sigue intentándolo");
                            Console.SetCursorPosition(Console.WindowWidth / 4 + 1, INICIO_LINEA_ESCRITURA + 1);
                            Console.WriteLine(" Pulsa cualquier tecla para continuar");
                            Console.ReadLine();
                        }
                    }
                }
            }
            return resuelto;
        }
        public void DarPista(Jugador j, string hab)
        {
            bool salir = false;
            int contador = 0;

            List<Pista> pistaProvisional = pistas.FindAll(p => p.LetraAsociada == hab);

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
        protected bool Resolver()
        {
            List<Pista> pistas = CargaFichero.CargarPistas("pistas.xml");
            Sospechoso propuestaCulpable = (Sospechoso)SeleccionarSolucion(pistas, "sospechoso");
            Arma propuestaArma = (Arma)SeleccionarSolucion(pistas, "arma");
            Habitacion propuestaEstancia = (Habitacion)SeleccionarSolucion(pistas, "habitacion");

            return solucionCaso == new Solucion(propuestaCulpable, propuestaArma, propuestaEstancia);
        }
        private Pista SeleccionarSolucion(List<Pista> pistas, string tipo)
        {
            List<Pista> aux = new List<Pista>();
            Pista pista = null;
            bool salir = false;
            int indice = 0;

            switch (tipo)
            {
                case "sospechoso":
                    aux = pistas.Where(s => s is Sospechoso).ToList();
                    break;
                case "arma":
                    aux = pistas.Where(a => a is Arma).ToList();
                    break;
                case "habitacion":
                    aux.AddRange(CargaFichero.CargarHabitaciones());
                    break;
            }

            Console.SetCursorPosition(0, indice);
            Console.CursorVisible = false;

            while (!salir)
            {
                Console.ResetColor();
                DibujarCuadro();
                Console.SetCursorPosition(Console.WindowWidth / 4 + 1, INICIO_LINEA_ESCRITURA);
                Console.WriteLine($"Elige {tipo}: ");

                Mostrar(aux, indice);
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (!Console.KeyAvailable)
                {
                    if (tecla.Key == ConsoleKey.UpArrow && indice > 0)
                        indice--;
                    if (tecla.Key == ConsoleKey.DownArrow && indice < aux.Count - 1)
                        indice++;
                    if (tecla.Key == ConsoleKey.Enter)
                    {
                        pista = aux[indice];
                        salir = true;
                    }
                }
            }

            return pista;
        }
        protected void DibujarCuadro()
        {
            string[] cuadro = CargaFichero.Cargar("cuadro.txt");

            for (int i = 0; i < cuadro.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 4, INICIO_LINEA_ESCRITURA - 1 + i);
                Console.WriteLine(cuadro[i]);

            }
        }
        private void Mostrar(List<Pista> lista, int indice)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if (indice == i)
                    Console.ForegroundColor = ConsoleColor.Red;
                else
                    Console.ResetColor();

                Console.SetCursorPosition(Console.WindowWidth / 4 + 1, INICIO_LINEA_ESCRITURA + i + 1);
                Console.WriteLine(lista[i]);
            }
        }
        public List<Jugador> Jugadores { get => jugadores; set => jugadores = value; }
        public Tablero Tablero { get => tablero; set => tablero = value; }
        public DadoEvento DadoE { get => dadoE; set => dadoE = value; }
        public DadoMovimiento DadoM { get => dadoM; set => dadoM = value; }
        public Solucion SolucionCaso { get => solucionCaso; set => solucionCaso = value; }
        public List<Pista> Pistas { get => pistas; set => pistas = value; }
        public Random R { get => r; set => r = value; }
    }
}
