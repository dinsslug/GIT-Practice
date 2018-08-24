using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class MemoryTest
    {
        public event EventHandler<EventArgs> Changed;

        public string text;

        public bool stop = false;

        public MemoryTest()
        {
        }

        public void WriteLine(string value)
        {
            text += value + "\r\n";
            //if (stop == true) return;
            if (Changed != null) Changed(this, new EventArgs());
        }

        public void Run()
        {
            for (int i = 0; i < 100000000; i++)
            {
                WriteLine("STRING " + i);
            }
        }
    }
}
