namespace ProyectoFinal_ivinader
{
    public class Tablero
    {
        private const int X1_TABLERO = 11;
        private const int XN_TABLERO = 39;
        private const int Y1_TABLERO = 1;
        private const int YN_TABLERO = 14;

        private string[] tablero;
        public Tablero()
        {
            tablero = CargaFichero.Cargar("tableroJuego.txt");
        }

        public static int X1_TABLERO1 => X1_TABLERO;

        public static int XN_TABLERO1 => XN_TABLERO;

        public static int Y1_TABLERO1 => Y1_TABLERO;

        public static int YN_TABLERO1 => YN_TABLERO;


        public void MostrarTablero()
        {
            Console.ResetColor();

            for (int i = 0; i < tablero.Length; i++)
            {
                Console.WriteLine(tablero[i]);
            }
        }
        public void GestionPuertas(string letra)
        {
            string aux1, aux2, resultado;

            for (int i = Y1_TABLERO; i < YN_TABLERO; i++)
            {
                for(int j = X1_TABLERO; j < XN_TABLERO; j++)
                {
                    if (tablero[i][j] == 'P' && (tablero[i - 1][j] == Convert.ToChar(letra) || tablero[i + 1][j] == Convert.ToChar(letra) || tablero[i][j+1] == Convert.ToChar(letra) || tablero[i][j-1] == Convert.ToChar(letra)))
                    {
                        aux1 = tablero[i].Substring(0, j);
                        aux2 = tablero[i].Substring(j + 1);
                        resultado = aux1 + 'p' + aux2;
                        tablero[i] = resultado;
                    }
                    else
                    {
                        if (tablero[i][j] == 'p' && (tablero[i - 1][j] == Convert.ToChar(letra) || tablero[i + 1][j] == Convert.ToChar(letra) || tablero[i][j + 1] == Convert.ToChar(letra) || tablero[i][j - 1] == Convert.ToChar(letra)))
                        {
                            aux1 = tablero[i].Substring(0, j);
                            aux2 = tablero[i].Substring(j + 1);
                            resultado = aux1 + 'P' + aux2;
                            tablero[i] = resultado;
                        }
                    }
                }
            }
        }
        public void GestionPuertas(Posicion pos)
        {
            string aux1, aux2, resultado;

            aux1 = tablero[pos.Y].Substring(0, pos.X);
            aux2 = tablero[pos.Y].Substring(pos.X + 1);
            resultado = aux1 + 'p' + aux2;
            tablero[pos.Y] = resultado;
        }
        public bool ComprobarPuerta(Posicion pos)
        {
            return tablero[pos.Y][pos.X] == 'P';
        }
        public bool ComprobarEspacio(Posicion pos)
        {
            return tablero[pos.Y][pos.X] != '1' && tablero[pos.Y][pos.X] != 'P';
        }
        public bool EstaEnHabitacion(Posicion pos)
        {
            return tablero[pos.Y][pos.X] != '1' && tablero[pos.Y][pos.X] != 'P' && tablero[pos.Y][pos.X] != 'p' && tablero[pos.Y][pos.X] != ' ' && tablero[pos.Y][pos.X] != 'R';
        }
        public string DevolverLetra(Posicion pos)
        {
            return tablero[pos.Y][pos.X].ToString();
        }
    }
}
