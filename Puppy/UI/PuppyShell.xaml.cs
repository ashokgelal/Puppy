#region Usings

using System.ComponentModel.Composition;
using PuppyFramework.Interfaces;

#endregion

namespace PuppyFramework.UI
{
    [Export]
    public partial class PuppyShell
    {
        #region Properties

        public IPuppyShellViewModel ViewModel
        {
            set { DataContext = value; }
        }

        #endregion

        #region Constructors

        public PuppyShell()
        {
            InitializeComponent();
        }

        #endregion
    }
}