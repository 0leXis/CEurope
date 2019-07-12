using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEVirtualMachine
{
    static partial class Interpreter
    {
        static private string InterpretExpression(string expression, out MemorySlot result)
        {
            List<ExpressionMember> postfix_form;
            var postfix_result = GetPostfixForm(expression, out postfix_form);
            if (postfix_result != null)
            {
                result = new MemorySlot("Unknown", null);
                return postfix_result;
            }

            var OperandStack = new Stack<MemorySlot>();
            try
            {
                MemorySlot Operand1_;
                MemorySlot Operand2_;
                dynamic Operand1;
                dynamic Operand2;
                for (var postfix_item = 0; postfix_item < postfix_form.Count; postfix_item++)
                {
                    if (postfix_form[postfix_item].data == null)
                    {
                        if (postfix_form[postfix_item].operation == OperatorType.Decrement || postfix_form[postfix_item].operation == OperatorType.Increment
                            || postfix_form[postfix_item].operation == OperatorType.Not || postfix_form[postfix_item].operation == OperatorType.UnaryMinus
                            || postfix_form[postfix_item].operation == OperatorType.UnaryPlus)
                        {
                            Operand1_ = OperandStack.Pop();
                            if (Operand1_.GetRealTypeVariable(out Operand1))
                            {
                                switch (postfix_form[postfix_item].operation)
                                {
                                    case OperatorType.Decrement:
                                        Operand1--;
                                        OperandStack.Push(new MemorySlot(Operand1_.DataType, Operand1.ToString()));
                                        break;
                                    case OperatorType.Increment:
                                        Operand1++;
                                        OperandStack.Push(new MemorySlot(Operand1_.DataType, Operand1.ToString()));
                                        break;
                                    case OperatorType.UnaryMinus:
                                        Operand1 = -Operand1;
                                        OperandStack.Push(new MemorySlot(Operand1_.DataType, Operand1.ToString()));
                                        break;
                                    case OperatorType.UnaryPlus:
                                        Operand1 = +Operand1;
                                        OperandStack.Push(new MemorySlot(Operand1_.DataType, Operand1.ToString()));
                                        break;
                                    case OperatorType.Not:
                                        Operand1 = !Operand1;
                                        OperandStack.Push(new MemorySlot(Operand1_.DataType, Operand1.ToString()));
                                        break;
                                }
                            }
                            else
                            {
                                result = new MemorySlot("Unknown", null);
                                return "BAD_CAST";
                            }
                        }
                        else
                        {
                            Operand2_ = OperandStack.Pop();
                            Operand1_ = OperandStack.Pop();
                            if (Operand1_.GetRealTypeVariable(out Operand1) | Operand2_.GetRealTypeVariable(out Operand2))
                            {
                                switch (postfix_form[postfix_item].operation)
                                {
                                    case OperatorType.Add:
                                        Operand1 = Operand1 + Operand2;
                                        string var_type;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.And:
                                        Operand1 = Operand1 & Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.Divide:
                                        Operand1 = Operand1 / Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.Equal:
                                        Operand1 = Operand1 == Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.LeftShift:
                                        Operand1 = Operand1 << Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.Less:
                                        Operand1 = Operand1 < Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.LessOrEqual:
                                        Operand1 = Operand1 <= Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.ModuleDivide:
                                        Operand1 = Operand1 % Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.More:
                                        Operand1 = Operand1 > Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.MoreOrEqual:
                                        Operand1 = Operand1 >= Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.Multiply:
                                        Operand1 = Operand1 * Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.NotEqual:
                                        Operand1 = Operand1 != Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.Or:
                                        Operand1 = Operand1 | Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.RightShift:
                                        Operand1 = Operand1 >> Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.Substract:
                                        Operand1 = Operand1 - Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    case OperatorType.Xor:
                                        Operand1 = Operand1 ^ Operand2;
                                        if (!GetVariableTypeFromCSharp(Operand1, out var_type))
                                        {
                                            result = new MemorySlot("Unknown", null);
                                            return "BAD_TYPE";
                                        }
                                        OperandStack.Push(new MemorySlot(var_type, Operand1.ToString()));
                                        break;
                                    default:
                                        result = new MemorySlot("Unknown", null);
                                        return "BAD_OPERATOR";
                                }
                            }
                            else
                            {
                                result = new MemorySlot("Unknown", null);
                                return "BAD_CAST";
                            }
                        }
                    }
                    else
                    {
                        if (postfix_form[postfix_item].data == null)
                        {
                            result = new MemorySlot("Unknown", null);
                            return "BAD_VARIABLE";
                        }
                        OperandStack.Push(postfix_form[postfix_item].data.Value);
                    }
                }
            }
            catch (InvalidCastException)
            {
                result = new MemorySlot("Unknown", null);
                return "BAD_TYPE";
            }
            catch
            {
                result = new MemorySlot("Unknown", null);
                return "BAD_EXPRESSION";
            }
            if(OperandStack.Count != 1)
            {
                result = new MemorySlot("Unknown", null);
                return "BAD_EXPRESSION";
            }
            result = OperandStack.Pop();
            return null;
        }

        static private string GetPostfixForm(string expression, out List<ExpressionMember> result)
        {
            //TODO: извлечь значение из функции и заменить заголовки результатом функции
            result = new List<ExpressionMember>();
            var operators = new Stack<OperatorType>();

            var token = "";
            var IsOperator = false;
            var IsTokenFound = false;
            var IsStringFinding = false;
            for (var i = 0; i < expression.Length; i++)
            {
                if (IsStringFinding && expression[i] != '\"')
                {
                    token += expression[i];
                }
                else
                if(expression[i] == '\"' || expression[i] == '\'' || expression[i] == '(' || expression[i] == ')')
                {
                    if (IsTokenFound)
                    {
                        if (IsOperator)
                        {
                            OperatorType token_type;
                            var Result = GetOperatorType(token, (i - 1 == -1) ? null : (char?)expression[i - 1], out token_type);
                            if (!Result)
                                return "BAD_OPERATOR";
                            if (operators.Count > 0)
                                while (operators.Count > 0 && OperatorPriority[operators.Peek()] <= OperatorPriority[token_type])
                                {
                                    result.Add(new ExpressionMember(operators.Pop(), null));
                                }
                            operators.Push(token_type);
                            token = "";
                            IsOperator = false;
                            IsTokenFound = false;
                        }
                        else
                        {
                            var res = AddValue(token, result);
                            if (res != null)
                                return res;
                            token = "";
                            IsOperator = false;
                            IsTokenFound = false;
                        }
                    }
                    switch (expression[i])
                    {
                        case '\"':
                            if (IsStringFinding)
                            {
                                IsStringFinding = false;
                                result.Add(new ExpressionMember(OperatorType.None, new MemorySlot("String", token)));
                                token = "";
                            }
                            else
                                IsStringFinding = true;
                            break;
                        case '\'':
                            result.Add(new ExpressionMember(OperatorType.None, new MemorySlot("Char", expression[++i] + "")));
                            if(expression[++i] != '\'')
                                return "BAD_TYPE";
                            break;
                        case '(':
                            operators.Push(OperatorType.LeftBracket);
                            break;
                        case ')':
                            var tmp_token = operators.Pop();
                            while (tmp_token != OperatorType.LeftBracket)
                            {
                                result.Add(new ExpressionMember(tmp_token, null));
                                if (operators.Count == 0)
                                    return "BAD_EXPRESSION";
                                tmp_token = operators.Pop();
                            }
                            break;
                    }
                }
                else
                if (IsTokenFound)
                {
                    if (IsOperator)
                    {
                        if (operator_tokens.Contains(expression[i]) && expression[i] != '!')
                        {
                            token += expression[i];
                        }
                        else
                        {
                            i--;
                            OperatorType token_type;
                            var Result = GetOperatorType(token, (i-1 == -1) ? null : (char?)expression[i - 1], out token_type);
                            if (!Result)
                                return "BAD_OPERATOR";
                            if (operators.Count > 0)
                                while (operators.Count > 0 && OperatorPriority[operators.Peek()] <= OperatorPriority[token_type])
                                {
                                    result.Add(new ExpressionMember(operators.Pop(), null));
                                }
                            operators.Push(token_type);
                            token = "";
                            IsOperator = false;
                            IsTokenFound = false;
                        }
                    }
                    else
                    {
                        if (!operator_tokens.Contains(expression[i]))
                        {
                            token += expression[i];
                        }
                        else
                        {
                            i--;
                            var res = AddValue(token, result);
                            if (res != null)
                                return res;
                            token = "";
                            IsOperator = false;
                            IsTokenFound = false;
                        }
                    }
                }
                else
                {
                    IsTokenFound = true;
                    if (operator_tokens.Contains(expression[i]))
                    {
                        IsOperator = true;
                    }
                    else
                    {
                        IsOperator = false;
                    }
                    token += expression[i];
                }
            }
            if (token != "")
            {
                var res = AddValue(token, result);
                if (res != null)
                    return res;
            }

            while (operators.Count > 0)
                result.Add(new ExpressionMember(operators.Pop(), null));

            return null;
        }

        static private string AddValue(string Value, List<ExpressionMember> destination_list)
        {
            string token_type;
            if (!GetLiteralType(Value, out token_type))
            {
                foreach(var block in OpenedBlocks)
                {
                    var vrb = block.variables.Find((variable) => variable.Name == Value);
                    if(vrb != null)
                    {
                        destination_list.Add(new ExpressionMember(OperatorType.None, new MemorySlot(vrb.Data.DataType, vrb.Data.Data)));
                        return null;
                    }
                }
                return "BADNAME_VARIABLE";
            }
            destination_list.Add(new ExpressionMember(OperatorType.None, new MemorySlot(token_type, Value)));
            return null;
        }

        static private bool GetOperatorType(string operation, char? pred_symbol, out OperatorType type)
        {
            switch (operation)
            {
                case "(":
                    type = OperatorType.LeftBracket;
                    break;
                case ")":
                    type = OperatorType.RightBracket;
                    break;
                case "++":
                    type = OperatorType.Increment;
                    break;
                case "--":
                    type = OperatorType.Decrement;
                    break;
                case "-":
                    if (pred_symbol == null || pred_symbol == '(')
                        type = OperatorType.UnaryMinus;
                    else
                        type = OperatorType.Substract;
                    break;
                case "+":
                    if (pred_symbol == null || pred_symbol == '(')
                        type = OperatorType.UnaryPlus;
                    else
                        type = OperatorType.Add;
                    break;
                case "!":
                    type = OperatorType.Not;
                    break;
                case "*":
                    type = OperatorType.Multiply;
                    break;
                case "/":
                    type = OperatorType.Divide;
                    break;
                case "%":
                    type = OperatorType.ModuleDivide;
                    break;
                case "<<":
                    type = OperatorType.LeftShift;
                    break;
                case ">>":
                    type = OperatorType.RightShift;
                    break;
                case ">":
                    type = OperatorType.More;
                    break;
                case "<":
                    type = OperatorType.Less;
                    break;
                case ">=":
                    type = OperatorType.MoreOrEqual;
                    break;
                case "<=":
                    type = OperatorType.LessOrEqual;
                    break;
                case "==":
                    type = OperatorType.Equal;
                    break;
                case "!=":
                    type = OperatorType.NotEqual;
                    break;
                case "&":
                    type = OperatorType.And;
                    break;
                case "^":
                    type = OperatorType.Xor;
                    break;
                case "|":
                    type = OperatorType.Or;
                    break;
                default:
                    type = OperatorType.None;
                    return false;
            }
            return true;
        }
    }
}