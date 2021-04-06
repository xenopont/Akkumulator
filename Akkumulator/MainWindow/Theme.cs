using System.Windows;
using System.Drawing;

namespace Akkumulator
{
    public partial class MainWindow : Window
    {
        private readonly Icon TrayIconWhite = Properties.Resources.tray_icon_white;
        private readonly Icon TrayIconBlack = Properties.Resources.tray_icon_black;

        private void SetWhiteTrayIcon()
        {
            TrayIcon.Icon = TrayIconWhite;
        }

        private void SetBlackTrayIcon()
        {
            TrayIcon.Icon = TrayIconBlack;
        }

        private void ThemePartInit()
        {
            if (TrayIcon != null && Util.Theme.SystemUsesLightTheme())
            {
                SetBlackTrayIcon();
            }
        }
    }
}
