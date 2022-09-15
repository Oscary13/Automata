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
        //Variable para nuestro arrchivo
        FileStream archivo;
        //Variable para leer nuestro archivo
        StreamReader leer;

        //Variable de estados de nuesta quintupla eje q1,12,q3,...
        private string[] estados;
        //Variable del alfabeto de nuesta quintupla eje 0,1
        private string[] alfabeto;
        //Variable de estado inicial de nuestro automata ejm q0 solo ponemos el 0
        private int estadoInicial;
        //Variable de estados finales de nuesta quintupla puede tener uno o varios solo se pone el numero eje q2 = 2
        private string[] estadosFinales;
        //Variable de tabla de tranciciones es un arreglo bidimencional
        private int[,] tablaTranciciones;

        public Form1()
        {
            InitializeComponent();
        }

        //Se ejecuta cuando damos clic en Abrir archivo en el menuStrip
        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Creamos un DialogResult pra despues 
            DialogResult resultado;
            //Damos la propiedad de titulo a la ventana que nos dara la ruta de nuestro archivo
            openfiledialogArchivoTxt.Title = "Seleciona un automata";
            //Damos la propiedad de tipo de archivo a la ventana que nos dara la ruta de nuestro archivo
            openfiledialogArchivoTxt.Filter = "Archivo atm|*.atm";
            //Se abre la ventana
            resultado = openfiledialogArchivoTxt.ShowDialog();

            //Condicional que si lee la ruta del archivo
            if (resultado == DialogResult.OK)
            {
                //Constructor de el archivo y le damos la ruta, acceso al archivo, y poder leerlo
                archivo = new FileStream(openfiledialogArchivoTxt.FileName, FileMode.Open, FileAccess.Read);
                //Constructor para leer el archivo y acceder a el
                leer = new StreamReader(archivo);
                //igualamos los estados a lo que exista en la primer linea y los separe con una (",") con => Split();
                estados = leer.ReadLine().Split(',');
                //igualamos el alfabeto a lo que exista en la segunda linea y los separe con una (",") con => Split();
                alfabeto = leer.ReadLine().Split(',');
                //igualamos el estado Inicial a lo que exista en la tercera linea y los separe con una (",") con => Split();
                estadoInicial = int.Parse(leer.ReadLine());
                //igualamos los estados finales a lo que exista en la cuarta linea y los separe con una (",") con => Split();
                estadosFinales = leer.ReadLine().Split(',');
                //Leemos una linea por que en nuestro archivo esta vacia y no encontrara nada
                leer.ReadLine();
                //Tabla transiciones estara conformada [numero de estados, numero de digitos en el alfabeto]
                tablaTranciciones = new int[estados.Length, alfabeto.Length];

                for (int i = 0; i < estados.Length; i++)
                {
                    //Variable de tranciciones sera igual a lo que encuentre en la siguiente linea que llea del archivo cada valor se separara po un TAB ('\t')
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
