using System.Windows;

namespace Akkumulator.Util
{
    class General
    {
        public static void CopyTextToClipboard(string text)
        {
            Clipboard.SetText(text);
        }

        public static string ReadRegistryString(string keyName, string valueName, string defaultValue)
        {
            try 
            {
                var v = Microsoft.Win32.Registry.GetValue(keyName, valueName, defaultValue);
                if (v != null)
                {
                    return v.ToString();
                }
            }
            catch { }

            return defaultValue;
        }
    }
}
