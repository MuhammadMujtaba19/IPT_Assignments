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
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

namespace Assignment2Queston4A
{
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer timeDelay;
        EventLog myLog;
        
        string BaseDirectory, UserChart, ConsolidatedChart, OutputDirectory;
        List<PatientDetails> patientDetails;
        PatientInfo patientInfo;
        string[] directoryEntries;
        RegistryKey updatedKey;
        
        public Service1()
        {
            InitializeComponent();
            timeDelay = new Timer();
            timeDelay.Elapsed += new ElapsedEventHandler(CheckUpdates);
            timeDelay.Interval = 60000 * 15;
            BaseDirectory = ConfigurationManager.AppSettings["BasePath"];
            
            OutputDirectory = ConfigurationManager.AppSettings["OutputPath"]; 
            this.AutoLog = false;
            myLog = new EventLog();
            myLog.Source = "Assignment-Question4";

        }

        protected override void OnStart(string[] args)
        {
            myLog.WriteEntry("OnStart Question4A");
            timeDelay.Enabled = true;
            updatedKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\IPTAssignmentSettings\Question4");

            if (!Directory.Exists(OutputDirectory)) { 
                Directory.CreateDirectory(OutputDirectory);
                UserChart = OutputDirectory + "\\UserChart.xml";
                ConsolidatedChart = OutputDirectory + "\\ConsolidatedChart.xml";
                init_xml();
                
                directoryEntries = Directory.GetDirectories(BaseDirectory);

                foreach (string directory in directoryEntries)
                {
                    string[] files = Directory.GetFiles(directory);
                    String x = directory + "//user-profile//user-profile.json";

                    string jsonText = File.ReadAllText(files[0]);
                    patientDetails = JsonConvert.DeserializeObject<List<PatientDetails>>(jsonText);

                    jsonText = File.ReadAllText(x);
                    patientInfo = JsonConvert.DeserializeObject<PatientInfo>(jsonText);

                    GenerateUserChartXML();
                    GenerateConsolidatedChartXML();
            }
        }
    }

        protected override void OnStop()
        {
            myLog.WriteEntry("OnStop Question4A");
            timeDelay.Enabled = false;
        }
        public void CheckUpdates(object sender, ElapsedEventArgs e)
        {
            string[] directoryEntries = Directory.GetDirectories(BaseDirectory);
            foreach (string directory in directoryEntries)
            {
                myLog.WriteEntry(directory);
                string[] files = Directory.GetFiles(directory);
                string x = directory + "//user-profile//user-profile.json";

                DateTime date1 = File.GetLastWriteTime(files[0]);
                DateTime date2 = DateTime.Now;
                TimeSpan dt = date2 - date1;
                if (dt.TotalMinutes > 15) continue;

                string jsonText = File.ReadAllText(files[0]);
                patientDetails = JsonConvert.DeserializeObject<List<PatientDetails>>(jsonText);
                
                jsonText = File.ReadAllText(x);
                patientInfo = JsonConvert.DeserializeObject<PatientInfo>(jsonText);

                GenerateUserChartXML();
                GenerateConsolidatedChartXML();
            
            }
        }
        public void init_xml()
        {

            XDocument userChartXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Users")
            );
            userChartXML.Save(UserChart);

