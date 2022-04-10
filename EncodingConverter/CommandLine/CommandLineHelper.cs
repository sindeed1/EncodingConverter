//using EncodingConverter.Commands;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CommandLine;

//namespace EncodingConverter
//{
//    /* Processing command line arguments:
//     * Old implementation:
//     *  It is based on ICommandLineCommand interface, witch has a 'Name' property.
//     *  The logic assumes the following structure of the command line arguments:
//     *  <CommandName>[ arguments]
//     *  The first argument MUST BE the name of the command.
//     *  If the arguments matches the 'Name' property of the command, then the command will be executed.
//     *  Notice here, that the command will then process the rest of the arguments and NOT the first one, witch is always
//     *  assumed to be the name of the command.
//     *  
//     * New Implementation:
//     *  Here, it is based on Func<string[], int, int>.
//     *  string[]: is the arguments-array
//     *  int: is the starting index in the arguments, where the processing must begin.
//     *  Returned int: is the last index in the string[] used while processing the command
//     *      If we passed arguments to a command, but the arguments were not accepted by the command, then the returned
//     *      value must be the 'StartingIndex-1' because the command didn't use any argument from the list.
//     *      This returned value will determine if the command was executed or not.
//     *      
//     *  The advantages of this way is that is allows chaining of commands, if needed.
//     *  We can write multiple commands after one another with all the needed arguments. When a command finishes execution
//     *  and uses certain count of arguments, the rest of the argument will be re-processed as a new command.
//     *  
//     *  The processing of the name of the command rest now on the shoulders of the command itself.
//     *  
//     */
//    /// <summary>
//    /// 
//    /// </summary>
//    static class CommandLineHelper
//    {
//        const string CLARG_SWITCH = "-";

//        //public static bool IsSwitch(this string arg, string switchName) { return arg.Substring(1, switchName.Length) == switchName; }
//        public static bool IsSwitch(this string arg, string switchName) { return arg.StartsWith(switchName); }
//        public static bool IsSwitch(this string arg) { return arg.StartsWith(CLARG_SWITCH); }
//        public static string GetSwitchData(this string arg, string fullSwitchName)
//        {
//            string switchData;
//            //int switchLength = CLARG_SWITCH.Length + switchName.Length;
//            int switchLength = fullSwitchName.Length;
//            switchData = arg.Substring(switchLength, arg.Length - switchLength);
//            return switchData;
//        }
//        public static string GetSwitchData(this string arg, string switchName, char switchDataSeparator)
//        {
//            string switchData;
//            //int switchLength = CLARG_SWITCH.Length + switchName.Length;
//            int switchLength = switchName.Length + 1;
//            switchData = arg.Substring(switchLength, arg.Length - switchLength);
//            return switchData;
//        }

//        /// <summary>
//        /// The new command line argument processor. This function takes arguments of the kine 
//        /// </summary>
//        /// <param name="args"></param>
//        /// <param name="startingArgIndex"></param>
//        /// <param name="switches"></param>
//        /// <param name="defaultSwitch"></param>
//        public static int ProcessCommadLineSwitches(this string[] args, int startingArgIndex, Func<string[], int, int>[] switches, Func<string[], int, int> defaultSwitch)
//        {
//            int switchIndex = 0;
//            return ChainProcessCommandLine(args, startingArgIndex, switches, true, defaultSwitch, out switchIndex);
//        }

//        public static void ProcessCommadLineSwitches(this string[] args, int startingArgIndex, Func<string, bool>[] switches, Func<string, bool> defaultSwitch)
//        {
//            for (int i = startingArgIndex; i < args.Length; i++)
//            {
//                string arg = args[i].ToLower();
//                if (!arg.IsSwitch())
//                {
//                    defaultSwitch?.Invoke(arg);
//                    ////If it's not a switch then it should be the input file:
//                    //this._InputFilePath = arg;
//                    continue;
//                }

//                bool switchFound = false;
//                for (int j = 0; j < switches.Length; j++)
//                {
//                    if (switches[j](arg))
//                    {
//                        switchFound = true;
//                        break;
//                    }
//                }//j _CommandLineSwitches

//                if (!switchFound)
//                    defaultSwitch?.Invoke(arg);
//            }//i args
//        }

