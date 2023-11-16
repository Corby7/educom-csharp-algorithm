using System.ComponentModel.DataAnnotations;

namespace MvCBornToMove.Models
{
    public class MoveRating
    {
        public int Id { get; set; }

        public Move? Move { get; set; }

        [Range(1, 5)]
        public double Rating { get; set; }

        [Range(1, 5)]
        public double Intensity { get; set; }

    }
}

