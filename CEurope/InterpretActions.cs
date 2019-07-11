using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace CEurope
{
    static class InterpretActions
    {
        public const string INTERPRETER_OUT = "tmp.iout";
        static public void Interpret(string InterpreterFile, string InterpreterDirectoryPath, string ScriptFile, EventHandler OnExit)
        {
            ProcessStartInfo procInfo = new ProcessStartInfo
            {
                FileName = InterpreterFile,
                Arguments = $"-script {ScriptFile} -dtime -out {InterpreterDirectoryPath + "\\" + INTERPRETER_OUT}",
                UseShellExecute = false
            };
            var InterpreterProcess = new Process();
            InterpreterProcess.EnableRaisingEvents = true;
            InterpreterProcess.StartInfo = procInfo;
            InterpreterProcess.Exited += OnExit;
            InterpreterProcess.Start();
        }
    }
}
