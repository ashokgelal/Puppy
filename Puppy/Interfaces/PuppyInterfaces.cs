using Microsoft.Practices.Prism.Logging;

namespace PuppyFramework.Interfaces
{
    public interface IPuppyShellView
    {
        void Show();
        IPuppyShellViewModel ViewModel { set; }
    }

    public interface IPuppyShellViewModel
    {
    }

    public interface ILogger
    {
        void Log(string message, Category category, string logSource = null, params object[] propertyValues);
    }
}
