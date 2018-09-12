using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Test
    {
        public event EventHandler<EventArgs> ConsoleChanged;
        public string ConsoleText;

        public void WriteLine(string value)
        {
            ConsoleText = value + "\r\n";
            if (ConsoleChanged != null) ConsoleChanged(this, new EventArgs());
        }

        public void Run()
        {
            for (int i = 0; i < 100000; i++)
            {
                WriteLine("STRING " + i);
            }
        }
    }
}
