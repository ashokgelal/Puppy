#region Usings

using PuppyFramework.Bootstrap;
using System.Windows;

#endregion

namespace Puppy.Tests.Bootstrap
{
    internal class TestBootstrapper : PuppyBootstrapper
    {
        #region Methods

        protected override DependencyObject CreateShell()
        {
            return new DependencyObject();
        }

        protected override void InitializeShell()
        {
            // do nothing
        }

        #endregion
    }
}
