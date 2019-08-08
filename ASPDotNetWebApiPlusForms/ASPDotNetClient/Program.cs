using ASPDotNetApiProxy;
using ASPDotNetApiProxy.Proxies;
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
            //-- APIのルートURL設定
            HttpClientProxy.SetRootUri(ConfigurationManager.AppSettings["RootUri"]);
            ApiClient.SetRootUrl(new Uri(ConfigurationManager.AppSettings["RootUri"]));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new LoginForm());
            //TODO
            Application.Run(new MainForm());
        }
    }
}
