#region Usings

using PuppyFramework.Models;
using System.Collections.Generic;

#endregion

namespace PuppyFramework.Services
{
    public class WeightBasedMenuItemComparer : IComparer<MenuItemBase>
    {
        #region Methods

        public virtual int Compare(MenuItemBase x, MenuItemBase y)
        {
            if (x == null)
                return -1;
            return y == null ? 1 : x.Weight.CompareTo(y.Weight);
        }

        #endregion
    }
}