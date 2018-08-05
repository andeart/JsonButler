using System.IO;
using System.Windows.Forms;



namespace Andeart.JsonButler.IO
{

    public class ButlerWriter
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