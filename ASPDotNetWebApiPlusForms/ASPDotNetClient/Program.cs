using ASPDotNetApiProxy;
using ASPDotNetClient.Forms;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace ASPDotNetClient
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            HttpClientProxy.SetRootUri(ConfigurationManager.AppSettings["RootUri"]);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
