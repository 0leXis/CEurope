using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CEVirtualMachine
{
    static partial class Interpreter
    {

        static private string WhileBlockAdd(int command_ptr, ref int NextIndex, ref int line_ptr, string Command, ref int? SkipTo, ref bool DontSkipNextCommand)
        {
            OpenedBlocks.Push(new Block(command_ptr, NextIndex, line_ptr, BlockType.While));
            string expression;
            var res = GetNextExpression(Command, ref NextIndex, ref line_ptr, out expression, true);
            if (!res)
                return "BAD_EXPRESSION";
            bool while_result;
            var Result = GetBoolFuncExpressionResult(expression, out while_result);
            if (Result == null)
            {
                DontSkipNextCommand = true;
                if (while_result)
                {
                    return null;
                }
                else
                {
                    SkipTo = OpenedBlocks.Count - 1;
                    return null;
                }
            }
            else
                return Result;
        }

        static private string ForBlockAdd(int command_ptr, ref int NextIndex, ref int line_ptr, string Command, ref int? SkipTo, ref bool DontSkipNextCommand)
        {
            var ForBlock = new Block(command_ptr, NextIndex, line_ptr, BlockType.For);
            if (!CheckNextSymbol(Command, NextIndex, ','))
            {
                var result = DefineVariable(ref NextIndex, ref line_ptr, Command, GetNextLiteral(Command, ref NextIndex, ref line_ptr), ForBlock, true);
                if (result != null)
                    return result;
            }
            else
                GetNextSymbol(Command, ref NextIndex, ref line_ptr);
            ForBlock.NextIndex = NextIndex;
            ForBlock.line_ptr = line_ptr;
            OpenedBlocks.Push(ForBlock);
            string expression;
            if (!CheckNextSymbol(Command, NextIndex, ','))
            {
                var res = GetNextExpression(Command, ref NextIndex, ref line_ptr, out expression, true);
                if (!res)
                    return "BAD_EXPRESSION";
            }
            else
            {
                GetNextSymbol(Command, ref NextIndex, ref line_ptr);
                expression = "true";
            }
            bool for_result;
            var Result = GetBoolFuncExpressionResult(expression, out for_result);
            if (Result == null)
            {
                DontSkipNextCommand = true;
                if (for_result)
                {
                    var result = GetNextExpression(Command, ref NextIndex, ref line_ptr, out expression, true);
                    if (result)
                        return null;
                    return "BAD_EXPRESSION";
                }
                else
                {
                    SkipTo = OpenedBlocks.Count - 1;
                    return null;
                }
            }
            else
                return Result;
        }
    }
}