using EncodingConverter.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            //AllocConsole();
            try
            {
                args.ProcessCommandLine(Program.GetCommands, new ShowUICommand());
                return;
            }
            catch (Exception ex)
            {
                ex.WriteToTrace();
                throw ex;
            }

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
                var commands = new List<ICommandLineCommand>();
                commands.Add(new QuestionmarkCommand());
                commands.Add(new HelpCommand());
                commands.Add(new ConvertCommand());
                commands.Add(new ShowUICommand());
                commands.Add(new ConsoleCommand());

                _Commands = commands.ToArray();
                //int index = 0;
                //_Commands[index++] = new ConvertCommand();
                //_Commands[index++] = new ShowUICommand();
                //_Commands[index++] = new ConsoleCommand();
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
        //public static System.Resources.ResourceManager ResourceManager { get { return Properties.Resources.ResourceManager; } }
        public static Properties.Settings Settings { get { return Properties.Settings.Default; } }

        public static EncodingConverterCore ECC { get { return _ECC; } }

        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool AllocConsole();

    }
}
