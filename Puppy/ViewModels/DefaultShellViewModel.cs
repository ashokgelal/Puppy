﻿#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using PuppyFramework.Interfaces;
using PuppyFramework.Properties;

#endregion

namespace PuppyFramework.ViewModels
{
    [Export(typeof (DefaultShellViewModel))]
    public class DefaultShellViewModel : BindableBase, IShellViewModel
    {
        #region Fields

        protected readonly ILogger _logger;
        private string _title;

        #endregion

        #region Properties

        public DelegateCommand<CancelEventArgs> AppClosingCommand { get; private set; }

        [Import(AllowDefault = true)]
        public Lazy<IApplicationCloseHandler> ApplicationCloseHandler { get; set; }

        public DelegateCommand AppLoadedCommand { get; private set; }

        public virtual string Title
        {
            get { return _title ?? Resources._appTitle; }
            protected set { SetProperty(ref _title, value); }
        }

        #endregion

        #region Constructors

        [ImportingConstructor]
        public DefaultShellViewModel(ILogger logger)
        {
            _logger = logger;
            Initialize();
            _logger.Log("Initialized {ClassName:l}", Category.Info, null, GetType().FullName);
        }

        #endregion

        #region Methods

        protected virtual bool CanExecuteAppClosingCommand(CancelEventArgs args)
        {
            return true;
        }

        protected virtual bool CanExecuteAppLoadedCommand()
        {
            return true;
        }

        protected virtual async void HandleAppClosingCommandAsync(CancelEventArgs args)
        {
            args.Cancel = true;
            var canClose = ApplicationCloseHandler == null || await ApplicationCloseHandler.Value.ShouldCloseApplicationAsync() == UserPromptResult.Yes;
            _logger.Log("Handling AppClosingCommand. Can close? {CanCloseApp:l}", Category.Info, null, canClose);
            args.Cancel = !canClose;
            if (canClose)
            {
                Shutdown();
            }
        }

        protected virtual void HandleAppLoadedCommandAsync()
        {
            _logger.Log("Handling AppLoadedCommand", Category.Info);
        }

        private void Initialize()
        {
            AppClosingCommand = new DelegateCommand<CancelEventArgs>(HandleAppClosingCommandAsync, CanExecuteAppClosingCommand);
            AppLoadedCommand = new DelegateCommand(HandleAppLoadedCommandAsync, CanExecuteAppLoadedCommand);
        }

        private void Shutdown()
        {
            _logger.Log("App is going to shutdown", Category.Info);
            if (Application.Current != null)
            {
                Application.Current.Shutdown();
            }
        }

        #endregion
    }
}