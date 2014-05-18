#region Usings

using System.ComponentModel.Composition;
using Puppy.Sample.ResourceSatellite.Properties;
using PuppyFramework.Interfaces;
using PuppyFramework.ViewModels;

#endregion

namespace Puppy.Sample.ViewModel
{
    [Export(typeof (IPuppyShellViewModel))]
    internal class SampleShellViewModel : PuppyShellViewModel
    {
        #region Properties

        public override string Title
        {
            get { return Resources._appTitle; }
        }

        #endregion
    }
}