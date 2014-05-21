#region Usings

using System.ComponentModel.Composition;
using Puppy.Sample.ResourceSatellite.Properties;
using PuppyFramework.Interfaces;
using PuppyFramework.ViewModels;

#endregion

namespace Puppy.Sample.ViewModel
{
    // comment out this Export attribute if you don't want to provide your own ShellViewModel
    [Export(typeof (IShellViewModel))]
    internal class SampleShellViewModel : DefaultShellViewModel
    {
        #region Properties

        [ImportingConstructor]
        public SampleShellViewModel(ILogger logger) : base(logger)
        {
        }

        public override string Title
        {
            get { return Resources._appTitle; }
        }

        #endregion
    }
}