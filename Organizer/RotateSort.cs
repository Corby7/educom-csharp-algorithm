﻿using System;
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
        /// <param name="low">De index within this.array to start with</param>
        /// <param name="high">De index within this.array to stop with</param>
        private void SortFunction(int low, int high)
        {
            if (high <= low)
            {
                return; // stop if 1 or less items in array
            }

            int pivot = Partitioning(low, high);
            SortFunction(low, pivot - 1);
            SortFunction(pivot + 1, high);

        }

        /// 
        /// Partition the array in a group 'low' digits (e.a. lower than a choosen pivot) and a group 'high' digits
        /// </summary>
        /// <param name="low">De index within this.array to start with</param>
        /// <param name="high">De index within this.array to stop with</param>
        /// <returns>The index in the array of the first of the 'high' digits</returns>
        private int Partitioning(int low, int high)
        {
            int pivot = this.array[high];
            int i = low - 1;

            for (int j = low; j <= high - 1; j++) 
            {
                if (this.array[j] < pivot)
                {
                    i++;
                    // swap values if smaller than pivot
                    int temp = this.array[i];
                    this.array[i] = this.array[j];
                    this.array[j] = temp;
                }

                i++; // now at position of pivot (i + 1)
                int pivotSpot = this.array[i];
                array[i] = this.array[high];
                this.array[high] = pivotSpot; // places the pivot (which was at the end of the array) on its correct pivotspot
            }

            return i; 
        }
    }
}
