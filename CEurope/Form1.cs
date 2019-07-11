using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FastColoredTextBoxNS;

namespace CEurope
{

    public partial class Form1 : Form
    {
        List<ScriptInfo> Scripts = new List<ScriptInfo>();
        int LastScript = -1;

        public Form1()
        {
            InitializeComponent();
        }

        public void RichTextBox_MouseWheel(object Sender, MouseEventArgs e)
        {
            var RealSender = Sender as FastColoredTextBox;
            if (e.Delta > 0 && Control.ModifierKeys == Keys.Control && RealSender.Font.Size < 72)
            {
                RealSender.Font = new Font("Courier New", RealSender.Font.Size + 1, FontStyle.Regular);
            }
            else
            if (e.Delta < 0 && Control.ModifierKeys == Keys.Control && RealSender.Font.Size > 5)
            {
                RealSender.Font = new Font("Courier New", RealSender.Font.Size - 1, FontStyle.Regular);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fastColoredTextBox1.MouseWheel += new MouseEventHandler(RichTextBox_MouseWheel);
            fastColoredTextBox1.Language = Language.Custom;
            fastColoredTextBox1.AutoIndent = true;
            fastColoredTextBox1.AutoIndentExistingLines = true;
            fastColoredTextBox1.Visible = false;
            tabControlScripts.Visible = false;
            DisableCodeNeededButtons();
        }

        private void DisableCodeNeededButtons()
        {
            виконатиToolStripMenuItem.Enabled = false;
            закритиToolStripMenuItem.Enabled = false;
            закритиВсіToolStripMenuItem.Enabled = false;
            зберегтиToolStripMenuItem.Enabled = false;
            зберегтиВсіToolStripMenuItem.Enabled = false;
            зберегтиЯкToolStripMenuItem.Enabled = false;
            toolStripButtonRun.Enabled = false;
            toolStripButtonSave.Enabled = false;
            toolStripButtonSaveAll.Enabled = false;
        }

        private void EnableCodeNeededButtons()
        {
            виконатиToolStripMenuItem.Enabled = true;
            закритиToolStripMenuItem.Enabled = true;
            закритиВсіToolStripMenuItem.Enabled = true;
            зберегтиToolStripMenuItem.Enabled = true;
            зберегтиВсіToolStripMenuItem.Enabled = true;
            зберегтиЯкToolStripMenuItem.Enabled = true;
            toolStripButtonRun.Enabled = true;
            toolStripButtonSave.Enabled = true;
            toolStripButtonSaveAll.Enabled = true;
        }

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void FastColoredTextBox1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            TextHighlite.SetStyles((sender as FastColoredTextBox).Range, e.ChangedRange);
        }

        static public readonly string BeginBlockOperatorsPattern = "програма|простірімен|почати|робити|перемикач";
        static public readonly string BeginLineOperatorsPattern = "якщо|інакше|поки|для|відбирати|зазамовчуванням";
        static public readonly string EndBlockOperatorsPattern = "кінець|нераніше";

        static public readonly string[] BeginBlockOperators = BeginBlockOperatorsPattern.Split('|');
        static public readonly string[] BeginLineOperators = BeginLineOperatorsPattern.Split('|');
        static public readonly string[] EndBlockOperators = EndBlockOperatorsPattern.Split('|');
        private void FastColoredTextBox1_AutoIndentNeeded(object sender, AutoIndentEventArgs e)
        {
            foreach (var @operator in BeginBlockOperators)
                if (e.LineText.Trim().Contains(@operator))
                {
                    e.ShiftNextLines = e.TabLength;
                    return;
                }

            foreach (var @operator in EndBlockOperators)
                if (e.LineText.Trim().Contains(@operator))
                {
                    e.Shift = -e.TabLength;
                    e.ShiftNextLines = -e.TabLength;
                    return;
                }

            foreach (var @operator in BeginLineOperators)
                if (e.PrevLineText.Trim().Contains(@operator))
                {
                    e.Shift = e.TabLength;
                    return;
                }
        }

