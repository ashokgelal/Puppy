#region Using

using Microsoft.Practices.Prism.Mvvm;

#endregion

namespace PuppyFramework.MenuService
{
    public abstract class MenuItemBase : BindableBase
    {
        #region Fields

        private bool _isHidden;
        private double _weight;

        #endregion

        #region Properties

        public bool IsHidden
        {
            get { return _isHidden; }
            set { SetProperty(ref _isHidden, value); }
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