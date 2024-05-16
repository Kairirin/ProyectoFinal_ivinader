namespace ProyectoFinal_ivinader
{
    internal interface IEsMovible
    {
        void Mover(Tablero t, List<Jugador> jugadores, Jugador j, ref bool ocupada);
    }
}
