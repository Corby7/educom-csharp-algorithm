using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Organizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Press <F5> to run this code, when "Hello World!" appears in a black box, remove the line below and write your code below.
            Console.WriteLine("Enter amount of list items:");
            int amount = Convert.ToInt32(Console.ReadLine());

            ShowList("Example of ShowList", new List<int>() { -33, 3, 2, 2, 3, 34, 34, 32, 1, 3, 5, 3, -22, -99, 33, -22, 11, 3, 33, 12, -2, -21, 4, 34, 22, 15, 34,-22 });

            

            List<int> unsortedRandomList = GenerateRandomList(amount);
            ShowList("Unsorted RandomList", unsortedRandomList);

            // First sorting method
            ShiftHighestSort shiftHighestSort = new ShiftHighestSort();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            List<int> sortedRandomList1 = shiftHighestSort.Sort(unsortedRandomList);
            stopWatch.Stop();
            var timeElapsed1 = stopWatch.Elapsed;

            ShowList("Sorted RandomList (shift highest sort)", sortedRandomList1);

            // Second sorting method
            RotateSort rotateSort = new RotateSort();

            stopWatch.Restart();
            List<int> sortedRandomList2 = rotateSort.Sort(unsortedRandomList);
            stopWatch.Stop();
            var timeElapsed2 = stopWatch.Elapsed;

            ShowList("Sorted RandomList (rotate sort)", sortedRandomList2);

            // Write down stopwatch time

            Console.WriteLine();
            Console.WriteLine("Stopwatch time first sorting method (shift highest sort): " + timeElapsed1);
            Console.WriteLine();
            Console.WriteLine("Stopwatch time second sorting method (rotate sort): " + timeElapsed2);
            Console.WriteLine();

            // Check first sorting method
            Console.WriteLine();
            if (CheckSort(unsortedRandomList))
            {
                Console.WriteLine("Checksort unsorted list is true");
            } else
            {
                Console.WriteLine("Checksort unsorted list is false");
            }

            Console.WriteLine();
            if (CheckSort(sortedRandomList1))
            {
                Console.WriteLine("Checksort sorted list is true");
            }
            else
            {
                Console.WriteLine("Checksort sorted list is false");
            }
        }

        public static bool CheckSort(List<int> sortedList)
        {
            for (int i = 0; i < sortedList.Count - 1; i++)
            {
                int currentElement = sortedList[i];
                int nextElement = sortedList[i + 1];
                
                if (currentElement > nextElement)
                {
                    return false;
                }
            }
            return true;
        }

        public static List<int> GenerateRandomList(int amount)
        {
    
            var randomList = new List<int>();
            Random rnd = new Random();

            // Add a random number between -99 and 99 to the list, repeat $amount of times
            for (int i = 0; i < amount; i++) 
            {
                randomList.Add(rnd.Next(-99, 99));
            }

            return randomList;
        }


        /* Example of a static function */

        /// <summary>
        /// Show the list in lines of 20 numbers each
        /// </summary>
        /// <param name="label">The label for this list</param>
        /// <param name="theList">The list to show</param>
        public static void ShowList(string label, List<int> theList)
        {
            int count = theList.Count;
            if (count > 100)
            {
                count = 200; // Do not show more than 300 numbers
            }

            Console.WriteLine();
            Console.Write(label);
            Console.Write(':');
            for (int index = 0; index < count; index++)
            {
                if (index % 20 == 0) // when index can be divided by 20 exactly, start a new line
                {
                    Console.WriteLine();
                }
                Console.Write(string.Format("{0,3}, ", theList[index]));  // Show each number right aligned within 3 characters, with a comma and a space
            }
            Console.WriteLine();
        }
    }
}
