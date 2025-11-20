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
    public partial class PatientCard : UserControl
    {
        public event EventHandler<Patient> PatientClicked;
        public event EventHandler<Patient> PatientDeleted;
        public event EventHandler<Patient> PatientStarted;

        private Patient patient;

        private Size originalSize = new Size(200, 140);
        private Size hoverSize = new Size(202, 142);

        public PatientCard(Patient patient)
        {
            this.patient = patient;
            InitializeComponent();
            SetupCard();
        }

        private void SetupCard()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Margin = new Padding(10);
            this.Cursor = Cursors.Hand;
            this.BackColor = Color.White;
            this.Size = originalSize;

            // Обработчик клика по всей карточке
            this.Click += (s, e) => PatientClicked?.Invoke(this, patient);

            // Добавляем обработчики ко всем дочерним контролам
            AddClickHandlerToChildren(this);
        }

        void SetNewSize(bool up)
        {
            if(up && this.Size != hoverSize) this.Size = hoverSize;
            else if(!up && this.Size != originalSize) this.Size = originalSize;
        }

        private void AddClickHandlerToChildren(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                control.MouseEnter += (s, e) => SetNewSize(true);
                control.MouseLeave += (s, e) => SetNewSize(false);

                if (control.Name == "delete_button")
                {
                    control.Click += (s, e) => PatientDeleted?.Invoke(this, patient);
                    control.Cursor = Cursors.Hand;
                    continue;
                }
                else if(control.Name == "start_seans_button")
                {
                    control.Click += (s, e) => PatientStarted?.Invoke(this, patient);
                    control.Cursor = Cursors.Hand;
                    continue;
                }

                control.Click += (s, e) => PatientClicked?.Invoke(this, patient);
                control.Cursor = Cursors.Hand;

                // Рекурсивно для вложенных контролов
                if (control.HasChildren)
                {
                    AddClickHandlerToChildren(control);
                }
            }
        }

        public void SetPatientData(int id, string fullName, string birthDate, int sessionCount, string lastSession)
        {
            lblFullName.Text = fullName;
            lblBirthDate.Text = $"Дата рождения: {birthDate}";
            lblSessionCount.Text = $"Сеансов: {sessionCount}";

            if (sessionCount <= 0) lblFullName.BackColor = Color.Aquamarine;

            if (lastSession != "")
                lblLastSession.Text = $"Последний: {lastSession}";
            else
                lblLastSession.Text = "Сеансов нет";

            lblId.Text = $"ID: {id.ToString()}";
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void delete_button_Click(object sender, EventArgs e)
        {

        }
    }
}
