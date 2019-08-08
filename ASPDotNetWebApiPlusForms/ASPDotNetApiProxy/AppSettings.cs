using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using This = ASPDotNetApiProxy.AppSettings;

namespace ASPDotNetApiProxy
{
    /// <summary>
    /// アプリケーションの設定を提供します。
    /// </summary>
    public class AppSettings
    {

        #region プロパティ

        /// <summary>
        /// リクエストトークンの有効期限を取得します。
        /// </summary>
        public static TimeSpan RequestTokenValidDuration { get { return TimeSpan.Parse(This.GetValue()); } }

        /// <summary>
        /// アクセストークンの有効期限を取得します。
        /// </summary>
        public static TimeSpan AccessTokenValidDuration { get { return TimeSpan.Parse(This.GetValue()); } }

        /// <summary>
        /// を取得します。
        /// </summary>
        public static string SqlClient { get { return This.GetValue(); } }

        #endregion



        #region 補助

        /// <summary>
        /// 指定したキーに一致するappSettingsの値を取得します。
        /// </summary>
        /// <param name="key">キー名</param>
        /// <returns>値</returns>
        private static string GetValue([CallerMemberName] string key = null)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            return WebConfigurationManager.AppSettings[key];
        }

        #endregion

    }
}
