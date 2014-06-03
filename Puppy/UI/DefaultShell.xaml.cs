#region Usings

using PuppyFramework.Helpers;
using PuppyFramework.Interfaces;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Threading;
using System.Windows.Input;

#endregion

namespace PuppyFramework.UI
{
    [Export(typeof(DefaultShell))]
    public partial class DefaultShell : IShell
    {
        #region Properties

        public IShellViewModel ViewModel
        {
            set { DataContext = value; }
        }

        #endregion

        #region Constructors

        public DefaultShell()
        {
            InitializeComponent();
#if DEBUG_NP
            SetCultureInfo(new CultureInfo("ne-NP"));
#else
            SetCultureInfo();
#endif
        }

        #endregion

        #region Methods

        public void AddGlobalKeyBinding(KeyBinding keyBinding)
        {
            keyBinding.EnsureParameterNotNull("keyBinding");
            InputBindings.Add(keyBinding);
        }

        public void SetCultureInfo(CultureInfo cultureInfo = null)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo ?? CultureInfo.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = cultureInfo ?? CultureInfo.CurrentCulture;
        }

        #endregion
    }
}
