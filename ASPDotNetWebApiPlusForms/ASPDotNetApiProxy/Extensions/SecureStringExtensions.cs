using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;

namespace ASPDotNetApiProxy.Extensions
{
    /// <summary>
    /// System.Security.SecureStringの拡張機能を提供します。
    /// </summary>
    public static class SecureStringExtensions
    {
        /// <summary>
        /// 文字列を暗号化します。
        /// </summary>
        /// <param name="value">対象文字列</param>
        /// <returns>暗号化された文字列</returns>
        public static System.Security.SecureString Encrypt(this string value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            var secure = new SecureString();
            //value.ForEach(secure.AppendChar);
            value.ToCharArray().ToList().ForEach(c => secure.AppendChar(c));
            return secure;
        }


        /// <summary>
        /// 暗号化された文字列を復号します。
        /// </summary>
        /// <param name="value">暗号化された文字列</param>
        /// <returns>復号された文字列</returns>
        public static string Decrypt(this SecureString value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            var pointer = Marshal.SecureStringToBSTR(value);
            var natural = Marshal.PtrToStringBSTR(pointer);
            Marshal.ZeroFreeBSTR(pointer);
            return natural;
        }
    }
}
