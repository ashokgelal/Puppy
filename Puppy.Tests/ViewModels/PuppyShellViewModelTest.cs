#region Usings

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using PuppyFramework;
using PuppyFramework.Interfaces;
using PuppyFramework.Services;
using PuppyFramework.ViewModels;
using Serilog;

#endregion

namespace Puppy.Tests.ViewModels
{
    [TestFixture]
    public class PuppyShellViewModelTest
    {
        private SerilogLogger _logger;

        #region Methods

	[SetUp]
        public void Setup()
        {
            _logger = new SerilogLogger("Test", new LoggerConfiguration().WriteTo.Console());
        }

        [Test]
        public async Task TestClosingCommand()
        {
            var applicationHandler = Substitute.For<IApplicationCloseHandler>();
            applicationHandler.ShouldCloseApplicationAsync().ReturnsForAnyArgs(info => Task.FromResult(UserPromptResult.Yes));
            var model = new DefaultShellViewModel(_logger)
            {
                ApplicationCloseHandler = new Lazy<IApplicationCloseHandler>(() => applicationHandler)
            };
            await model.AppClosingCommand.Execute(new CancelEventArgs());
            applicationHandler.Received(1).ShouldCloseApplicationAsync();
        }

        [Test]
        public async Task TestClosingCommandCancel_NoApplicationHandler()
        {
            var model = new DefaultShellViewModel(_logger);
            var cancelEventArgs = new CancelEventArgs();
            await model.AppClosingCommand.Execute(cancelEventArgs);
            Assert.False(cancelEventArgs.Cancel);
        }

        [Test]
        public async Task TestClosingCommandCancel_WithApplicationHandler()
        {
            var applicationHandler = Substitute.For<IApplicationCloseHandler>();
            applicationHandler.ShouldCloseApplicationAsync().Returns(Task.FromResult(UserPromptResult.Yes));
            var model = new DefaultShellViewModel(_logger)
            {
                ApplicationCloseHandler = new Lazy<IApplicationCloseHandler>(() => applicationHandler)
            };
            var cancelEventArgs = new CancelEventArgs();
            await model.AppClosingCommand.Execute(cancelEventArgs);
            Assert.False(cancelEventArgs.Cancel);
        }

        [Test]
        public async Task TestClosingCommandNoCancel_WithApplicationHandler()
        {
            var applicationHandler = Substitute.For<IApplicationCloseHandler>();
            applicationHandler.ShouldCloseApplicationAsync().Returns(Task.FromResult(UserPromptResult.No));
            var model = new DefaultShellViewModel(_logger)
            {
                ApplicationCloseHandler = new Lazy<IApplicationCloseHandler>(() => applicationHandler)
            };
            var cancelEventArgs = new CancelEventArgs();
            await model.AppClosingCommand.Execute(cancelEventArgs);
            Assert.True(cancelEventArgs.Cancel);
        }

        #endregion
    }
}