            XDocument consolidatedChartXML = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Patients")
            );
            consolidatedChartXML.Save(ConsolidatedChart);

        }
        public void GenerateUserChartXML()
        {
            var values = CheckIfUserExistIntheXML(patientInfo.Patient_name);
            int max = values.Item2;
            int min = values.Item3;
            int total = values.Item4;
            

            foreach (PatientDetails pd in patientDetails)
            {
                if (pd.value.bpm > max)
                    max = pd.value.bpm;
                if (pd.value.bpm < min)
                    min = pd.value.bpm;
                total = pd.value.bpm + total;
            }
            if (!values.Item1)
            {
                myLog.WriteEntry("if condition" + values.Item1);
                AddNewUserNode(max, min, total);
            }else
            {
                UpdateCurrentUserNode(max, min, total);
                }
            
        }

        public void GenerateConsolidatedChartXML()
        {
            int age = patientInfo.age;
            int ageGroup = ReturnAgeGroup(age);
            var values = ReturnAgeGroupMinMax(ageGroup);

            int max = values.Item2;
            int min = values.Item3;
            int total = values.Item4;
            int count = values.Item5;
            int temp = 0;
            count++;
            
            foreach (PatientDetails pd in patientDetails)
            {
                if (pd.value.bpm > max)
                    max = pd.value.bpm;
                if (pd.value.bpm < min)
                    min = pd.value.bpm;
                temp = temp+ pd.value.bpm;
            }
            temp = temp / patientDetails.Count;
            total = total + temp;
            if (!values.Item1)
            {
                AddNewAgeGroupNode(ageGroup, max, min, total, count);
            }
            else
            {
                UpdateExistingAgeGroupNode(ageGroup, max, min, total, count);
            }
        }
        public int ReturnAgeGroup(int age)
        {
            int ageGroup = 0;
            if (age > 0 && age <= 10)
                ageGroup = 1;
            else if (age > 10 && age <= 20)
                ageGroup = 2;
            else if (age > 20 && age <= 30)
                ageGroup = 3;
            else if (age > 30 && age <= 40)
                ageGroup = 4;
            else if (age > 40 && age <= 50)
                ageGroup = 5;
            else if (age > 50 && age <= 60)
                ageGroup = 6;
            else if (age > 60 && age <= 70)
                ageGroup = 7;
            else if (age > 70 && age <= 80)
                ageGroup = 8;

            return ageGroup;
        }
        
        public Tuple<bool, int, int, int, int> ReturnAgeGroupMinMax(int ageGroup)
        {
            try
            {
                XElement Group = (from patients in XDocument.Load(ConsolidatedChart)
                                                .Descendants("AgeGroup")
                                  where (int)patients.Attribute("value") == ageGroup
                                  select patients).First();
                int max = (int)Group.Element("High");
                int min = (int)Group.Element("Low");
                int avg = (int)Group.Element("Average");
                int count = (int)Group.Element("Count");
                return Tuple.Create(true, max, min, avg, count);
            }
            catch
            {
                return Tuple.Create(false, -1, 1000, 0, 0);
            }
        }
        public void AddNewAgeGroupNode(int ageGroup, int max, int min, int avg, int count)
        {
            XDocument xmlDocument = XDocument.Load(ConsolidatedChart);
            xmlDocument.Element("Patients").Add(
                new XElement("AgeGroup", new XAttribute("value", ageGroup),
                new XElement("High", max),
                new XElement("Average", avg/count),
                new XElement("Low", min),
                new XElement("Count", count)
            ));
            xmlDocument.Save(ConsolidatedChart);
        }
        public void UpdateExistingAgeGroupNode(int ageGroup, int max, int min, int avg, int count)
        {
            XDocument xmlDocument = XDocument.Load(ConsolidatedChart);
            XElement ageGroups = (from patients in xmlDocument
                                           .Descendants("AgeGroup")
                                  where (int)patients.Attribute("value") == ageGroup
                                  select patients).FirstOrDefault();

            ageGroups.Element("High").Value = max.ToString();
            ageGroups.Element("Low").Value = min.ToString();
            ageGroups.Element("Average").Value = (avg/count).ToString();
            ageGroups.Element("Count").Value = count.ToString();

            xmlDocument.Save(ConsolidatedChart);

        }
        
        public Tuple<bool,int,int,int> CheckIfUserExistIntheXML(string name)
        {
            try { 
                XElement user = (from Users in XDocument.Load(UserChart)
                                                 .Descendants("User")
                             where (string)Users.Attribute("name") == name
                             select Users).First();

                int max = (int)user.Element("High");
                int min = (int)user.Element("Low");
                int avg = (int)user.Element("Average");
                return Tuple.Create(true, max, min, avg);
            }
            catch(Exception e )
            {
                myLog.WriteEntry(e.Message);
                return Tuple.Create(false, -1, 1000, 0);
            }
        }

        public void AddNewUserNode(int max, int min,int total)
        {
            
            XDocument xmlDocument = XDocument.Load(UserChart);
            xmlDocument.Element("Users").Add(
                new XElement("User", new XAttribute("name", patientInfo.Patient_name),
                new XAttribute("email", patientInfo.Patient_email),
                new XElement("High", max),
                new XElement("Average", total / patientDetails.Count),
                new XElement("Low", min),
                new XElement("Range", calculateRange(patientInfo.age))
            ));
            xmlDocument.Save(UserChart);
        }
        public void UpdateCurrentUserNode(int max,int min,int total)
        {
            XDocument xmlDocument = XDocument.Load(UserChart);
           IEnumerable<XElement> users = (from Users in xmlDocument
                                               .Descendants("User")
                             //where (string)Users.Attribute("name") == patientInfo.Patient_name
                             select Users);
            for (int i = 0; i < users.Count(); i++)
            {
                XElement user = users.ElementAt(i);
                if ((string)user.Attribute("name") == patientInfo.Patient_name)
                {
                    user.Element("High").Value = max.ToString();
                    user.Element("Low").Value = min.ToString();
                    user.Element("Average").Value = (total / patientDetails.Count).ToString();
                    updatedKey.SetValue("value", i);
                }
            }
            xmlDocument.Save(UserChart);
        }
        public string calculateRange(int age)
        {
            double Range = (200 - age);
            double highRange = Range * 0.85;
            double lowRange = Range * 0.50;
            highRange=Math.Round(highRange);
            lowRange =Math.Round(lowRange);

            return highRange.ToString()+"--"+lowRange.ToString();
        }
    }
}
