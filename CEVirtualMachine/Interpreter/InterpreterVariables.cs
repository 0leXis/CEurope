using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEVirtualMachine
{
    static partial class Interpreter
    {
        static private string DefineVariable(ref int NextIndex, ref int line_ptr, string Command, string DataType, Block VarBlock, bool IsInFunction = false)
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
                if (CheckNextSymbol(Command, NextIndex, '{'))
                {
                    variable_data = new MemorySlot("Table");
                    var res = InitTable(ref NextIndex, ref line_ptr, Command, Convert.ToInt32(variable_data.Data));
                    if (res != null)
                        return res;
                    IsInitialized = true;
                }
                else
                {
                    string expression;
                    var result = GetNextExpression(Command, ref NextIndex, ref line_ptr, out expression, IsInFunction);
                    if (!result)
                        return "BAD_EXPRESSION";
                    var res = InterpretExpression(expression, out variable_data);
                    if (res != null)
                        return res;
                    IsInitialized = true;
                }
            }
            string RealType;
            if (DataType == "змінна")
            {
                if (IsInitialized)
                {
                    VarBlock.variables.Add(new Variable(VarName, variable_data));
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
                        VarBlock.variables.Add(new Variable(VarName, variable_data));
                    else
                        return "BAD_TYPE";
                }
                else
                    VarBlock.variables.Add(new Variable(VarName, new MemorySlot(RealType)));
            }
            else
                return "BAD_TYPE";
            return null;
        }

        static private string UpdateVariable(ref int NextIndex, ref int line_ptr, string Command, Variable variable, bool IsFunc = false)
        {
            MemorySlot variable_data = new MemorySlot();
            if (GetNextSymbol(Command, ref NextIndex, ref line_ptr) == '=')
            {
                string expression;
                var result = GetNextExpression(Command, ref NextIndex, ref line_ptr, out expression, IsFunc);
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

        static private bool GetVariableTypeFromCSharp(dynamic variable, out string type)
        {
            switch (variable.GetType().ToString())
            {
                case "System.Int32":
                    type = "Int";
                    break;
                case "System.Int64":
                    type = "Long";
                    break;
                case "System.Single":
                    type = "Float";
                    break;
                case "System.Double":
                    type = "Double";
                    break;
                case "System.Char":
                    type = "Char";
                    break;
                case "System.String":
                    type = "String";
                    break;
                case "System.Boolean":
                    type = "Bool";
                    break;
                default:
                    type = "Unknown";
                    return false;
            }
            return true;
        }

        static private bool GetVariableTypeFromCEurope(string variable, out string type)
        {
            switch (variable)
            {
                case "ціле":
                    type = "Int";
                    break;
                case "довгоціле":
                    type = "Long";
                    break;
                case "плавати":
                    type = "Float";
                    break;
                case "подвійний":
                    type = "Double";
                    break;
                case "знак":
                    type = "Char";
                    break;
                case "рядок":
                    type = "String";
                    break;
                case "логічний":
                    type = "Bool";
                    break;
                case "таблиця":
                    type = "Table";
                    break;
                default:
                    type = "Unknown";
                    return false;
            }
            return true;
        }

        static private bool GetLiteralType(string literal, out string type)
        {
            if(literal[0] == '\"' && literal[literal.Length - 1] == '\"')
            {
                type = BasicTypes.String.ToString();
                return true;
            }
            if (literal.Length == 3 && literal[0] == '\'' && literal[2] == '\'')
            {
                type = BasicTypes.Char.ToString();
                return true;
            }
            try
            {
                Convert.ToInt32(literal);
                type = BasicTypes.Int.ToString();
                return true;
            }
            catch{ }
            try
            {
                Convert.ToInt64(literal);
                type = BasicTypes.Long.ToString();
                return true;
            }
            catch { }
            try
            {
                Convert.ToDouble(literal);
                type = BasicTypes.Double.ToString();
                return true;
            }
            catch { }
            try
            {
                Convert.ToSingle(literal);
                type = BasicTypes.Float.ToString();
                return true;
            }
            catch { }
            if (literal == "False" || literal == "True" || literal == "false" || literal == "true")
            {
                type = BasicTypes.Bool.ToString();
                return true;
            }
            if (literal == "null")
            {
                type = BasicTypes.Unknown.ToString();
                return true;
            }
            type = BasicTypes.Unknown.ToString();
            return false;
        }

        static private bool CheckLiteralName(string Name)
        {
            if (!KeyWords.Contains(Name) && (char.IsLetter(Name[0]) || Name[0] == '_'))
            {
                for (var i = 1; i < Name.Length; i++)
                    if (!char.IsLetterOrDigit(Name[i]) && Name[i] != '_')
                        return false;
            }
            else
                return false;
            return true;
        }

        static private bool CheckVariableName(string Name, out bool IsTable)
        {
            IsTable = false;
            if (Name.Contains('.') || Name.Contains('['))
                IsTable = true;
            if (!KeyWords.Contains(Name) && (char.IsLetter(Name[0]) || Name[0] == '_'))
            {
                for (var i = 1; i < Name.Length; i++)
                    if (!char.IsLetterOrDigit(Name[i]) && !(Name[i] == '_' || Name[i] == '.' || Name[i] == '[' || Name[i] == ']'))
                        return false;
            }
            else
                return false;
            return true;
        }

        static private bool CheckTableLiteralName(string Name)
        {
            if (!KeyWords.Contains(Name) && (char.IsLetterOrDigit(Name[0]) || Name[0] == '_'))
            {
                for (var i = 1; i < Name.Length; i++)
                    if (!char.IsLetterOrDigit(Name[i]) && Name[i] != '_')
                        return false;
            }
            else
                return false;
            return true;
        }
    }
}