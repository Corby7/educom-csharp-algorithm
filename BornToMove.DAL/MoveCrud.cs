using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace BornToMove.DAL
{

    public class MoveCrud
    {
        private readonly MoveContext context;

        public MoveCrud(MoveContext context)
        {
            this.context = context;
        }

        public void createMove(Move newmove)
        {
            context.Move.Add(newmove);
            context.SaveChanges();
        }

        public void updateMove(Move updatedMove)
        {
            context.Move.Update(updatedMove);
            context.SaveChanges();
        }

        public void deleteMove(int id)
        {
            var movement = context.Move.Find(id);

            if (movement != null )
            {
                context.Move.Remove(movement);
                context.SaveChanges();
            }
        }

        public Move? readMoveById(int id)
        {
            return context.Move.Find(id);
        }


        public Dictionary<int, Move>? readAllMoves()
        {
            return context.Move.ToDictionary(move => move.Id, move => move);         
        }


    }
}
