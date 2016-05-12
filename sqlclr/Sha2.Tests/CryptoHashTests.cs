using NUnit.Framework;

namespace Sha2.Tests
{
    [TestFixture]
    public class CryptoHashTests
    {
        [Test]
        public void Md5ShouldEncodeString()
        {
            var result = CryptoHash.Md5("test");
            Assert.That(result, Is.Not.Null);
        }
    }
}
