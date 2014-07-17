#region Using

using System.Collections.Generic;
using System.Windows.Input;
using PuppyFramework.Services;

#endregion

namespace PuppyFramework.MenuService
{
    public class MenuItem : MenuItemBase
    {
        #region Fields

        private ObservableSortedList<MenuItemBase> _children;
        private bool _isEnabled;
        private string _title;

        #endregion

        #region Properties

        public object CommandParameter { get; set; }

        public CommandBinding CommandBinding { get; set; }

        public ObservableSortedList<MenuItemBase> Children
        {
            get { return _children; }
            private set { SetProperty(ref _children, value); }
        }

        public string Title
        {
            get { return _title; }
            protected set { SetProperty(ref _title, value); }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }

        #endregion

        #region Constructors

        public MenuItem(string title, double weight)
            : base(weight)
        {
            Title = title;
            Children = new ObservableSortedList<MenuItemBase>();
            IsHidden = false;
            IsEnabled = true;
        }

        #endregion

        #region Methods

        public void AddChild(MenuItemBase child, IComparer<MenuItemBase> menuItemComparer = null)
        {
            Children.Add(child);
            if (menuItemComparer != null)
            {
                Children.Sort(menuItemComparer);
            }
        }

        public bool RemoveChild(MenuItemBase child)
        {
            return Children.Remove(child);
        }

        #endregion
    }
}