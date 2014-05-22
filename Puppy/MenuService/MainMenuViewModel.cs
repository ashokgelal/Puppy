#region Usings

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using PuppyFramework.Interfaces;
using PuppyFramework.Models;
using PuppyFramework.Services;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;

#endregion

namespace PuppyFramework.MenuService
{
    [Export]
    public class MainMenuViewModel : BindableBase
    {
        #region Fields

        private readonly IMenuFactory _menuFactory;
        private readonly ILogger _logger;
        private readonly IMenuRegisterService _registerService;
        private ObservableCollection<MenuItemBase> _menuItems;

        #endregion

        #region Properties

        public ObservableCollection<MenuItemBase> MenuItems
        {
            get { return _menuItems; }
            set { SetProperty(ref _menuItems, value); }
        }

        #endregion

        #region Constructors

        [ImportingConstructor]
        public MainMenuViewModel(IMenuRegisterService registerService, IMenuFactory menuFactory, ILogger logger)
        {
            _registerService = registerService;
            _menuFactory = menuFactory;
            _logger = logger;
            AddMainMenuItems();
            _logger.Log("Initialized {ClassName:l}", Category.Info, null, GetType().FullName);
        }

        #endregion

        #region Methods

        private void AddMainMenuItems()
        {
            var filemenu = _menuFactory.MakeCoreMenuItem(CoreMenuItemType.File);
            var exitmenu = _menuFactory.MakeCoreMenuItem(CoreMenuItemType.Exit);
            var helpmenu = _menuFactory.MakeCoreMenuItem(CoreMenuItemType.Help);

            _registerService.Register(exitmenu, filemenu);
            _registerService.Register(helpmenu);

            exitmenu.Command = new DelegateCommand(HandleExitCommand);
            MenuItems = new ObservableCollection<MenuItemBase>(_registerService.MenuItems);
        }

        private void HandleExitCommand()
        {
            _logger.Log("Received Exit command", Category.Info);
            var current = Application.Current;
            if (current != null && current.MainWindow != null)
            {
                current.MainWindow.Close();
            }
        }

        #endregion
    }
}
