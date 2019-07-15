using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEVirtualMachine
{
    static partial class Interpreter
    {

        struct MemorySlot
        {
            public string DataType;
            public string Data;

            public MemorySlot(string DataType, string Data)
            {
                this.DataType = DataType;
                this.Data = Data;
            }

            public MemorySlot(string DataType)
            {
                this.DataType = DataType;
                switch (DataType)
                {
                    case "Int":
                    case "Long":
                    case "Float":
                    case "Double":
                        Data = "0";
                        break;
                    case "Char":
                        Data = "\0";
                        break;
                    case "String":
                        Data = "";
                        break;
                    case "Boolean":
                        Data = "false";
                        break;
                    default:
                        Data = null;
                        break;
                }
            }

            public bool GetRealTypeVariable(out dynamic RealData)
            {
                switch (DataType)
                {
                    case "Int":
                        RealData = Convert.ToInt32(Data);
                        break;
                    case "Long":
                        RealData = Convert.ToInt64(Data);
                        break;
                    case "Float":
                        RealData = Convert.ToSingle(Data);
                        break;
                    case "Double":
                        RealData = Convert.ToDouble(Data);
                        break;
                    case "Char":
                        RealData = Data[0];
                        break;
                    case "String":
                        RealData = Data;
                        break;
                    case "Boolean":
                        RealData = Convert.ToBoolean(Data);
                        break;
                    default:
                        RealData = null;
                        return false;
                }
                return true;
            }
        }

        class Variable
        {
            public string Name;
            public MemorySlot Data;

            public Variable(string Name, MemorySlot Data)
            {
                this.Name = Name;
                this.Data = Data;
            }
        }

        enum OperatorType
        {
            None, LeftBracket, RightBracket, //100
            Increment, Decrement,//0
            UnaryMinus, UnaryPlus, Not,//1
            Multiply, Divide, ModuleDivide,//2
            Substract, Add,//3
            LeftShift, RightShift,//4
            More, Less, MoreOrEqual, LessOrEqual,//5
            Equal, NotEqual,//6
            And,//7
            Xor,//8
            Or//9
        }

        struct ExpressionMember
        {
            public OperatorType operation;
            public MemorySlot? data;

            public ExpressionMember(OperatorType operation, MemorySlot? data)
            {
                this.operation = operation;
                this.data = data;
            }

            public override string ToString()
            {
                if (data == null)
                    return "Operation: " + operation.ToString();
                else
                    return "Data: " + data.Value.Data;
            }
        }

        enum BlockType { Unknown, Block, Program, NameSpace, While, For, DoUntil, Switch, If, Else };

        struct Block
        {
            public int command_ptr;
            public int NextIndex;
            public BlockType type;
            public List<Variable> variables;

            public Block(int command_ptr, int NextIndex, BlockType type)
            {
                this.command_ptr = command_ptr;
                this.NextIndex = NextIndex;
                this.type = type;
                variables = new List<Variable>();
            }
        }

        enum BasicTypes
        {
            Unknown,
            Int,
            Long,
            Float,
            Double,
            String,
            Char,
            Bool
        }
    }
}