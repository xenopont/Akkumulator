using System.Windows;

namespace Akkumulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Util.Ip.AddListener(IpButtonListener);
            Util.Ip.Start();
        }

        private void IpButtonListener(string newIp)
        {
            IpButton.Content = "My IP:  " + (newIp == Util.Ip.ERROR_DETECTING_IP ? "Unknown" : newIp);
        }
    }
}
