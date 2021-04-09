using System.Windows;
using System.Drawing;

namespace Akkumulator
{
    public partial class MainWindow : Window
    {
        private readonly Icon TrayIconWhite = Properties.Resources.tray_icon_white;
        private readonly Icon TrayIconBlack = Properties.Resources.tray_icon_black;

        private void UpdateTrayIconColor(bool light)
        {
            if (TrayIcon == null)
            {
                return;
            }
            TrayIcon.Icon = light ? TrayIconBlack : TrayIconWhite;
        }

        private void UpdateTrayMenuStyle(bool light)
        {
            if (TrayMenu == null)
            {
                return;
            }
            string key = light ? "TrayMenuStyleLight" : "TrayMenuStyleDark";
            TrayMenu.Style = Application.Current.Resources[key] as Style;
        }

        private void UserPreferencesChangedHandler(object sender, Microsoft.Win32.UserPreferenceChangedEventArgs e)
        {
            if (e.Category == Microsoft.Win32.UserPreferenceCategory.General)
            {
                bool lightTheme = Util.Theme.SystemUsesLightTheme();
                UpdateTrayIconColor(lightTheme);
                UpdateTrayMenuStyle(lightTheme);
            }
        }

        private void ThemePartInit()
        {
            bool lightTheme = Util.Theme.SystemUsesLightTheme();
            UpdateTrayIconColor(lightTheme);
            UpdateTrayMenuStyle(lightTheme);
            Microsoft.Win32.SystemEvents.UserPreferenceChanged += UserPreferencesChangedHandler;
        }
    }
}
