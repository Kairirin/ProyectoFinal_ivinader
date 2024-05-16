namespace ProyectoFinal_ivinader
{
    internal class Jugador
    {
        private const int INICIO_NOMBRE_X = 65;
        private const int INICIO_NOMBRE_Y = 1;
        private string nombre;
        private Sprite icono;
        private int turno;
        private bool activo;
        private List<Pista> listaPistas;
        private List<Objeto> listaObjetos;

        public Jugador(string nombre, string color, int turno)
        {
            this.nombre = nombre;
            icono = new Sprite(color);
            this.turno = turno;
            if (turno == 1)
                activo = true;
            else
                activo = false;
            listaPistas = new List<Pista>();
            listaObjetos = new List<Objeto>();
        }
        public void AddPista(Pista p)
        {
            listaPistas.Add(p);
        }
        public void AddObjeto(Objeto obj)
        {
            listaObjetos.Add(obj);  
        }
        public string Nombre { get => nombre; set => nombre = value; }
        public int Turno { get => turno; set => turno = value; }
        public bool Activo { get => activo; set => activo = value; }
        public List<Pista> ListaPistas { get => listaPistas; set => listaPistas = value; }
        public List<Objeto> ListaObjetos { get => listaObjetos; set => listaObjetos = value; }
        public Sprite Icono { get => icono; set => icono = value; }

        public void MostrarPistas(int x, int y)
        {
            Console.ResetColor();
            if (listaPistas.Count > 0)
            {
                for(int i = 0; i < listaPistas.Count; i++)
                {
                    Console.SetCursorPosition(x, y + i);
                    Console.WriteLine(listaPistas[i]);
                }
            }
            else
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine("Sin pistas");
            }
        }
        public void MostrarObjetos(int x, int y)
        {
            Console.ResetColor();
            if (listaObjetos.Count > 0)
            {
                for(int i = 0; i <listaObjetos.Count; i++)
                {
                    Console.SetCursorPosition(x, y + i);
                    Console.WriteLine(listaObjetos[i]);
                }
            }
            else
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine("Sin objetos");
            }
        }

        public override string ToString()
        {
            return nombre;
        }
        public void MostrarDatos()
        {
            Console.ResetColor();
            Console.SetCursorPosition(INICIO_NOMBRE_X, INICIO_NOMBRE_Y);
            Console.WriteLine(this);
        }
        public void MostrarDatos(int pasos)
        {
            Console.ResetColor();
            Console.SetCursorPosition(INICIO_NOMBRE_X, INICIO_NOMBRE_Y);
            Console.WriteLine(this);
            Console.SetCursorPosition(INICIO_NOMBRE_X - 15, INICIO_NOMBRE_Y + 1);
            Console.WriteLine($"Pasos restantes: {pasos}");
        }
        public override bool Equals(object? obj)
        {
            return obj is Jugador jugador &&
                   nombre == jugador.nombre;
        }
    }
}
