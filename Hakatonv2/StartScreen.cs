using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hakatonv2
{
    public partial class StartScreen : Form
    {
        private PatientRepository patientRepo;
        private string placeHolderTextBox = "Введите Фамилию Имя или ID...";

        public StartScreen()
        {
            DataBaseManager.InitializeBD();

            InitializeComponent();
            patientRepo = new PatientRepository();
            SetupUI();
            LoadPatients();
        }

        private void SetupUI()
        {
            this.Text = "Панель врача";
            this.Size = new Size(950, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            txtSearch.Text = placeHolderTextBox;

            txtSearch.GotFocus += (sender, e) =>
            {
                if (txtSearch.Text == placeHolderTextBox)
                {
                    txtSearch.Text = "";
                }
            };

            txtSearch.LostFocus += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                    txtSearch.Text = placeHolderTextBox;
            };

            txtSearch.TextChanged += txtSearch_TextChanged;

            btnClearSearch.Click += (s, e) =>
            {
                txtSearch.Clear();
                LoadPatients();
            };
        }

        private void LoadPatients(string searchTerm = null)
        {
            flowLayoutPanel.Controls.Clear();

            List<Patient> patients;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                patients = patientRepo.GetAllPatients();
            }
            else
            {
                patients = patientRepo.SearchPatients(searchTerm);
            }

            if (!patients.Any())
            {
                var noResultsLabel = new Label
                {
                    Text = "Пациенты не найдены",
                    Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                    ForeColor = Color.Gray,
                    AutoSize = true
                };
                flowLayoutPanel.Controls.Add(noResultsLabel);
                return;
            }

            foreach (Patient patient in patients)
            {
                List<Session> sessions = patient.Sessions;
                Session lastSession = patient.GetLastSession();
                string lastSessionStr = "";

                if (lastSession != null) lastSessionStr = lastSession.Date; 

                PatientCard card = new PatientCard(patient);

                card.SetPatientData(
                    patient.Id,
                    $"{patient.LastName} {patient.FirstName} {patient.MiddleName}",
                    patient.BirthDate,
                    sessions.Count,
                    lastSessionStr
                );

                card.PatientClicked += OnPatientCardClicked;

                card.PatientDeleted += OnPatientCardDeleteClicked;
                card.PatientStarted += OnPatientCardStartClicked;

                flowLayoutPanel.Controls.Add(card);
            }
        }

        private void OnPatientCardClicked(object sender, Patient patient)
        {
            using (FormPatientAllData form = new FormPatientAllData(patient))
            {
                // Показываем форму как диалоговое окно
                DialogResult resultForm = form.ShowDialog();

                if (resultForm == DialogResult.OK)
                {

                }
                else
                {
                    //MessageBox.Show("Сеанс завершен");
                }
            }

            /*var patientDetailForm = new PatientDetailForm(patientId);
            patientDetailForm.ShowDialog();*/

            // Обновляем список после закрытия формы деталей
            //LoadPatients(txtSearch.Text);
        }

        private void OnPatientCardDeleteClicked(object sender, Patient patient)
        {
            DialogResult result = MessageBox.Show(
                $"Вы действительно хотите удалить все данные пациента\n{patient.LastName} {patient.FirstName}?",
                "Подтверждение удаления данных клиента",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                return;
            }

            patientRepo.DeletePatient(patient);

            /*var patientDetailForm = new PatientDetailForm(patientId);
            patientDetailForm.ShowDialog();*/

            // Обновляем список после закрытия формы деталей
            //LoadPatients(txtSearch.Text);
        }

        private void OnPatientCardStartClicked(object sender, Patient patient)
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

            /*var patientDetailForm = new PatientDetailForm(patientId);
            patientDetailForm.ShowDialog();*/

            // Обновляем список после закрытия формы деталей
            //LoadPatients(txtSearch.Text);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if(txtSearch.Text == placeHolderTextBox)
            {
                return;
            }

            // Задержка для избежания частых обновлений при быстром вводе
            Timer searchTimer = new Timer { Interval = 500 };
            searchTimer.Tick += (s, args) =>
            {
                searchTimer.Stop();
                searchTimer.Dispose();
                LoadPatients(txtSearch.Text.Trim());
            };
            searchTimer.Start();
        }

        private void StartScreen_Load(object sender, EventArgs e)
        {

        }

        private void add_new_client_button_Click(object sender, EventArgs e)
        {
            using (FormPatientRegistration inputForm = new FormPatientRegistration(patientRepo.GetMaxIdPatient() + 1))
            {
                // Показываем форму как диалоговое окно
                DialogResult result = inputForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Patient patient = inputForm.patientData;

                    patientRepo.AddPatient(patient);
                    LoadPatients();
                    DataBaseManager.SaveNewPatient(patient);
                }
                else
                {
                    MessageBox.Show("Ввод отменен");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                saveDialog.FilterIndex = 1;
                saveDialog.DefaultExt = "csv";
                saveDialog.FileName = $"patients_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    CSVManager.ExportToCsv(patientRepo.GetAllPatients(), saveDialog.FileName);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }


}
