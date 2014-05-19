#region Usings

using PuppyFramework.Interfaces;
using System.ComponentModel.Composition;

#endregion

namespace Puppy.Sample
{
    // comment out this Export attribute if you won't want to provide your own Shell
    [Export(typeof(IPuppyShellView))]
    public partial class SampleShell : IPuppyShellView
    {
        #region Properties

        public IPuppyShellViewModel ViewModel
        {
            set { DataContext = value; }
        }

        #endregion

        #region Constructors

        public SampleShell()
        {
            InitializeComponent();
        }

        #endregion
    }
}
