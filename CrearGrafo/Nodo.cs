using System;
using System.Collections.Generic;
using System.Text;

namespace CrearGrafo
{
    class Nodo
    {
        public string Nombre { set; get; }
        public List<Arista> Aristas { set; get; } 

        public Nodo(String nombre)
        {
            this.Nombre = nombre;
            Aristas = new List<Arista>();
        }

        public void AgregaArista(Arista arista)
        {
            Aristas.Add(arista);
        }
    }
}
