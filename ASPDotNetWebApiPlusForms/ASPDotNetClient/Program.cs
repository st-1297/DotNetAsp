using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASPDotNetClient.Forms;

namespace ASPDotNetClient
{
    static class Program
    {
        /// <summary>
        /// APIのルートURL です。
        /// </summary>
        public static string rootUri = ConfigurationManager.AppSettings["RootUri"];

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
