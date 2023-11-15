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

        [StringLength(200, MinimumLength = 10)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required]
        public string? Description { get; set; }

        [Display(Name = "Sweat Rate")]
        [Range(1, 10)]
        [Column(TypeName = "decimal(3, 1)")] //sweatrate in decimalen doen? wordt sowieso nog verkeerd weergegeven op de site
        public decimal SweatRate { get; set; }

    }
}
