using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejemplo1
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
            if (File.Exists(txtArchivo.Text))
            {
                try
                {
                    int iContador = 0, iCaracteresM1 = 0, iCaracteresM2 = 0, iCaracteresN = 0;
                    using (StreamReader ReaderObject = new StreamReader(txtArchivo.Text))
                    {
                        string linea = "", sPrimerInstruccion = "", sSegundaInstruccion = "", sTexto = "", sResultado = "C://Resultado.txt";
                        bool bInstruccion = false;
                        while ((linea = ReaderObject.ReadLine()) != null)
                        {
                            iContador++;
                            switch (iContador)
                            {
                                case 1:
                                    iCaracteresM1 += int.Parse(linea.Split(' ')[0]);
                                    iCaracteresM2 += int.Parse(linea.Split(' ')[1]);
                                    iCaracteresN += int.Parse(linea.Split(' ')[2]);
                                    if ((iCaracteresM1 <= 2 || iCaracteresM1 >= 50) || (iCaracteresM2 <= 2 || iCaracteresM2 >= 50))
                                    {
                                        MessageBox.Show("ERROR: Número de caracteres invalido en M1 o M2.");
                                        return;
                                    }
                                    else if (iCaracteresN <= 3 || iCaracteresN >= 5000)
                                    {
                                        MessageBox.Show("ERROR: Número de caracteres invalido en N.");
                                        return;
                                    }
                                    break;
                                case 2:
                                    if(linea.Length != iCaracteresM1)
                                    {
                                        MessageBox.Show("ERROR: No coincide la longitud con el dato de M1.");
                                        return;
                                    }
                                    else
                                    {
                                        sPrimerInstruccion = linea;
                                    }
                                    break;
                                case 3:
                                    if (linea.Length != iCaracteresM2)
                                    {
                                        MessageBox.Show("ERROR: No coincide la longitud con el dato de M2.");
                                        return;
                                    }
                                    else
                                    {
                                        sSegundaInstruccion = linea;
                                    }
                                    break;
                                case 4:
                                    if (linea.Length != iCaracteresN)
                                    {
                                        MessageBox.Show("ERROR: No coincide la longitud con el dato de M2.");
                                        return;
                                    }
                                    else
                                    {
                                        string sValidacion = Regex.Replace(linea, "[A-Za-z0-9]", "");
                                        if (Regex.IsMatch(sValidacion, "[A-Za-z0-9]") || sValidacion.Trim() == "")
                                        {
                                            sTexto = Regex.Replace(linea, @"(.)\1{1,}", "$1");
                                            StreamWriter sw = new StreamWriter(sResultado);
                                            if (sTexto.Contains(sPrimerInstruccion))
                                            {
                                                sw.WriteLine("SI");
                                                bInstruccion = true;
                                            }
                                            else
                                            {
                                                sw.WriteLine("NO");
                                            }
                                            if (sTexto.Contains(sSegundaInstruccion))
                                            {
                                                if (bInstruccion)
                                                {
                                                    MessageBox.Show("ERROR: Existen más de 1 instrucción en el mensaje");
                                                    return;
                                                }
                                                else
                                                {
                                                    sw.WriteLine("SI");
                                                }
                                                sw.Close();
                                            }
                                            else
                                            {
                                                sw.WriteLine("NO");
                                                sw.Close();
                                            }
                                            MessageBox.Show("Se ha generado el archivo en la ruta: " + sResultado);
                                            return;
                                        }
                                        else
                                        {
                                            MessageBox.Show("ERROR: Caracteres invalidos en el mensaje");
                                            return;
                                        }
                                    }
                            }
                        }
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