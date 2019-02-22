using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CrearGrafo
{
    class LlenarGrafo
    {
        public List<Nodo> Grafo { set; get; }
        public List<Arista> AristasGlobales { set; get; }
        public Dictionary<string, int> Heuristica { set; get; }
        public void inicio()
        {
            Grafo = new List<Nodo>();
            AristasGlobales = new List<Arista>();
            Heuristica = new Dictionary<string, int>();
            ReadFile();          
            ImprimirGrafo(Grafo);
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
            foreach (KeyValuePair<string, int> Item in Heuristica)
                Console.WriteLine(Item.Key + ":" + Heuristica[Item.Key]);
            char i = Console.ReadKey().KeyChar;
        }

        public void ReadFile()
        {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\Isaac\Desktop\Cosas-De-La-U\Semestre XIII\Inteligencia Artificial\Tareas\Tareas 3 y 4\IA\Prueba1.txt");
            foreach (string line in lines)
            {
                string[] linea = line.Split(new Char[] { ',',':', ' ' });
                switch (linea.Length)
                {
                    case 1:
                        Grafo.Add(new Nodo(linea[0]));
                        break;
                    case 2:
                        Heuristica.Add(linea[0], Convert.ToInt32(linea[1]));
                        break;
                    case 3:
                        AristasGlobales.Add(new Arista(Convert.ToInt32(linea[0]), BuscaNodo(linea[1]), BuscaNodo(linea[2])));
                        break;
                }
            }
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
    }
}