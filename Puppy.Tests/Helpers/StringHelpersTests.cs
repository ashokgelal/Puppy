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

        [Test]
        public void EmptyStringIsValidHttpTest()
        {
           Assert.IsFalse(string.Empty.IsValidHttpUrl()); 
           Assert.IsFalse(StringHelpers.IsValidHttpUrl(null)); 
        }

        [Test]
        public void StringIsInValidHttpTest()
        {
            Assert.IsFalse("not/a/url".IsValidHttpUrl());
            Assert.IsFalse("file://not/apath".IsValidHttpUrl());
        }

        [Test]
        public void StringIsValidHttpTest()
        {
            Assert.IsTrue("https://google.com".IsValidHttpUrl());
            Assert.IsTrue("http://google.com/doodle".IsValidHttpUrl());
            Assert.IsTrue("http://google.com/doodle.html".IsValidHttpUrl());
        }
    }
}