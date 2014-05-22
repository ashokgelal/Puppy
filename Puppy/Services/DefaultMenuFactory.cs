#region Usings

using PuppyFramework.Interfaces;
using PuppyFramework.Models;
using PuppyFramework.Properties;
using System;

#endregion

namespace PuppyFramework.Services
{
    public class DefaultMenuFactory : IMenuFactory
    {
        #region Fields

        private MenuItem _fileMenuItem;
        private MenuItem _helpMenuItem;
        private MenuItem _exitMenuItem;

        #endregion

        #region Methods

        public virtual MenuItem MakeCoreMenuItem(CoreMenuItemType coreMenuItemType)
        {
            var weight = ((int) coreMenuItemType);
            switch (coreMenuItemType)
            {
                case CoreMenuItemType.File:
                    return _fileMenuItem = _fileMenuItem ?? new MenuItem(Resources._fileMenuHeader, weight);
                case CoreMenuItemType.Help:
                    return _helpMenuItem = _helpMenuItem ?? new MenuItem(Resources._helpMenuHeader, weight);
                case CoreMenuItemType.Exit:
                    return _exitMenuItem = _exitMenuItem ?? new MenuItem("Exit", weight);
            }
            throw new ArgumentOutOfRangeException(Resources._invalidTopLevelPositionException);
        }

        #endregion
    }

    public enum CoreMenuItemType
    {
        File = 0,
        Edit = 10,
        View = 20,
        Help = 100,
        Exit = 1000
    }
}