//        /// <summary>
//        /// processes and executes command line <paramref name="args"/> using the provided commands from <paramref name="getCommands"/>.
//        /// If execution is not possible, then the it will execute <paramref name="defaultCommand"/>.
//        /// </summary>
//        /// <param name="args">Command line arguments</param>
//        /// <param name="getCommands">Returns an array of <see cref="ICommandLineCommand"/> that represents the possible internal commands.</param>
//        /// <param name="defaultCommand">A default <see cref="ICommandLineCommand"/> to be executed if processing of arguments fails.</param>
//        /// <remarks>Command line is something like this:
//        /// CommadName [args] </remarks>
//        public static bool ProcessCommandLine(this string[] args, Func<ICommandLineCommand[]> getCommands, ICommandLineCommand defaultCommand)
//        {
//            return ProcessCommandLine(args, getCommands, defaultCommand, 0);
//        }
//        /// <summary>
//        /// processes and executes command line <paramref name="args"/> starting at a given <paramref name="startingArgIndex"/> 
//        /// using the provided commands from <paramref name="getCommands"/>.
//        /// If execution is not possible, then the it will execute <paramref name="defaultCommand"/>.
//        /// </summary>
//        /// <param name="args">Command line arguments</param>
//        /// <param name="getCommands">Returns an array of <see cref="ICommandLineCommand"/> that represents the possible internal commands.</param>
//        /// <param name="defaultCommand">A default <see cref="ICommandLineCommand"/> to be executed if processing of arguments fails.</param>
//        /// <param name="startingArgIndex">At this index the processing of arguments will start.</param>
//        /// <returns>True, if processing succeeded; Otherwise returns false.</returns>
//        public static bool ProcessCommandLine(this string[] args, Func<ICommandLineCommand[]> getCommands, ICommandLineCommand defaultCommand, int startingArgIndex)
//        {
//            Trace.TraceInformation("Processing command line...");

//            if (args == null || args.Length <= startingArgIndex)
//            {
//                Console.Write("No command line arguments!");
//                return ProcessDefaultCommand(args, defaultCommand, startingArgIndex);
//            }

//            ICommandLineCommand[] commands;
//            commands = getCommands();

//            if (commands == null || commands.Length <= 0)
//            {
//                Console.Write("Command list not available!");
//                return ProcessDefaultCommand(args, defaultCommand, startingArgIndex);
//            }

//            string commandName = args[startingArgIndex].Trim().ToLower();
//            ICommandLineCommand cmd;
//            cmd = commands.FirstOrDefault(x => x.Name.ToLower() == commandName);
//            if (cmd == null)
//            {
//                return ProcessDefaultCommand(args, defaultCommand, startingArgIndex);
//            }

//            cmd.Execute(args, startingArgIndex + 1);

//            return true;

//        }
//        static bool ProcessDefaultCommand(string[] args, ICommandLineCommand defaultCommand, int startingArgIndex)
//        {
//            //Console.WriteLine("No default command line. Further processing is not possible.");
//            Trace.TraceInformation("Processing default command '");

//            if (defaultCommand == null)
//            {
//                Console.WriteLine("No default command line. Further processing is not possible.");
//                return false; ;
//            }

//            if (args == null || args.Length <= 0)
//            {
//                Trace.TraceInformation("Command line arguments are not provided.");
//                Trace.TraceInformation("Execute the default command with no arguments");
//                defaultCommand.Execute(args, 0);
//            }
//            else
//            {
//                if (args[0].Trim().ToLower() == defaultCommand.Name.Trim().ToLower())
//                {
//                    Trace.TraceInformation("Execute default command with first argument as command name...");
//                    defaultCommand.Execute(args, 1);
//                }
//                else
//                {
//                    Trace.TraceInformation("Execute default command with no command name in the arguments...");
//                    defaultCommand.Execute(args, 0);
//                }
//            }

//            return true;
//        }

//        //public static Func<string[], bool> ProcessCommandLine(string[] args, Func<string[], bool>[] commands, Func<string[], bool> defaultCommand)
//        //{
//        //    Console.WriteLine("Processing command line...");
//        //    if (args == null || args.Length <= 0)
//        //    {
//        //        Console.WriteLine("No command line arguments.");
//        //        return null;
//        //    }

//        //    for (int i = 0; i < commands.Length; i++)
//        //    {
//        //        if (commands[i](args))
//        //        {
//        //            break;
//        //        }
//        //    }//for i

//        //    if (_CommandLineCommand != null)
//        //        return;

//        //    //No Command was found. Load the switches:
//        //    InitCommonSwitches();

//        //    args.ProcessCommadLineSwitches(0, _CommonCommandLineSwitches, this.ProcessNoSwitch);
//        //}


//        /// <summary>
//        /// Searches given <paramref name="commands"/> for a command that can process the argument of <paramref name="args"/> at the given <paramref name="startingArgIndex"/>
//        /// and then executes the command. If no command is found, then it will execute the <paramref name="defaultCommand"/>.
//        /// </summary>
//        /// <param name="args"></param>
//        /// <param name="startingArgIndex"></param>
//        /// <param name="commands"></param>
//        /// <param name="defaultCommand"></param>
//        /// <param name="commandIndex">Index of the command used</param>
//        /// <returns>Index of the last argument used by the command.</returns>
//        public static int ProcessCommandLine(this string[] args, int startingArgIndex, Func<string[], int, int>[] commands, Func<string[], int, int> defaultCommand, out int commandIndex)
//        {

