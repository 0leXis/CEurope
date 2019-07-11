using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CEurope
{
    static class Program
    {
        public const string DEFAULT_CONFIG_FILE = "config.cfg";

        public static Config config;

        public static Form1 MainForm;
        public static FormOptions OptionsForm;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                config = new Config(DEFAULT_CONFIG_FILE);
            }
            catch
            {
                config = new Config();
            }
            MainForm = new Form1();
            OptionsForm = new FormOptions(config);
            Application.Run(MainForm);
            config.SaveConfig(DEFAULT_CONFIG_FILE);
        }
    }
}
