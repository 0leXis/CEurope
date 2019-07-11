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
            if(OutFile == null)
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

        static public bool Interpret(string script, StreamWriter OutFile = null)
        {
            var NameSpaceDefined = false;
            var ProgramDefined = false;
            var WasProgramDefined = false;
            var WasNameSpaceDefined = false;

            var Commands = script.Split(';');
            var command_ptr = 0;
            var line_ptr = 1;
            for (command_ptr = 0; command_ptr < Commands.Length; command_ptr++)
            {
                var NextIndex = 0;
                while (NextIndex < Commands[command_ptr].Length)
                {
                    var literal = GetNextLiteral(Commands[command_ptr], ref NextIndex, ref line_ptr);
                    if (!ProgramDefined && NameSpaceDefined)
                        switch (literal)
                        {
                            case "простірімен":
                                literal = GetNextLiteral(Commands[command_ptr], ref NextIndex, ref line_ptr);
                                var Result = DefineNameSpace(command_ptr, ref NameSpaceDefined, literal);
                                if(Result == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    SendError(line_ptr, ErrorCodes[Result], OutFile);
                                    return false;
                                }
                            case "програма":
                                Result = DefineMain(command_ptr, ref ProgramDefined);
                                if (Result == null)
                                {
                                    WasProgramDefined = true;
                                    continue;
                                }
                                else
                                {
                                    SendError(line_ptr, ErrorCodes[Result], OutFile);
                                    return false;
                                }
                            case "кінець":
                                Result = EndBlock(command_ptr, ref NameSpaceDefined, ref ProgramDefined);
                                if (Result == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    SendError(line_ptr, ErrorCodes[Result], OutFile);
                                    return false;
                                }
                            default:
                                SendError(line_ptr, ErrorCodes["BAD_LITERAL"], OutFile);
                                return false;
                        }
                    else
                    if (ProgramDefined)
                        switch (literal)
                        {
                            case "почати":
                                BeginBlock(command_ptr);
                                continue;
                            case "кінець":
                                var Result = EndBlock(command_ptr, ref NameSpaceDefined, ref ProgramDefined);
                                if (Result == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    SendError(line_ptr, ErrorCodes[Result], OutFile);
                                    return false;
                                }
                            case "ПисатиРядок": //TODO: Вынести в отдельный модуль, когда доделаю procedures
                                string WriteLiteral;
                                var res = GetNextExpression(Commands[command_ptr], ref NextIndex, ref line_ptr, out WriteLiteral, false);
                                if(!res)
                                {
                                    SendError(line_ptr, ErrorCodes["BAD_EXPRESSION"], OutFile);
                                    return false;
                                }
                                MemorySlot expression_result;
                                Result = InterpretExpression(WriteLiteral, out expression_result);
                                if (Result == null)
                                {
                                    SendInfo(expression_result.Data, OutFile);
                                    continue;
                                }
                                else
                                {
                                    SendError(line_ptr, ErrorCodes[Result], OutFile);
                                    return false;
                                }
                            default:
                                SendError(line_ptr, ErrorCodes["BAD_LITERAL"], OutFile);
                                return false;
                        }
                    else
                        switch (literal)
                        {
                            case "використовуючи":
                                literal = GetNextLiteral(Commands[command_ptr], ref NextIndex, ref line_ptr);
                                var Result = LoadNameSpace(command_ptr, ref NameSpaceDefined, literal);
                                if (Result == null)
                                {
                                    WasNameSpaceDefined = true;
                                    continue;
                                }
                                else
                                {
                                    SendError(line_ptr, ErrorCodes[Result], OutFile);
                                    return false;
                                }
                            case "простірімен":
                                literal = GetNextLiteral(Commands[command_ptr], ref NextIndex, ref line_ptr);
                                Result = DefineNameSpace(command_ptr, ref NameSpaceDefined, literal);
                                if (Result == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    SendError(line_ptr, ErrorCodes[Result], OutFile);
                                    return false;
                                }
                            default:
                                SendError(line_ptr, ErrorCodes["BAD_LITERAL"], OutFile);
                                return false;
                        }
                }
            }
            if (!WasNameSpaceDefined)
            {
                SendError(line_ptr, ErrorCodes["NO_NAMESPACE"], OutFile);
                return false;
            }
            if (!WasProgramDefined)
            {
                SendError(line_ptr, ErrorCodes["NO_PROGRAM"], OutFile);
                return false;
            }
            if (OpenedBlocks.Count > 0 || DefinedNamespaces.Count > 0)
            {
                SendError(line_ptr, ErrorCodes["BADEND_BLOCK"], OutFile);
                return false;
            }
            else
                return true;
        }
    }
}
