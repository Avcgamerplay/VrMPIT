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
    public partial class FormAddSeansDay : Form
    {
        public DateTime dateSession;
        public DateTime timeSession;

        public FormAddSeansDay()
        {
            InitializeComponent();

            timePicker.Format = DateTimePickerFormat.Custom;
            timePicker.CustomFormat = "HH:mm";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dateSession = dateTimePicker1.Value;
            timeSession = timePicker.Value;

            DateTime now = DateTime.Now;

            DateTime newTime = new DateTime(dateSession.Year, dateSession.Month, dateSession.Day, timeSession.Hour, timeSession.Minute, timeSession.Second);

            TimeSpan difference = now.Subtract(newTime);

            if(difference.TotalDays > 0)
            {
                MessageBox.Show("Дата не может быть раньше текущей");
                return;
            }

            dateSession = newTime;

            Console.WriteLine(difference);

            DialogResult = DialogResult.OK;
            Hide();
        }
    }
}
