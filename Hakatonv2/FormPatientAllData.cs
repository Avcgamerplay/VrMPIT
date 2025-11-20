using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hakatonv2
{
    public partial class FormPatientAllData : Form
    {
        private Patient patient;
        public FormPatientAllData(Patient patient)
        {
            this.patient = patient;
            InitializeComponent();

            InitInfo();
            LoadAllSessionData();
        }

        void InitInfo()
        {
            name_label.Text = patient.LastName;
            last_name_label.Text = patient.FirstName;
            middle_name_label.Text = patient.MiddleName;

            if (patient.Notes.Length == 0) notes_label.Text = "Заметок нет...";
            else notes_label.Text = patient.Notes;

            SetNewDateSession();
        }

        void SetNewDateSession()
        {
            Console.WriteLine($"ссс {patient.NextDateSession} {patient.NextDateSession.Length}");

            if (patient.NextDateSession.Length < 5)
            {
                next_date_label.Text = "Прием не назначен";
                close_seans_button.Visible = false;
                add_seans_button.Text = "Назначить прием";
            }
            else
            {
                next_date_label.Text = $"Сеанс будет: {patient.NextDateSession}";
                close_seans_button.Visible = true;
                add_seans_button.Text = "Начать прием";
            }
        }

        void LoadAllSessionData()
        {
            if (!patient.Sessions.Any())
            {
                var noResultsLabel = new Label
                {
                    Text = "Сеансы не найдены",
                    Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                    ForeColor = Color.Gray,
                    AutoSize = true
                };
                flowLayoutPanel.Controls.Add(noResultsLabel);
                return;
            }

            foreach (Session session in patient.Sessions)
            {
                PatientSeansInfoCard card = new PatientSeansInfoCard(session);

                card.SessionClicked += OnSessionCardClicked;

                flowLayoutPanel.Controls.Add(card);
            }
        }

        void OnSessionCardClicked(object sender, Session session)
        {
            using (FormSessionAllInformation form = new FormSessionAllInformation(session))
            {
                DialogResult resultForm = form.ShowDialog();

                if (resultForm == DialogResult.OK)
                {
                    
                }
                else
                {
                    
                }
            }
        }

        private void name_label_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void add_seans_button_Click(object sender, EventArgs e)
        {
            if(add_seans_button.Text == "Назначить прием")
            {
                using (FormAddSeansDay form = new FormAddSeansDay())
                {
                    DialogResult resultForm = form.ShowDialog();

                    if (resultForm == DialogResult.OK)
                    {
                        DateTime date = form.dateSession;

                        string strDate = $"{date.Day}.{date.Month}.{date.Year} {date.Hour}:{date.Minute}";
                        patient.NextDateSession = strDate;
                        SetNewDateSession();

                        DataBaseManager.SavePatient(patient);

                        MessageBox.Show("Прием назначен");
                    }
                    else
                    {
                        MessageBox.Show("Прием не назначен");
                    }
                }
            }
            else
            {
                DialogResult result = MessageBox.Show(
                    $"Вы действительно хотите запустить сеанс с пациентом\n{patient.LastName} {patient.FirstName}?",
                    "Подтверждение запуска сеанса",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.No)
                {
                    return;
                }


                using (Form1 form = new Form1(patient))
                {
                    // Показываем форму как диалоговое окно
                    DialogResult resultForm = form.ShowDialog();

                    if (resultForm == DialogResult.OK)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Сеанс завершен");
                    }
                }
            }
        }

        private void close_seans_button_Click(object sender, EventArgs e)
        {
            patient.NextDateSession = "";
            SetNewDateSession();
            DataBaseManager.SavePatient(patient);
        }

        private void download_info__button_Click(object sender, EventArgs e)
        {

        }
    }
}
