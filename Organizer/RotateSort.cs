using System;
using System.Collections.Generic;

namespace Organizer
{
	public class RotateSort
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
        /// 
        /// </summary>
        /// <param name="start">De index within this.array to start with</param>
        /// <param name="end">De index within this.array to stop with</param>
        private void SortFunction(int start, int end)
        {
            if (end <= start)
            {
                return; // stop if 1 or less items in array
            }

            int pivot = Partitioning(start, end);
            SortFunction(start, pivot - 1);
            SortFunction(pivot + 1, end);

        }

        /// 
        /// Partition the array in a group 'low' digits (e.a. lower than a choosen pivot) and a group 'high' digits
        /// </summary>
        /// <param name="low">De index within this.array to start with</param>
        /// <param name="end">De index within this.array to stop with</param>
        /// <returns>The index in the array of the first of the 'high' digits</returns>
        private int Partitioning(int start, int end)
        {
            int pivot = this.array[end];
            int i = start - 1;

            for (int j = start; j <= end - 1; j++) 
            {
                if (this.array[j] < pivot)
                {
                    i++;
                    // swap values if smaller than pivot
                    int temp = this.array[i];
                    this.array[i] = this.array[j];
                    this.array[j] = temp;
                }
            }
            int pivotSpot = this.array[i + 1]; // position of pivot at (i + 1)
            array[i + 1] = this.array[end];
            this.array[end] = pivotSpot; // places the pivot (which was at the end of the array) on its correct pivotspot

            return i + 1; // return location of pivot
        }
    }
}
