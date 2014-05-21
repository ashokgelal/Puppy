#region Usings

using Microsoft.Practices.Prism.Logging;
using PuppyFramework.Models;
using System.Collections.Generic;
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

    public interface ILogger
    {
        #region Methods

        void Log(string message, Category category, string logSource = null, params object[] propertyValues);

        #endregion
    }

    public interface IMenuFactory
    {
    }

    public interface IMenuRegisterService
    {
        #region Methods

        bool Register(MenuItemBase menuItemToRegister, MenuItem attachToMenuItem);

        bool Register(MenuItemBase menuItemToRegister);

        #endregion
    }

    public interface IPuppyShellView
    {
        #region Properties

        IPuppyShellViewModel ViewModel { set; }

        #endregion

        #region Methods

        void Show();

        #endregion
    }

    public interface IPuppyShellViewModel
    {
    }
}
