using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CEurope
{
    static class FileActions
    {
        static OpenFileDialog opendialog = new OpenFileDialog()
        {
            Filter = "Файл з кодом CEurope (*.ce)| *.ce"
        };
        static SaveFileDialog savedialog = new SaveFileDialog()
        {
            Filter = "Файл з кодом CEurope (*.ce)|*.ce|Любий тип (*.*)|*.*",
            FilterIndex = 0
        };

        static public bool OpenCodeFile(out string Code, out string FileName)
        {
            if(opendialog.ShowDialog() == DialogResult.OK)
            {
                using(var reader = new StreamReader(opendialog.FileName, Encoding.Default))
                {
                    Code = reader.ReadToEnd();
                    FileName = opendialog.FileName;
                    return true;
                }
            }
            FileName = null;
            Code = null;
            return false;
        }

        static public bool SaveCodeFile(string Code, out string FileName, string DefaultName = null)
        {
            if (DefaultName != null)
                savedialog.FileName = DefaultName;
            if (savedialog.ShowDialog() == DialogResult.OK)
            {
                using (var writer = new StreamWriter(savedialog.FileName, false, Encoding.Default))
                {
                    writer.Write(Code);
                    FileName = savedialog.FileName;
                    return true;
                }
            }
            FileName = null;
            savedialog.FileName = "";
            return false;
        }

        static public void SaveExistingCodeFile(string Code, string FileName)
        {
            using (var writer = new StreamWriter(FileName, false, Encoding.Default))
            {
                writer.Write(Code);
            }
        }
    }
}
