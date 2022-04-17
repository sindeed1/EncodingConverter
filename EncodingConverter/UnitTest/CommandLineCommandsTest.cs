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

        public const string SwitchCmdName = ShowUICommand.CL_Name;

        public const string SwitchForm = ShowUICommand.CLARG_FORM;
        public const string SwitchInputEncoding = ShowUICommand.CLARG_InputEncoding;
        public const string SwitchOutputEncoding = ShowUICommand.CLARG_OutputEncoding;
    }

    public class ConvertCommandTest : CommandTestWrapper
    {
        public ConvertCommandTest()
        {
            _command = new ConvertCommand();
        }

        public OutputPathFormatter OPF { get => ((ConvertCommand)base._command)._OPF; }

        public const string SwitchCmdName = ConvertCommand.CLARG_Name;

        public const string Overwrite = ConvertCommand.CLARG_Overwrite;
        public const string SwitchAutoDetectEncoding = ConvertCommand.CLARG_AutoDetectEncoding;
        public const string SwitchInputEncoding = ConvertCommand.CLARG_InputEncoding;
        public const string SwitchPrefferedInputEncoding = ConvertCommand.CLARG_PreferredEncoding;
        public const string SwitchOutputEncoding = ConvertCommand.CLARG_OutputEncoding;
        public const string SwitchOutputPathFormat = ConvertCommand.CLARG_OutputPathFormat;
        public const string SwitchCompanionFile = ConvertCommand.CLARG_CompFile;
        public const string SwitchCompanionFileSearchPattern = ConvertCommand.CLARG_CompFileSearchPattern;
    }

}
#endif