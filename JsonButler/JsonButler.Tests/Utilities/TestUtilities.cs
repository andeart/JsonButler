using System;



namespace Andeart.JsonButler.Tests.Utilities
{

    internal static class TestUtilities
    {
        public static Tuple<string, string> PeekAtFirstDiff (string source, string modified)
        {
            if (string.IsNullOrEmpty (source))
            {
                if (string.IsNullOrEmpty (modified))
                {
                    return new Tuple<string, string> ("No difference.", null);
                }

                return new Tuple<string, string> ("Source is blank. Modified contains:", modified);
            }

            if (string.IsNullOrEmpty (modified))
            {
                return new Tuple<string, string> ("Modified is blank. Source contains:", source);
            }

            // If any character is different from source, return peek at that position.
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] != modified[i])
                {
                    return new Tuple<string, string> ("Diff in source at:", PeekSnippetAtPosition (source, i));
                }
            }

            // ...at this point, it must mean modified has all of source in it, at the correct positions.
            // Check lengths.

            if (source.Length != modified.Length)
            {
                return new Tuple<string, string> ("Diff in modified at:", PeekSnippetAtPosition (modified, source.Length));
            }

            return new Tuple<string, string> ("No difference.", null);
        }

        private static string PeekSnippetAtPosition (string source, int position)
        {
            int start = position;
            int end = position;
            int maxEndIndex = source.Length - 1;

            // Move start caret to nearest preceding word. If already in word, do nothing.
            while (char.IsWhiteSpace (source[start]) && start > 0)
            {
                start--;
            }

            // Continue moving start caret to start of this word.
            while (!char.IsWhiteSpace (source[start]) && start > 0)
            {
                start--;
            }

            // Move end caret to nearest following whitespace. If already in whitespace, do nothing.
            while (!char.IsWhiteSpace (source[end]) && end < maxEndIndex)
            {
                end++;
            }

            // Continue moving end caret to nearest following word. If already in word, do nothing.
            while (char.IsWhiteSpace (source[end]) && end < maxEndIndex)
            {
                end++;
            }

            // Continue moving end caret to start of this word.
            while (!char.IsWhiteSpace (source[end]) && end < maxEndIndex)
            {
                end++;
            }

            return source.Substring (start, end - start);
        }
    }

}