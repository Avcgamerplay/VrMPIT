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

    public static class DataBaseManager
    {
        public static string basePath => Path.Combine(Application.StartupPath, "DataPatients");

        public static List<string> patientsPath { get; set; } = new List<string>();

        public static void InitializeBD()
        {
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);

            string[] patients = Directory.GetDirectories(basePath);

            foreach (string dirPath in patients)
            {
                patientsPath.Add(dirPath);
            }
        }

        public static string ReadTextFile(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    return reader.ReadToEnd(); // Читаем весь файл
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка чтения файла: {ex.Message}");
                return string.Empty;
            }
        }

        public static void DeletePatient(Patient patient)
        {
            string newPath = Path.Combine(basePath, patient.Id.ToString());

            if (!Directory.Exists(newPath)) return;

            Debug.WriteLine(newPath);

            Directory.Delete(newPath, true);

            MessageBox.Show($"Пациент удален");
        }

        public static void SavePatient(Patient patient)
        {
            string newPath = basePath + "/" + patient.Id.ToString();

            if (!Directory.Exists(newPath))
            {
                SaveNewPatient(patient);
                return;
            }

            if (!Directory.Exists(newPath + "/PatientData"))
            {
                SaveNewPatient(patient);
                return;
            }

            if (!Directory.Exists(newPath + "/PatientSession"))
            {
                SaveNewPatient(patient);
                return;
            }

            using (StreamWriter writer = new StreamWriter(newPath + "/PatientData/" + "patient_data.txt", false, Encoding.UTF8))
            {
                writer.WriteLine(patient.GetDataToSave());
            }

            for (int i = 0; i < patient.Sessions.Count; i++)
            {
                string pathSess = newPath + "/PatientSession/" + patient.Sessions[i].Id.ToString();


                if (!Directory.Exists(pathSess)) Directory.CreateDirectory(pathSess);

                using (StreamWriter writer = new StreamWriter(pathSess + "/" + "session_data.txt", false, Encoding.UTF8))
                {
                    writer.WriteLine(patient.Sessions[i].GetDataToSave());
                }
            }
        }

        public static void SaveNewPatient(Patient patient)
        {
            string newPath = basePath + "/" + patient.Id.ToString();

            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            Directory.CreateDirectory(newPath + "/PatientData");

            using (StreamWriter writer = new StreamWriter(newPath + "/PatientData/" + "patient_data.txt", false, Encoding.UTF8))
            {
                writer.WriteLine(patient.GetDataToSave());
            }

            Directory.CreateDirectory(newPath + "/PatientSession");

            for(int i = 0; i < patient.Sessions.Count; i++)
            {
                string pathSess = newPath + "/PatientSession/" + patient.Sessions[i].Id.ToString();
                Directory.CreateDirectory(pathSess);

                using (StreamWriter writer = new StreamWriter(pathSess + "/" + "session_data.txt", false, Encoding.UTF8))
                {
                    writer.WriteLine(patient.Sessions[i].GetDataToSave());
                }
            }
        }
    }
}
