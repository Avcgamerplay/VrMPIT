using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hakatonv2
{
    public partial class FormAccountEntered : Form
    {
        public FormAccountEntered()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "Тест" || textBox2.Text != "Тест") return;

            StartScreen newForm = new StartScreen();

            newForm.FormClosed += (s, args) => this.Close(); // Когда новая форма закрывается, закрываем и главную
            this.Hide();
            newForm.Show();
        }
    }
}
