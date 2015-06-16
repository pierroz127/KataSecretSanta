using System;
using System.Collections.Generic;

namespace KataSecretSanta
{
    public static class Extension
    {
        /// <summary>
        /// Swap the ith element with jth element in a list  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public static void Swap<T>(this IList<T> list, int i, int j)
        {
            T tmp = list[j];
            list[j] = list[i];
            list[i] = tmp;
        }

        /// <summary>
        /// Randomly shuffle in place the elements of a list  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < list.Count; i++)
            {
                // Exchange list[i] with a random element in list[i..N-1]
                int r = i + rnd.Next(list.Count - i);
                list.Swap(i, r);
            }
        }

        public static Dictionary<T, int> ToIndexDictionary<T>(this IList<T> list)
        {
            var dict = new Dictionary<T, int>();
            for (int i = 0; i < list.Count; i++)
            {
                dict[list[i]] = i;
            }
            return dict;
        }
    }
}
