﻿using System;
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

        static private string DefineNameSpace(int command_ptr, int NextIndex, ref bool NameSpaceDefined, string NameSpaceName)
        {
            if (!CheckLiteralName(NameSpaceName))
                return "BAD_LITERALNAME";
            if (KeyWords.Contains(NameSpaceName) || OpenedNamespaces.Contains(NameSpaceName))
                return "BAD_NAMESPACE";
            DefinedNamespaces.Push(NameSpaceName);
            OpenedBlocks.Push(new Block(command_ptr, NextIndex, BlockType.NameSpace));
            NameSpaceDefined = true;
            return null;
        }

        static private string DefineMain(int command_ptr, int NextIndex, ref bool ProgramDefined)
        {
            if (ProgramDefined)
            {
                return "DEFINED_PROGRAM";
            }
            ProgramDefined = true;
            OpenedBlocks.Push(new Block(command_ptr, NextIndex, BlockType.Program));
            return null;
        }

        static private void BeginBlock(int command_ptr, int NextIndex)
        {
            OpenedBlocks.Push(new Block(command_ptr, NextIndex, BlockType.Block));
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
<<<<<<< HEAD
            if (Result == null)
            {
                if(expression_result.DataType != "Bool")
                    return "BAD_TYPE";
                OpenedBlocks.Push(new Block(command_ptr, NextIndex, BlockType.If));
=======
            if (Result == null && expression_result.DataType == "Bool")
            {
                OpenedBlocks.Push(new Block(command_ptr, BlockType.If));
>>>>>>> 55c335d... If and Else operator
                DontSkipNextCommand = true;
                if (expression_result.Data == "true")
                {
                    return null;
                }
                else
                if (expression_result.Data == "false")
                {
                    SkipTo = OpenedBlocks.Count - 1;
                    return null;
                }
                return "BAD_VARIABLE";
            }
            else
                return Result;
        }

<<<<<<< HEAD
        static private string ElseBlockAdd(int command_ptr, int NextIndex, ref int? SkipTo, ref bool DontSkipNextCommand)
=======
        static private string ElseBlockAdd(int command_ptr, ref int? SkipTo, ref bool DontSkipNextCommand)
>>>>>>> 55c335d... If and Else operator
        {
            var Block = OpenedBlocks.Pop();
            if(Block.type != BlockType.If)
                return "NOTFOUND_IF";
<<<<<<< HEAD
            OpenedBlocks.Push(new Block(command_ptr, NextIndex, BlockType.Else));
=======
            OpenedBlocks.Push(new Block(command_ptr, BlockType.Else));
>>>>>>> 55c335d... If and Else operator
            DontSkipNextCommand = true;
            SkipTo = OpenedBlocks.Count - 1;
            return null;
        }

<<<<<<< HEAD
        static private string WhileBlockAdd(int command_ptr, ref int NextIndex, ref int line_ptr, string Command, ref int? SkipTo, ref bool DontSkipNextCommand)
        {
            OpenedBlocks.Push(new Block(command_ptr, NextIndex, BlockType.While));
            bool while_result;
            var Result = GetWhileExpressionResult(ref NextIndex, ref line_ptr, Command, out while_result);
            if (Result == null)
            {
                if(while_result)
                {
                    return null;
                }
                else
                {
                    DontSkipNextCommand = true;
                    SkipTo = OpenedBlocks.Count - 1;
                    return null;
                }
            }
            else
                return Result;
        }

        static private string GetWhileExpressionResult(ref int NextIndex, ref int line_ptr, string Command, out bool WhileResult)
        {
            WhileResult = false;
            string expression;
            var res = GetNextExpression(Command, ref NextIndex, ref line_ptr, out expression, true);
            if (!res)
                return "BAD_EXPRESSION";
            MemorySlot expression_result;
            var Result = InterpretExpression(expression, out expression_result);
            if (Result == null)
            {
                if (expression_result.DataType != "Bool")
                    return "BAD_TYPE";
                if (expression_result.Data == "true")
                {
                    WhileResult = true;
                    return null;
                }
                else
                if (expression_result.Data == "false")
                    return null;
                return "BAD_VARIABLE";
            }
            else
                return Result;
        }

=======
>>>>>>> 55c335d... If and Else operator
        static private string DefineVariable(ref int NextIndex, ref int line_ptr, string Command, string DataType)
        {
            var VarName = GetNextLiteral(Command, ref NextIndex, ref line_ptr);
            if (!CheckLiteralName(VarName))
                return "BAD_LITERALNAME";
            foreach (var block in OpenedBlocks)
            {
                if (block.variables.Find((variable) => variable.Name == VarName) != null)
                    return "EXISTS_LITERALNAME";
            }

            bool IsInitialized = false;
            MemorySlot variable_data = new MemorySlot();
            if (GetNextSymbol(Command, ref NextIndex, ref line_ptr) == '=')
            {
                string expression;
                var result = GetNextExpression(Command, ref NextIndex, ref line_ptr, out expression);
                if (!result)
                    return "BAD_EXPRESSION";
                var res = InterpretExpression(expression, out variable_data);
                if (res != null)
                    return res;
                IsInitialized = true;
            }
            string RealType;
            if (DataType == "змінна")
            {
                if (IsInitialized)
                {
                    OpenedBlocks.Peek().variables.Add(new Variable(VarName, variable_data));
                }
                else
                    return "BADINIT_VAR";
            }
            else
            if (GetVariableTypeFromCEurope(DataType, out RealType))
            {
                if (IsInitialized)
                {
                    if (variable_data.DataType == RealType)
                        OpenedBlocks.Peek().variables.Add(new Variable(VarName, variable_data));
                    else
                        return "BAD_TYPE";
                }
                else
                    OpenedBlocks.Peek().variables.Add(new Variable(VarName, new MemorySlot(RealType)));
            }
            else
                return "BAD_TYPE";
            return null;
        }

        static private string UpdateVariable(ref int NextIndex, ref int line_ptr, string Command, Variable variable)
        {
            MemorySlot variable_data = new MemorySlot();
            if (GetNextSymbol(Command, ref NextIndex, ref line_ptr) == '=')
            {
                string expression;
                var result = GetNextExpression(Command, ref NextIndex, ref line_ptr, out expression);
                if (!result)
                    return "BAD_EXPRESSION";
                var res = InterpretExpression(expression, out variable_data);
                if (res != null)
                    return res;
                variable.Data = variable_data;
                return null;
            }
            else
                return "BAD_EXPRESSION";
        }
    }
}