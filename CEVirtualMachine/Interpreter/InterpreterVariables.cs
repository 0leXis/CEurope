using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEVirtualMachine
{
    static partial class Interpreter
    {
        static private bool GetVariableType(dynamic variable, out string type)
        {
            if(variable == null)
            {
                type = "NULL";
                return true;
            }
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
                    type = "Boolean";
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
            type = BasicTypes.Unknown.ToString();
            return false;
        }
    }
}