#region Using

using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Logging;
using PuppyFramework.Bootstrap;

#endregion

namespace PuppyFramework.Interfaces
{
    public interface IApplicationCloseHandler
    {
        #region Methods

        Task<UserPromptResult> ShouldCloseApplicationAsync();

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

        void AddCommandBinding(CommandBinding binding);

        void Show();

        #endregion
    }

    public interface IShellViewModel
    {
    }

    public interface ISettingsAccessor
    {
        #region Methods

        T ReadDefaultSetting<T>(string key);
        void WriteDefaultSetting<T>(string key, T value);
        void Save();

        #endregion
    }
}