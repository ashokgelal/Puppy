#region Usings

using PuppyFramework.Interfaces;
using PuppyFramework.Properties;
using System;

#endregion

namespace PuppyFramework.MenuService
{
    public class DefaultMenuFactory : IMenuFactory
    {
        #region Fields

        public MenuItem _fileMenuItem;
        public MenuItem _helpMenuItem;
        public MenuItem _exitMenuItem;

        #endregion

        #region Methods

        public virtual MenuItem MakeCoreMenuItem(CoreMenuItemType coreMenuItemType)
        {
            var weight = ((int)coreMenuItemType);
            switch (coreMenuItemType)
            {
                case CoreMenuItemType.File:
                    return _fileMenuItem = _fileMenuItem ?? new MenuItem(Resources._fileMenuHeader, weight);
                case CoreMenuItemType.Help:
                    return _helpMenuItem = _helpMenuItem ?? new MenuItem(Resources._helpMenuHeader, weight);
                case CoreMenuItemType.Exit:
                    return _exitMenuItem = _exitMenuItem ?? new MenuItem(Resources._exitMenuHeader, weight);
            }
            throw new ArgumentOutOfRangeException(Resources._invalidTopLevelPositionException);
        }

        #endregion
    }
}
