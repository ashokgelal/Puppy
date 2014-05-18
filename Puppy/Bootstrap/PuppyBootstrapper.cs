#region Usings

using System.Reflection;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions;
using PuppyFramework.Interfaces;
using PuppyFramework.Services;
using PuppyFramework.UI;
using PuppyFramework.ViewModels;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

#endregion

namespace PuppyFramework.Bootstrap
{
    public class PuppyBootstrapper : MefBootstrapper
    {
        #region Fields

        private BootstrapConfig _bootstrapConfig;

        #endregion

        #region Methods

        protected override void ConfigureAggregateCatalog()
        {
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(PuppyBootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetEntryAssembly()));
        }

        protected override ILoggerFacade CreateLogger()
        {
            return new SerilogLogger();
        }

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<PuppyShell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            var shell = (PuppyShell)Shell;
            shell.ViewModel = Container.GetExportedValueOrDefault<IPuppyShellViewModel>()
                              ?? Container.GetExportedValue<PuppyShellViewModel>();
            shell.Show();
        }

        private BootstrapConfig ReadConfigFromApplicationSettings()
        {
            return new BootstrapConfig();
        }

        public void Run(BootstrapConfig config = null)
        {
            if (config == null)
            {
                config = ReadConfigFromApplicationSettings();
            }
            _bootstrapConfig = config;
            Run(config.RunWithDefaultConfig);
        }

        public override void Run(bool runWithDefaultConfiguration)
        {
            if (_bootstrapConfig == null)
            {
                _bootstrapConfig = ReadConfigFromApplicationSettings();
            }
            base.Run(runWithDefaultConfiguration);
        }

        #endregion
    }
}
