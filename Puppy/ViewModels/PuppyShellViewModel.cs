#region Usings

using Microsoft.Practices.Prism.Mvvm;
using PuppyFramework.Interfaces;
using System.ComponentModel.Composition;

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
            get { return _title ?? "Puppy"; }
            protected set
            {
                SetProperty(ref _title, value);
            }
        }

        #endregion
    }
}
