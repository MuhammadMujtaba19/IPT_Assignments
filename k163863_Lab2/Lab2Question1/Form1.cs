using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2Question1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            var x = GetNumbers();
            label.Text = Convert.ToString(x.Item1 + x.Item2);
        }

        private void subButton_Click(object sender, EventArgs e)
        {
            var x = GetNumbers();
            label.Text = Convert.ToString(x.Item1 - x.Item2);
        }

        private void multiplyButton_Click(object sender, EventArgs e)
        {
            var x = GetNumbers();
            label.Text = Convert.ToString(x.Item1 * x.Item2);

        }

        private void divideButton_Click(object sender, EventArgs e)
        {
            var x = GetNumbers();
            label.Text = Convert.ToString(x.Item1 / x.Item2);
        }
        public Tuple<float, float, bool> GetNumbers()
        {
            bool flag = false;
            float num1 = 0f;
            float num2 = 0f;
            if (number1.Text == string.Empty )
            {
                errorProvider1.SetError(number1, "Please dont Leave this feild empty !");
            }else if(number2.Text == string.Empty)
            {
                errorProvider2.SetError(number2, "Please dont Leave this feild empty !");
            }
            else {
                errorProvider2.Clear();
                errorProvider1.Clear();
                flag = true;
                num1 = Convert.ToInt32(number1.Text.ToString());
                num2 = Convert.ToInt32(number2.Text.ToString());
            }
            return Tuple.Create(num1, num2, flag);

        }

        private void number1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if(!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void number2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
    }
}
