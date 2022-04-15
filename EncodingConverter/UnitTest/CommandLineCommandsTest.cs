using EncodingConverter.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

#if DEBUG == true
namespace EncodingConverter.UnitTest
{
    public class CommandTestWrapper : ICommandLineCommand
    {
        protected ICommandLineCommand _command;
        public string Name => _command.Name;

        public string ShortDescription => _command.ShortDescription;

        public string LongDescription => _command.LongDescription;

        public int Execute(string[] args, int startingArgIndex) => _command.Execute(args, startingArgIndex);

        
        public EncodingConverterCore ECC { get => Program.ECC; }

        public const char DataSeparationChar = CommandLine.CLARG_DataSeparator;
    }

    public class ShowUICommandTest : CommandTestWrapper
    {
        public ShowUICommandTest()
        {
            _command = new ShowUICommand();
        }

        public static string SwitchCmdName { get => ShowUICommand.CL_Name; }

        public static string SwitchForm { get => ShowUICommand.CLARG_FORM; }
        public static string SwitchInputEncoding { get => ShowUICommand.CLARG_InputEncoding; }
        public static string SwitchOutputEncoding { get => ShowUICommand.CLARG_OutputEncoding; }
    }

    public class ConvertCommandTest : CommandTestWrapper
    {
        public ConvertCommandTest()
        {
            _command = new ConvertCommand();
        }

        public static string SwitchCmdName { get => ConvertCommand.CLARG_Name; }

        public static string SwitchAutoDetectEncoding { get => ConvertCommand.CLARG_AutoDetectEncoding; }
        public static string SwitchInputEncoding { get => ConvertCommand.CLARG_InputEncoding; }
        public static string SwitchPrefferedInputEncoding { get => ConvertCommand.CLARG_PreferredEncoding; }
        public static string SwitchOutputEncoding { get => ConvertCommand.CLARG_OutputEncoding; }
    }

}
#endif