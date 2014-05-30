#region Usings

using Microsoft.Practices.Prism.Modularity;

#endregion

namespace PuppyFramework.Bootstrap
{
    /// <summary>
    /// A dummy Module that can be returned when a real module cannot be loaded.
    /// This is to avoid any kind of NREs deep down in the prism libraries.
    /// </summary>
    public class DevNullModule : IModule
    {
        #region Methods

        public void Initialize()
        {
        }

        #endregion
    }
}
