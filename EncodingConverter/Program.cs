using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncodingConverter
{
    static class Program
    {
        static EncodingConverterCore _ECC = new EncodingConverterCore();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            _ECC.ProcessCommandLine(args);
            if (_ECC.CommandLineCommand != null)
            {
                _ECC.CommandLineCommand();
                return;
            }

            StartUI();
        }

        static void StartUI()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new AEC.FormMain(args));
            Application.Run(new Forms.FormMain2());
            //Application.Run(new Form1());
        }
        public static System.Resources.ResourceManager ResourceManager { get { return Properties.Resources.ResourceManager; } }
        public static Properties.Settings Settings { get { return Properties.Settings.Default; } }

        public static EncodingConverterCore ECC { get { return _ECC; } }

    }
}
