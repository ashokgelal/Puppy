#region Usings

using Microsoft.VisualStudio.TestTools.UnitTesting;
using PuppyFramework.Bootstrap;
using System;

#endregion

namespace Puppy.Tests.Bootstrap
{
    [TestClass]
    public class BootstrapperTest
    {
        #region Methods

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void TestDefaultRunMethod()
        {
            new TestBootstrapper().Run(true);
        }

        [TestMethod]
        public void TestRunWithCustomConfig()
        {
            var config = new BootstrapConfig
            {
                EnableMenuService = false,
                EnableUpdaterService = true
            };
            var bootstrapper = new TestBootstrapper();
            bootstrapper.Run(config);
            Assert.IsFalse(bootstrapper.BootstrapConfig.EnableMenuService);
            Assert.IsTrue(bootstrapper.BootstrapConfig.EnableUpdaterService);
        }

        [TestMethod]
        public void TestRunWithDefaultConfig()
        {
            var bootstrapper = new TestBootstrapper();
            bootstrapper.Run();
            Assert.IsTrue(bootstrapper.BootstrapConfig.EnableMenuService);
            Assert.IsFalse(bootstrapper.BootstrapConfig.EnableUpdaterService);
        }

        #endregion
    }

    internal class TestBootstrapper : PuppyBootstrapper
    {
        #region Methods

        protected override void InitializeShell()
        {
            // do nothing
        }

        #endregion
    }
}
