using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafoAEstrella
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                string filePath = openFileDialog1.FileName;
                textBox1.Text = filePath;

                LlenarGrafo grafo = new LlenarGrafo();
                grafo.inicio();
                grafo.ReadFile(filePath);
                foreach(Nodo nodo in grafo.Grafo)
                {
                    listBox1.Items.Add(nodo.Nombre);
                    listBox2.Items.Add(nodo.Nombre);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sim = new Simulador(openFileDialog1.FileName, listBox1.GetItemText(listBox1.SelectedItem), listBox2.GetItemText(listBox2.SelectedItem));
            sim.Closed += (s, args) => this.Close();
            sim.Show();
            this.Hide();
        }
    }
}
