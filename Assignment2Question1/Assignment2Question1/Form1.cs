using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Assignment2Question1
{
    public partial class Form1 : Form
    {
        Patient p = new Patient();

        string path = ConfigurationManager.AppSettings["Path"];
        string todayDate = DateTime.Now.ToString("yyyy_MM_dd");

        string fileLocation;
        public Form1()
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            fileLocation = path + "\\PatientDetails_" + todayDate+".xml";
            if (!File.Exists(fileLocation))
            {
                Console.WriteLine("File does not exists...");
                init_xml();
            }
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AllFeildEntered())
            {
                if (!DateTime.Now.ToString("yyyy_MM_dd").Equals(todayDate))
                {
                    todayDate = DateTime.Now.ToString("yyyy_MM_dd");
                    fileLocation = path + "PatientDetails_" + todayDate + ".xml";
                    init_xml();
                }
                patientName_editbox.Text = Regex.Replace(patientName_editbox.Text, @"\s", "");
                
                string gender = (genderMale.Checked) ? "Male" : "Female";
                string dob = Convert.ToDateTime(patient_DOB.Text).ToString("dd/MM/yyyy");
                
                XDocument xmlDocument = XDocument.Load(fileLocation);
                xmlDocument.Element("Patients").Add(
                    new XElement("Patient", new XAttribute("name", patientName_editbox.Text),
                    new XAttribute("email", email_editbox.Text),
                    new XAttribute("DOB",dob),
                    new XAttribute("gender", gender),
                    new XElement("bpm", bpm_text.Text),
                    new XElement("time", DateTime.Now.ToString("HH:mm:ss")),
                    new XElement("confidence", 0)
                ));
                xmlDocument.Save(fileLocation);
                MessageBox.Show("Submitted");
                bpm_text.Text = "";
            }
        }
        public void init_xml()
        {

            XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8","yes"),
                new XElement("Patients")
            );
            xmlDocument.Save(fileLocation);
        }
        private bool AllFeildEntered()
        {
            bool flag = false;
            if (patientName_editbox.Text == string.Empty)
                errorProvider1.SetError(patientName_editbox, "Please dont Leave this feild empty !");
            else if (email_editbox.Text == string.Empty)
                errorProvider2.SetError(email_editbox, "Please dont Leave this feild empty !");
            else if (bpm_text.Text == string.Empty)
                errorProvider3.SetError(bpm_text, "Please dont Leave this feild empty !");
            else if (!IsValid(email_editbox.Text.ToString()))
                errorProvider4.SetError(email_editbox, "Mazrat! Invalid Format of email");
            else
            {
                errorProvider1.Clear();
                errorProvider2.Clear();
                errorProvider3.Clear();
                errorProvider4.Clear();
                flag = true;
            }
            return flag;
        }
        private void reset_Click(object sender, EventArgs e)
        {
            patientName_editbox.Text = "";
            email_editbox.Text = "";
            patient_DOB.ResetText();
            bpm_text.Text = "";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }
        private bool IsValid(string emailaddress)
        {
            try{
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException){
                return false;
            }
        }
    }
}
