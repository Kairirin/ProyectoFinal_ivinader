using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    public class Posicion //Añadirlo como valor a Sprite. Sobrecargar operador + y - si funciona
    {
        private int x;
        private int y;
        public Posicion(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
    }
}
