using System;
using System.Windows;
using ContextMenu = System.Windows.Controls.ContextMenu;
using MenuItem = System.Windows.Controls.MenuItem;
using NotifyIcon = System.Windows.Forms.NotifyIcon;

namespace Akkumulator
{
    public partial class MainWindow
    {
        private class TrayWindowSettings
        {
            public WindowState InititalWindowState { get; set; } = WindowState.Minimized;
            public System.Drawing.Icon TrayIcon { get; set; }
            public string TrayMenuResourceKey { get; set; } // can't access ContextMenu directly
            public string TrayIconText { get; set; } = "Application";
        }

        /*
         * Replace required settings here
         *
         */
        private readonly TrayWindowSettings traySettings = new TrayWindowSettings
        {
            TrayIcon = Properties.Resources.tray_icon_white,
            TrayMenuResourceKey = "TrayMenu",
            TrayIconText = "Akkumulator",
        };

        private WindowState fCurrentWindowState = WindowState.Normal;
        public WindowState CurrentWindowState
        {
            get { return fCurrentWindowState; }
            set { fCurrentWindowState = value; }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            CreateTrayIcon();
            WindowState = WindowState.Minimized;
        }

        private NotifyIcon TrayIcon = null;
        private ContextMenu TrayMenu = null;

        private bool CreateTrayIcon()
        {
            bool result = false;
            if (TrayIcon == null)
            {
                TrayIcon = new NotifyIcon
                {
                    Icon = traySettings.TrayIcon,
                    Text = traySettings.TrayIconText,
                };
                TrayMenu = Resources[traySettings.TrayMenuResourceKey] as ContextMenu;

                TrayIcon.Click += delegate (object sender, EventArgs e) {
                    if ((e as System.Windows.Forms.MouseEventArgs).Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        ShowHideMainWindow(sender, null);
                    }
                    else
                    {
                        TrayMenu.IsOpen = true;
                        Activate(); // must activate Window
                    }
                };
                result = true;
            }
            else
            {
                result = true;
            }
            TrayIcon.Visible = true;
            return result;
        }

        private void ShowHideMainWindow(object sender, RoutedEventArgs e)
        {
            TrayMenu.IsOpen = false;
            if (IsVisible)
            {
                Hide();
                (TrayMenu.Items[0] as MenuItem).Header = "Show";
            }
            else
            {
                Show();
                (TrayMenu.Items[0] as MenuItem).Header = "Hide";
                WindowState = CurrentWindowState;
                Activate(); // otherwise it doesn't get focus
            }
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                (TrayMenu.Items[0] as MenuItem).Header = "Show";
            }
            else
            {
                CurrentWindowState = WindowState;
            }
        }

        private bool fCanClose = false;
        public bool CanClose
        {
            get { return fCanClose; }
            set { fCanClose = value; }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!CanClose)
            {
                e.Cancel = true;
                CurrentWindowState = WindowState;
                (TrayMenu.Items[0] as MenuItem).Header = "Show";
                Hide();
            }
            else
            {
                TrayIcon.Visible = false;
            }
        }

        private void MenuExitClick(object sender, RoutedEventArgs e)
        {
            CanClose = true;
            Close();
        }
    }
}
