#region Usings

using PuppyFramework.Helpers;
using PuppyFramework.Interfaces;
using PuppyFramework.Models;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

#endregion

namespace PuppyFramework.Services
{
    [Export]
    public class MenuRegisterService : IMenuRegisterService
    {
        #region Fields

        private readonly IComparer<MenuItemBase> _menuItemComparer;

        #endregion

        #region Properties

        public ObservableSortedList<MenuItemBase> MenuItems { get; private set; }

        #endregion

        #region Constructors

        [ImportingConstructor]
        public MenuRegisterService([Import(AllowDefault = true)] WeightBasedMenuItemComparer menuItemComparer)
        {
            _menuItemComparer = menuItemComparer ?? new WeightBasedMenuItemComparer();
            MenuItems = new ObservableSortedList<MenuItemBase>(4, _menuItemComparer);
        }

        #endregion

        #region Methods

        private static void HideHalfOrphanSeparators(MenuItem parentMenuItem)
        {
            parentMenuItem.EnsureParameterNotNull("parentMenuItem");
            var separators = parentMenuItem.Children.OfType<SeparatorMenuItem>().ToList();
            // turn on the visibility and afterwards selectively turn it off
            separators.ForEach(sep => sep.HiddenFlag = true);

            // hide top separator
            var menuItem = separators.FirstOrDefault();
            if (menuItem == null)
                return;
            menuItem.HiddenFlag = false;

            // hide bottom separator
            menuItem = separators.LastOrDefault();
            if (menuItem == null)
                return;
            menuItem.HiddenFlag = false;
        }

        public bool Register(MenuItemBase menuItemToRegister, MenuItem attachToMenuItem)
        {
            menuItemToRegister.EnsureParameterNotNull("menuItemToRegister");
            attachToMenuItem.EnsureParameterNotNull("attachToMenuItem");
            attachToMenuItem.AddChild(menuItemToRegister, _menuItemComparer);
            HideHalfOrphanSeparators(attachToMenuItem);
            Register(attachToMenuItem);
            return true;
        }

        public bool Register(MenuItemBase menuItemToRegister)
        {
            menuItemToRegister.EnsureParameterNotNull("menuItemToRegister");
            if (MenuItems.Contains(menuItemToRegister)) return false;
            MenuItems.Add(menuItemToRegister);
            return true;
        }

        public bool Deregister(MenuItemBase menuItemToDeregister, MenuItem detachFrommenuItem)
        {
            menuItemToDeregister.EnsureParameterNotNull("menuItemToDeregister");
            detachFrommenuItem.EnsureParameterNotNull("detachFrommenuItem");
            return detachFrommenuItem.Children.Remove(menuItemToDeregister);
        }

        #endregion
    }
}
