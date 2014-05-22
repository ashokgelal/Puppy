#region Usings

using System.Windows.Input;
using PuppyFramework.Services;
using System.Collections.Generic;

#endregion

namespace PuppyFramework.Models
{
    public class MenuItem : MenuItemBase
    {
        #region Fields

        private ObservableSortedList<MenuItemBase> _children;
        private string _title;

        #endregion

        #region Properties

        public ICommand Command { get; set; }

        public object CommandParamter { get; set; }

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

        #endregion

        #region Constructors

        public MenuItem(string title, double weight)
            : base(weight)
        {
            Title = title;
            Children = new ObservableSortedList<MenuItemBase>();
            HiddenFlag = false;
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
