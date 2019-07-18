using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Diagnostics;

namespace CEVirtualMachine
{
    class Program
    {
        //----------------------------------------

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Default;
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US"); // For . instead , in Convert

            var IsNormalMode = false;
            var IsRedirectOut = false;

            var IsDiagnosticTime = false;
            var watch = new Stopwatch();
            string FileName = "";
            string OutFileName = "";
            for (var i = 0; i < args.Length; i++)
                switch (args[i])
                {
                    case "-script":
                        if (i + 1 < args.Length)
                        {
                            IsNormalMode = true;
                            FileName = args[i++ + 1];
                            while (i + 1 < args.Length && args[i + 1][0] != '+' && args[i + 1][0] != '-')
                            {
                                i++;
                                FileName += " " + args[i];
                            }
                        }
                        break;
                    case "-dtime":
                        IsDiagnosticTime = true;
                        break;
                    case "-out":
                        if (i + 1 < args.Length)
                        {
                            IsRedirectOut = true;
                            OutFileName = args[i++ + 1];
                            while (i + 1 < args.Length && args[i + 1][0] != '+' && args[i + 1][0] != '-')
                            {
                                i++;
                                OutFileName += " " + args[i];
                            }
                        }
                        break;
                }

            StreamWriter OutFile = null;
            if(IsRedirectOut)
                OutFile = new StreamWriter(OutFileName, false, Encoding.Default);

            if (!IsNormalMode)
            {
                Console.WriteLine("--------------------------");
                Console.WriteLine("Віртуальна машина CEurope");
                Console.WriteLine("--------------------------");
                Console.WriteLine("Щоб запустити скрипт передайте в параметри:");
                Console.WriteLine("-script <шлях>");
                Console.ReadKey();
            }
            else
            {
                Interpreter.SendInfoLine("Виконую " + FileName, OutFile);
                if (File.Exists(FileName))
                    using (var script_file = new StreamReader(FileName, Encoding.Default))
                    {
                        var script = script_file.ReadToEnd();
                        watch.Start();
                        Interpreter.Interpret(script, OutFile);
                        watch.Stop();
                    }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Interpreter.SendInfoLine("Файл не знайдено", OutFile);
                    Console.ReadKey();
                    return;
                }
                if (IsDiagnosticTime)
                    Interpreter.SendInfoLine(string.Format("Програма завершена за {0} ...", watch.Elapsed), OutFile);
                else
                    Interpreter.SendInfoLine("Програма завершена...", OutFile);
            }
            if (!IsRedirectOut)
                Console.ReadKey();
            else
                OutFile.Close();
        }
    }
}
