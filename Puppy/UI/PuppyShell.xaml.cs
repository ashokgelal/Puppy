#region Usings

using System.ComponentModel.Composition;
using System.Globalization;
using System.Threading;
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
#if DEBUG_NP
            SetCultureInfo(new CultureInfo("ne-NP"));
#else
            SetCultureInfo();
#endif
        }

        public void SetCultureInfo(CultureInfo cultureInfo = null)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo ?? CultureInfo.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = cultureInfo ?? CultureInfo.CurrentCulture;
        }

        #endregion
    }
}