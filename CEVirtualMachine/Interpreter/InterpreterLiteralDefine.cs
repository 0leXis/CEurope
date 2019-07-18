using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEVirtualMachine
{
    static partial class Interpreter
    {
        //static List<MemorySlot> Memory = new List<MemorySlot>();
        static Stack<string> DefinedNamespaces = new Stack<string>();
        static Stack<string> OpenedNamespaces = new Stack<string>();
        static Stack<Block> OpenedBlocks = new Stack<Block>();

        static private string LoadNameSpace(int command_ptr, ref bool NameSpaceDefined, string NameSpaceName)
        {
            //TODO: Load from file
            return null;
        }

        static private string DefineNameSpace(int command_ptr, int NextIndex, ref int line_ptr, ref bool NameSpaceDefined, string NameSpaceName)
        {
            if (!CheckLiteralName(NameSpaceName))
                return "BAD_LITERALNAME";
            if (KeyWords.Contains(NameSpaceName) || OpenedNamespaces.Contains(NameSpaceName))
                return "BAD_NAMESPACE";
            DefinedNamespaces.Push(NameSpaceName);
            OpenedBlocks.Push(new Block(command_ptr, NextIndex, line_ptr, BlockType.NameSpace));
            NameSpaceDefined = true;
            return null;
        }

        static private string DefineMain(int command_ptr, int NextIndex, ref int line_ptr, ref bool ProgramDefined)
        {
            if (ProgramDefined)
            {
                return "DEFINED_PROGRAM";
            }
            ProgramDefined = true;
            OpenedBlocks.Push(new Block(command_ptr, NextIndex, line_ptr, BlockType.Program));
            return null;
        }

        static private void BeginBlock(int command_ptr, int NextIndex, ref int line_ptr)
        {
            OpenedBlocks.Push(new Block(command_ptr, NextIndex, line_ptr, BlockType.Block));
        }

        static private string EndBlock(ref bool NameSpaceDefined, ref bool ProgramDefined)
        {
            if (OpenedBlocks.Count == 0)
            {
                return "BADEND_BLOCK";
            }
            var CurrBlock = OpenedBlocks.Pop();
            switch (CurrBlock.type)
            {
                case BlockType.Block:
                    //Переход в начало для некоторых операций
                    return null;
                case BlockType.DoUntil:
                    //Цикл Do - Untill
                    return null;
                case BlockType.NameSpace:
                    DefinedNamespaces.Pop();
                    if (DefinedNamespaces.Count == 0)
                        NameSpaceDefined = false;
                    return null;
                case BlockType.Program:
                    ProgramDefined = false;
                    return null;
                default:
                    return "BAD_BLOCK";
            }
        }

        static private string IfBlockAdd(int command_ptr, ref int NextIndex, ref int line_ptr, string Command, ref int? SkipTo, ref bool DontSkipNextCommand)
        {
            string expression;
            var res = GetNextExpression(Command, ref NextIndex, ref line_ptr, out expression, true);
            if (!res)
                return "BAD_EXPRESSION";
            MemorySlot expression_result;
            var Result = InterpretExpression(expression, out expression_result);
            if (Result == null)
            {
                if(expression_result.DataType != "Bool")
                    return "BAD_TYPE";
                OpenedBlocks.Push(new Block(command_ptr, NextIndex, line_ptr, BlockType.If));
                DontSkipNextCommand = true;
                if (expression_result.Data == "true" || expression_result.Data == "True")
                {
                    return null;
                }
                else
                if (expression_result.Data == "false" || expression_result.Data == "False")
                {
                    SkipTo = OpenedBlocks.Count - 1;
                    return null;
                }
                return "BAD_VARIABLE";
            }
            else
                return Result;
        }

        static private string ElseBlockAdd(int command_ptr, int NextIndex, ref int line_ptr, ref int? SkipTo, ref bool DontSkipNextCommand)
        {
            var Block = OpenedBlocks.Pop();
            if(Block.type != BlockType.If)
                return "NOTFOUND_IF";
            OpenedBlocks.Push(new Block(command_ptr, NextIndex, line_ptr, BlockType.Else));
            DontSkipNextCommand = true;
            SkipTo = OpenedBlocks.Count - 1;
            return null;
        }
    }
}