using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Lab2Question3
{
    
    public class HelperClass
    {
        private int _value;
        private int _startIndex;
        private int _lastIndex;
        private string[] lines;
        private int _threadIndex;
        public SearchArrayCallback _searchArrayCallback;
        public HelperClass(int value, int threadIndex, int startIndex, int lastIndex, SearchArrayCallback s)
        {
            string textFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Values.txt");

            _value = value;
            _threadIndex = threadIndex;
            _startIndex = startIndex;
            _lastIndex = lastIndex;
            _searchArrayCallback = s;
            lines = File.ReadAllLines(textFile);
            
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void searchArray()
        {
            bool isFound = false;
            for (int i = _startIndex; i < _lastIndex; i++)
            {
                if (lines[i].Equals(Convert.ToString(_value)))
                {
                    isFound = true;
                    _searchArrayCallback(_threadIndex, i);
                }
            }
            if (!isFound)
            {
                _searchArrayCallback(_threadIndex, -1);
            }
        }
        
    
    }

}
