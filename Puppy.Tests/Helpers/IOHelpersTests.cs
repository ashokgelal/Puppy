#region usings

using System;
using System.IO;
using NUnit.Framework;
using PuppyFramework.Helpers;

#endregion

namespace Puppy.Tests.Helpers
{
    [TestFixture]
    public class IOHelpersTests
    {
	[TestFixtureSetUp]
        public void Setup()
        {
            AssemblyHelpers.SetEntryAssembly();
        }

        [Test]
        public void LocalAppDirTest()
        {
            var dir = IOHelpers.LocalAppDir();
            Assert.True(dir.EndsWith(@"AppData\Local\Puppy.Tests", StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        public void CombineWithLocalAppDataPathTest()
        {
            var path = "foobar.txt".CombineWithLocalAppDataPath();
            Assert.True(path.EndsWith(@"AppData\Local\Puppy.Tests\foobar.txt", StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        public void CombineWithLocalAppDataPathTestNested()
        {
            var path = Path.Combine("foobar", "foobar.txt").CombineWithLocalAppDataPath();
            Assert.True(path.EndsWith(@"AppData\Local\Puppy.Tests\foobar\foobar.txt", StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        public void CombineWithLocalAppDataPathTestRootDir()
        {
            var path =  "foobar.txt".CombineWithLocalAppDataPath("foobar");
            Assert.True(path.EndsWith(@"AppData\Local\foobar\foobar.txt", StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        public void LocalAppDirReplaceWhitespaces()
        {
            var path = IOHelpers.LocalAppDir("foo bar");
            Assert.True(path.EndsWith(@"AppData\Local\foo_bar", StringComparison.CurrentCultureIgnoreCase));
            path = IOHelpers.LocalAppDir("foo bar", false);
            Assert.True(path.EndsWith(@"AppData\Local\foo bar", StringComparison.CurrentCultureIgnoreCase));
        }
    }
}