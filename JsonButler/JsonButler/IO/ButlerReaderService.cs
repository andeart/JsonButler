using System;
using System.IO;



namespace Andeart.JsonButler.IO
{

    internal static class ButlerReaderService
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

        public static string GetFirstTypeInFile (string filePath)
        {
            string fileContents = ReadAllText (filePath);
            return GetFirstType (fileContents);
        }

        private static string GetFirstType (string textContents)
        {
            int classIndex = textContents.IndexOf ("class ");
            int structIndex = textContents.IndexOf ("struct ");
            int maxEndIndex = textContents.Length - 1;

            int nameStart;
            if (classIndex == -1)
            {
                if (structIndex == -1)
                {
                    throw new InvalidOperationException ("No types found in file.");
                }

                nameStart = structIndex + 7;
            } else
            {
                nameStart = classIndex + 6;
            }

            int nameEnd = nameStart;
            while (!char.IsWhiteSpace (textContents[nameEnd]) && nameEnd < maxEndIndex)
            {
                nameEnd++;
            }

            return textContents.Substring (nameStart, nameEnd - nameStart);
        }
    }

}