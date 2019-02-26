using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafoAEstrella
{
    public partial class Simulador : Form
    {
        private CGrafo grafo;
        private Cvertice nuevoNodo;
        private Cvertice NodoOrigen;
        private Cvertice NodoDestino;
        private int var_control = 0;
        LlenarGrafo ListaNodos = new LlenarGrafo();
        List<String> listaRecorrido;
        bool flag = false;

        private Vertice Vertice;
        public Simulador(String path, String stringInicio, String stringFinal)
        {
            InitializeComponent();
            grafo = new CGrafo();
            nuevoNodo = null;
            var_control = 0;
            ListaNodos.inicio();
            ListaNodos.ReadFile(path);
            String inicio = stringInicio;
            String final = stringFinal;
            Vertice = new Vertice();
            listaRecorrido = ListaNodos.AEstrella(ListaNodos.BuscaNodo(stringInicio), ListaNodos.BuscaNodo(stringFinal));
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
        }

        private Cvertice BuscaNodo(string arista)
        {
            foreach (Cvertice vertice  in grafo.nodos)
            {
                if(vertice.Valor == arista)
                {
                    return vertice;
                }
            }
            return null;
        }

        private void EscogeCamino(String camino)
        {
            foreach(Cvertice vertice in grafo.nodos)
            {
                if(vertice.Valor== camino)
                {
                    vertice.Color = Color.Yellow;
                }
            }
            Pizarra.Refresh();
        }

        private void Pizarra_Paint(object senser, PaintEventArgs e)
        {
            try
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
                grafo.DibujarGrafo(e.Graphics);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void nuevoVerticeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int posX = 200;
            
            int offsetY = Pizarra.Height / 5;
            int posY = 0;
            for (int i = 0; i<ListaNodos.Grafo.Count;i++)
            {
                nuevoNodo = new Cvertice();
                nuevoNodo.Valor = ListaNodos.Grafo[i].Nombre;
                grafo.AgregarVertice(nuevoNodo);
                if (i == 0)
                {
                    nuevoNodo.Posicion = new Point(30, Pizarra.Height/2);
                }
                else
                {
                    posY += offsetY;
                    if (posY >= Pizarra.Height-offsetY)
                    {
                        posX += 120;
                        posY = offsetY;
                    }
                    nuevoNodo.Posicion = new Point(posX, posY);
                }
                Pizarra.Refresh();
                nuevoNodo.DibujarVertice(Pizarra.CreateGraphics());
            }

            foreach(Nodo nodo in ListaNodos.Grafo)
            {
                foreach(Arista arista in nodo.Aristas)
                {
                    NodoOrigen = BuscaNodo(arista.partida.Nombre);
                    NodoDestino = BuscaNodo(arista.destino.Nombre);
                    if (grafo.AgregarArco(NodoOrigen, NodoDestino))
                    {
                        int distancia = arista.peso;
                        NodoOrigen.ListaAdyacencia.Find(v => v.nDestino == NodoDestino).peso = distancia;
                    }
                }
            }
            flag = true;
            Pizarra.Refresh();
        }
        

        private void Pizarra_MouseDown(object sender,MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && flag)
            {
                if (var_control < listaRecorrido.Count)
                    EscogeCamino(listaRecorrido[var_control]);
                var_control += 1;
            }

            if (e.Button == MouseButtons.Right)
            {
                Pizarra.ContextMenuStrip = this.CMSCrearVertice;
            }            
        }        
    }
}
