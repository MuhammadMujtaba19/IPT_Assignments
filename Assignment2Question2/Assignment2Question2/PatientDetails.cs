using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Question2
{
    [Serializable]
    public class PatientDetails
    {
        public string dateTime {get; set; }
        public Value value ;
        public PatientDetails()
        {
            value = new Value();
        }
    }
    public class Value
    {
        public int confidence { set; get; }
        public int bpm { set; get; }

    }
}
