using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab2Question2
{
    public class PartB
    {
        public volatile int threadCounter = 5;
        private int _value;
        private int _startIndex;
        private int _lastIndex;
        private string[] lines;
        private int _threadIndex;
        private Stopwatch currentThreadExecutionTime;
        

        public SearchArrayCallback _searchArrayCallback;
        public PartB(int value,int threadIndex, int startIndex, int lastIndex, SearchArrayCallback s,Stopwatch stopwatch)
        {
            string textFile = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName, "Values.txt");
            
            _value = value;
            _threadIndex = threadIndex;
            _startIndex = startIndex;
            _lastIndex = lastIndex;
            _searchArrayCallback = s;
            lines = File.ReadAllLines(textFile);
            currentThreadExecutionTime = stopwatch;
            currentThreadExecutionTime.Start();
        }
        public void searchArray(){
            Interlocked.Decrement(ref threadCounter);
            bool isFound = false;
            for (int i = _startIndex; i < _lastIndex; i++) { 
                if (lines[i].Equals(Convert.ToString(_value))) { 
                    isFound = true;
                    currentThreadExecutionTime.Stop();
                    _searchArrayCallback(_threadIndex,i,currentThreadExecutionTime);
                }
            }
            if (!isFound) {
                currentThreadExecutionTime.Stop();
                _searchArrayCallback(_threadIndex,-1,currentThreadExecutionTime);
            }
        }
    }

}
