using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hakatonv2
{
    public partial class FormPatientRegistration : Form
    {
        public Patient patientData;
        int patientCount;
        public FormPatientRegistration(int patientCount)
        {
            this.patientCount = patientCount;
            InitializeComponent();
            InitTextBoxes();
        }

        void InitTextBox(TextBox box, string placeHolder)
        {
            box.Text = placeHolder;

            box.GotFocus += (sender, e) =>
            {
                if (box.Text == placeHolder)
                {
                    box.Text = "";
                }
            };

            box.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(box.Text))
                    box.Text = placeHolder;
            };
        }

        void InitTextBoxes()
        {
            InitTextBox(name_box, "Введите имя...");
            InitTextBox(last_name_box, "Введите фамилию...");
            InitTextBox(middle_name_box, "Введите отчество...");
        }

        private bool CheckForIntAndMax(string str, int max, int min)
        {
            int res = 0;
            if (int.TryParse(str, out res) == false) return false;

            if (res > max) return false;
            if (res < min) return false;

            return true;
        }

        private void add_user_button_Click(object sender, EventArgs e)
        {

            DateTime birthDate = dateTimePicker1.Value;

            string birthText = $"{birthDate.Day}.{birthDate.Month}.{birthDate.Year}";

            if (name_box.Text == " " || name_box.Text == "Введите имя...") return;
            if (last_name_box.Text == " " || last_name_box.Text == "Введите фамилию...") return;
            if (middle_name_box.Text == " " || middle_name_box.Text == "Введите отчество...") return;

            patientData = new Patient(name_box.Text, last_name_box.Text, "", middle_name_box.Text, birthText, patientCount);

            this.DialogResult = DialogResult.OK;
            this.Hide();
        }
    }
}
