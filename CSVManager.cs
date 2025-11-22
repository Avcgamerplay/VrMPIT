using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hakatonv2
{
    public static class CSVManager
    {
        public static void ExportSudToCsv(List<Session> sessions, string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // Заголовки
                    writer.WriteLine("Session_id Date log notes");

                    // Данные
                    foreach (Session session in sessions)
                    {
                        writer.WriteLine($"{session.Id} {session.Date} {session.SessionLog} {session.DoctorNotes}");
                    }
                }

                MessageBox.Show($"Данные успешно экспортированы в {filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}");
            }
        }

        public static void ExportToCsv(List<Patient> patients, string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // Заголовки
                    writer.WriteLine("LastName Name MiddleName Id SessionData");

                    // Данные
                    foreach (Patient patient in patients)
                    {
                        // Экранируем специальные символы
                        string name = patient.FirstName;
                        string lastName = patient.LastName;
                        string middleName = patient.MiddleName;
                        string birthDate = patient.BirthDate;

                        writer.WriteLine($"{lastName} {name} {middleName} {patient.BirthDate} {patient.Id} {patient.GetAllDataSession().Replace(' ', '&')}");
                    }
                }

                MessageBox.Show($"Данные успешно экспортированы в {filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}");
            }
        }

        public static List<Patient> LoadFromCsv(string filePath)
        {
            List<Patient> patients = new List<Patient>();

            try
            {
                if (!File.Exists(filePath))
                {
                    MessageBox.Show("Файл не найден!");
                    return patients;
                }

                var lines = File.ReadAllLines(filePath, Encoding.UTF8);

                // Пропускаем заголовок (первую строку)
                for (int i = 1; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    if (line.Length < 5) continue;

                    //Debug.WriteLine(line);

                    string[] values = line.Split(' ');//ParseCsvLine(line);

                    if (values.Length >= 3)
                    {

                        Patient patient = new Patient(values[1], values[0], "", values[2], values[3], int.Parse(values[4]))
                        {
                            
                            //patient.
                        };
                        patient.LoadAllDataSession(values[5]);
                        patients.Add(patient);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}");
            }

            return patients;
        }
    }
}
