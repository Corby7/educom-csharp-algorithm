using System.ComponentModel.DataAnnotations;

namespace MvCBornToMove.Models
{
    public class MoveAverageRating
    {
        public Move? Move { get; set; }

        [Display(Name = "Rating (/5)")]
        public double AverageRating { get; set; }

        [Display(Name = "Intensity (/10)")]
        public double AverageIntensity { get; set; }  
    }
}
