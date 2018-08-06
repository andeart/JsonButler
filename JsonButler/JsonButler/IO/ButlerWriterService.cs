using System.IO;
using System.Windows.Forms;



namespace Andeart.JsonButler.IO
{

    internal static class ButlerWriterService
    {
        public static void WriteAllText (string filePath, string contents)
        {
            File.WriteAllText (filePath, contents);
        }

        public static void SetClipboardText (string text)
        {
            Clipboard.SetText (text);
        }
    }

}