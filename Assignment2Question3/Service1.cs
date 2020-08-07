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

namespace Assignment2Question3
{
    public partial class Service1 : ServiceBase
    {
        Timer timeDelay;
        EventLog myLog;  //For logging custom output in Event Viewer 
        string InputDirectory;
        List<PatientDetails> tempPatientDetails;
        List<PatientDetails> patientDetails;
       
        //The globalKey Registry is declared in global Scope so all the methods of the class can access it and read/write the 
        // Values inside the Register using the refernce initialied in this variable (i.e @"SOFTWARE\IPTAssignmentSettings\Question3")
        // for e.g Mujtaba --  2 | Mustafa -- 4  | Murtaza -- 3
        RegistryKey globalKey;

        public Service1()
        {
            InitializeComponent();
            timeDelay = new System.Timers.Timer();
            timeDelay.Elapsed += new System.Timers.ElapsedEventHandler(CheckUpdates);
            timeDelay.Interval = 60000 *10;
            
            InputDirectory  = ConfigurationManager.AppSettings["InputPath"];

            this.AutoLog = false;
            myLog = new EventLog();
            myLog.Source = "Assignment-Question3";

        }

        //The Onstart Function is wrapped inside a Try Catch block since if the input directory does not exist 
        //when the Service is started it would throw an exception and would run the service anyway without terminating it 
        
        protected override void OnStart(string[] args)
        {
            myLog.WriteEntry("OnStart Question3");
            timeDelay.Enabled = true;
            globalKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\IPTAssignmentSettings\Question3");
            
            try
            { 
                string[] directoryEntries = Directory.GetDirectories(InputDirectory);

                foreach (string directory in directoryEntries)
                {
                    //Get the Name of Folder so it could be used as a Value in the Key 
                    string name = new DirectoryInfo(directory).Name; 
                    globalKey.SetValue(name, 0);
                    string inputPath = directory + "\\user_detail\\";
                    string outputPath = directory + "\\heart_rate-Consolidated.json";
                    ConsolidateMultipleJsonIntoSingle(name,inputPath, outputPath);
                }
            }catch(Exception exception)
            {
                myLog.WriteEntry("Started with error");
                myLog.WriteEntry(exception.Message);
            }
        }

        protected override void OnStop()
        {
            myLog.WriteEntry("OnStop Question3");
            timeDelay.Enabled = false;
            globalKey.Close();

        }

        //Gets called every 10mins after the service is started means before reaching this function we already have 
        //Consolidated all the possible files of previous dates of every directories inside the InputDirectory(InputPath in app.config)
        // Therefore here we need to only Watch the Current Date files and incase of any modification we need to process it
        public void CheckUpdates(object sender, ElapsedEventArgs e)
        {
            try { 
            String[] directoryEntries = Directory.GetDirectories(InputDirectory);

            foreach (string directory in directoryEntries)
            {
                string inputPath = directory + "\\user_detail\\heart_rate-"+DateTime.Now.ToString("yyyy-MM-dd")+".json";
                string outputPath = directory + "\\heart_rate-consolidated.json";
                
                string name = new DirectoryInfo(directory).Name;

                DateTime date1 = File.GetLastWriteTime(inputPath);
                DateTime date2 = DateTime.Now;
                TimeSpan dt = date2 - date1;

                patientDetails = new List<PatientDetails>();
                tempPatientDetails = new List<PatientDetails>();
                if (dt.TotalMinutes < 10)
                {
                    myLog.WriteEntry("File Modified at " + date1);
                    updateSingleFile(name,inputPath, outputPath);
                }
                else
                {
                    string json = File.ReadAllText(outputPath);
                    tempPatientDetails = JsonConvert.DeserializeObject<List<PatientDetails>>(json);
                    //flag = true;
                }
            }
            }catch (Exception exception)
            {
                myLog.WriteEntry(exception.Message);
            }
        }
       
        //Takes inputFilePath and Fetchs all the Files inside the Directory and then loops over each Individual file and invokes the 
        //updateSingleFile Function along with OutputFilePath where the Consolidated File is created 
        public void ConsolidateMultipleJsonIntoSingle(string name,string inputFilePath, string outputFilePath)
         {
            string[] inputFilePaths = Directory.GetFiles(inputFilePath);

            patientDetails = new List<PatientDetails>();
            tempPatientDetails= new List<PatientDetails>();
            foreach (String singleFile in inputFilePaths)
            {
                updateSingleFile(name,singleFile, outputFilePath);
            }

        }


        void updateSingleFile(string name, string singleFile, string outputFilePath)
        {
            string fileName = Path.GetFileName(singleFile);
            
            //Deserialize the file to List of Patient Details 
            string json = File.ReadAllText(singleFile);
            patientDetails = JsonConvert.DeserializeObject<List<PatientDetails>>(json);

            // If the Consolidated Json file exist we deserialize them inside tempPatientDetails List
            // and new values are appended to this List 
            if (File.Exists(outputFilePath))
            {
                json = File.ReadAllText(outputFilePath);
                tempPatientDetails = JsonConvert.DeserializeObject<List<PatientDetails>>(json);
            }
            
           // Checks if the singleFile recieved through argument is File of Current Date 
           // If True means we need to Fetch the Value from globalKey of the Index that was processed Last 
          // after completion the value of GlobalKey is update with Count of elements in the patientDetails list

            if (fileName.Equals("heart_rate-" + DateTime.Now.ToString("yyyy-MM-dd") + ".json"))
            {
                int Index= (int)globalKey.GetValue(name);
                for (int i = Index; i < patientDetails.Count; i++)
                {
                    tempPatientDetails.Add(patientDetails[i]);
                }
                globalKey.SetValue(name, patientDetails.Count);
            }
            // Old Date Files simply Append them to the List 
            else
            {
                foreach (PatientDetails pd in patientDetails)
                {
                    tempPatientDetails.Add(pd);
                }
            }
            string json1 = JsonConvert.SerializeObject(tempPatientDetails, Formatting.Indented);
            File.WriteAllText(outputFilePath, json1);
        }
    }
}

