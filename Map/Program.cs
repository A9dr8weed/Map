using Map.Model;

using System;

namespace Map
{
    public static class Program
    {
        private static void Main()
        {
            Map<int, string> map = new Map<int, string>();
            map.Add(new Item<int, string>(1, "A"));
            map.Add(new Item<int, string>(2, "B"));
            map.Add(new Item<int, string>(2, "C"));
            map.Add(new Item<int, string>(4, "D"));
            map.Add(new Item<int, string>(5, "E"));
            map.Add(4, "F");
            map.Update(4, "AB");
            map.Remove(7);

            ShowMap(map, "Created map");

            Console.ReadLine();

            Dict<int, string> dict = new Dict<int, string>();
            dict.Add(new Item<int, string>(1, "One"));
            dict.Add(new Item<int, string>(2, "Two"));
            dict.Add(new Item<int, string>(4, "Four"));
            dict.Add(new Item<int, string>(101, "Hundred and one"));
            dict.Add(new Item<int, string>(201, "Two hundred and one"));
            dict.Add(5, "Five");
            dict.Update(4, "AB");

            foreach (object item in dict)
            {
                Console.WriteLine(item.ToString());
            }

            Console.WriteLine(dict.Search(101) ?? "Not found");
            Console.WriteLine(dict.Search(1) ?? "Not found");

            dict.Remove(7);
            dict.Remove(101);
            dict.Remove(103);
            dict.Remove(3);

            foreach (object item in dict)
            {
                Console.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// Display the dictionary on the screen.
        /// </summary>
        /// <param name="map"> Dictionary. </param>
        /// <param name="title"> Title before the dictionary output. </param>
        private static void ShowMap(Map<int, string> map, string title)
        {
            Console.WriteLine($"{title}: ");
            foreach (int key in map.keys)
            {
                Console.Write($"{key}: ");
                Console.WriteLine(map.Search(key));
            }
            Console.WriteLine();
        }
    }
}