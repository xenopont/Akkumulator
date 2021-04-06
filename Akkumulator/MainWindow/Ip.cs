using System.Windows;
using System.Windows.Controls;

namespace Akkumulator
{
    public partial class MainWindow
    {
        private const string MESSAGE_IP_NOT_DETERMINED = "IP not determined";
        private const string TRAY_MENU_IP_ITEM_X_NAME = "TrayMenuIpItem";

        private void IpListener(string newIp)
        {
            bool error = newIp == Util.Ip.ERROR_DETECTING_IP;
            string message = error ? MESSAGE_IP_NOT_DETERMINED : ("Copy " + newIp);

            // window button
            IpButton.IsEnabled = !error;
            IpButton.Content = message;

            // tray menu item
            if (TrayMenuIpItem == null)
            {
                return;
            }
            TrayMenuIpItem.Header = message;
            TrayMenuIpItem.IsEnabled = !error;
        }

        private MenuItem TrayMenuIpItem = null;
        private void IpPartInit(string trayMenuResourceKey)
        {
            Util.Ip.AddListener(IpListener);

            TrayMenuIpItem = FindMenuItemByName(Resources[trayMenuResourceKey] as ContextMenu, TRAY_MENU_IP_ITEM_X_NAME);
            if (TrayMenuIpItem != null)
            {
                TrayMenuIpItem.Click += IpButton_Click;
            }
            
            Util.Ip.Start();
        }

        private void IpButton_Click(object sender, RoutedEventArgs e)
        {
            Util.General.CopyTextToClipboard(Util.Ip.Current);
            if (sender == IpButton)
            {
                WindowState = WindowState.Minimized;
            }
        }

        private MenuItem FindMenuItemByName(ContextMenu menu, string xName)
        {
            foreach (object item in menu.Items)
            {
                if (!(item is MenuItem))
                {
                    continue;
                }
                if ((item as MenuItem).Name == xName)
                {
                    return item as MenuItem;
                }
            }

            return null;
        }
    }
}
