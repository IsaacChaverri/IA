using System;
using System.Collections.Generic;
using System.Text;

namespace CrearGrafo
{
    class Arista
    {
        public int peso { get; set; }
        public Nodo partida { get; set; }
        public Nodo destino { get; set; }

        public Arista(int peso, Nodo partida, Nodo destino)
        {
            this.peso = peso;
            this.partida = partida;
            this.destino = destino;
        }
    }
}
