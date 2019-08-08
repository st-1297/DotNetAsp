using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ASPDotNetApiProxy
{

    /// <summary>
    /// 暗号化サービスを提供します。
    /// </summary>
    public class CryptoService
    {
        #region フィールド
        
        /// <summary>
        /// 暗号化アルゴリズムを保持します。
        /// </summary>
        private readonly SymmetricAlgorithm algorithm = null;
        
        #endregion


        #region コンストラクタ

        /// <summary>
        /// 暗号化キーを指定してAES暗号化を行うためのインスタンスを生成します。
        /// </summary>
        /// <param name="key">暗号化キー</param>
        /// <remarks>Unicode変換後に32ビット以上あるキーを指定する必要があります。</remarks>
        public CryptoService(string key)
        {
            this.algorithm = new AesCryptoServiceProvider();
            this.algorithm.Key = Encoding.Unicode.GetBytes(key)
                                .Take(this.algorithm.Key.Length)
                                .OrderByDescending(x => x)  //--- ちょっと小細工しておく
                                .ToArray();
        }


        /// <summary>
        /// 暗号化キーを指定してAES暗号化を行うためのインスタンスを生成します。
        /// </summary>
        /// <param name="key">暗号化キー</param>
        public CryptoService(byte[] key)
            : this(new AesCryptoServiceProvider() { Key = key })
        { }


        /// <summary>
        /// 暗号化アルゴリズムを指定してインスタンスを生成します。
        /// </summary>
        /// <param name="algorithm"></param>
        public CryptoService(SymmetricAlgorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        #endregion


        #region メソッド
        
        /// <summary>
        /// 文字列を暗号化します。
        /// </summary>
        /// <param name="text">暗号化する文字列</param>
        /// <returns>暗号化されたデータ</returns>
        public byte[] Encrypt(string text)
        {
            this.algorithm.GenerateIV();
            var encryptor = this.algorithm.CreateEncryptor();
            using (var memoryStream = new MemoryStream())
            {
                //--- IVは自動生成されたものを利用
                //--- IVを暗号化なしでヘッダに書き込み
                memoryStream.Write(this.algorithm.IV, 0, this.algorithm.IV.Length);

                //--- 本文を暗号化しながら続けて書き込み
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    var buffer = Encoding.Unicode.GetBytes(text);
                    cryptoStream.Write(buffer, 0, buffer.Length);
                }
                return memoryStream.ToArray();
            }
        }


        /// <summary>
        /// 暗号化されたデータを文字列に復号します。
        /// </summary>
        /// <param name="encrypted">暗号化されたデータ</param>
        /// <returns>復号された文字列</returns>
        public string Decrypt(byte[] encrypted)
        {
            using (var memoryStream = new MemoryStream(encrypted))
            {
                //--- IVをヘッダから取得
                var iv = new byte[this.algorithm.IV.Length];
                memoryStream.Read(iv, 0, iv.Length);

                //--- 復号
                this.algorithm.IV = iv;
                var decryptor = this.algorithm.CreateDecryptor();
                using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                using (var outputStream = new MemoryStream())
                {
                    var buffer = new byte[encrypted.Length];
                    while (true)
                    {
                        var length = cryptoStream.Read(buffer, 0, buffer.Length);
                        if (length <= 0)
                            break;
                        outputStream.Write(buffer, 0, length);
                    }
                    return Encoding.Unicode.GetString(outputStream.ToArray());
                }
            }
        }
        
        #endregion
    }
}
