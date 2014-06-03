#region Usings

using System.Windows.Input;
using PuppyFramework.Interfaces;
using System.ComponentModel.Composition;

#endregion

namespace Puppy.Sample
{
    // comment out this Export attribute if you don't want to provide your own Shell
    [Export(typeof(IShell))]
    public partial class SampleShell : IShell
    {
        #region Properties

        public IShellViewModel ViewModel
        {
            set { DataContext = value; }
        }

        public void AddGlobalKeyBinding(KeyBinding keyBinding)
        {
            InputBindings.Add(keyBinding);
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
