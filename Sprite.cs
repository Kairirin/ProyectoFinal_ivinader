using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Sprite: IEsMovible
    {
        private int x;
        private int y;
        private string color;
        //private imagen???? Asociar aquí a las imágenes
        public Sprite(string color)
        {
            x = 0; //Cambiar por posición de inicio cuando se pueda calcular
            y = 0;
            this.color = color;
        }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public string Color { get => color; set => color = value; }

        public void Mover(int x, int y)
        {
            this.x += x;
            this.y += y;
        }
    }
}
