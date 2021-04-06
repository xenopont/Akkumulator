using System.Windows;
using System.Drawing;

namespace Akkumulator
{
    public partial class MainWindow : Window
    {
        private readonly Icon TrayIconWhite = Properties.Resources.tray_icon_white;
        private readonly Icon TrayIconBlack = Properties.Resources.tray_icon_black;

        private void UpdateTrayIconColor()
        {
            if (TrayIcon == null)
            {
                return;
            }
            TrayIcon.Icon = Util.Theme.SystemUsesLightTheme() ? TrayIconBlack : TrayIconWhite;
        }

        private void UserPreferencesChangedHandler(object sender, Microsoft.Win32.UserPreferenceChangedEventArgs e)
        {
            if (e.Category == Microsoft.Win32.UserPreferenceCategory.General)
            {
                UpdateTrayIconColor();
            }
        }

        private void ThemePartInit()
        {
            UpdateTrayIconColor();
            Microsoft.Win32.SystemEvents.UserPreferenceChanged += UserPreferencesChangedHandler;
        }
    }
}
