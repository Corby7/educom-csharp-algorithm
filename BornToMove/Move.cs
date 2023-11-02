﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove
{
    public class Move
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SweatRate { get; set; }

        public Move(int id, string name, string description, int sweatrate)
        {
            Id = id;
            Name = name;
            Description = description;
            SweatRate = sweatrate;
        }
            
    }
}

