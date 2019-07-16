using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CEVirtualMachine
{
    static partial class Interpreter
    {
        static public void SendError(int line_ptr, string error_text, StreamWriter OutFile = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (OutFile == null)
                Console.WriteLine("ПОМИЛКА: " + error_text + ". Рядок з командою: " + line_ptr);
            else
                OutFile.WriteLine("SERROR: ПОМИЛКА: " + error_text + ". Рядок з командою: " + line_ptr);
        }

        static public void SendInfo(string infotext, StreamWriter OutFile = null)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (OutFile != null)
                OutFile.WriteLine("SOUT: " + infotext);
            Console.WriteLine(infotext);
        }
    }
}