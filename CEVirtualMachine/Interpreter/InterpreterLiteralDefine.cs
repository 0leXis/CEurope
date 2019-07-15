using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEVirtualMachine
{
    static partial class Interpreter
    {
        static List<MemorySlot> Memory = new List<MemorySlot>();
        static List<string> Defines = new List<string>();
        static Stack<string> DefinedNamespaces = new Stack<string>();
        static Stack<string> OpenedNamespaces = new Stack<string>();
        static Stack<Block> OpenedBlocks = new Stack<Block>();

        static private string LoadNameSpace(int command_ptr, ref bool NameSpaceDefined, string NameSpaceName)
        {
            //TODO: Load from file
            return null;
        }

        static private string DefineNameSpace(int command_ptr, ref bool NameSpaceDefined, string NameSpaceName)
        {
            if (!CheckLiteralName(NameSpaceName))
                return "BAD_LITERALNAME";
            if (KeyWords.Contains(NameSpaceName) || Defines.Contains(NameSpaceName))
                return "BAD_NAMESPACE";
            Defines.Add(NameSpaceName);
            DefinedNamespaces.Push(NameSpaceName);
            OpenedBlocks.Push(new Block(command_ptr, BlockType.NameSpace));
            NameSpaceDefined = true;
            return null;
        }

        static private string DefineMain(int command_ptr, ref bool ProgramDefined)
        {
            if (ProgramDefined)
            {
                return "DEFINED_PROGRAM";
            }
            ProgramDefined = true;
            OpenedBlocks.Push(new Block(command_ptr, BlockType.Program));
            return null;
        }

        static private void BeginBlock(int command_ptr)
        {
            OpenedBlocks.Push(new Block(command_ptr, BlockType.Block));
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