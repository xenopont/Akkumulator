namespace Akkumulator.Util
{
    internal class Theme
    {
        private const string REGISTRY_THEME_KEY = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string REGISTRY_THEME_VALUE_NAME_APPS = "AppsUseLightTheme";
        private const string REGISTRY_THEME_VALUE_NAME_SYSTEM = "SystemUsesLightTheme";

        private static bool LightThemeUsedBy(string owner)
        {
            return General.ReadRegistryString(REGISTRY_THEME_KEY, owner, "0") == "1";
        }

        public static bool AppsUseLightTheme()
        {
            return LightThemeUsedBy(REGISTRY_THEME_VALUE_NAME_APPS);
        }

        public static bool SystemUsesLightTheme()
        {
            return LightThemeUsedBy(REGISTRY_THEME_VALUE_NAME_SYSTEM);
        }
    }
}
