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
        public List<ObjetoCamino> listaRecorrido;
        public void inicio()
        {
            Grafo = new List<Nodo>();
            AristasGlobales = new List<Arista>();
            Heuristica = new Dictionary<string, int>();
            listaRecorrido = new List<ObjetoCamino>();
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

            MeteVecinos(BuscaNodo("Cartago"), BuscaNodo("TEC")); /* Metodo prueba*/

            char i = Console.ReadKey().KeyChar;
        }

        public void ReadFile()
        {
            string[] lines = System.IO.File.ReadAllLines(@"E:\IA\IsaacChaverri\IA\Prueba1.txt");
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

        public void MeteVecinos(Nodo nodoInicio, Nodo nodoFinal)
        {
            ObjetoCamino temp = null;
            ObjetoCamino objetoPivote = null;
            if (listaRecorrido.Count == 0)
            {
                Console.WriteLine(listaRecorrido.Count);
                foreach (Arista arista in nodoInicio.Aristas)
                {
                    temp = new ObjetoCamino();
                    temp.nombre = arista.destino.Nombre;
                    temp.value = arista.peso + Heuristica[arista.destino.Nombre];
                    temp.ruta.Add(arista.partida.Nombre);
                    listaRecorrido.Add(temp);
                }
            }
            else
            {
                
                foreach (ObjetoCamino obj in listaRecorrido)
                {
                    if (obj.nombre.Equals(nodoInicio.Nombre))
                    {
                        objetoPivote = obj;
                    }
                }
                listaRecorrido.Remove(objetoPivote);
                foreach (Arista arista in nodoInicio.Aristas)
                {
                    temp = new ObjetoCamino();
                    temp.nombre = arista.destino.Nombre;
                    temp.value = arista.peso + Heuristica[arista.destino.Nombre] + objetoPivote.value;
                    temp.ruta = objetoPivote.ruta;
                    temp.ruta.Add(arista.partida.Nombre);
                    listaRecorrido.Add(temp);
                }
            }
            int pivote = int.MaxValue;
            foreach (ObjetoCamino obj in listaRecorrido)
            {
                if (objetoPivote != null)
                {
                    if (obj.value <= pivote && objetoPivote.nombre.Equals(obj.ruta[obj.ruta.Count - 1]))
                    {
                        pivote = obj.value;
                        temp = obj;
                    }
                }
                else
                {
                    if (obj.value <= pivote)
                    {
                        pivote = obj.value;
                        temp = obj;
                    }
                }
            }

            Console.WriteLine("Temporal: " + temp.nombre + " Final: " + nodoFinal.Nombre + "\n");
            if (!temp.nombre.Equals(nodoFinal.Nombre))
            {
                Nodo tempo = BuscaNodo(temp.nombre);
                
                MeteVecinos(tempo, nodoFinal);
                Console.WriteLine(tempo.Nombre);
            }
        }
    }

    class ObjetoCamino
    {
        public String nombre { set; get; }
        public int value { set; get; }
        public List<String> ruta = new List<String>();
    }
}