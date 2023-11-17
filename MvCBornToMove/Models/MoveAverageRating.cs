using System.ComponentModel.DataAnnotations;

namespace MvCBornToMove.Models
{
    public class MoveAverageRating
    {
        [Key]
        public int Id { get; set; }

        public Move? Move { get; set; }

        [Display(Name = "Rating (/5)")]
        [Range(1, 5)]
        public double AverageRating { get; set; }

        [Display(Name = "Intensity (/5)")]
        [Range(1, 5)]
        public double AverageIntensity { get; set; }  
    }
}
