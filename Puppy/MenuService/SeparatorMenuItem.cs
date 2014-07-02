namespace PuppyFramework.MenuService
{
    public class SeparatorMenuItem : MenuItemBase
    {
        #region Constructors

        public bool ItsSeparatorFlag { get { return true; } }

        public SeparatorMenuItem(double weight)
            : base(weight)
        {
        }

        #endregion
    }
}
