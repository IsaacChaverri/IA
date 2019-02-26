using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GrafoAEstrella
{
    class CArco
    {
        public Cvertice nDestino;
        public int peso;

        public float grosor_flecha;
        public Color color;

        //Metodos
        public CArco(Cvertice destino) : this(destino, 1)
        {
            this.nDestino = destino;
        }

        public CArco(Cvertice destino,int peso)
        {
            this.nDestino = destino;
            this.peso = peso;
            this.grosor_flecha = 2;
            this.color = Color.Gray;
        }
    }
}
