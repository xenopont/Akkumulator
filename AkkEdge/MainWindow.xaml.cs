using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Markup;

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

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            string trayMenuResourceKey = "TrayMenu";
            TrayPartInit(new TrayWindowSettings
            {
                TrayIcon = TrayIconWhite,
                TrayMenuResourceKey = trayMenuResourceKey,
                TrayIconText = "Akkumulator",
                InitialWindowState = WindowState.Normal,
            });
            IpPartInit(trayMenuResourceKey);
            ThemePartInit();
        }
    }
}
