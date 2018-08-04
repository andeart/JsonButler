using System;
using System.Collections.Generic;



namespace Andeart.JsonButler.Utilities
{

    // This class only exists because this guy isn't a big fan of LINQ's performance.
    // He will buy you a beer if you have a while to chat about it.


    internal static class CollectionUtilities
    {
        public static bool IsNullOrEmpty<T> (this IList<T> list)
        {
            return list == null || list.Count == 0;
        }

        public static bool Any<T> (this IList<T> list, Func<T, bool> predicate, out T element)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate (list[i]))
                {
                    element = list[i];
                    return true;
                }
            }

            element = default(T);
            return false;
        }
    }

}