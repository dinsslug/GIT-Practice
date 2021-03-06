﻿using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfApplication1.Engine;
using WpfApplication1.Utility;

namespace WpfApplication1
{
    public class VMMainWindow : ModelBase
    {
        private string text = "";

        public string Text { get { return text; } set {
                text = value;
                if (count >= 1000)
                {
                    AcScrollToEnd.Invoke();
                    OnPropertyChanged("Text");
                    count = 0;
                }
                count++;
            }
        }

        public MemoryStream Stream;
        public ConsoleTest Test;
        public BackgroundWorker worker = new BackgroundWorker();

        public RelayCommand RcExecute { get; set; }
        public RelayCommand RcCancel { get; set; }
        public RelayCommand RcSaveJson { get; set; }
        public RelayCommand RcReadJson { get; set; }

        public Action AcScrollToEnd;
        public int count = 0;

        public JsonTest JsonTest = new JsonTest();

        public void OnExecute(object param)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "ConsoleApplication1.exe";
            /*
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            */

            using (var process = new Process())
            {
                process.StartInfo = startInfo;

                process.Start();
                worker.RunWorkerAsync();

                Task.Factory.StartNew(() =>
                {
                    /*
                    var process = new Process();
                    process.StartInfo.FileName = "TestApp.exe";
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.Start();

                    Application.Current.Dispatcher.Invoke(() => Text += process.StandardOutput.ReadToEnd());

                    process.WaitForExit();
                    */
                });
            }
        }

        public void OnCancel(object param)
        {
            worker.CancelAsync();
        }

        public void OnSaveJson(object param)
        {
            JsonTest.Save();
        }

        public void OnReadJson(object param)
        {
            JsonTest.Read();
        }

        public void OnWorkerChanged(object sender, EventArgs args)
        {
            Text += Test.ConsoleText;
        }

        public async void MyMethod()
        {
            using (var sr = new StreamReader(Stream))
            {
                string line;

                while((line = await sr.ReadLineAsync()) != "END")
                {
                }
            }
        }

        public VMMainWindow()
        {
            RcExecute = new RelayCommand(OnExecute);
            RcCancel = new RelayCommand(OnCancel);
            RcSaveJson = new RelayCommand(OnSaveJson);
            RcReadJson = new RelayCommand(OnReadJson);

            Test = new ConsoleTest();
            Test.ConsoleChanged += OnWorkerChanged;

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.WorkerSupportsCancellation = true;
        }

        public void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Test.Run();
        }
    }

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public VMMainWindow ViewModel;

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new VMMainWindow();
            ViewModel.AcScrollToEnd = OnScrollToEnd;

            DataContext = ViewModel;
        }

        public void OnScrollToEnd()
        {
            Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
            {
                tb.Focus();
                tb.CaretIndex = tb.Text.Length;
                tb.ScrollToEnd();
            }));
        }
    }
}
