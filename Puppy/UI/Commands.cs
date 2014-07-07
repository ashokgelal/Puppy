#region usings

using System.Windows.Input;

#endregion

namespace PuppyFramework.UI
{
    public class Commands
    {
        private static readonly RoutedUICommand _exitCommand;

        static Commands()
        {
            var exitGestures = new InputGestureCollection
                               {
                                   new KeyGesture(Key.F4, ModifierKeys.Alt, "Alt+F4")
                               };
            _exitCommand = new RoutedUICommand("Exit", "Exit", typeof (Commands), exitGestures);
        }

        public static RoutedUICommand ExitCommand
        {
            get { return _exitCommand; }
        }
    }
}