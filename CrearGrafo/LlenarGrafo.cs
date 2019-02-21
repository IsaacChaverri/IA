using System;
using System.Collections.Generic;
using System.Text;

namespace CrearGrafo
{
    class LlenarGrafo
    {
        public List<Nodo> Grafo { set; get; }
        public List<Arista> AristasGlobales { set; get; }
        public void inicio()
        {
            Grafo = new List<Nodo>();
            AristasGlobales = new List<Arista>();
            creaGrafo();
            ImprimirGrafo(Grafo);
        }
        public void creaGrafo()
        {
            for (int i = 0; i < 5; i++)
            {
                Grafo.Add(new Nodo(Convert.ToString(i + 1)));
            }
            creaAristas();
            foreach (Arista arista in AristasGlobales)
            {
                foreach (Nodo nodo in Grafo)
                {
                    if (arista.partida.Equals(nodo))
                    {
                        nodo.AgregaArista(arista);
                    }
                }
            }

        }

        public void creaAristas()
        {
            AristasGlobales.Add(new Arista(7, BuscaNodo("2"), BuscaNodo("4")));
            AristasGlobales.Add(new Arista(15, BuscaNodo("1"), BuscaNodo("2")));
            AristasGlobales.Add(new Arista(6, BuscaNodo("3"), BuscaNodo("2")));
            AristasGlobales.Add(new Arista(4, BuscaNodo("5"), BuscaNodo("4")));
            AristasGlobales.Add(new Arista(20, BuscaNodo("4"), BuscaNodo("3")));
            AristasGlobales.Add(new Arista(10, BuscaNodo("1"), BuscaNodo("5")));
        }

        public Nodo BuscaNodo(String nombre)
        {

            foreach (Nodo nodo in Grafo)
            {
                if (nodo.Nombre == nombre)
                {
                    return nodo;
                }
            }
            return null;

        }
        public void ImprimirGrafo(List<Nodo> Grafo)
        {
            foreach (Nodo nodo in Grafo)
            {
                Console.WriteLine(nodo.Nombre + "\n");
                foreach (Arista arista in nodo.Aristas)
                {
                    Console.WriteLine("Peso: " + arista.peso + "\n");
                    Console.WriteLine("Partido: " + arista.partida.Nombre + "\n");
                    Console.WriteLine("Destino: " + arista.destino.Nombre + "\n");
                }
            }
            char i = Console.ReadKey().KeyChar;
        }
    }
}