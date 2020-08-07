using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

namespace Assignment2Question2
{
    public partial class Service1 : ServiceBase
    {
        public Timer timeDelay;
        public string OutputDirectory,InputDirectory;
        IEnumerable<XElement> patients;
        private int lastNodeIndexFromRegistryIndex;

        EventLog myLog;
        string[] inputFiles;
        public Service1()
        {
            InitializeComponent();
            timeDelay = new System.Timers.Timer();

            timeDelay.Elapsed += new System.Timers.ElapsedEventHandler(CheckUpdates);
            timeDelay.Interval = 60000 *5;
            //The InputDirectory is the Path from where the Service will read Files.
            //The OutputDirectory is the Path where the Service will Write it values/Files.
            OutputDirectory = ConfigurationManager.AppSettings["OutputPath"];
            InputDirectory = ConfigurationManager.AppSettings["InputPath"];
           

            //Custom Logger To view output in EventViewer 
            this.AutoLog = false;
            myLog = new EventLog();
            myLog.Source = "Assignment-Question2";
        }

        // This function is invoked as soon as the Service is started (i.e Only Once) 
        // Therefore here we Get all files present inside Input Directory and process them individually 
        // inside GetUpdatedValue function 
        protected override void OnStart(string[] args)
        {
            myLog.WriteEntry("OnStart Question2");
            timeDelay.Enabled = true;
            try { 
            //Check if the Outdput directory exist if not it will create the directory 
            if (!Directory.Exists(OutputDirectory))
            {
                Directory.CreateDirectory(OutputDirectory);
            }

            inputFiles = Directory.GetFiles(InputDirectory);
            lastNodeIndexFromRegistryIndex = 0;
            foreach (string file in inputFiles)
                GetUpdatedValue(file);
            }catch(Exception)
            {

                myLog.WriteEntry("Q2 Started with a Error");
            }
        }

        protected override void OnStop()
        {
            myLog.WriteEntry("OnStop Question2");
            timeDelay.Enabled = false;
        }
        
        // Since this function will be called 5 mins after the Service starts therefore we already have process
        // all the previous date files(if any) inside the OnStart() function and now we only need to continously 
        // watch the current Date file if there is any modifications and if there is modifications we need to 
        //process it accordingly
        public void CheckUpdates(object sender, ElapsedEventArgs e)
        {
            try {
                string InputFilePath = InputDirectory + "\\PatientDetails_" + DateTime.Now.ToString("yyyy_MM_dd")+".xml";

                myLog.WriteEntry(InputFilePath);
                if(isFileUpdatedSinceLastRead(InputFilePath)){
                    lastNodeIndexFromRegistryIndex = GetLastElementReadIndex();
                    GetUpdatedValue(InputFilePath);
                    myLog.WriteEntry("File has been Updated");
                }

            }catch(Exception exception)
            {
                myLog.WriteEntry(exception.Message);
            }
        }

