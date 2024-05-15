using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProyectoFinal_ivinader
{
    internal class Juego
    {
        protected List<Jugador> jugadores;
        protected Tablero tablero;
        protected DadoEvento dadoE; 
        protected DadoMovimiento dadoM;
        protected Solucion solucionCaso;
        protected List<Pista> pistas;
        //private List<IEsEvento> eventos; -> Se han movido a la clase DadoEventos
        protected Random r;

        public Juego()
        {
            tablero = new Tablero();
            r = new Random();
            //CargarHabitacionesEnFichero();
            pistas = CargarPistas();
            pistas.AddRange(CargarHabitaciones());
            //eventos = CargarEventos();
            solucionCaso = CargarSolucion();
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
        private List<Pista> CargarPistas()
        {
            List<Pista> pistas = new List<Pista>();
            XmlSerializer formatter = new XmlSerializer(pistas.GetType());
            FileStream stream = new FileStream("pistas.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
            pistas = (List<Pista>)formatter.Deserialize(stream);

            stream.Close();

            return pistas;
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
        public List<Jugador> GetJugadores() //Borrar y hacer propiedades
        {
            return jugadores;
        }
        public Solucion GetSolucion()
        {
            return solucionCaso;
        }
        public List<Pista> GetPistas()
        {
            return pistas;
        }
        /*public List<IEsEvento> GetEventos()
        {
            return eventos;
        }*/
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
            int contadorPasos = 0;
            bool resuelto = false;

            while(!resuelto)
            {
                Console.Clear();
                bool colision = false;
                tablero.MostrarTablero();

                j.MostrarDatos();
                j.MostrarObjetos(4, 18);
                j.MostrarPistas(54, 11);

                if (contadorPasos % 8 == 0)
                    dadoE.ObtenerEvento(j, tablero);

                j.Icono.Dibujar();
                j.Icono.Mover(tablero, jugadores, j, ref colision);
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
        //Método PonerTrampa por implementar
        /*public void BloquearPuertas(string letra, Tablero tablero) -> movido a Dado Eventos
        {
            eventos.ForEach(h =>
            {
                if(h is Habitacion && ((Habitacion)h).LetraAsociada == letra)
                {
                    ((Habitacion)h).Puertas.ForEach(p => p.BloquearDesbloquear(letra));
                    tablero.GestionPuertas(letra);
                }
            });
        }*/
        protected bool Resolver()
        {
            string[] cuadro = CargaFichero.Cargar("cuadro.txt");
            List<Pista> pistas = CargarPistas();
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

            Console.SetCursorPosition(0, indice);
            Console.CursorVisible = false;

            while (!salir)
            {
                Console.ResetColor();
                DibujarCuadro();
                Console.SetCursorPosition(Console.WindowWidth / 4 + 1, 8);
                Console.WriteLine($"Elige {tipo}: ");

                switch(tipo)
                {
                    case "sospechoso":
                        aux = pistas.Where(s => s is Sospechoso).ToList();
                        break;
                    case "arma":
                        aux = pistas.Where(a => a is Arma).ToList();
                        break;
                    case "habitacion":
                        aux = CargarHabitaciones();
                        break;
                }

                Mostrar(aux, indice);
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (!Console.KeyAvailable)
                {
                    if (tecla.Key == ConsoleKey.UpArrow && indice > 0)
                        indice--;
                    if (tecla.Key == ConsoleKey.DownArrow && indice < 6)
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
                Console.SetCursorPosition(Console.WindowWidth / 4, 7 + i);
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

                Console.SetCursorPosition(Console.WindowWidth / 4 + 1, 8 + i + 1);
                Console.WriteLine(lista[i]);
            }
        }
        private List<Pista> CargarHabitaciones()
        {
            List<Pista> habs = new List<Pista>();
            habs.Add(new Habitacion("Cocina", "K"));
            habs.Add(new Habitacion("Comedor", "C"));
            habs.Add(new Habitacion("Sala de estar", "S"));
            habs.Add(new Habitacion("Habitación de invitados", "I"));
            habs.Add(new Habitacion("Habitación principal", "H"));
            habs.Add(new Habitacion("Baño", "B"));
            habs.Add(new Habitacion("Estudio", "E"));

            return habs;
        }
    }
}
