using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace automata0
{
    public partial class Form1 : Form
    {
        FileStream archivo;
        StreamReader leer;
        private string[] estados;
        private string[] alfabeto;
        private int estadoInicial;
        private string[] estadosFinales;
        private int[,] tablaTranciciones;

        public Form1()
        {
            InitializeComponent();
        }

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult resultado;
            openfiledialogArchivoTxt.Title = "Seleciona un automata";
            openfiledialogArchivoTxt.Filter = "Archivo atm|*.atm";
            resultado = openfiledialogArchivoTxt.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                archivo = new FileStream(openfiledialogArchivoTxt.FileName, FileMode.Open, FileAccess.Read);
                leer = new StreamReader(archivo);
                estados = leer.ReadLine().Split(',');
                alfabeto = leer.ReadLine().Split(',');
                estadoInicial = int.Parse(leer.ReadLine());
                estadosFinales = leer.ReadLine().Split(',');

                leer.ReadLine();

                tablaTranciciones = new int[estados.Length, alfabeto.Length];
                for (int i = 0; i < estados.Length; i++)
                {
                    String[] transiciones = leer.ReadLine().Split('\t');
                    for (int j = 0; j < alfabeto.Length; j++)
                    {
                        tablaTranciciones[i, j] = int.Parse(transiciones[j]);
                    }
                }
            }
        }

        private void btnValidar_Click(object sender, EventArgs e)
        {
            int estadoActual = estadoInicial;
            foreach (char c in txtCadena.Text)
            {
                estadoActual = tablaTranciciones[estadoActual, int.Parse(c.ToString())];
            }
            if (int.Parse(estadosFinales[0]) == estadoActual)
            {
                MessageBox.Show("CADENA VALIDA");
            }
            else
            {
                MessageBox.Show("CADENA NO VALIDA");
            }

        }
    }
}
