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

        public void CreateMove(Move newMove)
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

        public void UpdateMove(Move updatedMove)
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

        public void DeleteMove(int id)
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

        public Move? ReadMoveById(int id)
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


        public List<Move>? ReadAllMoves()
        {
            try
            {
                return context.Move.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading all moves: " + ex.Message);
                return null;
            }
        }

        public Dictionary<int, MoveRating>? ReadAllAverageMoveRatings()
        {
            try
            {
                var moveData = context.Move.ToList();  // Fetch Move data

                var AllAverageMoveRatings = context.MoveRating
                    .GroupBy(rating => rating.Move.Id)
                    .ToDictionary(
                        group => group.Key,
                        group => new MoveRating
                        {
                            Id = group.Key, // Assuming Move ID is the key
                            Move = moveData.FirstOrDefault(move => move.Id == group.Key), // Fetch associated Move object
                            Rating = group.Average(r => r.Rating), // Calculate average rating
                                                                   // Include other properties as needed
                        }
                    );

                return AllAverageMoveRatings;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading all moveratings: " + ex.Message);
                return null;
            }
        }

        public List<double> ReadAllRatings()
        {
            try
            {
                return context.MoveRating.Select(rating => rating.Rating).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading all ratings: " + ex.Message);
                return null;
            }
        }

        public List<int>? ReadAllMoveIds()
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

        public bool NameExists(string name)
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

        public bool IsMovesTableEmpty()
        {
            try
            {
                return !context.Move.Any();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while checking if database move is empty: " + ex.Message);
                return false;
            }
        }

        public void CreateMoveRating(MoveRating newMoveRating)
        {
            context.MoveRating.Add(newMoveRating);
            context.SaveChanges();
        }

        public double ReadAverageRatingById(int id)
        {
            var ratings = from rating in context.MoveRating
                          where rating.Move.Id == id
                          select rating.Rating;

            double averageRating = ratings.DefaultIfEmpty().Average();
            double roundedRating = Math.Round(averageRating, 1);

            return roundedRating;
        }

       

    }
}
