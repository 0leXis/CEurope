using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CEurope
{
    public class Config
    {
        string _InterpreterPath;
        public string InterpreterPath {
            get => _InterpreterPath;
            set
            {
                _InterpreterPath = value;
                _InterpreterDirectoryPath = value.Substring(0, value.LastIndexOf('\\'));
            }
        }

        string _InterpreterDirectoryPath;
        public string InterpreterDirectoryPath { get => _InterpreterDirectoryPath; }
        //Default
        public Config()
        {
            InterpreterPath = @"\..\..\..\..\CEVirtualMachine\bin\Debug\CEVM.exe";
        }

        public Config(string FileName)
        {
            var doc = new XmlDocument();
            doc.Load(FileName);
            var Root = doc.DocumentElement;
            foreach (XmlNode ChildNode1 in Root.ChildNodes)
            {
                if (ChildNode1.Name == "Interpreter")
                {
                    foreach (XmlNode ChildNode2 in ChildNode1.ChildNodes)
                    {
                        if (ChildNode2.Name == "InterpreterPath")
                        {
                            InterpreterPath = ChildNode2.InnerText;
                        }
                    }
                }
            }
        }

        public void SaveConfig(string FileName)
        {
            XDocument xdoc = new XDocument
                (
                new XElement("options",
                            new XElement("Interpreter",
                                        new XElement("InterpreterPath", InterpreterPath)
                                        )
                            )
                );
            xdoc.Save(FileName);
        }
    }
}
