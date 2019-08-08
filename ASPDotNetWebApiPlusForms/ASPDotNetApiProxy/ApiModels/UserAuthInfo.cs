using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPDotNetApiProxy.ApiModels
{
    /// <summary>
    /// ユーザー認証情報を表します。
    /// </summary>
    public class UserAuthInfo
    {
        #region プロパティ

        /// <summary>
        /// IDを取得します。
        /// </summary>
        public int Id { get; private set; }


        /// <summary>
        /// パスワードを取得します。
        /// </summary>
        public string Password { get; private set; }
        
        #endregion

        #region コンストラクタ
        
        /// <summary>
        /// インスタンスを生成します。
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="password">パスワード</param>
        public UserAuthInfo(int id, string password)
        {
            this.Id = id;
            this.Password = password;
        }
        
        #endregion
    }
}