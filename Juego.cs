using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Juego
    {
        private List<Jugador> jugadores;
        private Tablero tablero;
        private Dado[] dados;
        private Solucion solucionCaso;
        private List<Pista> pistas;
        private List<IEsEvento> eventos;
        private Random r;

        public Juego(List<Jugador> jugadores)
        {
            this.jugadores = jugadores;
            tablero = new Tablero();
            r = new Random();
            pistas = new List<Pista>();
            CargarPistas();
            eventos = new List<IEsEvento>();
            CargarEventos();
            solucionCaso = CargarSolucion();

            if (jugadores.Count > 1)
            {
                dados = new Dado[2];
                dados[0] = new DadoMovimiento();
                dados[1] = new DadoEvento();
            }
            else
            {
                dados = new Dado[1];
                dados[1] = new DadoEvento();
            }
        }
        private void CargarPistas()
        {
            //Leer conjunto total de pistas de archivo de persistencia de objetos
        }
        private void CargarEventos()
        {
            //Leer conjunto total de eventos de archivo de persistencia de objetos
        }
        private Solucion CargarSolucion()
        {
            //Sacar uno de cada tipo de la lista de pistas, borrarlo y devolver Solucion construida
        }
        public List<Jugador> GetJugadores()
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
        public List<IEsEvento> GetEventos()
        {
            return eventos;
        }
        public void JugarTurno(Jugador j)
        {
            //Implementar juego por turnos
        }
        public void DarObjeto(Jugador j)
        {
            List<IEsEvento> objetos = eventos.FindAll(o => o is Objeto);

            j.ListaObjetos.Add((Objeto)objetos[r.Next(1, objetos.Count)]);
        }
        public void DarPista(Jugador j)
        {
            bool salir = false;

            while (!salir)
            {
                Pista pistaProvisional = pistas[r.Next(1, pistas.Count)];

                if (!j.ListaPistas.Contains(pistaProvisional))
                {
                    j.ListaPistas.Add(pistaProvisional);
                    salir = true;
                }
            }
        }
        //Método PonerTrampa por implementar
        public void BloquearPuertas(Habitacion hab)
        {
            hab.Puertas.ForEach(p => p.BloquearDesbloquear());
        }
        public void Resolver(Solucion sol)
        {
            //Implementar
        }
    }
}
