using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogueLib
{
    public static class EnumerableHelper<E>
    {
        private static Random rand;

        static EnumerableHelper()
        {
            rand = new Random();
        }

        public static T RandomItem<T>(IEnumerable<T> input)
        {
            return input.ElementAt(rand.Next(input.Count()));
        }

    }

    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns a random item in the IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T RandomItem<T>(this IEnumerable<T> input)
        {
            return EnumerableHelper<T>.RandomItem(input);
        }
    }
}
