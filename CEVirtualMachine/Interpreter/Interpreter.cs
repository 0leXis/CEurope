using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CEVirtualMachine
{
    static partial class Interpreter
    {

        static public bool Interpret(string script, StreamWriter OutFile = null)
        {
            DefinedNamespaces = new Stack<string>();
            OpenedNamespaces = new Stack<string>();
            OpenedBlocks = new Stack<Block>();

            var NameSpaceDefined = false;
            var ProgramDefined = false;
            var WasProgramDefined = false;
            var WasNameSpaceDefined = false;
            var DontSkipNextCommand = false;
            int? SkipTo = null;

            var Commands = script.Split(';');
            var command_ptr = 0;
            var line_ptr = 1;
            for (command_ptr = 0; command_ptr < Commands.Length; command_ptr++)
            {
                DontSkipNextCommand = false;
                var NextIndex = 0;
                while (NextIndex < Commands[command_ptr].Length)
                {
                    var literal = GetNextLiteral(Commands[command_ptr], ref NextIndex, ref line_ptr);

                    if (SkipTo != null)
                    {
                        var NextBlock = OpenedBlocks.Peek();
                        if (!DontSkipNextCommand)
                            while (((NextBlock.type == BlockType.If && literal != "інакше") 
                                || NextBlock.type == BlockType.Else
                                || NextBlock.type == BlockType.While
                                || NextBlock.type == BlockType.For) && SkipTo != OpenedBlocks.Count - 1)
                            {
                                OpenedBlocks.Pop();
                                NextBlock = OpenedBlocks.Peek();
                            }
                        if (!DontSkipNextCommand && SkipTo == OpenedBlocks.Count - 1)
                        {
                            SkipTo = null;
                            NextBlock = OpenedBlocks.Peek();
                            OpenedBlocks.Pop();
                            if (NextBlock.type == BlockType.If && literal == "інакше")
                            {
                                DontSkipNextCommand = true;
                                continue;
                            }
                        }
                        else
                        {
                            DontSkipNextCommand = false;
                            switch (literal)
                            {
                                case "почати":
                                    OpenedBlocks.Push(new Block(command_ptr, NextIndex, line_ptr, BlockType.Block));
                                    break;
                                case "якщо(":
                                    OpenedBlocks.Push(new Block(command_ptr, NextIndex, line_ptr, BlockType.If));
                                    GetNextExpression(Commands[command_ptr], ref NextIndex, ref line_ptr, out literal, true);
                                    DontSkipNextCommand = true;
                                    break;
                                case "поки(":
                                    OpenedBlocks.Push(new Block(command_ptr, NextIndex, line_ptr, BlockType.While));
                                    GetNextExpression(Commands[command_ptr], ref NextIndex, ref line_ptr, out literal, true);
                                    DontSkipNextCommand = true;
                                    break;
                                case "робити":
                                    OpenedBlocks.Push(new Block(command_ptr, NextIndex, line_ptr, BlockType.DoUntil));
                                    break;
                                case "для(":
                                    OpenedBlocks.Push(new Block(command_ptr, NextIndex, line_ptr, BlockType.For));
                                    string expression;
                                    GetNextExpression(Commands[command_ptr], ref NextIndex, ref line_ptr, out expression, true);
                                    GetNextExpression(Commands[command_ptr], ref NextIndex, ref line_ptr, out expression, true);
                                    GetNextExpression(Commands[command_ptr], ref NextIndex, ref line_ptr, out expression, true);
                                    DontSkipNextCommand = true;
                                    break;
                                //case "перемикач(":
                                //    OpenedBlocks.Push(new Block(command_ptr, NextIndex, line_ptr, BlockType.Switch));
                                //    GetNextExpression(Commands[command_ptr], ref NextIndex, ref line_ptr, out literal, true);
                                //    break;
                                case "нераніше(":
                                    var UntilBlock = OpenedBlocks.Pop();
                                    GetNextExpression(Commands[command_ptr], ref NextIndex, ref line_ptr, out literal, true);
                                    break;
                                case "інакше":
                                    DontSkipNextCommand = true;
                                    OpenedBlocks.Pop();
                                    break;
                                case "кінець":
                                    OpenedBlocks.Pop();
                                    break;
                                default:
                                    NextIndex = 0;
                                    command_ptr++;
                                    break;
                            }
                            continue;
                        }
                    }

                    if (!DontSkipNextCommand && OpenedBlocks.Count > 0)
                    {
                        var NextBlock = OpenedBlocks.Peek();
                        var DoNextCommand = false;
                        var BlockFound = true;
                        while (BlockFound && !DoNextCommand && OpenedBlocks.Count > 0)
                        {
                            if ((NextBlock.type == BlockType.If && literal != "інакше")
                            || NextBlock.type == BlockType.Else)
                            {
                                OpenedBlocks.Pop();
                            }
                            else
                            if (NextBlock.type == BlockType.While)
                            {
                                var OldIndex = NextIndex;
                                var OldCommand_ptr = command_ptr;
                                var OldLine_ptr = line_ptr;
                                NextIndex = NextBlock.NextIndex;
                                command_ptr = NextBlock.command_ptr;
                                line_ptr = NextBlock.line_ptr;

                                bool while_result;
                                string expression;
                                var res = GetNextExpression(Commands[command_ptr], ref NextIndex, ref line_ptr, out expression, true);
                                if (!res)
                                {
                                    SendError(line_ptr, ErrorCodes["BAD_EXPRESSION"], OutFile);
                                    return false;
                                }
                                var Result = GetBoolFuncExpressionResult(expression, out while_result);
                                if (Result == null)
                                {
                                    if (while_result)
                                    {
                                        DontSkipNextCommand = true;
                                        DoNextCommand = true;
                                    }
                                    else
                                    {
                                        OpenedBlocks.Pop();
                                        NextIndex = OldIndex;
                                        command_ptr = OldCommand_ptr;
                                        line_ptr = OldLine_ptr;
                                    }
                                }
                                else
                                {
                                    SendError(line_ptr, ErrorCodes[Result], OutFile);
                                    return false;
                                }
                            }
                            else
                            if(NextBlock.type == BlockType.For)
                            {
                                var OldIndex = NextIndex;
                                var OldCommand_ptr = command_ptr;
                                var OldLine_ptr = line_ptr;
                                NextIndex = NextBlock.NextIndex;
                                command_ptr = NextBlock.command_ptr;
                                line_ptr = NextBlock.line_ptr;

                                string continue_expression;
                                if (!CheckNextSymbol(Commands[command_ptr], NextIndex, ','))
                                {
                                    var res = GetNextExpression(Commands[command_ptr], ref NextIndex, ref line_ptr, out continue_expression, true);
                                    if (!res)
                                    {
                                        SendError(line_ptr, ErrorCodes["BAD_EXPRESSION"], OutFile);
                                        return false;
                                    }
                                }
                                else
                                {
                                    GetNextSymbol(Commands[command_ptr], ref NextIndex, ref line_ptr);
                                    continue_expression = "true";
                                }
                                if (!CheckNextSymbol(Commands[command_ptr], NextIndex, ')'))
                                {
                                    var for_literal = GetNextLiteral(Commands[command_ptr], ref NextIndex, ref line_ptr);
                                    if (CheckLiteralName(for_literal))
                                    {
                                        Variable vrb = null;
                                        var IsVarFinded = false;
                                        foreach (var block in OpenedBlocks)
                                        {
                                            vrb = block.variables.Find((variable) => variable.Name == for_literal);
                                            if (vrb != null)
                                            {
                                                IsVarFinded = true;
                                                break;
                                            }
                                        }
                                        if (IsVarFinded)
                                        {
                                            var result = UpdateVariable(ref NextIndex, ref line_ptr, Commands[command_ptr], vrb, true);
                                            if (result != null)
                                            {
                                                SendError(line_ptr, ErrorCodes[result], OutFile);
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            SendError(line_ptr, ErrorCodes["BADNAME_VARIABLE"], OutFile);
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        SendError(line_ptr, ErrorCodes["BAD_LITERAL"], OutFile);
                                        return false;
                                    }
                                }
                                else
                                    GetNextSymbol(Commands[command_ptr], ref NextIndex, ref line_ptr);

                                bool for_result;
                                var Result = GetBoolFuncExpressionResult(continue_expression, out for_result);
                                if (Result == null)
                                {
                                    if (for_result)
                                    {
                                        DontSkipNextCommand = true;
                                        DoNextCommand = true;
                                    }
                                    else
                                    {
                                        OpenedBlocks.Pop();
                                        NextIndex = OldIndex;
                                        command_ptr = OldCommand_ptr;
                                        line_ptr = OldLine_ptr;
                                    }
                                }
                                else
                                {
                                    SendError(line_ptr, ErrorCodes[Result], OutFile);
                                    return false;
                                }
                            }
                            else
                                BlockFound = false;
                            NextBlock = OpenedBlocks.Peek();
                        }
                        if (DoNextCommand)
                            continue;
                    }
                    else
                        DontSkipNextCommand = false;

                    if (!ProgramDefined && NameSpaceDefined)
                        switch (literal)
                        {
                            case "простірімен":
                                literal = GetNextLiteral(Commands[command_ptr], ref NextIndex, ref line_ptr);
                                var Result = DefineNameSpace(command_ptr, NextIndex, ref line_ptr, ref NameSpaceDefined, literal);
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
                                Result = DefineMain(command_ptr, NextIndex, ref line_ptr, ref ProgramDefined);
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
                                Result = EndBlock(ref NameSpaceDefined, ref ProgramDefined);
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
                                BeginBlock(command_ptr, NextIndex, ref line_ptr);
                                continue;
                            case "кінець":
                                var Result = EndBlock(ref NameSpaceDefined, ref ProgramDefined);
                                if (Result == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    SendError(line_ptr, ErrorCodes[Result], OutFile);
                                    return false;
                                }
                            case "інакше":
                                Result = ElseBlockAdd(command_ptr, NextIndex, ref line_ptr, ref SkipTo, ref DontSkipNextCommand);
                                if (Result == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    SendError(line_ptr, ErrorCodes[Result], OutFile);
                                    return false;
                                }
                            case "робити":
                                DoUntilBlockAdd(command_ptr, NextIndex, line_ptr);
                                continue;
                            case "перерву":
                                Result = Break(ref SkipTo, ref DontSkipNextCommand);
                                if (Result == null)
                                {
                                    continue;
                                }
                                else
                                {
                                    SendError(line_ptr, ErrorCodes[Result], OutFile);
                                    return false;
                                }
                            case "продовжуй":
                                Result = Continue(ref command_ptr, ref NextIndex, ref line_ptr, ref DontSkipNextCommand);
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
                                var indx = literal.IndexOf('(');
                                if(DataDefs.Contains(literal))
                                {
                                    Result = DefineVariable(ref NextIndex, ref line_ptr, Commands[command_ptr], literal, OpenedBlocks.Peek());
                                    if (Result == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        SendError(line_ptr, ErrorCodes[Result], OutFile);
                                        return false;
                                    }
                                }
                                else
                                if (indx > 0 && char.IsLetter(literal[0]))
                                {
                                    switch (literal)
                                    {
                                        case "якщо(":
                                            Result = IfBlockAdd(command_ptr, ref NextIndex, ref line_ptr, Commands[command_ptr], ref SkipTo, ref DontSkipNextCommand);
                                            if (Result == null)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                SendError(line_ptr, ErrorCodes[Result], OutFile);
                                                return false;
                                            }
                                        case "поки(":
                                            Result = WhileBlockAdd(command_ptr, ref NextIndex, ref line_ptr, Commands[command_ptr], ref SkipTo, ref DontSkipNextCommand);
                                            if (Result == null)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                SendError(line_ptr, ErrorCodes[Result], OutFile);
                                                return false;
                                            }
                                        case "для(":
                                            Result = ForBlockAdd(command_ptr, ref NextIndex, ref line_ptr, Commands[command_ptr], ref SkipTo, ref DontSkipNextCommand);
                                            if (Result == null)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                SendError(line_ptr, ErrorCodes[Result], OutFile);
                                                return false;
                                            }
                                        case "нераніше(":
                                            Result = DoUntilBlockProcess(ref command_ptr, ref NextIndex, ref line_ptr, Commands[command_ptr]);
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
                                            //TODO: FunctionsDoHere
                                            break;
                                    }
                                }
                                else
                                if(CheckLiteralName(literal))
                                {
                                    Variable vrb = null;
                                    var IsVarFinded = false;
                                    foreach (var block in OpenedBlocks)
                                    {
                                        vrb = block.variables.Find((variable) => variable.Name == literal);
                                        if (vrb != null)
                                        {
                                            IsVarFinded = true;
                                            break;
                                        }
                                    }
                                    if (IsVarFinded)
                                    {
                                        Result = UpdateVariable(ref NextIndex, ref line_ptr, Commands[command_ptr], vrb);
                                        if (Result != null)
                                        {
                                            SendError(line_ptr, ErrorCodes[Result], OutFile);
                                            return false;
                                        }
                                        continue;
                                    }
                                    else
                                    {
                                        SendError(line_ptr, ErrorCodes["BADNAME_VARIABLE"], OutFile);
                                        return false;
                                    }
                                }

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
                                    continue;
                                }
                                else
                                {
                                    SendError(line_ptr, ErrorCodes[Result], OutFile);
                                    return false;
                                }
                            case "простірімен":
                                literal = GetNextLiteral(Commands[command_ptr], ref NextIndex, ref line_ptr);
                                Result = DefineNameSpace(command_ptr, NextIndex, ref line_ptr, ref NameSpaceDefined, literal);
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
