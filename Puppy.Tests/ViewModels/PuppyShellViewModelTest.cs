#region Usings

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using NSubstitute;
using PuppyFramework;
using PuppyFramework.Interfaces;
using PuppyFramework.Services;
using PuppyFramework.ViewModels;
using Serilog;
using Xunit;

#endregion

namespace Puppy.Tests.ViewModels
{
    public class PuppyShellViewModelTest
    {
        private readonly SerilogLogger _logger;

        #region Methods

        public PuppyShellViewModelTest()
        {
            _logger = new SerilogLogger("Test", new LoggerConfiguration().WriteTo.Console());
        }

        [Fact]
        public async Task TestClosingCommand()
        {
            var applicationHandler = Substitute.For<IApplicationCloseHandler>();
            applicationHandler.ShoulCloseApplicationAsync().Returns(Task.FromResult(UserPromptResult.Yes));
            var model = new DefaultShellViewModel(_logger)
            {
                ApplicationCloseHandler = new Lazy<IApplicationCloseHandler>(() => applicationHandler)
            };
            await model.AppClosingCommand.Execute(new CancelEventArgs());
            applicationHandler.Received(1).ShoulCloseApplicationAsync();
        }

        [Fact]
        public async Task TestClosingCommandCancel_NoApplicationHandler()
        {
            var model = new DefaultShellViewModel(_logger);
            var cancelEventArgs = new CancelEventArgs();
            await model.AppClosingCommand.Execute(cancelEventArgs);
            Assert.False(cancelEventArgs.Cancel);
        }

        [Fact]
        public async Task TestClosingCommandCancel_WithApplicationHandler()
        {
            var applicationHandler = Substitute.For<IApplicationCloseHandler>();
            applicationHandler.ShoulCloseApplicationAsync().Returns(Task.FromResult(UserPromptResult.Yes));
            var model = new DefaultShellViewModel(_logger)
            {
                ApplicationCloseHandler = new Lazy<IApplicationCloseHandler>(() => applicationHandler)
            };
            var cancelEventArgs = new CancelEventArgs();
            await model.AppClosingCommand.Execute(cancelEventArgs);
            Assert.False(cancelEventArgs.Cancel);
        }

        [Fact]
        public async Task TestClosingCommandNoCancel_WithApplicationHandler()
        {
            var applicationHandler = Substitute.For<IApplicationCloseHandler>();
            applicationHandler.ShoulCloseApplicationAsync().Returns(Task.FromResult(UserPromptResult.No));
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