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
        /*public create(Move move)
        {

        }

        public update(Move updated Move)
        {

        }

        public delete(int id)
        {

        }

        public readMoveById(int id)
        {

        }

        public readAllMoves()
        {

        }*/
        /*    public Dictionary<int, Move> readMoveById(int id)
            {
                using var context = new MoveContext();

                var move = from move in context.Move
                           where move.Id == id
                           select move;

            }*/

        public void delete(int id)
        {
            using var context = new MoveContext();

            context.Remove(id);
        }

        public Dictionary<int, Move> readMoveById(int id)
        {
            using var context = new MoveContext(); //using naar buiten halen?

            var movement = from move in context.Move
                           where move.Id == id
                            select move;

            Dictionary<int, Move> result = movement.ToDictionary(m => m.Id, m => m);

            return result;
        }


        public void readAllMoves()
        {
            using var context = new MoveContext();

            var movements = from move in context.Move
                            select move;

            /*return movements;*/

            foreach (Move m in movements)
            {
                Console.WriteLine($"Id: {m.Id}");
                Console.WriteLine($"Name: {m.Name}");
                Console.WriteLine($"Description: {m.Description}");
                Console.WriteLine($"Sweatrate: {m.SweatRate}");
                Console.WriteLine();
            }
        }


    }
}
