#region Usings

using Microsoft.Practices.Prism.Mvvm;

#endregion

namespace PuppyFramework.MenuService
{
    public abstract class MenuItemBase : BindableBase
    {
        #region Fields

        private bool _hiddenFlag;
        private double _weight;

        #endregion

        #region Properties

        public bool HiddenFlag
        {
            get { return _hiddenFlag; }
            set { SetProperty(ref _hiddenFlag, value); }
        }

        public double Weight
        {
            get { return _weight; }
            private set { SetProperty(ref _weight, value); }
        }

        #endregion

        #region Constructors

        protected MenuItemBase(double weight)
        {
            Weight = weight;
        }

        #endregion
    }
}
