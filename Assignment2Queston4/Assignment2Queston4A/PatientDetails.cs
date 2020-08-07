using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2Queston4A
{
    class PatientDetails
    {
        public DateTime date { set; get; }
        public Value value;
        public PatientDetails()
        {
            value = new Value();
        }


    }
    public class Value
    {
        public string confidence { set; get; }
        public int bpm { set; get; }

    }
}