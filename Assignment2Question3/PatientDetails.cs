using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Question3
{
    class PatientDetails
    {
        public string dateTime { set; get; }
        public Value value;
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