        //Explained inside
        public void GetUpdatedValue(string InputFilePath)
        {
            try { 
               int currentNode = 1;
                //LINQ Query to get all the patients inside the InputFilePath 
                patients = from patient in XDocument.Load(InputFilePath)
                                         .Element("Patients").Elements("Patient")
                                          select patient;
                String temp = Path.GetFileNameWithoutExtension(InputFilePath);
                int index = temp.IndexOf('_');
                string dateString = temp.Substring(index+1);
                DateTime d  = DateTime.ParseExact(dateString, "yyyy_MM_dd",System.Globalization.CultureInfo.InvariantCulture);
                dateString = Regex.Replace(dateString, @"_", "-");
                myLog.WriteEntry(dateString);
                foreach (XElement patient in patients)
                {   
                    if (currentNode > lastNodeIndexFromRegistryIndex) {
                        
                        PatientInfo patientInfo = new PatientInfo();
                        PatientDetails pd = new PatientDetails();
                        List<PatientDetails> patientDetails = new List<PatientDetails>();

                        patientInfo.Patient_name = (string)patient.Attribute("name");
                        patientInfo.Patient_email = (string)patient.Attribute("email");
                        patientInfo.gender = (string)patient.Attribute("gender");
                        patientInfo.age = CalculateAge((string)patient.Attribute("DOB"));
                
                        DateTime t = (DateTime)patient.Element("time");
                        pd.dateTime = d.Date.Add(t.TimeOfDay).ToString("MM/dd/yyyy HH:mm:ss");
                        pd.value.bpm = (int)patient.Element("bpm");
                        pd.value.confidence = (int)patient.Element("confidence");

                        // Path Creations 
                        string BasePath = OutputDirectory + "\\" + patient.Attribute("name").Value;
                        string IdPath = BasePath + "\\user-profile\\";
                        string DataPath = BasePath + "\\user_detail\\";
                        string filePath = DataPath +"heart_rate-"+ dateString+ ".json";

                        //This If condition check if the Directory with the name of Patient exists 
                        // If the Condition is true means new Patient and it will the Directory with the 
                        // name of patient, userProfile, User-details
                        if (!Directory.Exists(BasePath))
                        {
                            Directory.CreateDirectory(BasePath);
                            Directory.CreateDirectory(IdPath);
                            Directory.CreateDirectory(DataPath);
                    
                            string json = JsonConvert.SerializeObject(patientInfo);
                            File.WriteAllText(IdPath + "\\user-profile.json", json);
                    
                            patientDetails.Add(pd);
                            string json2 = JsonConvert.SerializeObject(patientDetails);
                            File.WriteAllText(filePath, json2);
                        }
                        // Check if the Inputfile Exists inside the Patient folder 
                        // This Condition mean we the Directory aswell as the file exits of the patient 
                        // exist and we need to append new data with the current contents of the file
                        else if (File.Exists(filePath))
                        {
                            string json = File.ReadAllText(filePath );
                            patientDetails = JsonConvert.DeserializeObject<List<PatientDetails>>(json);
                            patientDetails.Add(pd);
                            json = JsonConvert.SerializeObject(patientDetails);
                            File.WriteAllText(filePath, json);
                        }
                        // If the Patient directory exist but the file does not exist means The patient has 
                        // Entered record for new Date therefore no need to append just Write to file directly 
                        else if (!File.Exists(filePath))
                        {
                            patientDetails.Add(pd);
                            string json = JsonConvert.SerializeObject(patientDetails);
                            File.WriteAllText(filePath, json);
                        }
                    }
                    currentNode++;
                }

                // This If Condition compares the file Date with current date. 
                // It creates a Key Entry inside the Registry Editior that is used to store User preferences in general
                // It will set the value to the LastNode processed by the service so  next time the service runs it fetchs 
                // the Values of LastNode and then process it form there
                if (dateString.Equals(DateTime.Now.ToString("yyyy-MM-dd"))) { 
                    RegistryKey newKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\IPTAssignmentSettings\Question2");
                    newKey.SetValue("value", patients.Count());
                    newKey.Close();
                }
                else
                {
                    RegistryKey newKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\IPTAssignmentSettings\Question2");
                    newKey.SetValue("value", 0);
                    newKey.Close();
                }
            }
            catch(Exception x)
            {
                myLog.WriteEntry(x.ToString());
            }
        }
        
        //This Function takes filename Input and Calculates the differece of LastWriteTime and Current time 
        //If the Difference is less then 5 minutes means the file have been modified since the last Time service ran
        // Therefore it needs to be processed
        public bool isFileUpdatedSinceLastRead(string x)
        {
            DateTime date1 = File.GetLastWriteTime(x);
            DateTime date2 = DateTime.Now;
            TimeSpan dt = date2 - date1;

            if (dt.TotalMinutes < 5)
            {
                return true;
            }
            return false;
        }

        //This Function will return the Index of the Last Node that was process by service 
        public int GetLastElementReadIndex()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\IPTAssignmentSettings\Question2");
            int value;
            if (key != null)
            {
                value = (int)key.GetValue("value");
                key.Close();
            }
            else
            {
                RegistryKey newKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\IPTAssignmentSettings\Question2");
                newKey.SetValue("value", patients.Count());
                value = 0;
                newKey.Close();
            }
            return value;
        }

        //Takes input String DateOfBirth converts to DateFormat and Calculates/Returns the Age of Patient
        public int CalculateAge(string dob)
        {
            DateTime date = DateTime.ParseExact(dob, "dd/MM/yyyy",System.Globalization.CultureInfo.InvariantCulture);
            return (DateTime.Now.Year - date.Year);
        }

    }

}
