using System;
using System.IO;



namespace Andeart.JsonButler.IO
{

    public class ButlerReader
    {
        public static string ReadAllText (string filePath)
        {
            if (!File.Exists (filePath))
            {
                throw new ArgumentException ($"File at {filePath} does not exist.", nameof(filePath));
            }

            string fileContents = File.ReadAllText (filePath);
            return fileContents;
        }

        public static bool Exists (string filePath)
        {
            return File.Exists (filePath);
        }
    }

}