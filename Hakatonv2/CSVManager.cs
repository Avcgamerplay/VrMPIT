using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hakatonv2
{
    public static class CSVManager
    {
        public static void ExportToCsv(List<Patient> patients, string filePath)
        {
            try
            {
                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // Заголовки
                    writer.WriteLine("LastName,Name,MiddleName,Id,SessionData");

                    // Данные
                    foreach (Patient patient in patients)
                    {
                        // Экранируем специальные символы
                        string name = EscapeCsvField(patient.FirstName);
                        string lastName = EscapeCsvField(patient.LastName);
                        string middleName = EscapeCsvField(patient.MiddleName);
                        string birthDate = EscapeCsvField(patient.BirthDate);

                        writer.WriteLine($"{lastName},{name},{middleName},{patient.Id},{patient.GetAllDataSession()}");
                    }
                }

                MessageBox.Show($"Данные успешно экспортированы в {filePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при экспорте: {ex.Message}");
            }
        }

        private static string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field)) return "";

            // Если поле содержит запятые, кавычки или переносы строк - заключаем в кавычки
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\r") || field.Contains("\n"))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            return field;
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

                    string[] values = ParseCsvLine(line);

                    if (values.Length >= 3)
                    {
                        Patient patient = new Patient(values[0], values[1], values[2], values[3], values[4], int.Parse(values[5]))
                        {
                            
                        };
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

        private static string[] ParseCsvLine(string line)
        {
            List<string> values = new List<string>();
            StringBuilder current = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    values.Add(current.ToString().Trim());
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }

            // Добавляем последнее значение
            values.Add(current.ToString().Trim());

            // Убираем кавычки из значений
            /*for (int i = 0; i < values.Count; i++)
            {
                if (values[i].StartsWith("\"") && values[i].EndsWith("\""))
                {
                    values[i] = values[i].Substring(1, values[i].Length - 2)
                                        .Replace("\"\"", "\"");
                }
            }*/

            return values.ToArray();
        }
    }
}
