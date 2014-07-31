#region Using

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using PuppyFramework.Interfaces;
using PuppyFramework.MenuService;
using PuppyFramework.Models;
using PuppyFramework.Properties;

#endregion

namespace PuppyFramework.Services
{
    [Export]
    public class RecentFilesManager : IDisposable
    {
        #region Fields

        private const int RECENT_LIST_COUNT = 10;
#pragma warning disable 649
        [Import] private Lazy<IMenuRegisterService> _menuRegisterService;
        [Import] private Lazy<PuppySettingsAccessor> _settingsAccessor;
#pragma warning restore 649

        private List<RecentFileInfo> _recentFilesList;
        private List<RecentFileInfo> _tempFileListForSwapping;
        public event EventHandler<RecentFileInfo> FileSelectedEvent;

        #endregion

        #region Propreties

        public List<RecentFileInfo> RecentFilesList
        {
            get { return _recentFilesList; }
            set
            {
                if (Equals(value, _recentFilesList)) return;
                _recentFilesList = value.Take(RECENT_LIST_COUNT).ToList();
            }
        }

        public MenuItem RecentFilesMenuItem { get; set; }

        #endregion

        #region Methods

        public void Dispose()
        {
            SaveRecentFileListToSettings();
        }

        private void SaveRecentFileListToSettings()
        {
            var coll = new StringCollection();
            foreach (var fileInfo in RecentFilesList)
            {
                coll.Add(fileInfo.Uri.LocalPath);
            }
            _settingsAccessor.Value.WriteDefaultSetting(MagicStrings.Keys.ASK_RECENT_FILES_SETTING_KEY, coll);
        }

        public void StartManaging(IEnumerable<RecentFileInfo> defaultRecentFileInfoList = null)
        {
            if (RecentFilesMenuItem == null)
            {
                RecentFilesMenuItem = new MenuItem(Resources._recentFilesMenuHeader, 4);
            }
            InitializeList(defaultRecentFileInfoList);

            CreateRecentFilesMenuItems();
        }

        private void InitializeList(IEnumerable<RecentFileInfo> defaultRecentFileInfoList = null)
        {
            RecentFilesList = new List<RecentFileInfo>();
            var savedPaths = _settingsAccessor.Value.ReadDefaultSetting<StringCollection>(MagicStrings.Keys.ASK_RECENT_FILES_SETTING_KEY);
            if (savedPaths == null)
            {
                savedPaths = new StringCollection();
                _settingsAccessor.Value.WriteDefaultSetting(MagicStrings.Keys.ASK_RECENT_FILES_SETTING_KEY, savedPaths);
            }

            foreach (var path in savedPaths)
            {
                var fileInfo = new RecentFileInfo(path);
                if (!RecentFilesList.Contains(fileInfo))
                {
                    RecentFilesList.Add(fileInfo);
                }
            }

            if (defaultRecentFileInfoList == null) return;
            foreach (var fileInfo in defaultRecentFileInfoList.Where(fileInfo => !RecentFilesList.Contains(fileInfo)))
            {
                RecentFilesList.Add(fileInfo);
            }
        }

        private void CreateRecentFilesMenuItems()
        {
            var weight = 0;
            var count = 1;
            var menuItems = RecentFilesList.Select(recentFileInfo =>
                new MenuItem(string.Format("{0}. {1}", count++, recentFileInfo.ShortName), weight++)
                {
                    CommandBinding = new CommandBinding(new DelegateCommand<RecentFileInfo>(RecentFileOpenCommandHandler)),
                    CommandParameter = recentFileInfo
                }).Cast<MenuItemBase>().ToList();

            RecentFilesMenuItem.IsEnabled = menuItems.Any();
            if (RecentFilesMenuItem.IsEnabled)
            {
                menuItems.Add(new SeparatorMenuItem(weight++));
                menuItems.Add(
                    new MenuItem("Clear Recent", weight)
                    {
                        CommandBinding = new CommandBinding(new DelegateCommand(ClearRecentFilesCommandHandler)),
                    });
            }
            _menuRegisterService.Value.Register(menuItems, RecentFilesMenuItem, false);
        }

        private void ClearRecentFilesCommandHandler()
        {
            RecentFilesList.Clear();
            CreateRecentFilesMenuItems();
        }

        private void RecentFileOpenCommandHandler(RecentFileInfo fileInfo)
        {
            RaiseFileSelected(fileInfo);
        }

        protected virtual void RaiseFileSelected(RecentFileInfo e)
        {
            var handler = FileSelectedEvent;
            if (handler != null) handler(this, e);
        }

        public bool TryAddingFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return false;
            }
            SetupTempFileList();
            RemoveFileFromList(fileName);
            _tempFileListForSwapping.Insert(0, new RecentFileInfo(fileName));
            ResetRecentFilesList();
            ResetRecentMenuItems();
            return true;
        }

        private void ResetRecentMenuItems()
        {
            RecentFilesMenuItem.Children.Clear();
            CreateRecentFilesMenuItems();
        }

        public void RemoveMissingFile(RecentFileInfo fileInfo)
        {
            SetupTempFileList();
            RemoveFileFromList(fileInfo.Uri.LocalPath);
            ResetRecentFilesList();
            ResetRecentMenuItems();
        }

        private void RemoveFileFromList(string fileName)
        {
            var bas = Path.GetTempPath();
            if (fileName.Contains(bas)) return;
            _tempFileListForSwapping.RemoveAll(rf => rf.Uri.LocalPath == fileName);
        }

        private void ResetRecentFilesList()
        {
            RecentFilesList = _tempFileListForSwapping;
        }

        private void SetupTempFileList()
        {
            _tempFileListForSwapping = _recentFilesList.ToList();
        }

        #endregion
    }
}