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

namespace Ejemplo2
{
    public partial class Form1 : Form
    {
        private void obtenerArchivo()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Text|*.txt|All|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtArchivo.Text = openFileDialog.FileName;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            bool bContinuar = true;
            if (File.Exists(txtArchivo.Text))
            {
                try
                {
                    int iContador = 0, iLineaMaxima = 0, iContador1 = 0, iContador2 = 0;
                    using (StreamReader ReaderObject = new StreamReader(txtArchivo.Text))
                    {
                        string linea = "";
                        while ((linea = ReaderObject.ReadLine()) != null)
                        {
                            if (iContador == 0)
                            {
                                iLineaMaxima = int.Parse(linea);
                                if (iLineaMaxima > 10000)
                                {
                                    MessageBox.Show("ERROR: Ha superado el máximo de rondas.");
                                    iContador = iLineaMaxima + 1;
                                    bContinuar = false;
                                    break;
                                }
                                else if(iLineaMaxima <= 0)
                                {
                                    MessageBox.Show("ERROR: Número de rondas incorrecto.");
                                    iContador = iLineaMaxima + 1;
                                    bContinuar = false;
                                    break;
                                }
                            }
                            else
                            {
                                iContador1 += int.Parse(linea.Split(' ')[0]);
                                iContador2 += int.Parse(linea.Split(' ')[1]);
                            }
                            iContador++;
                            if (iLineaMaxima == iContador - 1)
                            {
                                break;
                            }
                        }
                    }
                    if (iLineaMaxima != iContador - 1)
                    {
                        MessageBox.Show("ERROR: No cuenta con el número de lineas indicado");
                    }
                    if(bContinuar)
                    {
                        string sResultado = "C://Resultado.txt";
                        StreamWriter sw = new StreamWriter(sResultado);
                        if (iContador1 > iContador2)
                        {
                            sw.WriteLine("1 " + (iContador1 - iContador2));
                            MessageBox.Show("Se ha generado el archivo en la ruta: " + sResultado);
                        }
                        else if (iContador2 > iContador1)
                        {
                            sw.WriteLine("1 " + (iContador2 - iContador1));
                            MessageBox.Show("Se ha generado el archivo en la ruta: " + sResultado);
                        }
                        else
                        {
                            MessageBox.Show("ERROR: Ha ocurrido un empate.");
                        }
                        sw.Close();
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("ERROR: Formato dentro de las lineas incorrecto.");
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("ERROR: Estructura incorrecta dentro de las lineas.");
                }
            }
            else
            {
                MessageBox.Show("ERROR: No existe el archivo seleccionado");
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            obtenerArchivo();
        }
    }
}