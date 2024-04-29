using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinal_ivinader
{
    internal class Puerta
    {
        private Habitacion estancia;
        private bool bloqueada;
        public Puerta(Habitacion estancia, bool bloqueada)
        {
            this.estancia = estancia;
            this.bloqueada = bloqueada;
        }
        public bool Bloqueada { get => bloqueada; set => bloqueada = value; }
        public Habitacion Estancia { get => estancia; set => estancia = value; }
        public void BloquearDesbloquear()
        {
            if(bloqueada)
            {
                bloqueada = false;
            }
            else
            {
                bloqueada = true;
            }
        }
    }
}
