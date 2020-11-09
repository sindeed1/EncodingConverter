using EncodingConverter.Commands;
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
        static ICommandLineCommand[] _Commands;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            args.ProcessCommandLine(Program.GetCommands, new ShowUICommand());
            return;

            _ECC.ProcessCommandLine(args);
            if (_ECC.CommandLineCommand != null)
            {
                _ECC.CommandLineCommand();
                return;
            }

            StartUI();
        }

        public static ICommandLineCommand[] GetCommands()
        {
            if (_Commands == null)
            {
                _Commands = new ICommandLineCommand[3];
                int index = 0;
                _Commands[index++] = new ConvertCommand();
                _Commands[index++] = new ShowUICommand();
                _Commands[index++] = new ConsoleCommand();
            }


            return _Commands;
        }
        static void StartUI()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new AEC.FormMain(args));
            Application.Run(new Forms.MainForm3());
            //Application.Run(new Form1());
        }
        public static System.Resources.ResourceManager ResourceManager { get { return Properties.Resources.ResourceManager; } }
        public static Properties.Settings Settings { get { return Properties.Settings.Default; } }

        public static EncodingConverterCore ECC { get { return _ECC; } }

    }
}
