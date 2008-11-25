namespace SqlZip
{
    using System;
    using System.Text;
    using Microsoft.SqlServer.Server;
    
    /// <summary>
    /// SQL Server CLR User defined functions used for gzipping and unzipping data.
    /// </summary>
    public static class UserDefinedFunctions
    {
        /// <summary>
        /// Zips the specified not compressed string.
        /// </summary>
        /// <param name="notCompressed">The not compressed input string.</param>
        /// <returns>a compressed string encoded as Base64</returns>
        [return: SqlFacet(MaxSize = -1)]
        [SqlFunction(Name = "fn_zip_string")]
        public static string ZipString([SqlFacet(MaxSize = -1)] string notCompressed)
        {
            byte[] notCompressedBytes = Encoding.Unicode.GetBytes(notCompressed);

            byte[] compressedBytes = GZip.Compress(notCompressedBytes);

            string base64String = Convert.ToBase64String(compressedBytes, Base64FormattingOptions.None);

            return base64String;
        }

        /// <summary>
        /// Unzips the compressed base64 string.
        /// </summary>
        /// <param name="compressed">The compressed string in Base64 encoding.</param>
        /// <returns>A not compressed unicode string</returns>
        [return: SqlFacet(MaxSize = -1)]
        [SqlFunction(Name = "fn_unzip_string")]
        public static string UnzipString([SqlFacet(MaxSize = -1)] string compressed)
        {
            byte[] compressedBytes = Convert.FromBase64String(compressed);

            byte[] notCompressedBytes = GZip.Decompress(compressedBytes);

            string unicodeString = Encoding.Unicode.GetString(notCompressedBytes);

            return unicodeString;
        }

        /// <summary>
        /// Zips the binary array.
        /// </summary>
        /// <param name="notCompressedBytes">The uncompressed bytes.</param>
        /// <returns>A compressed byte array</returns>
        [return: SqlFacet(MaxSize = -1)]
        [SqlFunction(Name = "fn_zip_binary")]
        public static byte[] ZipBinary([SqlFacet(MaxSize = -1)] byte[] notCompressedBytes)
        {
            return GZip.Compress(notCompressedBytes);
        }

        /// <summary>
        /// Zips the binary array.
        /// </summary>
        /// <param name="compressedBytes">The compressed bytes.</param>
        /// <returns>An uncompressed byte array</returns>
        [return: SqlFacet(MaxSize = -1)]
        [SqlFunction(Name = "fn_unzip_binary")]
        public static byte[] UnzipBinary([SqlFacet(MaxSize = -1)] byte[] compressedBytes)
        {
            return GZip.Decompress(compressedBytes);
        }
    }
}
