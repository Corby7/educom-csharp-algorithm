using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvCBornToMove.Models
{
    public class Move
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        [Display(Name = "Sweat Rate")]
        [Column(TypeName = "decimal(3, 1)")] //sweatrate in decimalen doen? wordt sowieso nog verkeerd weergegeven op de site
        public decimal SweatRate { get; set; }

    }
}
