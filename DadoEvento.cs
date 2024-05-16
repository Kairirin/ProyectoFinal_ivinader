namespace ProyectoFinal_ivinader
{
    internal class DadoEvento: Dado
    {
        private const int INICIO_LINEA_ESCRITURA = 8;
        private List<IEsEvento> eventos;
        public DadoEvento()
        {
            eventos = new List<IEsEvento> ();   
            eventos.AddRange(CargaFichero.CargarObjetos("eventos.xml"));
            eventos.AddRange(CargaFichero.CargarTrampas("trampas.xml"));
            eventos.AddRange(CargaFichero.CargarHabitaciones());
        }
        public DadoEvento(List<IEsEvento> eventos)
        {
        this.eventos = eventos;
        }
        public override short Lanzar()
        {
            return (short)r.Next(1, eventos.Count);
        }
        public void ObtenerEvento(Jugador j, ref int pasos, Tablero tablero)
        {
            IEsEvento eventoElegido = eventos[Lanzar()];

            switch (eventoElegido.GetType().Name)
            {
                case "Objeto":
                    int probabilidad = r.Next(1, 50);
                    if (probabilidad % 2 == 0)
                        DarObjeto(j);
                    break;
                case "Habitacion":
                    string letra = ((Habitacion)eventoElegido).LetraAsociada;
                    BloquearPuertas(letra, tablero);
                    break;
                case "Trampa":
                    PonerTrampa(j, ref pasos, tablero);
                    break;
            }
        }
        public void DarObjeto(Jugador j)
        {
            List<IEsEvento> objetos = eventos.FindAll(o => o is Objeto);

            j.ListaObjetos.Add((Objeto)objetos[r.Next(0, objetos.Count)]);
        }
        public void BloquearPuertas(string letra, Tablero tablero)
        {
            eventos.ForEach(h =>
            {
                if (h is Habitacion && ((Habitacion)h).LetraAsociada == letra)
                {
                    ((Habitacion)h).Puertas.ForEach(p => p.BloquearDesbloquear(letra));
                    tablero.GestionPuertas(letra);
                }
            });
        }
        public void PonerTrampa(Jugador j, ref int pasos, Tablero tab)
        {
            List<IEsEvento> trampas = eventos.FindAll(t => t is Trampa);
            Trampa trampa = (Trampa)trampas[r.Next(0, trampas.Count - 1)];

            DibujarCuadro();

            switch (trampa.Nombre)
            {
                case "Familiar lejano":
                    Console.SetCursorPosition(Console.WindowWidth / 4 + 1, INICIO_LINEA_ESCRITURA);
                    Console.WriteLine($"{trampa}");
                    Console.ReadLine();
                    if (j.ListaObjetos.Contains(new Objeto("Aspirina", "")))
                    {
                        j.ListaObjetos.Remove(new Objeto("Aspirina", ""));
                    }
                    else
                    {
                        Posicion actual = j.Icono.PosicionSprite;
                        while(actual == j.Icono.PosicionSprite)
                        {
                            Posicion posAleatoria = new Posicion(0, r.Next(0, 4));
                            if (tab.ComprobarEspacio(j.Icono.PosicionSprite - posAleatoria))
                            {
                                j.Icono.PosicionSprite -= posAleatoria;
                            }
                        }
                        pasos = 0;
                    }
                    break;
                case "Madre desconsolada":
                    Console.SetCursorPosition(Console.WindowWidth / 4 + 1, INICIO_LINEA_ESCRITURA);
                    Console.WriteLine($"{trampa}");
                    Console.ReadLine();
                    if (j.ListaObjetos.Contains(new Objeto("Fotografía", "")))
                    {
                        j.ListaObjetos.Remove(new Objeto("Fotografía", ""));
                    }
                    else
                    {
                        pasos = 0;
                    }
                    break;
            }
        }
        private void DibujarCuadro()
        {
            string[] cuadro = CargaFichero.Cargar("cuadro.txt");

            for (int i = 0; i < cuadro.Length; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 4, (INICIO_LINEA_ESCRITURA - 1) + i);
                Console.WriteLine(cuadro[i]);

            }
        }
    }
}
