#region Using

using System.Threading;
using System.Windows;
using System.Windows.Threading;

#endregion

namespace PuppyFramework.Helpers
{
    public static class FocusHelper
    {
        public static void DoFocus(this UIElement element)
        {
            if (!element.Focus())
            {
                element.Dispatcher.BeginInvoke(DispatcherPriority.Input, new ThreadStart(()=> element.Focus()));
            }
        }
    }
}
