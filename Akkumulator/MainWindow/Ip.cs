

using System.Windows;

namespace Akkumulator
{
    public partial class MainWindow
    {
        private void IpButtonListener(string newIp)
        {
            IpButton.Content = "My IP:  " + (newIp == Util.Ip.ERROR_DETECTING_IP ? "Unknown" : newIp);
        }

        private void IpButton_Click(object sender, RoutedEventArgs e)
        {
            Util.General.CopyTextToClipboard(Util.Ip.Current);
            WindowState = WindowState.Minimized;
        }
    }
}
