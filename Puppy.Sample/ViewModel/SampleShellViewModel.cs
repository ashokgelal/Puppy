#region Usings

using PuppyFramework.Interfaces;
using PuppyFramework.ViewModels;
using System.ComponentModel.Composition;

#endregion

namespace Puppy.Sample.ViewModel
{
    [Export(typeof(IPuppyShellViewModel))]
    internal class SampleShellViewModel : PuppyShellViewModel
    {
        #region Constructors

        public SampleShellViewModel()
        {
            Title = "Sample Puppy";
        }

        #endregion
    }
}
