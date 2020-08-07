using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Queston4A
{
    class PatientInfo
    {
        public string Patient_name { get; set; }
        public int age { get; set; }
        public string Patient_email { get; set; }
        public string gender { get; set; }

        public PatientInfo(string name, int age, string email, string gen)
        {
            Patient_name = name;
            this.age = age;
            Patient_email = email;
            gender = gen;
        }
        public PatientInfo()
        {

        }

    }
}
