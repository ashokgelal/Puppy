#region Usings

using System.Threading.Tasks;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.ServiceLocation;

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
