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
    public class MenuLoader : IBootableService
    {
        #region Fields

        [ImportMany(AllowRecomposition = true), UsedImplicitly]
        private IEnumerable<Lazy<IMenuLoader>> _menuLoaders;

        #endregion

        #region Methods

        public void Boot(BootstrapConfig bootstrapConfig)
        {
            if (bootstrapConfig == null || !bootstrapConfig.AddMainMenu)
            {
                return;
            }
            if (_menuLoaders == null)
            {
                return;
            }
            foreach (var loader in _menuLoaders.Where(l => !l.Value.IsLoaded))
            {
                loader.Value.Load();
            }
        }

        #endregion
    }
}
