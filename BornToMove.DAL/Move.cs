using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove.DAL
{
    public class Move
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Description { get; set; } 
        public int SweatRate { get; set; }

        virtual public ICollection<MoveRating> Ratings { get; set; }

    }
}

