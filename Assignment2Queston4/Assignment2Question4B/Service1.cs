using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Net.Mail;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Microsoft.Win32;

namespace Assignment2Question4B
{
    public partial class Service1 : ServiceBase
    {
        System.Timers.Timer timeDelay;
        EventLog myLog;
        string BaseDirectory, UserChart;
        SmtpClient client = new SmtpClient();
        MailMessage mail = new MailMessage();
        RegistryKey updatedKey;

        public Service1()
        {
            InitializeComponent();
            timeDelay = new System.Timers.Timer();
            timeDelay.Elapsed += new System.Timers.ElapsedEventHandler(CheckUpdates);
            timeDelay.Interval = 60000 * 15;
            BaseDirectory = ConfigurationManager.AppSettings["BasePath"];

            this.AutoLog = false;
            myLog = new EventLog();
            myLog.Source = "Assignment-Question4B";


        }

        protected override void OnStart(string[] args)
        {
            myLog.WriteEntry("OnStart Question4B");
            timeDelay.Enabled = true;
            UserChart = BaseDirectory + "\\UserChart.xml";
            
            string email = ConfigurationManager.AppSettings["EmailID"];
            string pass = ConfigurationManager.AppSettings["Password"];

            client.Port = 587;
            client.Host = "smtp.gmail.com"; //for gmail host  
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(email,pass);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            
            mail.From = new MailAddress("k163863@nu.edu.com");
            mail.Subject = "Your Value is Updated in UserChart.xml";

        }

        protected override void OnStop()
        {
            myLog.WriteEntry("OnStop Question 4B");
            timeDelay.Enabled = false;
        }
        public void CheckUpdates(object sender, ElapsedEventArgs e)
        {
            DateTime date1 = File.GetLastWriteTime(UserChart);
            DateTime date2 = DateTime.Now;
            TimeSpan dt = date2 - date1;
            if (dt.TotalMinutes < 15.2)
            {
                RegistryKey newKey = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\IPTAssignmentSettings\Question4");
                int value;
                if (newKey != null)
                {
                    value = (int)newKey.GetValue("value");
                    newKey.Close();
                   string email =  GetEmailAddresses(value);
                    string Text = File.ReadAllText(UserChart);
                    SendEmail(email,Text);
                }
            }
        }
        public string GetEmailAddresses(int value)
        {
           
            IEnumerable<XElement> users = (from Users in XDocument.Load(UserChart)
                                                  .Descendants("User")
                                   select Users);
           
            string emailAddress = (string)users.ElementAt(value).Attribute("email");
            myLog.WriteEntry(emailAddress);
            return emailAddress;
        }
        public void SendEmail(string recvEmail,string text )
        {
            //foreach (string x in recvEmail)
                mail.To.Add(recvEmail);
            mail.Body = text;
            client.Send(mail);
        }
    }
}
