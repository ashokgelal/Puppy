#region Usings

using PuppyFramework.MenuService;
using System.ComponentModel.Composition;

#endregion

namespace PuppyFramework.UI
{
    [Export]
    public partial class MainMenuView
    {
        #region Properties

        [Import]
        public MainMenuViewModel ViewModel
        {
            set { DataContext = value; }
        }

        #endregion

        #region Constructors

        public MainMenuView()
        {
            InitializeComponent();
        }

        #endregion
    }
}
