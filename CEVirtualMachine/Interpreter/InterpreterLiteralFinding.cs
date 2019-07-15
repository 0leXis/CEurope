using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEVirtualMachine
{
    static partial class Interpreter
    {
        static private char GetNextSymbol(string source, ref int NextIndex, ref int line_ptr)
        {
            for (var i = NextIndex; i < source.Length; i++)
            {
                if (!IgnoreCharacters.Contains(source[i]))
                {
                    NextIndex = i + 1;
                    return source[i];
                }
                if (source[i] == '\n')
                    line_ptr++;
            }
            return '\0';
        }

        static private bool GetNextExpression(string source, ref int NextIndex, ref int line_ptr, out string expression, bool IsInFunction = false)
        {
            expression = "";
            var brackets = (IsInFunction) ? 1 : 0;
            for (var i = NextIndex; i < source.Length; i++)
            {
                NextIndex++;
                if (source[i] == '(')
                {
                    brackets++;
                    expression += source[i];
                }
                else
                if (source[i] == ')')
                {
                    brackets--;
                    expression += source[i];
                    if (brackets < 0)
                        return false;
                    if (IsInFunction && brackets == 0)
                    {
                        NextIndex = i;
                        return true;
                    }
                }
                else
                if (source[i] == ',')
                {
                    if (IsInFunction && brackets == 1)
                    {
                        NextIndex = i++;
                        return true;
                    }
                    else
                        return false;
                }
                else
                if (source[i] == '\'')
                {
                    if (i + 2 >= source.Length || source[i + 2] != '\'')
                        return false;
                    expression += "\'" + source[i + 1] + "\'";
                    i += 2;
                    NextIndex += i;
                }
                else
                if (source[i] == '\"')
                {
                    string Literal;
                    if(!GetStringLiteral(source, ref i, ref line_ptr, out Literal))
                        return false;
                    NextIndex = i;
                    expression += "\"" + Literal + "\"";
                }
                else
                if(!IgnoreCharacters.Contains(source[i]))
                {
                    expression += source[i];
                }
                if(i < source.Length)
                    if (source[i] == '\n')
                        line_ptr++;
            }
            if (brackets > 0)
                return false;
            else
                return true;
        }

        static private bool GetStringLiteral(string source, ref int NextIndex, ref int line_ptr, out string Literal)
        {
            var LiteralFinded = false;
            Literal = "";

            for (var i = NextIndex; i < source.Length; i++)
            {
                if (LiteralFinded)
                {
                    if (source[i] == '\"')
                    {
                        NextIndex = ++i;
                        return true;
                    }
                    else
                        Literal += source[i];
                }
                else
                if (!IgnoreCharacters.Contains(source[i]))
                {

                    LiteralFinded = true;
                    if (source[i++] != '\"')
                        return false;
                    if (i >= source.Length)
                        return false;
                    Literal += source[i];
                }
                if (source[i] == '\n')
                    line_ptr++;
            }
            NextIndex = source.Length;
            return false;
        }

        static private string GetNextLiteral(string source, ref int NextIndex, ref int line_ptr)
        {
            //DONE: Rework for functions
            var LiteralFinded = false;
            var Literal = "";

            for (var i = NextIndex; i < source.Length; i++)
            {
                if (!IgnoreCharacters.Contains(source[i]))
                {
                    LiteralFinded = true;
                    Literal += source[i];
                    if(source[i] == '(')
                    {
                        NextIndex = i++;
                        return Literal;
                    }
                }
                else
                if (LiteralFinded)
                {
                    NextIndex = i++;
                    return Literal;
                }
                if (source[i] == '\n')
                    line_ptr++;
            }
            NextIndex = source.Length;
            return Literal;
        }
    }
}