//            commandIndex = -1;
//            int lastUsedArgIndex = ProcessCommandLine(args, startingArgIndex, commands, out commandIndex);
//            if (lastUsedArgIndex <= startingArgIndex)
//                //No command was found that can execute on the given argument.
//                //Execute the default command:
//                lastUsedArgIndex = defaultCommand(args, startingArgIndex);

//            return lastUsedArgIndex;
//        }
//        public static int ProcessCommandLine(this string[] args, int startingArgIndex, Func<string[], int, int>[] commands, out int commandIndex)
//        {
//            commandIndex = -1;
//            int lastUsedArgIndex = -1;
//            for (int i = 0; i < commands.Length; i++)
//            {
//                lastUsedArgIndex = commands[i](args, startingArgIndex);
//                if (lastUsedArgIndex >= startingArgIndex)
//                {
//                    commandIndex = i;
//                    break;
//                }
//            }//i commands

//            return lastUsedArgIndex;
//        }

//        public static int ProcessCommandLine(this string[] args, int startingArgIndex, IEnumerable<Func<string[], int, int>> commands, out Func<string[], int, int> executedCommand)
//        {
//            executedCommand = null;
//            int lastUsedArgIndex = -1;
//            foreach (var command in commands)
//            {
//                lastUsedArgIndex = command(args, startingArgIndex);
//                if (lastUsedArgIndex >= startingArgIndex)
//                {
//                    executedCommand = command;
//                    break;
//                }

//            }

//            return lastUsedArgIndex;
//        }

//        /// <summary>
//        /// Processes command line arguments in a chained fashion. I.e. after executing a command on the arguments, another round of execution
//        /// is directly done on the rest of the arguments. The process will continue until the end of the arguments.
//        /// </summary>
//        /// <param name="args"></param>
//        /// <param name="startingArgIndex"></param>
//        /// <param name="commands"></param>
//        /// <param name="commandIndex"></param>
//        /// <param name="breakOnFailure">Breaks the execution on incorrect arguments.</param>
//        /// <returns></returns>
//        public static int ChainProcessCommandLine(this string[] args, int startingArgIndex, Func<string[], int, int>[] commands, bool breakOnFailure, out int commandIndex)
//        {
//            int lastUsedArgIndex = startingArgIndex - 1;
//            commandIndex = -1;
//            for (int i = startingArgIndex; i < args.Length; i++)
//            {
//                lastUsedArgIndex = ProcessCommandLine(args, i, commands, out commandIndex);
//                if (lastUsedArgIndex < i)
//                {
//                    if (commandIndex >= 0)
//                    {
//                        Trace.TraceWarning($"Command at index '{commandIndex}' returned an invalid argument index of '{lastUsedArgIndex}'." +
//                            $" Returned index was changed to valid range value '{startingArgIndex}'");
//                    }
//                    if (breakOnFailure)
//                    {
//                        break;
//                    }
//                    Trace.WriteLine("To prevent infinite loop logic will advance ");
//                    lastUsedArgIndex = i;
//                }
//                i = lastUsedArgIndex;
//            }
//            return lastUsedArgIndex;
//        }
//        /// <summary>
//        /// Processes command line arguments in a chained fashion. I.e. after executing a command on the arguments, another round of execution
//        /// is directly done on the rest of the arguments. The process will continue until the end of the arguments.
//        /// </summary>
//        /// <param name="args"></param>
//        /// <param name="startingArgIndex"></param>
//        /// <param name="commands"></param>
//        /// <param name="commandIndex"></param>
//        /// <param name="breakOnFailure">Breaks the execution on incorrect arguments.</param>
//        /// <returns></returns>
//        public static int ChainProcessCommandLine(this string[] args, int startingArgIndex, Func<string[], int, int>[] commands, bool breakOnFailure, Func<string[], int, int> defaultCommand, out int commandIndex)
//        {
//            int lastUsedArgIndex = startingArgIndex - 1;
//            commandIndex = -1;
//            for (int i = startingArgIndex; i < args.Length; i++)
//            {
//                lastUsedArgIndex = ProcessCommandLine(args, i, commands, defaultCommand, out commandIndex);
//                if (lastUsedArgIndex < i)
//                {
//                    if (commandIndex >= 0)
//                    {
//                        Trace.TraceWarning($"Command at index '{commandIndex}' returned an invalid argument index of '{lastUsedArgIndex}'." +
//                            $" Returned index was changed to valid range value '{startingArgIndex}'");
//                    }
//                    if (breakOnFailure)
//                    {
//                        break;
//                    }
//                    Trace.WriteLine("To prevent infinite loop logic will advance ");
//                    lastUsedArgIndex = i;
//                }
//                i = lastUsedArgIndex;
//            }
//            return lastUsedArgIndex;
//        }

//    }
//}
