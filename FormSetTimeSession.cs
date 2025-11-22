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
    public partial class FormSetTimeSession : Form
    {
        public int ValueTime;
        public FormSetTimeSession()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetworkManager.SendMessage($"time_max|{trackBar1.Value}");
            ValueTime = trackBar1.Value;

            DialogResult = DialogResult.OK;


            Hide();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = "Минут: " + trackBar1.Value.ToString();
        }
    }
}
