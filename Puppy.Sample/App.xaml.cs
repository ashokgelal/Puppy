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

            // Either ...
//            new BootstrapConfig
//            {
//                EnableUpdaterService = true, // false by default
//                AddMainMenu = true, // true by default
//            }.Run();

            // ... OR if you have your own bootstrapper that extends PuppyBootstrapper, do
            // new CustomBootstrapper().Run(config);

            // ... OR if you want to load settings from App.config, just do
             new PuppyBootstrapper().Run();
        }

        #endregion
    }
}