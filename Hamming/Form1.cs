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
    public partial class Form : System.Windows.Forms.Form
    {

        private int[] palabraOriginal;
        private int[] datos = new int[] { };
        private int[] pares = new int[] { };
        int p;
        private int total;
        private int[][] posiciones;

        public Form()
        {
            InitializeComponent();

        }

        private int[] intToArray(int numero)
        {
            palabraOriginal = new int[] { };
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

        private void deducirP()
        {
            // 2^p >= i + p + 1
            p = 0;
            while (Math.Pow(2, p) < (palabraOriginal.Length + 1))
            {
                p++;
            }
            Console.WriteLine(p);
            total = palabraOriginal.Length;
            arrayPosiciones(total);
        }//deveria toma un argumento

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

        private string arrayToString(int[] arr)
        {
            string join = string.Join("", arr);
            return join;
        }

        private int calcularParidad(DataGridViewRow row)
        {
            int unos = 0;
            for (int i = 1; i <= row.Cells.Count; i++)
            {
                try
                {
                    if (row.Cells[i].Value.ToString() == "1")
                    {
                        unos++;
                    }
                } catch{ }
            }

            if (unos % 2 == 0)
            {
                return 0;
            }
            else
            {
            return 1;
            }
        }

        private bool bajar(int d, int index)
        {
                if(posiciones[d - 1][index] == 1)
                {
                    return true;
                }
                else
                {
                return false;
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

            #region size
            dataGridView.Height = (dataGridView.Rows.Count + 1) * dataGridView.RowTemplate.Height;
            #endregion
        }

        private void fillB()
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

            dataGridView.Columns.Add("p1","");
            dataGridView.Columns.Add("p2", "");
            dataGridView.Columns.Add("p3", "");
            #endregion
            
            #region posiciones
            dataGridView.Rows.Add();
            for (int i = 1; i <= total; i++)
            {
                dataGridView.Rows[0].Cells[0].Value = "Posiciones";
                dataGridView.Rows[0].Cells[i].Value = arrayToString(posiciones[i - 1]);
            }
            #endregion

            #region palabra original
            dataGridView.Rows.Add();
            dataGridView.Rows[1].Cells[0].Value = "Palabra original";
            #endregion

            #region calculos
            dataGridView.Rows[1].Cells[total + 1].Value = "Calculo paridad";
            dataGridView.Rows[1].Cells[total + 2].Value = "Paridad almacenada";
            dataGridView.Rows[1].Cells[total + 3].Value = "Comprobación";
            #endregion

            #region paridades
            for (int i = 1; i <= p; i++)
            {
                dataGridView.Rows.Add();
                dataGridView.Rows[i + 1].Cells[0].Value = "P" + i.ToString();
            }
            #endregion

            #region size
            dataGridView.Height = (dataGridView.Rows.Count + 1) * dataGridView.RowTemplate.Height;
            #endregion
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

            #region bajar
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

            #region paridad
            int rowCount = 2;
            for (int i = pares.Length - 1; i >= 0; i--)
            {
                dataGridView.Rows[rowCount].Cells[pares[i]].Value = calcularParidad(dataGridView.Rows[rowCount]);
                rowCount++;
            }
            #endregion

            #region palabra + paridad
            int ultima = dataGridView.RowCount - 1;
            rowCount = 2;
            for (int i = pares.Length - 1; i >= 0; i--)
            {
                dataGridView.Rows[ultima].Cells[pares[i]].Value = dataGridView.Rows[rowCount].Cells[pares[i]].Value;
                rowCount++;
            }

            for (int i = datos.Length - 1; i >= 0; i--)
            {
                dataGridView.Rows[ultima].Cells[datos[i]].Value = dataGridView.Rows[1].Cells[datos[i]].Value;
            }
            #endregion
        }//palabra original cambiar palabraoriginal[dcopunt] por [datos[i]] copiar de B

        private void B()
        {
            #region palabra original
            for (int i = datos.Length - 1; i >= 0; i--)
            {
                dataGridView.Rows[1].Cells[datos[i]].Value = palabraOriginal[datos[i] -1];
            }
            #endregion

            #region bajar
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

            #region paridad, calculo, almacenada y comprobacion
            int rowCount = 2;
            int almacenada = 0;
            for (int i = pares.Length - 1; i >= 0; i--)
            {
                dataGridView.Rows[rowCount].Cells[pares[i]].Value = calcularParidad(dataGridView.Rows[rowCount]);
                dataGridView.Rows[rowCount].Cells[total + 1].Value = dataGridView.Rows[rowCount].Cells[pares[i]].Value;//calculo de paridad
                dataGridView.Rows[rowCount].Cells[total + 2].Value = palabraOriginal[almacenada];
                dataGridView.Rows[rowCount].Cells[total + 3].Value = calcularParidad(dataGridView.Rows[rowCount]);
                rowCount++;
                almacenada++;
            }
            #endregion

            #region comprobacion

            #region comp
            string comp = "";
            for (int i = p; i > 0; i--)
            {
                comp += dataGridView.Rows[i + 1].Cells[total + 3].Value;
            }
            int conv = Convert.ToInt32(comp, 2);

            label1.Text = "Comprobación = " + comp + "(2) = " + conv + "(10)";
            #endregion

            #region recib
            string recib = "";
            for (int i = datos.Length - 1; i >= 0; i--)
            {
                recib += palabraOriginal[datos[i] - 1];
            }
            #endregion

            #region corregir
            if (conv != 0)
            {
                if (palabraOriginal[conv - 1] == 0)
                {
                    palabraOriginal[conv - 1] = 1;
                }
            }

            string corr = "";
            for (int i = datos.Length - 1; i >= 0; i--)
            {
                corr += palabraOriginal[datos[i] - 1];
            }

            label2.Text = "Palabra recibidia = " + recib + "(2) Palabra correcta = " + corr + "(2)"; 
            #endregion

            #endregion
        }

        private void Form_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("A");
            comboBox1.Items.Add("B");
            comboBox1.SelectedItem = "A";
            label1.Text = "";
            label2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            while (dataGridView.RowCount > 0)
            {
                dataGridView.Rows.RemoveAt(0);
            }

            while (dataGridView.ColumnCount > 0)
            {
                dataGridView.Columns.RemoveAt(0);
            }

            string caso = comboBox1.SelectedItem.ToString();
            switch (caso)
            {
                case "A":
                    palabraOriginal = intToArray(int.Parse(textBox1.Text));
                    calcularP(palabraOriginal);
                    fillA();
                    A();
                    break;
                case "B":
                    palabraOriginal = intToArray(int.Parse(textBox1.Text));
                    deducirP();
                    fillB();
                    B();
                    break;
            }
        }
    }
}
