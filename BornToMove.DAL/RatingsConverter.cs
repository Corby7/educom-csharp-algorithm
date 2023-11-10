using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL
{
    public class RatingsConverter : IComparer<MoveAverageRating>
    {
        public int Compare(MoveAverageRating? x, MoveAverageRating? y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentNullException("Cannot compare null objects");
            }

            return y.AverageRating.CompareTo(x.AverageRating);
        }
    }
}
