using System.Text.RegularExpressions;
using BornToMove.DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BornToMove.Business
{
    public class BuMove
    {
        private readonly MoveCrud crud;

        public BuMove()
        {
            var context = new MoveContext();
            this.crud = new MoveCrud(context);
        }

        public int Randomizer()
        {
            List<int> Ids = crud.readAllMoveIds() ?? new List<int>();

            if (Ids.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(Ids.Count);
                int randomExerciseId = Ids[index];
                return randomExerciseId;
            }
            else
            {
                return -1; // if no values in Ids
            }
        }

        public static bool IsValidString(string input)
        {
            string pattern = @"^[A-Za-z\s]+$"; // Allows only letters and spaces
            return !string.IsNullOrEmpty(input) && Regex.IsMatch(input, pattern) && input.Length > 4;
        }

        public static bool IsValidRate(int input)
        {
            return input >= 1 && input <= 5;
        }

        public void getExercise(int id)
        {
            Move? exercise = crud.readMoveById(id);

            if (exercise != null)
            {
                Console.WriteLine($"Exercise: {exercise.Name}"); //dit nog verplaatsen uit business layer
                Console.WriteLine($"Description: {exercise.Description}");
                Console.WriteLine($"Sweat Rate: {exercise.SweatRate}");
            }
            else
            {
                Console.WriteLine("Exercise not found");
            }
        }

        public Dictionary<int, Move> getExerciseList()
        {
            Dictionary<int, Move>? exerciseList = crud.readAllMoves();

            if (exerciseList != null)
            {
                return exerciseList;
            }
            else
            {
                return new Dictionary<int, Move>();
            }

        }

        public static void AskForRatings()
        {
            //nog functionaliteit toevoegen
            Console.WriteLine("Congratulations, you finished the exercise!");
            Console.WriteLine("On a scale from 1-5, how do you rate this exercise?");
            Console.WriteLine("And on a scale from 1-5, how intense was this exercise?");
            Console.WriteLine();
        }

          
    }
}