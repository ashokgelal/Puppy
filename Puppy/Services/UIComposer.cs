#region Usings

using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Regions;
using PuppyFramework.Bootstrap;
using PuppyFramework.Interfaces;
using PuppyFramework.Properties;
using PuppyFramework.UI;

#endregion

namespace PuppyFramework.Services
{
    [Export(typeof (IBootableService))]
    [Export(typeof (UIComposer))]
    internal class UIComposer : IBootableService
    {
        [Import]
        internal IRegionManager RegionManager { get; set; }

        [Import]
        internal ILogger Logger { get; set; }

        #region Methods

        public void Boot(BootstrapConfig bootstrapConfig)
        {
            if (!bootstrapConfig.AddMainMenu) return;
            var menuContentRegion = MagicStrings.RegionNames.PUPPY_MAIN_MENU_CONTENT_REGION;

            if (bootstrapConfig.IsUsingCustomShell)
            {
                if (!RegionManager.Regions.ContainsRegionWithName(menuContentRegion))
                {
                    Logger.Log("MainMenu content region missing for custom shell", Category.Exception);
                    throw new InvalidOperationException(string.Format(Resources._noMainMenuContentRegionException, menuContentRegion));
                }
            }

            RegionManager.RegisterViewWithRegion(menuContentRegion, typeof (MainMenuView));
            Logger.Log("Added MainMenu to region {RegionName:l}", Category.Info, null, menuContentRegion);
            Logger.Log("Finished booting {ClassName:l}", Category.Info, null, GetType().FullName);
        }

        #endregion
    }
}