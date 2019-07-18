using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CEVirtualMachine
{
    static partial class Interpreter
    {
        static private List<Table> Tables = new List<Table>();
        static private Queue<int> UnUsedTableIndexes = new Queue<int>();

        static private int CreateTable()
        {
            int index;
            if (UnUsedTableIndexes.Count > 0)
                index = UnUsedTableIndexes.Dequeue();
            else
                index = Tables.Count;
            Tables.Add(new Table());
            return index;
        }

        static private string InitTable(ref int NextIndex, ref int line_ptr, string Command, int TableIndex)
        {
            var symbol = GetNextSymbol(Command, ref NextIndex, ref line_ptr);
            if (symbol == '{')
            {
                if (CheckNextSymbol(Command, NextIndex, '}'))
                {
                    GetNextSymbol(Command, ref NextIndex, ref line_ptr);
                    return null;
                }
                do
                {
                    var VarName = GetNextLiteral(Command, ref NextIndex, ref line_ptr);
                    if (!CheckTableLiteralName(VarName))
                        return "BAD_LITERALNAME";
                    if (GetNextSymbol(Command, ref NextIndex, ref line_ptr) == '=')
                    {
                        MemorySlot variable_data;
                        if (CheckNextSymbol(Command, NextIndex, '{'))
                        {
                            variable_data = new MemorySlot("Table");
                            var res = InitTable(ref NextIndex, ref line_ptr, Command, Convert.ToInt32(variable_data.Data));
                            if (res != null)
                                return res;
                        }
                        else
                        {
                            string expression;
                            var result = GetNextExpression(Command, ref NextIndex, ref line_ptr, out expression, true, true);
                            if (!result)
                                return "BAD_EXPRESSION";
                            var res = InterpretExpression(expression, out variable_data);
                            if (res != null)
                                return res;
                        }
                        Tables[TableIndex].AddUpdateValue(VarName, variable_data);
                    }
                    else
                        return "BAD_EXPRESSION";
                }
                while (!CheckNextSymbol(Command, NextIndex, '}'));
                GetNextSymbol(Command, ref NextIndex, ref line_ptr);
                return null;
            }
            else
                return "BAD_EXPRESSION";
        }

        static private readonly string[] Separator = { "." };

        static private string UpdateVariableInTable(ref int NextIndex, ref int line_ptr, string Command, Variable variable, string VarPath, bool IsFunc = false)
        {
            var DestinationTable = variable.Data.Data;
            string NormalPath;
            var res = GetNormalizedPath(VarPath, out NormalPath);
            if (res != null)
                return res;
            var PathList = NormalPath.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < PathList.Length - 1; i++)
            {
                MemorySlot tmp_slot;
                try
                {
                    tmp_slot = Tables[Convert.ToInt32(DestinationTable)].GetValue(PathList[i]);
                }
                catch
                {
                    return "BAD_TYPE";
                }
                if (tmp_slot.DataType != "Table")
                    return "BAD_TYPE";
                DestinationTable = tmp_slot.Data;
            }
            MemorySlot variable_data = new MemorySlot();
            if (GetNextSymbol(Command, ref NextIndex, ref line_ptr) == '=')
            {
                if (CheckNextSymbol(Command, NextIndex, '{'))
                {
                    variable_data = new MemorySlot("Table");
                    res = InitTable(ref NextIndex, ref line_ptr, Command, Convert.ToInt32(variable_data.Data));
                    if (res != null)
                        return res;
                }
                else
                {
                    string expression;
                    var result = GetNextExpression(Command, ref NextIndex, ref line_ptr, out expression, IsFunc);
                    if (!result)
                        return "BAD_EXPRESSION";
                    res = InterpretExpression(expression, out variable_data);
                    if (res != null)
                        return res;
                }
                Tables[Convert.ToInt32(DestinationTable)].AddUpdateValue(PathList[PathList.Length - 1], variable_data);
                return null;
            }
            else
                return "BAD_EXPRESSION";
        }

        static private string GetValueFromTable(string Variable, string VarPath, out MemorySlot Value)
        {
            foreach (var block in OpenedBlocks)
            {
                var vrb = block.variables.Find((variable) => variable.Name == Variable);
                if (vrb != null)
                {
                    var DestinationTable = vrb.Data.Data;
                    string NormalPath;
                    var res = GetNormalizedPath(VarPath, out NormalPath);
                    if (res != null)
                    {
                        Value = new MemorySlot();
                        return res;
                    }
                    var PathList = NormalPath.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; i < PathList.Length - 1; i++)
                    {
                        MemorySlot tmp_slot;
                        try
                        {
                            tmp_slot = Tables[Convert.ToInt32(DestinationTable)].GetValue(PathList[i]);
                        }
                        catch
                        {
                            Value = new MemorySlot();
                            return "BAD_TYPE";
                        }
                        if (tmp_slot.DataType != "Table")
                        {
                            Value = new MemorySlot();
                            return "BAD_TYPE";
                        }
                        DestinationTable = tmp_slot.Data;
                    }
                    Value = Tables[Convert.ToInt32(DestinationTable)].GetValue(PathList[PathList.Length - 1]);
                    return null;
                }
            }
            Value = new MemorySlot();
            return "BADNAME_VARIABLE";
        }

        static private string GetNormalizedPath(string Path, out string NormalizedPath)
        {
            NormalizedPath = "";
            var lastcopyindex = 0;
            for (var index = 0; index < Path.Length; index++)
            {
                if (Path[index] == '[')
                {
                    NormalizedPath += Path.Substring(lastcopyindex, index - lastcopyindex);
                    int endindex = index + 1;
                    int brackets = 1;
                    while (brackets != 0)
                    {
                        if (Path[endindex] == '[')
                            brackets++;
                        else
                        if (Path[endindex] == ']')
                            brackets--;
                        endindex++;
                    }
                    lastcopyindex = endindex;
                    endindex--;
                    if (endindex == index + 1)
                        return "BAD_EXPRESSION";
                    var expression_str = Path.Substring(index + 1, endindex - index - 1);
                    string expression;
                    var res = GetExpressionFromString(expression_str, out expression);
                    if (res)
                    {
                        MemorySlot tmp_slot;
                        var Result = InterpretExpression(expression, out tmp_slot);
                        if(Result == null)
                        {
                            NormalizedPath += '.' + tmp_slot.Data;
                        }
                        else
                            return Result;
                    }
                    else
                        return "BAD_EXPRESSION";
                }
            }
            NormalizedPath += Path.Substring(lastcopyindex);
            return null;
        }

        static private string GetNextTablePathElement(string Path, out string LastPath)
        {
            var index = Path.IndexOfAny(new char[] { '.', '[' });
            if (index == -1)
            {
                LastPath = Path;
                return "";
            }
            else
            {
                LastPath = Path.Substring(index);
                return Path.Substring(0, index);
            }
        }
    }
}