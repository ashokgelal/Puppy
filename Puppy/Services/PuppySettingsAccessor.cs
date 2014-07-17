#region Using

using System.ComponentModel.Composition;
using PuppyFramework.Properties;

#endregion

namespace PuppyFramework.Services
{
    [Export]
    internal class PuppySettingsAccessor : SettingsAccessorBase
    {
        public override void Save()
        {
            Settings.Default.Save();
        }

        protected override T ReadSetting<T>(string key)
        {
            return (T) Settings.Default[key];
        }

        protected override void WriteSetting<T>(string key, T value)
        {
            Settings.Default[key] = value;
        }
    }
}