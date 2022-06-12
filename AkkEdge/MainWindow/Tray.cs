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
            public WindowState InitialWindowState { get; set; } = WindowState.Minimized;
            public System.Drawing.Icon TrayIcon { get; set; }
            public string TrayMenuResourceKey { get; set; } // can't access ContextMenu directly
            public string TrayIconText { get; set; } = "Application";
        }

        public WindowState CurrentWindowState { get; set; } = WindowState.Normal;

        private void TrayPartInit(TrayWindowSettings settings)
        {
            CreateTrayIcon(settings);
            WindowState = settings.InitialWindowState;
        }

        private NotifyIcon TrayIcon;
        private ContextMenu TrayMenu;

        private void CreateTrayIcon(TrayWindowSettings settings)
        {
            if (TrayIcon == null)
            {
                TrayIcon = new NotifyIcon
                {
                    Icon = settings.TrayIcon,
                    Text = settings.TrayIconText,
                };
                TrayMenu = Resources[settings.TrayMenuResourceKey] as ContextMenu;

                TrayIcon.Click += delegate (object sender, EventArgs e)
                {
                    if ((e as System.Windows.Forms.MouseEventArgs).Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        ShowHideMainWindow(sender, null);
                    }
                    else
                    {
                        TrayMenu.IsOpen = true;
                        _ = Activate(); // must activate Window
                    }
                };
            }
            TrayIcon.Visible = true;
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
                _ = Activate(); // otherwise it doesn't get focus
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

        public bool CanClose { get; set; } = false;

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
