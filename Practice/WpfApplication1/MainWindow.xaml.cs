using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WpfApplication1.Engine;

namespace WpfApplication1
{
    public class VMMainWindow : ModelBase
    {
        private string text = "";

        public string Text { get { return text; } set { text = value; OnPropertyChanged("Text"); } }

        public RelayCommand RcExecute { get; set; }

        public void OnExecute(object param)
        {
            var startInfo = new ProcessStartInfo();
            process.StartInfo.FileName = "TestApp.exe";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            while (!process.HasExited)
            {
                Text += process.StandardOutput.ReadToEnd();
            }
        }

        public VMMainWindow()
        {
            RcExecute = new RelayCommand(OnExecute);    
        }
    }

    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new VMMainWindow();
        }
    }
}
