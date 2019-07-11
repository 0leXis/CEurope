using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Globalization;
using System.Threading;

namespace CEVirtualMachine
{
    static public partial class Interpreter
    {
        static public bool GetNextExpressionTEST(string source, ref int NextIndex, ref int line_ptr, out string expression, bool IsInFunction = false)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            return GetNextExpression(source, ref NextIndex, ref line_ptr, out expression, IsInFunction);
        }

        static public bool GetLiteralTypeTEST(string literal, out string type)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            return GetLiteralType(literal, out type);
        }

        static public string GetPostfixFormTEST(string expression, out List<string> result)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            List<ExpressionMember> tmp_result;
            var res = GetPostfixForm(expression, out tmp_result);
            result = new List<string>();
            if (res == null)
                foreach (var elem in tmp_result)
                    result.Add(elem.ToString());
            return res;
        }

        static public string InterpretExpressionTEST(string expression, out string result)
        {
            MemorySlot result1;
            var result2 = InterpretExpression(expression, out result1);
            result = result1.Data;
            return result2;
        }
    }
}