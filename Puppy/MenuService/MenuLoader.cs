#region Usings

using System;
using System.Linq;
using PuppyFramework.Annotations;
using PuppyFramework.Bootstrap;
using PuppyFramework.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;

#endregion

namespace PuppyFramework.MenuService
{
    [Export(typeof(IBootableService))]
    [Export(typeof(MenuLoader))]
    public class MenuLoader : IBootableService, IPartImportsSatisfiedNotification
    {
        #region Fields

        [ImportMany(AllowRecomposition = true), UsedImplicitly]
        private IEnumerable<Lazy<IMenuLoader>> _menuLoaders;
        private BootstrapConfig _bootstrapConfig;

        #endregion

        #region Methods

        public void Boot(BootstrapConfig bootstrapConfig)
        {
            _bootstrapConfig = bootstrapConfig;
        }

        #endregion

        public void OnImportsSatisfied()
        {
            if (_bootstrapConfig == null || !_bootstrapConfig.AddMainMenu)
            {
                return;
            }
            if (_menuLoaders == null)
            {
                return;
            }
            foreach (var loader in _menuLoaders.Where(l=>!l.Value.IsLoaded))
            {
                loader.Value.Load();
            }
        }
    }
}
