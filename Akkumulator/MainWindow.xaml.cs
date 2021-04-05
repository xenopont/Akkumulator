using System;
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
        }

        private readonly TrayWindowSettings traySettings = new TrayWindowSettings
        {
            TrayIcon = Properties.Resources.tray_icon_white,
            TrayMenuResourceKey = "TrayMenu",
            TrayIconText = "Akkumulator",
        };

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            TrayPartInit();
            IpPartInit();
        }
    }
}
