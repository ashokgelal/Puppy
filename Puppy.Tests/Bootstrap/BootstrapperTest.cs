#region Usings

using PuppyFramework.Bootstrap;
using System;
using Xunit;

#endregion

namespace Puppy.Tests.Bootstrap
{
    public class BootstrapperTest
    {
        #region Methods

        [Fact]
        public void TestDefaultRunMethod()
        {
            Assert.Throws<InvalidOperationException>(() => new TestBootstrapper().Run(true));
        }

        [Fact]
        public void TestRunWithCustomConfig()
        {
            var config = new BootstrapConfig
            {
                AddMainMenu = false,
                EnableUpdaterService = true
            };
            var bootstrapper = new TestBootstrapper();
            bootstrapper.Run(config);
            Assert.False(bootstrapper.BootstrapConfig.AddMainMenu);
            Assert.True(bootstrapper.BootstrapConfig.EnableUpdaterService);
        }

        [Fact]
        public void TestRunWithDefaultConfig()
        {
            var bootstrapper = new TestBootstrapper();
            bootstrapper.Run();
            Assert.True(bootstrapper.BootstrapConfig.AddMainMenu);
            Assert.False(bootstrapper.BootstrapConfig.EnableUpdaterService);
        }

        #endregion
    }
}
