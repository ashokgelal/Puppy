#region Usings

using PuppyFramework.MenuService;
using PuppyFramework.Services;

#endregion

namespace PuppyFramework.Interfaces
{
    public interface IMenuFactory
    {
        #region Methods

        MenuItem MakeCoreMenuItem(CoreMenuItemType coreMenuItemType);

        #endregion
    }

    public interface IMenuLoader
    {
        #region Properties

        bool IsLoaded { get; }

        #endregion

        #region Methods

        void Load();

        #endregion
    }

    public interface IMenuRegisterService
    {
        #region Properties

        ObservableSortedList<MenuItemBase> MenuItems { get; }

        #endregion

        #region Methods

        bool Register(MenuItemBase menuItemToRegister, MenuItem attachToMenuItem);

        bool Register(MenuItemBase menuItemToRegister);

        #endregion
    }
}
