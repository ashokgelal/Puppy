#region Usings

using System.Windows;
using PuppyFramework.Bootstrap;

#endregion

namespace Puppy.Sample
{
    public partial class App
    {
        #region Methods

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new BootstrapConfig
            {
                EnableSoftwareUpdater = true,
                EnableMenuService = true,
            }.Boot();
            // OR
            // new PuppyBootstrapper().Run(config);
        }

        #endregion
    }
}