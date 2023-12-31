﻿using System.Text.RegularExpressions;
using BornToMove.DAL;
using Organizer;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BornToMove.Business
{
    public class BuMove
    {
        private readonly MoveCrud crud;

        private readonly RotateSort<MoveAverageRating> sorter;
        public Move SelectedMove { get; set; }

        public double SelectedMoveRating { get; set; }

        public List<MoveAverageRating> ExerciseList = new List<MoveAverageRating>();

        public BuMove(MoveCrud crud)
        {
            this.crud = crud;
            this.sorter = new RotateSort<MoveAverageRating>();
        }

        public void GenerateRandomSuggestion()
        {
            var moveIds = crud.ReadAllMoveIds();

            if (moveIds != null && moveIds.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(moveIds.Count);
                int randomId = moveIds[index];

                SelectedMove = crud.ReadMoveById(randomId);
                SelectedMoveRating = crud.ReadAverageRatingById(randomId);
            }

        }

        public (bool isValid, string error) IsValidName(string name)
        {
            if (!IsValidString(name))
            {
                return (false, "name");
            }

            if (crud.NameExists(name))
            {
                return (false, "name exists");
            }

            return (true, string.Empty);
        }

        public static bool IsValidString(string input)
        {
            string pattern = @"^[A-Za-z\s]+$"; // Allows only letters and spaces
            return !string.IsNullOrEmpty(input) && Regex.IsMatch(input, pattern) && input.Length > 4;
        }

        public static bool IsValidRateInt(int input)
        {
            return input >= 1 && input <= 5;
        }

        public static bool IsValidRateDouble(double input)
        {
            return (input >= 1.0 && input <= 5.0) && Math.Round(input, 1) == input;
        }


        public void GetExercise(int id)
        {
            SelectedMove = crud.ReadMoveById(id);
            SelectedMoveRating = crud.ReadAverageRatingById(id);
        }

        public void GetExerciseList()
        {
            List<Move>? movements = crud.ReadAllMoves();

            if (movements != null)
            {
                foreach (Move move in movements)
                {
                    ExerciseList.Add(new MoveAverageRating()
                    {
                        Move = move,
                        AverageRating = crud.ReadAverageRatingById(move.Id)
                    });
                }
                ExerciseList = sorter.Sort(ExerciseList, new RatingsConverter());
            }
            
        }

        public void SaveMove (string name, string description, int sweatrate)
        {
            crud.CreateMove(new Move()
            {
                Name = name,
                Description = description,
                SweatRate = sweatrate
            });
        }

        public void SaveMoveRating(double rating, double vote)
        {

            Move selectedMove = SelectedMove;

            crud.CreateMoveRating(new MoveRating()
            {
                Move = selectedMove,
                Rating = rating,
                Vote = vote,
            });

        }

        public void FillEmptyDB()
        {
            if (crud.IsMovesTableEmpty())
            {
                List<Move> moves = new List<Move>
                {
                    new Move
                    {
                        Name = "Push Up",
                        Description = "Ga horizontaal liggen op teentoppen en handen. Laat het lijf langzaam zakken tot de neus de grond bijna raakt. Duw het lijf terug nu omhoog tot de ellebogen bijna gestrekt zijn. Vervolgens weer laten zakken. Doe dit 20 keer zonder tussenpauzes",
                        SweatRate = 3
                    },
                    new Move
                    {
                        Name = "Planking",
                        Description = "Ga horizontaal liggen op teentoppen en onderarmen. Houdt deze positie 1 minuut vast",
                        SweatRate = 3
                    },
                    new Move
                    {
                    Name = "Squat",
                    Description = "Ga staan met gestrekte armen. Zak door de knieën tot de billen de grond bijna raken. Ga weer volledig gestrekt staan. Herhaal dit 20 keer zonder tussenpauzes",
                    SweatRate = 5
                    }
                };

                try
                {
                    foreach (Move move in moves)
                    {
                        crud.CreateMove(move);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error adding moves to database: " + ex.Message);
                }

            }
        }
          
    }
}