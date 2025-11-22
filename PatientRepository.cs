using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakatonv2
{
    public class Patient
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string MiddleName { get; private set; } = string.Empty;
        public string BirthDate { get; private set; }
        public string Notes { get; set; } = string.Empty; // Примечания к пациенту
        public List<Session> Sessions { get; set; } = new List<Session>();

        public string NextDateSession { get; set; } = string.Empty;

        public string PathDistPatient { get; private set; }

        private int lastMaxIdIndex = -1;

        public string DoubledName { get; private set; }

        public Patient(string firstName, string lastName, string pathDistPatient, string middleName, string birthData, int id)
        {
            FirstName = firstName;
            LastName = lastName;
            PathDistPatient = pathDistPatient;
            MiddleName = middleName;
            BirthDate = birthData;
            Id = id;

            DoubledName = LastName + " " + FirstName + " " + MiddleName;
        }           

        public string GetDataToSave()
        {
            return $"{LastName}|{FirstName}|{MiddleName}|{BirthDate}|{Id}|{Notes.Replace("\n", "/n")}|{NextDateSession}";
        }

        public string GetAllDataSession()
        {
            string itogStr = "";

            foreach(Session session in Sessions)
            {
                itogStr += session.GetDataToSave() + DataProject.SeparatorStroke;
            }

            return itogStr;
        }

        public void LoadAllDataSession(string data)
        {
            string[] lines = data.Split(DataProject.SeparatorStrokeChar);

            for(int i = 0; i < lines.Length; i++)
            {
                string[] newParsedData = lines[i].Replace('&', ' ').Split('|');
                if (newParsedData.Length < 3) continue;
                Debug.WriteLine(newParsedData.Length);
                Session newS = new Session();
                newS.Id = int.Parse(newParsedData[0]);
                newS.Date = newParsedData[1];
                newS.SessionLog = newParsedData[2];
                newS.DoctorNotes = newParsedData[3];
                newS.LoadSudsFromSessionLog();

                AddSession(newS);
            }
        }

        public void AddSession(Session session)
        {
            Sessions.Add(session);

            if (lastMaxIdIndex != -1)
            {
                lastMaxIdIndex++;
            }
            else
            {
                lastMaxIdIndex = 0;
            }
        }

        public Session GetLastSession()
        {
            if(lastMaxIdIndex != -1)
            {
                return Sessions[lastMaxIdIndex];
            }

            int maxId = -1;
            int indexEl = -1;
            for(int i = Sessions.Count - 1; i > 0; i--)
            {
                if (Sessions[i].Id > maxId)
                {
                    indexEl = i;
                    maxId = Sessions[i].Id;
                }
            }

            if(indexEl == -1)
            {
                return null;
            }

            lastMaxIdIndex = indexEl;

            return Sessions[indexEl];
        }
    }

    public class Session
    {
        public int Id { get; set; }
        public string Date { get; set; } = string.Empty; // Дата как строка
        public string SessionLog { get; set; } = string.Empty; // Лог сеанса
        public string DoctorNotes { get; set; } = string.Empty; // Заметки врача

        public List<Sud> suds = new List<Sud>();

        public void AddSuds(Sud[] ss)
        {
            for(int i = 0; i < ss.Length; i++)
            {
                suds.Add(ss[i]);
            }
        }

        public void LoadSudsFromSessionLog()
        {
            if (SessionLog.Length < 4) return;

            string[] splited = SessionLog.Split('/');

            for(int i = 0; i < splited.Length; i++)
            {
                string[] data = splited[i].Split(';');

                if (data.Length < 2) continue;

                Sud newSud = new Sud();
                newSud.sudId = int.Parse(data[0]);
                newSud.sudLvl = data[2];
                newSud.volume = data[1];
                newSud.sceneName = data[3];

                suds.Add(newSud);
            }
        }

        public string GetDataToSave()
        {
            return $"{Id}|{Date}|{SessionLog}|{DoctorNotes.Replace("\n", "/n")}";
        }

        public void GenerateSessionLog(Sud[] suds)
        {
            string itog = "";
            for(int i = 0; i < suds.Length; i++)
            {
                itog += suds[i].GetLog() + "/";
            }
            itog = itog.Remove(itog.Length - 1);
            SessionLog = itog;
        }
    }

    public class PatientRepository
    {
        List<Patient> patients = new List<Patient>();

        int maxIdPatient = -1;

        public PatientRepository()
        {
            LoadAllPatients();
        }

        void LoadAllPatients()
        {
            for(int i = 0; i < DataBaseManager.patientsPath.Count; i++)
            {
                string directoryData = DataBaseManager.patientsPath[i] + "/PatientData";
                string directorySession = DataBaseManager.patientsPath[i] + "/PatientSession";

                string[] patienData = DataBaseManager.ReadTextFile(directoryData + "/patient_data.txt").Split('|');

                Patient newPatient = new Patient(patienData[1], patienData[0], DataBaseManager.patientsPath[i], patienData[2], patienData[3], int.Parse(patienData[4]));

                newPatient.Notes = patienData[5].Replace("/n", "\n");

                newPatient.NextDateSession = patienData[6];

                if (newPatient.Id > maxIdPatient) maxIdPatient = newPatient.Id;

                string[] patientsSessionFolders = Directory.GetDirectories(directorySession);

                for (int session = 0; session < patientsSessionFolders.Length; session++)
                {
                    string[] sessionData = DataBaseManager.ReadTextFile(patientsSessionFolders[session] + "/session_data.txt").Split('|');

                    Session newSession = new Session();
                    newSession.Id = int.Parse(sessionData[0]);
                    newSession.Date = sessionData[1];
                    newSession.SessionLog = sessionData[2];
                    newSession.DoctorNotes = sessionData[3].Replace("/n", "\n");

                    newPatient.AddSession(newSession);
                }

                patients.Add(newPatient);
            }
        }

        public void SaveAllPatient()
        {
            for (int i = 0; i < patients.Count; i++)
            {
                DataBaseManager.SavePatient(patients[i]);
            }
        }

        // Добавление пациента
        public int AddPatient(Patient patient)
        {
            maxIdPatient += 1;
            patients.Add(patient);
            return 0;
        }

        public int GetMaxIdPatient()
        {
            return maxIdPatient;
        }

        public int GetPatientCount()
        {
            return patients.Count;
        }

        // Получение всех пациентов
        public List<Patient> GetAllPatients()
        {
            return patients;
        }

        // Поиск пациента по ID
        public Patient GetPatientById(int id)
        {
            for(int i = 0; i < patients.Count; i++)
            {
                if (patients[i].Id == id) return patients[i];
            }
            return null;
        }

        // Поиск пациента по внешнему ID
        public Patient GetPatientByPatientId(string patientId)
        {
            
            return null;
        }

        // Поиск пациентов по имени/фамилии
        public List<Patient> SearchPatients(string searchTerm)
        {
            List<Patient> patientsFinded = new List<Patient>();

            for(int i = 0; i < patients.Count; i++)
            {
                Debug.WriteLine(patients[i].DoubledName);
                Debug.WriteLine(searchTerm);
                if (patients[i].DoubledName.StartsWith(searchTerm))
                {
                    patientsFinded.Add(patients[i]);
                }
                else if (patients[i].Id.ToString().StartsWith(searchTerm))
                {
                    patientsFinded.Add(patients[i]);
                }
            }

            
            return patientsFinded;
        }

        // Обновление пациента
        public void UpdatePatient(Patient patient)
        {
            
        }

        // Удаление пациента
        public void DeletePatient(Patient patient)
        {
            patients.Remove(patient);
            DataBaseManager.DeletePatient(patient);
        }
    }
}
