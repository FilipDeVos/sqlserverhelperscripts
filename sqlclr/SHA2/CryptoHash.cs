using System;
using System.Data.SqlTypes;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.SqlServer.Server;

namespace SqlServerHelperScripts
{
    public static class SHA2Functions
    {
        [SqlFunction(Name = "fn_md5")]
        public static SqlString Md5(string message)
        {
            return EncodeHash(message, MD5.Create());
        }

        [SqlFunction(Name = "fn_ripemd160")]
        public static SqlString RipEmd160(string message)
        {
            return EncodeHash(message, new RIPEMD160Managed());
        }
        
        [SqlFunction(Name = "fn_sha1")]
        public static SqlString Sha1(string message)
        {
            return EncodeHash(message, new SHA1Managed());
        }

        [SqlFunction(Name = "fn_sha256")]
        public static SqlString Sha256(string message)
        {
            return EncodeHash(message, new SHA256Managed());
        }

        [SqlFunction(Name = "fn_sha384")]
        public static SqlString Sha384(string message)
        {
            return EncodeHash(message, new SHA384Managed());
        }

        [SqlFunction(Name = "fn_sha512")]
        public static SqlString Sha512(string message)
        {
            return EncodeHash(message, new SHA512Managed());
        }

        /// <summary>
        /// Encodes the hash. The resulting string is a simple 2-char hex encoded string. 
        /// If you like base64 just use Convert.ToBase64String(); instead of ByteArrayToString();
        /// </summary>
        /// <param name="message">The message to encode.</param>
        /// <param name="algorithm">The Hashing algorithm.</param>
        /// <returns>Hex encoded hash value.</returns>
        private static string EncodeHash(string message, HashAlgorithm algorithm)
        {
            string saltedMessage = string.Format(CultureInfo.CurrentCulture, "ChocolateSaltyBalls{0}AreSoSalty!", message);
            byte[] saltedBytes = new UTF8Encoding().GetBytes(saltedMessage);

            return BitConverter.ToString(algorithm.ComputeHash(saltedBytes)).Replace("-", string.Empty);
        }
    }
}