#region Using

using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Mvvm;
using PuppyFramework.Interfaces;
using PuppyFramework.UI;

#endregion

namespace PuppyFramework.MenuService
{
    [Export(typeof(IMenuLoader))]
    [Export(typeof(MainMenuViewModel))]
    public class MainMenuViewModel : BindableBase, IMenuLoader
    {
        #region Fields

        private readonly ILogger _logger;
        private readonly IMenuFactory _menuFactory;
        private readonly IMenuRegisterService _registerService;
        private ObservableCollection<MenuItemBase> _menuItems;

        #endregion

        #region Properties

        public bool IsLoaded { get; private set; }

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
            _logger.Log("Initialized {ClassName:l}", Category.Info, null, GetType().FullName);
        }

        #endregion

        #region Methods

        public void Load()
        {
            AddMainMenuItems();
            IsLoaded = true;
        }

        private void AddMainMenuItems()
        {
            var filemenu = _menuFactory.MakeCoreMenuItem(CoreMenuItemType.File);
            var exitmenu = _menuFactory.MakeCoreMenuItem(CoreMenuItemType.Exit);
            var helpmenu = _menuFactory.MakeCoreMenuItem(CoreMenuItemType.Help);

            exitmenu.CommandBinding = new CommandBinding(Commands.ExitCommand, ExitCommandExecuted);
            _registerService.Register(new SeparatorMenuItem(exitmenu.Weight - 0.1), filemenu);
            _registerService.Register(exitmenu, filemenu);
            _registerService.Register(helpmenu);

            MenuItems = new ObservableCollection<MenuItemBase>(_registerService.MenuItems);
        }

        private void ExitCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            HandleExitCommand();
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