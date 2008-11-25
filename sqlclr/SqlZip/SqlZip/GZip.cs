using System.Runtime.InteropServices.ComTypes;

namespace SqlZip
{
    using System.IO;
    using System.IO.Compression;

    /// <summary>
    /// Helper class that wraps the GzipStream object to get compressed and uncompressed byte arrays as in and output.
    /// </summary>
    public static class GZip
    {
        /// <summary>
        /// Compresses the specified uncompressed buffer.
        /// </summary>
        /// <param name="uncompressedBuffer">The uncompressed buffer.</param>
        /// <returns>The compressed byte array.</returns>
        public static byte[] Compress(byte[] uncompressedBuffer)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    gzip.Write(uncompressedBuffer, 0, uncompressedBuffer.Length);
                }

                byte[] compressedBuffer = ms.ToArray();
                return compressedBuffer;
            }
        }

        /// <summary>
        /// Decompresses the specified compressed buffer.
        /// </summary>
        /// <param name="compressedBuffer">The compressed buffer.</param>
        /// <returns>The uncompressed byte array</returns>
        public static byte[] Decompress(byte[] compressedBuffer)
        {
            using (GZipStream gzip = new GZipStream(new MemoryStream(compressedBuffer), CompressionMode.Decompress))
            {
                byte[] uncompressedBuffer = ReadAllBytes(gzip);
                return uncompressedBuffer;
            }
        }

        private static byte[] ReadAllBytes(Stream stream)
        {
            byte[] buffer = new byte[4096];
            using (MemoryStream ms = new MemoryStream())
            {
                int bytesRead = 0;
                do
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        ms.Write(buffer, 0, bytesRead);
                    }
                } 
                while (bytesRead > 0);

                return ms.ToArray();
            }
        }
    }
}
