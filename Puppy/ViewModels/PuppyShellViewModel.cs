#region Usings

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using PuppyFramework.Interfaces;
using PuppyFramework.Properties;

#endregion

namespace PuppyFramework.ViewModels
{
    [Export(typeof (PuppyShellViewModel))]
    public class PuppyShellViewModel : BindableBase, IPuppyShellViewModel
    {
        #region Fields

        protected readonly ILogger _logger;
        private string _title;

        #endregion

        #region Properties

        public virtual string Title
        {
            get { return _title ?? Resources._appTitle; }
            protected set { SetProperty(ref _title, value); }
        }

        #endregion

        #region Constructors

        [ImportingConstructor]
        public PuppyShellViewModel(ILogger logger)
        {
            _logger = logger;
            _logger.Log("Initialized {ClassName:l}", Category.Info, null, GetType().Name);
        }

        #endregion
    }
}