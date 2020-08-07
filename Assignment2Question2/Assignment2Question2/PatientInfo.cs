using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Question2
{
    [Serializable]
    public class PatientInfo
    {
        public string Patient_name { get; set; }
        public int age { get; set; }
        public string Patient_email { get; set; }
        public string gender { get; set; }

    }
}
