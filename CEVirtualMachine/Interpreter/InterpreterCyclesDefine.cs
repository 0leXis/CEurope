using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CEVirtualMachine
{
    static partial class Interpreter
    {
        static private void DoUntilBlockAdd(int command_ptr, int NextIndex, int line_ptr)
        {
            OpenedBlocks.Push(new Block(command_ptr, NextIndex, line_ptr, BlockType.DoUntil));
        }

        static private string DoUntilBlockProcess(ref int command_ptr, ref int NextIndex, ref int line_ptr, string Command)
        {
            var DoUntilBlock = OpenedBlocks.Peek();
            if (DoUntilBlock.type != BlockType.DoUntil)
                return "BAD_BLOCK";
            string expression;
            var res = GetNextExpression(Command, ref NextIndex, ref line_ptr, out expression, true);
            if (!res)
                return "BAD_EXPRESSION";
            bool until_result;
            var Result = GetBoolFuncExpressionResult(expression, out until_result);
            if (Result == null)
            {
                if (until_result)
                {
                    OpenedBlocks.Pop();
                    return null;
                }
                else
                {
                    command_ptr = DoUntilBlock.command_ptr;
                    NextIndex = DoUntilBlock.NextIndex;
                    line_ptr = DoUntilBlock.line_ptr;
                    return null;
                }
            }
            else
                return Result;
        }

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
                var result = GetNextExpression(Command, ref NextIndex, ref line_ptr, out expression, true);
                if (for_result)
                {
                    if (result)
                        return null;
                }
                else
                {
                    SkipTo = OpenedBlocks.Count - 1;
                    if (result)
                        return null;
                }
                return "BAD_EXPRESSION";
            }
            else
                return Result;
        }

        static private string Break(ref int? SkipTo, ref bool DontSkipNextCommand)
        {
            var BlockList = OpenedBlocks.ToList();
            for (var i = 0; i < BlockList.Count; i++)
            {
                if (BlockList[i].type == BlockType.While || BlockList[i].type == BlockType.For || BlockList[i].type == BlockType.DoUntil)
                {
                    DontSkipNextCommand = false;
                    SkipTo = BlockList.Count - i - 1;
                    return null;
                }
            }
            return "BAD_BREAK";
        }

        static private string Continue(ref int command_ptr, ref int NextIndex, ref int line_ptr, ref bool DontSkipNextCommand)
        {
            while(OpenedBlocks.Count > 0)
            {
                var NextBlock = OpenedBlocks.Peek();
                if (NextBlock.type == BlockType.While || NextBlock.type == BlockType.For || NextBlock.type == BlockType.DoUntil)
                {
                    command_ptr = NextBlock.command_ptr;
                    line_ptr = NextBlock.line_ptr;
                    NextIndex = NextBlock.NextIndex;
                    DontSkipNextCommand = false;
                    return null;
                }
                OpenedBlocks.Pop();
            }
            return "BAD_CONTINUE";
        }
    }
}