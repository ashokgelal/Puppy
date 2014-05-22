#region Usings

using Microsoft.Practices.Prism.Logging;
using PuppyFramework.Bootstrap;
using PuppyFramework.Models;
using PuppyFramework.Services;
using System.Threading.Tasks;

#endregion

namespace PuppyFramework.Interfaces
{
    public interface IApplicationCloseHandler
    {
        #region Methods

        Task<UserPromptResult> ShoulCloseApplicationAsync();

        #endregion
    }

    public interface IBootableService
    {
        #region Methods

        void Boot(BootstrapConfig bootstrapConfig);

        #endregion
    }

    public interface ILogger
    {
        #region Methods

        void Log(string message, Category category, string logSource = null, params object[] propertyValues);

        #endregion
    }

    public interface IMenuFactory
    {
        #region Methods

        MenuItem MakeCoreMenuItem(CoreMenuItemType coreMenuItemType);

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

    public interface IShell
    {
        #region Properties

        IShellViewModel ViewModel { set; }

        #endregion

        #region Methods

        void Show();

        #endregion
    }

    public interface IShellViewModel
    {
    }
}
