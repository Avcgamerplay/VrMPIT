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
    public partial class FormSoundEdit : Form
    {
        public FormSoundEdit()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            NetworkManager.SendMessage($"volume|{trackBar1.Value}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NetworkManager.SendMessage($"set_amb|{0}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NetworkManager.SendMessage($"set_amb|{1}");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            NetworkManager.SendMessage($"set_amb|{2}");
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            NetworkManager.SendMessage($"video_volume|{trackBar1.Value}");
        }
    }
}
