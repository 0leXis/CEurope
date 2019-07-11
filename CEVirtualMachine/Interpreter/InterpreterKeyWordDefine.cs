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
            if (KeyWords.Contains(NameSpaceName) || !char.IsLetter(NameSpaceName[1]) || Defines.Contains(NameSpaceName))
            {
                return "BAD_NAMESPACE";
            }
            else
            {
                Defines.Add(NameSpaceName);
                DefinedNamespaces.Push(NameSpaceName);
                OpenedBlocks.Push(new Block(command_ptr, BlockType.NameSpace));
                NameSpaceDefined = true;
                return null;
            }
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

        static private string EndBlock(int command_ptr, ref bool NameSpaceDefined, ref bool ProgramDefined)
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
    }
}