using Bunifu.UI.WinForms.BunifuButton;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Hakatonv2
{
    public partial class FormPatientRegistration : Form
    {
        private readonly HttpClient _httpClient;

        public Patient patientData;
        int patientCount;
        public FormPatientRegistration(int patientCount)
        {
            this.patientCount = patientCount;
            InitializeComponent();
            InitTextBoxes();

            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
            _httpClient.BaseAddress = new Uri("http://localhost:5001/"); // Замените на ваш адрес сервера
        }

        private async void AuthButton_Click(object sender, EventArgs e)
        {
            await AuthenticateUser("/auth");
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

        private async Task AuthenticateUser(string endpoint)
        {
            /*string login = loginTextBox.Text.Trim();
            string password = passwordTextBox.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                statusLabel.Text = "Введите логин и пароль";
                return;
            }

            try
            {
                statusLabel.Text = "Отправка запроса...";
                authButton.Enabled = false;
                registerButton.Enabled = false;

                // Создаем объект для сериализации в JSON
                var authData = new
                {
                    login = login,
                    password = password
                };

                // Сериализуем в JSON
                string json = JsonSerializer.Serialize(authData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Отправляем POST запрос
                HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();

                    // Десериализуем ответ
                    using JsonDocument doc = JsonDocument.Parse(responseContent);
                    JsonElement root = doc.RootElement;

                    bool success = root.GetProperty("success").GetBoolean();
                    string message = root.GetProperty("message").GetString();

                    if (success)
                    {
                        statusLabel.Text = $"Успешно! {message}";
                        statusLabel.ForeColor = System.Drawing.Color.Green;

                        // Здесь можно добавить логику после успешной аутентификации
                        if (endpoint == "/auth")
                        {
                            // Действия после успешного входа
                            MessageBox.Show($"Добро пожаловать, {login}!", "Успешный вход",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        statusLabel.Text = $"Ошибка: {message}";
                        statusLabel.ForeColor = System.Drawing.Color.Red;
                    }
                }
                else
                {
                    statusLabel.Text = $"Ошибка сервера: {response.StatusCode}";
                    statusLabel.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (HttpRequestException ex)
            {
                statusLabel.Text = $"Ошибка сети: {ex.Message}";
                statusLabel.ForeColor = System.Drawing.Color.Red;
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Ошибка: {ex.Message}";
                statusLabel.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                authButton.Enabled = true;
                registerButton.Enabled = true;
            }*/
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

        private void FormPatientRegistration_Load(object sender, EventArgs e)
        {

        }
    }

    public class AuthResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string user { get; set; }
    }
}
