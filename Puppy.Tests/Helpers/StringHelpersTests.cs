#region usings

using NUnit.Framework;
using PuppyFramework.Helpers;

#endregion

namespace Puppy.Tests.Helpers
{
    [TestFixture]
    public class StringHelpersTests
    {
        [Test]
        public void EmptyStringEllipsizeTest()
        {
            Assert.AreEqual("…", string.Empty.Ellipsize());
        }

        [Test]
        public void NullStringEllipsizeTest()
        {
            string foo = null;
            Assert.IsNull(foo.Ellipsize());
        }

        [Test]
        public void NonEmptyStringEllipsizeTest()
        {
            Assert.AreEqual("foobar…", "foobar".Ellipsize());
        }
    }
}