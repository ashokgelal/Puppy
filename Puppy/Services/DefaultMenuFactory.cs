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

        public const int FILE_MENU_DEFAULT_POSITION = 0;
        public const int EDIT_MENU_DEFAULT_POSITION = 10;
        public const int VIEW_MENU_DEFAULT_POSITION = 20;
        public const int HELP_MENU_DEFAULT_POSITION = 100;
        private MenuItem _fileMenuItem;
        private MenuItem _helpMenuItem;

        #endregion

        #region Methods

        public virtual MenuItem MakeTopLevelMenuItem(int topLevelMenuPosition)
        {
            switch (topLevelMenuPosition)
            {
                case FILE_MENU_DEFAULT_POSITION:
                    return _fileMenuItem = _fileMenuItem ?? new MenuItem(Resources._fileMenuHeader, topLevelMenuPosition);
                case HELP_MENU_DEFAULT_POSITION:
                    return _helpMenuItem = _helpMenuItem ?? new MenuItem(Resources._helpMenuHeader, topLevelMenuPosition);
            }
            throw new ArgumentOutOfRangeException(Resources._invalidTopLevelPositionException);
        }

        #endregion
    }
}
