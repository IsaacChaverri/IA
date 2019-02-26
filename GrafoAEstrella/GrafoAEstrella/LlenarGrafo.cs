using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GrafoAEstrella
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
            //ReadFile();
            //ImprimirGrafo(Grafo);
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



            AEstrella(BuscaNodo("Paraiso"), BuscaNodo("TEC")); /* Metodo prueba*/

            char i = Console.ReadKey().KeyChar;
        }

        public void ReadFile(String path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] linea = line.Split(new Char[] { ',', ':', ' ' });
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
        //----------------------------------------------------------------------------------------------//        
        public List<String> AEstrella(Nodo nodoInicio, Nodo nodoFinal)
        {
            ObjetoCamino objetoPivote = null;
            Console.WriteLine(listaRecorrido.Count);
            MeteVecinosOrig(nodoInicio);

            while (true)
            {
                objetoPivote = listaRecorrido[0];
                foreach (ObjetoCamino obj in listaRecorrido)
                {
                    if (obj.value < objetoPivote.value)
                    {
                        objetoPivote = obj;                        
                    }    
                }
                if (objetoPivote.nombre == nodoFinal.Nombre)
                {
                    objetoPivote.ruta.Add(nodoFinal.Nombre);
                    foreach (string nodo in objetoPivote.ruta)
                        Console.Write(nodo + "-");
                    return objetoPivote.ruta;
                }
                listaRecorrido.Remove(objetoPivote);
                MeteVecinosEnLista(objetoPivote);
            } 
        }



        public void MeteVecinosOrig(Nodo nodo)
        {
            ObjetoCamino temp = null;
            foreach (Arista arista in nodo.Aristas)
            {
                temp = new ObjetoCamino();
                temp.nombre = arista.destino.Nombre;
                temp.value = arista.peso + Heuristica[arista.destino.Nombre];
                temp.ruta.Add(arista.partida.Nombre);
                listaRecorrido.Add(temp);
            }
        }

        public void MeteVecinosEnLista(ObjetoCamino ObjCamino)
        {
            ObjetoCamino temp = null;
            foreach (Arista arista in BuscaNodo(ObjCamino.nombre).Aristas)
            {
                temp = new ObjetoCamino();
                temp.nombre = arista.destino.Nombre;
                temp.value = arista.peso + Heuristica[arista.destino.Nombre] + ObjCamino.value;
                temp.ruta.AddRange(ObjCamino.ruta);
                temp.ruta.Add(arista.partida.Nombre);
                listaRecorrido.Add(temp);
            }
        }
    }
}