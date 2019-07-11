using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CEurope
{
    public partial class FormOptions : Form
    {
        OpenFileDialog _GetInterpreterPath = new OpenFileDialog()
        {
            Filter = "Файл інтерпретатора(*.exe) | *.exe"
        };

        Config cfg;
        public FormOptions(Config cfg)
        {
            InitializeComponent();
            this.cfg = cfg;
        }

        private void ButtonGetInterpreterPath_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBoxInterpreterPath.Text))
                _GetInterpreterPath.InitialDirectory = textBoxInterpreterPath.Text.Substring(0, textBoxInterpreterPath.Text.LastIndexOf('\\'));
            if (_GetInterpreterPath.ShowDialog() == DialogResult.OK)
                textBoxInterpreterPath.Text = _GetInterpreterPath.FileName;
        }

        public void GetDataFromConfig()
        {
            textBoxInterpreterPath.Text = cfg.InterpreterPath;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {

        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            cfg.InterpreterPath = textBoxInterpreterPath.Text;
            cfg.SaveConfig(Program.DEFAULT_CONFIG_FILE);
        }
    }
}
