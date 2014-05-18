#region Usings

using Microsoft.Practices.Prism.Mvvm;
using PuppyFramework.Interfaces;
using System.ComponentModel.Composition;
using PuppyFramework.Properties;
using Serilog;

#endregion

namespace PuppyFramework.ViewModels
{
    [Export(typeof(PuppyShellViewModel))]
    public class PuppyShellViewModel : BindableBase, IPuppyShellViewModel
    {
        #region Fields

        private string _title;

        #endregion

        #region Properties

        public virtual string Title
        {
            get { return _title ?? Resources._appTitle; }
            protected set { SetProperty(ref _title, value); }
        }

        #endregion

        public PuppyShellViewModel()
        {
            Log.Information("Initialized class {ClassName}", GetType().Name);
        }
    }
}
