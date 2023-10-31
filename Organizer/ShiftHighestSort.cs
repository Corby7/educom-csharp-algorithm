using System;
using System.Collections.Generic;

namespace Organizer
{
	public class ShiftHighestSort
    {
        private List<int> array = new List<int>();

        /// <summary>
        /// Sort an array using the functions below
        /// </summary>
        /// <param name="input">The unsorted array</param>
        /// <returns>The sorted array</returns>
        public List<int> Sort(List<int> input)
        {
            this.array = new List<int>(input);

            SortFunction(0, array.Count - 1);
            return this.array;
        }

        /// <summary>
        /// Sort the array from low to high
        /// </summary>
        /// <param name="low">De index within this.array to start with</param>
        /// <param name="high">De index within this.array to stop with</param>
        private void SortFunction(int low, int high)
        {
            bool swapped; // to check whether a swapp happened > avoid unnecessary loops when already finished

            for (int i = low; i <= high; i++) 
            { 
                swapped = false; // reset swapped each pass

                for (int j = low; j <= high - i - 1; j++)
                {
                    int currentElement = this.array[j];
                    int nextElement = this.array[j + 1];

                    if (currentElement > nextElement)
                    {
                        // Swap elements
                        this.array[j] = nextElement;
                        this.array[j + 1] = currentElement;

                        swapped = true; // set true when swap happened
                    }
                }

                if (!swapped)
                {
                    break;
                }
            }
        }    
    }
}
