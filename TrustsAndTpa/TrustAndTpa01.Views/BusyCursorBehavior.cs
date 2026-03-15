using System.Windows;
using System.Windows.Input;

namespace TrustsAndTpa.TrustAndTpa01.Views {
    public static class BusyCursorBehavior {
        public static readonly DependencyProperty ShowWaitCursorProperty =
            DependencyProperty.RegisterAttached(
                "ShowWaitCursor",
                typeof(bool),
                typeof(BusyCursorBehavior),
                new PropertyMetadata(false, OnShowWaitCursorChanged));

        public static bool GetShowWaitCursor(DependencyObject obj)
            => (bool)obj.GetValue(ShowWaitCursorProperty);

        public static void SetShowWaitCursor(DependencyObject obj, bool value)
            => obj.SetValue(ShowWaitCursorProperty, value);

        private static void OnShowWaitCursorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is FrameworkElement element) {
                if ((bool)e.NewValue)
                    //Mouse.OverrideCursor = Cursors.Wait;
                    Mouse.OverrideCursor = Cursors.AppStarting;
                else
                    Mouse.OverrideCursor = null;
            }
        }
    }
}