#region Usings

using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions;
using PuppyFramework.Helpers;
using PuppyFramework.Interfaces;
using PuppyFramework.Properties;
using PuppyFramework.Services;
using PuppyFramework.UI;
using PuppyFramework.ViewModels;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Reflection;
using System.Windows;

#endregion

namespace PuppyFramework.Bootstrap
{
    public class PuppyBootstrapper : MefBootstrapper
    {
        #region Fields

        private readonly string _assemblyName;
        private readonly string _version;
        private SerilogLogger _logger;

        #endregion

        #region Properties

        public BootstrapConfig BootstrapConfig { get; private set; }

        #endregion

        #region Constructors

        public PuppyBootstrapper()
        {
            var assembly = Assembly.GetAssembly(typeof(PuppyBootstrapper)).GetName();
            _assemblyName = assembly.Name;
            _version = assembly.Version.ToString();
        }

        #endregion

        #region Methods

        protected override void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(PuppyBootstrapper).Assembly));
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                AggregateCatalog.Catalogs.Add(new AssemblyCatalog(entryAssembly));
            }
        }

        protected override void ConfigureContainer()
        {
            Container.ComposeExportedValue<ILogger>(_logger);
            base.ConfigureContainer();
        }

        protected override ILoggerFacade CreateLogger()
        {
            var source = string.Format("{0} {1}", _assemblyName, _version);
            _logger = _logger ?? new SerilogLogger(source);
            return _logger;
        }

        protected override DependencyObject CreateShell()
        {
            var customShell = Container.GetExportedValueOrDefault<IPuppyShellView>();
            var shell = (customShell ?? Container.GetExportedValue<DefaultShell>()) as Window;
            if (shell == null)
            {
                throw new InvalidCastException(Resources._invalidShellTypeException);
            }
            return shell;
        }

        private T GetAppSetting<T>(string key, T defaultValue = default(T))
        {
            try
            {
                var value = ConfigurationManager.AppSettings[key];
                return TConverter.ChangeType<T>(value);
            }
            catch (Exception)
            {
                _logger.Log(string.Format("Error fetching app setting for key: {0}. Using default value.", key), Category.Warn, Priority.High);
                return defaultValue;
            }
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            var shell = ((IPuppyShellView)Shell);
            shell.ViewModel = Container.GetExportedValueOrDefault<IShellViewModel>()
                              ?? Container.GetExportedValue<DefaultShellViewModel>();
            shell.Show();
            _logger.Log(string.Format("Initialized {0}", shell.GetType().Name), Category.Info);
        }

        private BootstrapConfig ReadConfigFromApplicationSettings()
        {
            var enableMenu = GetAppSetting(MagicStrings.ASK_ENABLE_MENU_SERVICE, true);
            var enableUpdater = GetAppSetting(MagicStrings.ASK_ENABLE_UPDATER_SERVICE, false);
            _logger.Log("Created BootstrapConfig from App.Config", Category.Info);
            return new BootstrapConfig
            {
                EnableMenuService = enableMenu,
                EnableUpdaterService = enableUpdater,
            };
        }

        public void Run(BootstrapConfig config = null)
        {
            CreateLogger();
            if (config == null)
            {
                config = ReadConfigFromApplicationSettings();
            }
            BootstrapConfig = config;
            _logger.Log(string.Format("Running app"), Category.Info);
            _logger.Log(string.Format("Enable MenuService? {0}", BootstrapConfig.EnableMenuService), Category.Info);
            _logger.Log(string.Format("Enable UpdaterService? {0}", BootstrapConfig.EnableUpdaterService), Category.Info);
            _logger.Log(string.Format("Register Default Prism Library Services? {0}", BootstrapConfig.RegisterDefaultPrismServices), Category.Info);
            base.Run(config.RegisterDefaultPrismServices);
        }

        public override void Run(bool registerDefaultPrismServices)
        {
            throw new InvalidOperationException(Resources._invalidRunOperationException);
        }

        #endregion
    }
}
