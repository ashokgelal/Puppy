#region Using

using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using PuppyFramework.Interfaces;

#endregion

namespace PuppyFramework.Services
{
    public abstract class SettingsAccessorBase : ISettingsAccessor
    {
        #region Fields

        protected Dictionary<string, object> _embeddedSettings;

        #endregion

        #region Constructors

        protected SettingsAccessorBase()
        {
            _embeddedSettings = new Dictionary<string, object>();
        }

        #endregion

        #region Methods

        public T ReadDefaultSetting<T>(string key)
        {
            try
            {
                object constVal;
                if (_embeddedSettings.TryGetValue(key, out constVal))
                {
                    return (T) constVal;
                }
                return ReadSetting<T>(key);
            }
            catch (ConfigurationException ex)
            {
                Trace.TraceError("Unable to parse configuration file: {0}", ex.Message);
            }
            catch (SettingsPropertyNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
            }
            catch (SettingsPropertyIsReadOnlyException ex)
            {
                Trace.TraceError(ex.Message);
            }
            catch (SettingsPropertyWrongTypeException ex)
            {
                Trace.TraceError(ex.Message);
            }
            return default(T);
        }

        public void WriteDefaultSetting<T>(string key, T value)
        {
            try
            {
                if (_embeddedSettings.ContainsKey(key))
                {
                    _embeddedSettings[key] = value;
                    return;
                }
                WriteSetting(key, value);
            }
            catch (ConfigurationException ex)
            {
                Trace.TraceError("Unable to parse configuration file: {0}", ex.Message);
            }
            catch (SettingsPropertyNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
            }
            catch (SettingsPropertyIsReadOnlyException ex)
            {
                Trace.TraceError(ex.Message);
            }
            catch (SettingsPropertyWrongTypeException ex)
            {
                Trace.TraceError(ex.Message);
            }
        }

        public abstract void Save();

        protected abstract T ReadSetting<T>(string key);

        protected abstract void WriteSetting<T>(string key, T value);

        #endregion
    }
}