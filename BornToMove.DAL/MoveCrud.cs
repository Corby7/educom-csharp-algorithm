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

        public void createMove(Move newMove)
        {
            try
            {
                context.Move.Add(newMove);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while creating a move: " + ex.Message);
            }
        }

        public void updateMove(Move updatedMove)
        {
            try
            {
                context.Move.Update(updatedMove);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while updating a move: " + ex.Message);
            }
        }

        public void deleteMove(int id)
        {
            try
            {
                var movement = context.Move.Find(id);

                if (movement != null)
                {
                    context.Move.Remove(movement);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while deleting a move: " + ex.Message);
            }
        }

        public Move? readMoveById(int id)
        {
            try
            {
                return context.Move.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading a move by ID: " + ex.Message);
                return null;
            }
        }


        public Dictionary<int, Move>? readAllMoves()
        {
            try
            {
                return context.Move.ToDictionary(move => move.Id, move => move);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading all moves: " + ex.Message);
                return null;
            }
        }

        public List<int>? readMoveIds()
        {
            try
            {
                return context.Move.Select(move => move.Id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading move IDs: " + ex.Message);
                return null;
            }
        }

        public bool nameExists(string name)
        {
            try
            {
                return context.Move.Any(move => move.Name == name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while checking if a name exists: " + ex.Message);
                return false;
            }
        }


    }
}
