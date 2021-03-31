using System.Windows;

namespace Akkumulator.Util
{
    class General
    {
        public static void CopyTextToClipboard(string text)
        {
            Clipboard.SetText(text);
        }
    }
}
