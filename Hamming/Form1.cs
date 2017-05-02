using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Hamming
{
    public partial class Form1 : Form
    {

        private int[] palabraOriginal = new int[] { };
        private int[] datos = new int[] { };
        private int[] pares = new int[] { };
        int p;
        private int total;
        private int[][] posiciones;
        public Form1()
        {
            InitializeComponent();

        }

        private int[] intToArray(int numero)
        {
            var list = new Stack<int>();
            var remainder = numero;
            do
            {
                list.Push(remainder % 10);
                remainder /= 10;
            } while (remainder > 0);

            return list.ToArray();
        }

        private void calcularP(int[] palabra)
        {
            bool repetir = true;
            p = 0;

            while (repetir)
            {
                double i = Math.Pow(2, p);
                double j = palabra.Length + p + 1;

                if (i >= j)
                {
                    repetir = false;
                } else
                {
                    p++;
                }
            }
            total = palabraOriginal.Length + p;
            arrayPosiciones(total);
        }

        private void arrayPosiciones(int t)
        {
            posiciones = new int[t][];
            string s = Convert.ToString(t, 2);

            for (int i = 0; i < t; i++)
            {
                posiciones[i] = new int[s.Length];

                var list = new Stack<int>();
                var r = int.Parse(Convert.ToString((i + 1),2));
                do
                {
                    list.Push(r % 10);
                    r /= 10;
                } while (r > 0);

                while (list.ToArray().Length < s.Length)//agrega 0 al principio
                {
                    list.Push(0);
                }
                posiciones[i] = list.ToArray();
            }
        }

        private void fillA()
        {
            #region headers
            var dx = new Stack<int>();
            var px = new Stack<int>();
            int pot = 0;
            int par = 1;
            int dat = 1;
            dataGridView.Columns.Add("c0", "");
            for (int i = 1; i <= total; i++)
            {
                if (Math.Pow(2, pot) == i)
                {
                    dataGridView.Columns.Add("c" + i.ToString(), "p" + par.ToString());
                    px.Push(i);
                    par++;
                    pot++;
                }
                else
                {
                    dataGridView.Columns.Add("c" + i.ToString(), "d" + dat.ToString());
                    dx.Push(i);
                    dat++;
                }
            }
            datos = dx.ToArray();
            pares = px.ToArray();
            #endregion

            #region posiciones
            dataGridView.Rows.Add();
            for (int i = 1; i <= total; i++)
            {
                dataGridView.Rows[0].Cells[0].Value = "Posiciones";
                dataGridView.Rows[0].Cells[i].Value = arrayToString(posiciones[i -1]);
            }
            #endregion

            #region palabra original
            dataGridView.Rows.Add();
            dataGridView.Rows[1].Cells[0].Value = "Palabra original";
            #endregion

            #region paridades
            for (int i = 1; i <= p; i++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i + 1].Cells[0].Value = "P" + i.ToString();
            }
            #endregion

            #region resultado
                dataGridView.Rows.Add();
                dataGridView.Rows[(p + 2)].Cells[0].Value = "Palabra + paridad";
            #endregion
        }

        private string arrayToString(int[] arr)
        {
            string join = string.Join("", arr);
            return join;
        }

        private void A()
        {
            #region palabra original
            int dcount = 0;
            for (int i = datos.Length; i > 0; i--)
            {
                dataGridView.Rows[1].Cells[datos[i - 1]].Value = palabraOriginal[dcount];
                dcount++;
            }
            #endregion

            #region paridad
                int count = 2;
                for (int i = p - 1; i >= 0; i--)
                {
                    for (int j = datos.Length - 1; j >= 0; j--)
                    {
                        if (bajar(datos[j], i))
                        {
                            dataGridView.Rows[count].Cells[datos[j]].Value = dataGridView.Rows[1].Cells[datos[j]].Value;
                        }
                    }
                    count++;
                }
            #endregion
        }
        
        private bool bajar(int d, int index)
        {
            Console.WriteLine(posiciones[d - 1][index]);
                if(posiciones[d - 1][index] == 1)
                {
                    return true;
                }
                else
                {
                return false;
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            palabraOriginal = intToArray(1234567890);
            calcularP(palabraOriginal);
            fillA();
            A();
        }
    }
}