        private void FastColoredTextBox1_Pasting(object sender, TextChangingEventArgs e)
        {
            fastColoredTextBox1.DoAutoIndent();
        }

        private void НалаштуванняToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Program.OptionsForm.GetDataFromConfig();
            Program.OptionsForm.ShowDialog();
        }

        private void ВихідToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ВідкритиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Code;
            string tmp_filename;
            if (FileActions.OpenCodeFile(out Code, out tmp_filename))
            {
                tabControlScripts.Visible = true;
                fastColoredTextBox1.Visible = true;
                if (tabControlScripts.TabCount != 0)
                    Scripts[tabControlScripts.SelectedIndex].Script = fastColoredTextBox1.Text;
                Scripts.Add(new ScriptInfo(tmp_filename, Code));
                tabControlScripts.TabPages.Add(tmp_filename.Substring(tmp_filename.LastIndexOf('\\') + 1));
                tabControlScripts.SelectedIndex = tabControlScripts.TabCount - 1;
                EnableCodeNeededButtons();
            }
        }

        private void ЗберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Scripts[tabControlScripts.SelectedIndex].FileName == null)
            {
                string tmp_filename;
                if (FileActions.SaveCodeFile(fastColoredTextBox1.Text, out tmp_filename))
                {
                    richTextBoxResult.AppendText("Сохранено" + Environment.NewLine);
                    Scripts[tabControlScripts.SelectedIndex].FileName = tmp_filename;
                    tabControlScripts.TabPages[tabControlScripts.SelectedIndex].Text = tmp_filename.Substring(tmp_filename.LastIndexOf('\\') + 1);
                }
            }
            else
            {
                FileActions.SaveExistingCodeFile(fastColoredTextBox1.Text, Scripts[tabControlScripts.SelectedIndex].FileName);
            }
        }

        private void ToolStripButtonOpen_Click(object sender, EventArgs e)
        {
            ВідкритиToolStripMenuItem_Click(sender, e);
        }

        private void ToolStripButtonSave_Click(object sender, EventArgs e)
        {
            ЗберегтиToolStripMenuItem_Click(sender, e);
        }

        private void ВиконатиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ЗберегтиToolStripMenuItem_Click(sender, e);
            toolStripButtonRun.Enabled = false;
            створитиToolStripMenuItem.Enabled = false;
            InterpretActions.Interpret(Program.config.InterpreterPath, Program.config.InterpreterDirectoryPath, Scripts[tabControlScripts.SelectedIndex].FileName, new EventHandler(OnInterpretExit));
        }

        public void OnInterpretExit(object sender, EventArgs e)
        {
            if (File.Exists(Program.config.InterpreterDirectoryPath + "\\" + InterpretActions.INTERPRETER_OUT))
                using (var reader = new StreamReader(Program.config.InterpreterDirectoryPath + "\\" + InterpretActions.INTERPRETER_OUT, Encoding.Default))
                {
                    Program.MainForm.Invoke(new Action(() =>
                    {
                        richTextBoxResult.Clear();
                        richTextBoxResult.Text = reader.ReadToEnd();
                        var Index = richTextBoxResult.Text.IndexOf("SERROR: ПОМИЛКА: ", 0);
                        while (Index != -1)
                        {
                            richTextBoxResult.Select(Index, richTextBoxResult.Text.IndexOf("\n", Index + 1) - Index++);
                            richTextBoxResult.SelectionColor = Color.Red;
                            Index = richTextBoxResult.Text.IndexOf("SERROR: ПОМИЛКА: ", Index);
                        }
                    }));
                }
            Program.MainForm.Invoke(new Action(() =>
            {
                toolStripButtonRun.Enabled = true;
                створитиToolStripMenuItem.Enabled = true;
            }));
        }

        private void ToolStripButtonRun_Click(object sender, EventArgs e)
        {
            ВиконатиToolStripMenuItem_Click(sender, e);
        }

        private void СтворитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlScripts.Visible = true;
            fastColoredTextBox1.Visible = true;
            if (tabControlScripts.TabCount != 0)
                Scripts[tabControlScripts.SelectedIndex].Script = fastColoredTextBox1.Text;

            Scripts.Add(new ScriptInfo(null, ""));
            tabControlScripts.TabPages.Add("Безимянный");
            tabControlScripts.SelectedIndex = tabControlScripts.TabCount - 1;
            EnableCodeNeededButtons();
        }

        private void ToolStripButtonNew_Click(object sender, EventArgs e)
        {
            СтворитиToolStripMenuItem_Click(sender, e);
        }

        private void ЗберегтиЯкToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tmp_filename;
            if (FileActions.SaveCodeFile(fastColoredTextBox1.Text, out tmp_filename))
            {
                richTextBoxResult.AppendText("Сохранено" + Environment.NewLine);
                Scripts[tabControlScripts.SelectedIndex].FileName = tmp_filename;
                tabControlScripts.TabPages[tabControlScripts.SelectedIndex].Text = tmp_filename.Substring(tmp_filename.LastIndexOf('\\') + 1);
            }
        }

        private void ЗберегтиВсіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripts[tabControlScripts.SelectedIndex].Script = fastColoredTextBox1.Text;
            for(var i = 0; i < Scripts.Count; i++)
            {
                if (Scripts[i].FileName == null)
                {
                    string tmp_filename;
                    if (FileActions.SaveCodeFile(Scripts[i].Script, out tmp_filename, "Скрипт"+i))
                    {
                        richTextBoxResult.AppendText("Сохранено" + Environment.NewLine);
                        Scripts[i].FileName = tmp_filename;
                        tabControlScripts.TabPages[i].Text = tmp_filename.Substring(tmp_filename.LastIndexOf('\\') + 1);
                    }
                }
                else
                {
                    FileActions.SaveExistingCodeFile(fastColoredTextBox1.Text, Scripts[i].FileName);
                }
            }
        }

        private void ЗакритиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(tabControlScripts.TabCount == 1)
            {
                DisableCodeNeededButtons();
            }
            var Index = tabControlScripts.SelectedIndex;
            tabControlScripts.TabPages.RemoveAt(tabControlScripts.SelectedIndex);
            Scripts.RemoveAt(Index);
        }

        private void ЗакритиВсіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisableCodeNeededButtons();
            for(var i = 0; i < Scripts.Count; i++)
            {
                tabControlScripts.TabPages.RemoveAt(i);
                Scripts.RemoveAt(i);
                i--;
            }
        }

        private void TabControlScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlScripts.SelectedIndex != -1)
            {
                tabControlScripts.Visible = true;
                fastColoredTextBox1.Visible = true;
                if (LastScript != -1)
                    Scripts[LastScript].Script = fastColoredTextBox1.Text;
                fastColoredTextBox1.Clear();
                fastColoredTextBox1.Text = Scripts[tabControlScripts.SelectedIndex].Script;
            }
            else
            {
                tabControlScripts.Visible = false;
                fastColoredTextBox1.Visible = false;
            }
            LastScript = tabControlScripts.SelectedIndex;
        }

        private void SplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            richTextBoxResult.Width = richTextBoxResult.Parent.Width;
            richTextBoxResult.Height = richTextBoxResult.Parent.Height - 30;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (Width < 500)
                Width = 500;
            if (Height < 500)
                Height = 500;
            richTextBoxResult.Width = richTextBoxResult.Parent.Width;
            richTextBoxResult.Height = richTextBoxResult.Parent.Height - 30;
            splitContainer1.Width = splitContainer1.Parent.Width - 16;
            splitContainer1.Height = splitContainer1.Parent.Height - 116;
        }
    }

    public class ScriptInfo
    {
        public string FileName;
        public string Script;

        private ScriptInfo()
        {
        }

        public ScriptInfo(string FileName, string Script)
        {
            this.FileName = FileName;
            this.Script = Script;
        }
    }
}
