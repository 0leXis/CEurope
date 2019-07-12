namespace CEurope
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.richTextBoxResult = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.створитиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.відкритиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.зберегтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зберегтиВсіToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зберегтиЯкToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.закритиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.закритиВсіToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.вихідToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.виправленняToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.налаштуванняToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.скриптToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.виконатиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.налаштуванняToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonRun = new System.Windows.Forms.ToolStripButton();
            this.tabControlScripts = new System.Windows.Forms.TabControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fastColoredTextBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBoxResult
            // 
            this.richTextBoxResult.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBoxResult.Location = new System.Drawing.Point(0, 26);
            this.richTextBoxResult.Name = "richTextBoxResult";
            this.richTextBoxResult.Size = new System.Drawing.Size(1011, 133);
            this.richTextBoxResult.TabIndex = 2;
            this.richTextBoxResult.Text = "";
            this.richTextBoxResult.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.richTextBox2_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Результат інтерпретації:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.виправленняToolStripMenuItem,
            this.налаштуванняToolStripMenuItem,
            this.скриптToolStripMenuItem,
            this.налаштуванняToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1011, 25);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.створитиToolStripMenuItem,
            this.відкритиToolStripMenuItem,
            this.toolStripSeparator1,
            this.зберегтиToolStripMenuItem,
            this.зберегтиВсіToolStripMenuItem,
            this.зберегтиЯкToolStripMenuItem,
            this.toolStripSeparator2,
            this.закритиToolStripMenuItem,
            this.закритиВсіToolStripMenuItem,
            this.toolStripSeparator3,
            this.вихідToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(50, 21);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // створитиToolStripMenuItem
            // 
            this.створитиToolStripMenuItem.Name = "створитиToolStripMenuItem";
            this.створитиToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.створитиToolStripMenuItem.Text = "Створити";
            this.створитиToolStripMenuItem.Click += new System.EventHandler(this.СтворитиToolStripMenuItem_Click);
            // 
            // відкритиToolStripMenuItem
            // 
            this.відкритиToolStripMenuItem.Name = "відкритиToolStripMenuItem";
            this.відкритиToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.відкритиToolStripMenuItem.Text = "Відкрити";
            this.відкритиToolStripMenuItem.Click += new System.EventHandler(this.ВідкритиToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // зберегтиToolStripMenuItem
            // 
            this.зберегтиToolStripMenuItem.Name = "зберегтиToolStripMenuItem";
            this.зберегтиToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.зберегтиToolStripMenuItem.Text = "Зберегти";
            this.зберегтиToolStripMenuItem.Click += new System.EventHandler(this.ЗберегтиToolStripMenuItem_Click);
            // 
            // зберегтиВсіToolStripMenuItem
            // 
            this.зберегтиВсіToolStripMenuItem.Name = "зберегтиВсіToolStripMenuItem";
            this.зберегтиВсіToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.зберегтиВсіToolStripMenuItem.Text = "Зберегти всі";
            this.зберегтиВсіToolStripMenuItem.Click += new System.EventHandler(this.ЗберегтиВсіToolStripMenuItem_Click);
            // 
            // зберегтиЯкToolStripMenuItem
            // 
            this.зберегтиЯкToolStripMenuItem.Name = "зберегтиЯкToolStripMenuItem";
            this.зберегтиЯкToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.зберегтиЯкToolStripMenuItem.Text = "Зберегти як...";
            this.зберегтиЯкToolStripMenuItem.Click += new System.EventHandler(this.ЗберегтиЯкToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(153, 6);
            // 
            // закритиToolStripMenuItem
            // 
            this.закритиToolStripMenuItem.Name = "закритиToolStripMenuItem";
            this.закритиToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.закритиToolStripMenuItem.Text = "Закрити";
            this.закритиToolStripMenuItem.Click += new System.EventHandler(this.ЗакритиToolStripMenuItem_Click);
            // 
            // закритиВсіToolStripMenuItem
            // 
            this.закритиВсіToolStripMenuItem.Name = "закритиВсіToolStripMenuItem";
            this.закритиВсіToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.закритиВсіToolStripMenuItem.Text = "Закрити всі";
            this.закритиВсіToolStripMenuItem.Click += new System.EventHandler(this.ЗакритиВсіToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(153, 6);
            // 
            // вихідToolStripMenuItem
            // 
            this.вихідToolStripMenuItem.Name = "вихідToolStripMenuItem";
            this.вихідToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.вихідToolStripMenuItem.Text = "Вихід";
            this.вихідToolStripMenuItem.Click += new System.EventHandler(this.ВихідToolStripMenuItem_Click);
            // 
            // виправленняToolStripMenuItem
            // 
            this.виправленняToolStripMenuItem.Name = "виправленняToolStripMenuItem";
            this.виправленняToolStripMenuItem.Size = new System.Drawing.Size(98, 21);
            this.виправленняToolStripMenuItem.Text = "Виправлення";
            // 
            // налаштуванняToolStripMenuItem
            // 
            this.налаштуванняToolStripMenuItem.Name = "налаштуванняToolStripMenuItem";
            this.налаштуванняToolStripMenuItem.Size = new System.Drawing.Size(41, 21);
            this.налаштуванняToolStripMenuItem.Text = "Вид";
            // 
            // скриптToolStripMenuItem
            // 
            this.скриптToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.виконатиToolStripMenuItem});
            this.скриптToolStripMenuItem.Name = "скриптToolStripMenuItem";
            this.скриптToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.скриптToolStripMenuItem.Text = "Скрипт";
            // 
            // виконатиToolStripMenuItem
            // 
            this.виконатиToolStripMenuItem.Name = "виконатиToolStripMenuItem";
            this.виконатиToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.виконатиToolStripMenuItem.Text = "Виконати";
            this.виконатиToolStripMenuItem.Click += new System.EventHandler(this.ВиконатиToolStripMenuItem_Click);
            // 
            // налаштуванняToolStripMenuItem1
            // 
            this.налаштуванняToolStripMenuItem1.Name = "налаштуванняToolStripMenuItem1";
            this.налаштуванняToolStripMenuItem1.Size = new System.Drawing.Size(107, 21);
            this.налаштуванняToolStripMenuItem1.Text = "Налаштування";
            this.налаштуванняToolStripMenuItem1.Click += new System.EventHandler(this.НалаштуванняToolStripMenuItem1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNew,
            this.toolStripButtonOpen,
            this.toolStripButton3,
            this.toolStripButtonSave,
            this.toolStripButtonSaveAll,
            this.toolStripButton2,
            this.toolStripButtonRun});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(1011, 27);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonNew
            // 
            this.toolStripButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNew.Image = global::CEurope.Icons._new;
            this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonNew.Text = "toolStripButton1";
            this.toolStripButtonNew.Click += new System.EventHandler(this.ToolStripButtonNew_Click);
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = global::CEurope.Icons.open;
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonOpen.Text = "toolStripButton2";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.ToolStripButtonOpen_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = global::CEurope.Icons.save;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonSave.Text = "toolStripButton3";
            this.toolStripButtonSave.Click += new System.EventHandler(this.ToolStripButtonSave_Click);
            // 
            // toolStripButtonSaveAll
            // 
            this.toolStripButtonSaveAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSaveAll.Image = global::CEurope.Icons.save_all;
            this.toolStripButtonSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveAll.Name = "toolStripButtonSaveAll";
            this.toolStripButtonSaveAll.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonSaveAll.Text = "toolStripButton4";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonRun
            // 
            this.toolStripButtonRun.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRun.Image = global::CEurope.Icons.start;
            this.toolStripButtonRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRun.Name = "toolStripButtonRun";
            this.toolStripButtonRun.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonRun.Text = "toolStripButton1";
            this.toolStripButtonRun.Click += new System.EventHandler(this.ToolStripButtonRun_Click);
            // 
            // tabControlScripts
            // 
            this.tabControlScripts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlScripts.Location = new System.Drawing.Point(18, 55);
            this.tabControlScripts.Name = "tabControlScripts";
            this.tabControlScripts.SelectedIndex = 0;
            this.tabControlScripts.Size = new System.Drawing.Size(993, 24);
            this.tabControlScripts.TabIndex = 7;
            this.tabControlScripts.SelectedIndexChanged += new System.EventHandler(this.TabControlScripts_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 77);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.fastColoredTextBox1);
            this.splitContainer1.Panel1MinSize = 100;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.richTextBoxResult);
            this.splitContainer1.Panel2MinSize = 100;
            this.splitContainer1.Size = new System.Drawing.Size(1011, 505);
            this.splitContainer1.SplitterDistance = 345;
            this.splitContainer1.SplitterWidth = 7;
            this.splitContainer1.TabIndex = 8;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer1_SplitterMoved);
            // 
            // fastColoredTextBox1
            // 
            this.fastColoredTextBox1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.fastColoredTextBox1.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.fastColoredTextBox1.BackBrush = null;
            this.fastColoredTextBox1.CharHeight = 14;
            this.fastColoredTextBox1.CharWidth = 8;
            this.fastColoredTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fastColoredTextBox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fastColoredTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fastColoredTextBox1.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.fastColoredTextBox1.IsReplaceMode = false;
            this.fastColoredTextBox1.Location = new System.Drawing.Point(0, 0);
            this.fastColoredTextBox1.Name = "fastColoredTextBox1";
            this.fastColoredTextBox1.Paddings = new System.Windows.Forms.Padding(0);
            this.fastColoredTextBox1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fastColoredTextBox1.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("fastColoredTextBox1.ServiceColors")));
            this.fastColoredTextBox1.Size = new System.Drawing.Size(1011, 345);
            this.fastColoredTextBox1.TabIndex = 0;
            this.fastColoredTextBox1.Zoom = 100;
            this.fastColoredTextBox1.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.FastColoredTextBox1_TextChanged);
            this.fastColoredTextBox1.AutoIndentNeeded += new System.EventHandler<FastColoredTextBoxNS.AutoIndentEventArgs>(this.FastColoredTextBox1_AutoIndentNeeded);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 582);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tabControlScripts);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "CEurope IDE";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fastColoredTextBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBoxResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem створитиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem відкритиToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem зберегтиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem зберегтиВсіToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem закритиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem закритиВсіToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem вихідToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem виправленняToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem налаштуванняToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem налаштуванняToolStripMenuItem1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonNew;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveAll;
        private System.Windows.Forms.ToolStripMenuItem скриптToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem виконатиToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonRun;
        private System.Windows.Forms.ToolStripSeparator toolStripButton3;
        private System.Windows.Forms.ToolStripSeparator toolStripButton2;
        private System.Windows.Forms.ToolStripMenuItem зберегтиЯкToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlScripts;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FastColoredTextBoxNS.FastColoredTextBox fastColoredTextBox1;
    }
}