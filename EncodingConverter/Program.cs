using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncodingConverter
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new AEC.FormMain(args));
            Application.Run(new AEC.FormMain());
            //Application.Run(new Form1());
        }

        public static System.Resources.ResourceManager ResourceManager { get { return Properties.Resources.ResourceManager; } }
        public static Properties.Settings Settings { get { return Properties.Settings.Default; } }


    }
}
