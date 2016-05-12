using System.Text;
using NUnit.Framework;

namespace SqlZip.Tests
{
    [TestFixture]
    public class GZipTests
    {

        [Test]
        public void CompressTestCanHandleEmptyInput()
        {
            byte[] emptyByteArray = new byte[0];

            byte[] compressedEmptyByteArray = GZip.Compress(emptyByteArray);

            Assert.IsInstanceOf<byte[]>(compressedEmptyByteArray);
            Assert.AreEqual(0, compressedEmptyByteArray.Length);

        }

        [Test]
        public void CompressTestCanHandleSimpleInput()
        {
            byte[] smallByteArray = Encoding.Unicode.GetBytes("asimpleteststring");

            byte[] compressedEmptyByteArray = GZip.Compress(smallByteArray);

            Assert.IsInstanceOf<byte[]>(compressedEmptyByteArray);
            Assert.AreNotEqual(0, compressedEmptyByteArray.Length);
            Assert.GreaterOrEqual(compressedEmptyByteArray.Length, smallByteArray.Length);
        }

        [Test]
        public void CompressTestCanHandleComplexInput()
        {
            var sb = new StringBuilder();
            for(int i=0;i<=10000;i++)
            {
                sb.Append( @"!@#$%^&*()_qwertyuiop[ASDFGHJ" + 0 + 256) ;
            }

            byte[] complexByteArray = Encoding.Unicode.GetBytes(sb.ToString());

            byte[] compressedByteArray = GZip.Compress(complexByteArray);

            Assert.IsInstanceOf<byte[]>(compressedByteArray);
            Assert.GreaterOrEqual(complexByteArray.Length, compressedByteArray.Length);
        }

        [Test]
        public void DecompressTestCanHandleEmptyIntput()
        {
            byte[] emptyByteArray = new byte[0];

            byte[] deCompressedEmptyByteArray = GZip.Decompress(emptyByteArray);

            Assert.IsInstanceOf<byte[]>(deCompressedEmptyByteArray);
            Assert.AreEqual(0, deCompressedEmptyByteArray.Length);
        }

        [Test]
        public void DecompressTestCanHandleSimpleInput()
        {
            byte[] smallByteArray = Encoding.Unicode.GetBytes("asimpleteststring");

            byte[] deCompressedEmptyByteArray = GZip.Decompress(GZip.Compress(smallByteArray));

            Assert.IsInstanceOf<byte[]>(deCompressedEmptyByteArray);
            Assert.AreNotEqual(0, deCompressedEmptyByteArray.Length);
            Assert.GreaterOrEqual(smallByteArray.Length, deCompressedEmptyByteArray.Length);
        }

    }
}
