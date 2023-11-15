using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvCBornToMove.Models
{
    public class Move
    {
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 4)]
        [RegularExpression(@"(?:^|\s)[A-Z][a-z]*(?:\s[A-Z][a-z]*)*")]
        [Required]
        public string? Name { get; set; }

        [StringLength(2000, MinimumLength = 10)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9\s.,-]*$")]
        [Required]
        public string? Description { get; set; }

        [Display(Name = "Sweat Rate (/5)")]
        [Range(1, 5)]
        public int SweatRate { get; set; }

        virtual public ICollection<MoveRating>? Ratings { get; set; }

    }
}
