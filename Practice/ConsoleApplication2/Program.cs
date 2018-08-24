using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        public static Test Test;
        public static BackgroundWorker worker = new BackgroundWorker();

        public static void OnWorkerChanged(object sender, EventArgs args)
        {
            Console.WriteLine(Test.ConsoleText);
        }

        public static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Test.Run();
        }

        static void Main(string[] args)
        {
            Test = new Test();
            Test.ConsoleChanged += OnWorkerChanged;

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.WorkerSupportsCancellation = true;

            worker.RunWorkerAsync();

            Console.ReadKey(true);
        }
    }
}
