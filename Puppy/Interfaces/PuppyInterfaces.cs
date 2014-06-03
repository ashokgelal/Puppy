#region Usings

using Microsoft.Practices.Prism.Logging;
using PuppyFramework.Bootstrap;
using System.Threading.Tasks;
using System.Windows.Input;

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

    public interface IShell
    {
        #region Properties

        IShellViewModel ViewModel { set; }

        #endregion

        #region Methods

        void AddGlobalKeyBinding(KeyBinding keyBinding);

        void Show();

        #endregion
    }

    public interface IShellViewModel
    {
    }
}
