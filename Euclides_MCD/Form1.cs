using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Euclides_MCD
{
    public partial class Form1 : Form
    {
        int[] numeros;
        string historial = "";
        List<int> num1, num2, residuos, cocientes;
        List<int> num1f, num2f, residuosf, cocientesf;

        public Form1()
        {
            InitializeComponent();
            HelpButton = true;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private int euclides(int a, int b)
        {
            int residuo, cociente = Math.DivRem(a, b, out residuo);

            historial = a.ToString() + " mod " + b.ToString() + " = " + cociente.ToString() + "  | R= " + residuo.ToString() + "\r\n";
            textBox2.Text += historial;

            num1.Add(a);
            num2.Add(b);
            residuos.Add(residuo);
            cocientes.Add(cociente);

            if (residuo == 0)
                return b;

            return euclides(b, residuo);
        }

        private void calculo()
        {
            int final1=0, final2=0, final3=0, final4=0;
            num1 = new List<int>();
            num2 = new List<int>();
            residuos = new List<int>();
            cocientes = new List<int>();

            num1f = new List<int>();
            num2f = new List<int>();
            residuosf = new List<int>();
            cocientesf = new List<int>();

            historial = "";
            textBox2.Text = "";
            string[] valores = textBox1.Text.Split(',');

            for (int i = 0; i < valores.Count(); i++)
                if (!int.TryParse(valores[i], out _))
                {
                    MessageBox.Show("Sólo se aceptan datos numéricos.", "Euclides MCD - Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            if (valores.Count() < 2)
            {
                MessageBox.Show("Sólo se hacen calculos con dos o más números.", "Euclides MCD - Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            numeros = new int[valores.Count()];

            for (int i = 0; i < valores.Count(); i++)
                numeros[i] = int.Parse(valores[i]);

            int gcd = numeros[0];
            for (int i = 0; i < valores.Count() - 1; i++)
            {
                gcd = euclides(gcd, numeros[i + 1]);
                textBox2.Text += "MCD= " + gcd.ToString() + "\r\n\r\n";
            }

            textBox2.Text += "Combinación lineal\r\nParte 1:\r\n";

            for (int i = 0; i < num1.Count() - 1; i++)
            {
                textBox2.Text += num1[i].ToString() + " = " + cocientes[i].ToString() + " x " + num2[i].ToString() + " + " + residuos[i].ToString() + "\r\n";
            }

            textBox2.Text += "\r\nParte 2:\r\n";

            for (int i = num1.Count() - 2; i >= 0; i--)
            {
                textBox2.Text += residuos[i].ToString() + " = " + num1[i].ToString() + " - " + cocientes[i].ToString() + " x " + num2[i].ToString() + "\r\n";
                num1f.Add(num1[i]);
                num2f.Add(num2[i]);
                residuosf.Add(residuos[i]);
                cocientesf.Add(cocientes[i]);
            }

            textBox2.Text += "\r\nParte 3:\r\n";


            for (int i = 0; i < num1f.Count() - 1; i++)
            {
                string sig = num1f[i + 1].ToString() + " - " + cocientesf[i + 1].ToString() + " x " + num2f[i + 1].ToString();
                textBox2.Text += residuosf[i].ToString() + " = " + num1f[i].ToString() + " - " + cocientesf[i].ToString() + " x (" + sig + ")\r\n";

                final1 = cocientesf[i];
                final2 = num1f[i + 1];
                final3 = final1 * cocientesf[i + 1];
                final4 = num2f[i + 1];

                textBox2.Text += "-" + final1.ToString() + " x " + final2.ToString() + " + " + final3.ToString() + " x " + final4.ToString() + "\r\n\r\n";
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Properties.Resources.calcular;
            pictureBox2.BackgroundImageLayout = ImageLayout.Stretch;

            textBox1.Text = "126,34";
            calculo();
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Properties.Resources.calcular_hov;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Properties.Resources.calcular;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.BackgroundImage = Properties.Resources.calcular_dwn;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.BackgroundImage = Properties.Resources.calcular;
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            MessageBox.Show("Luis Esteban Murillo Claver\n20161020091", "Euclides MCD - Autor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e) { calculo(); }
    }
}
