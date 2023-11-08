using System.Text.RegularExpressions;
using BornToMove.DAL;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BornToMove.Business
{
    public class BuMove
    {
        private readonly MoveCrud crud;

        public Dictionary<int, Move> exerciseList = new Dictionary<int, Move>();

        public BuMove(MoveCrud crud)
        {
            this.crud = crud;
        }

        public int GenerateRandomId()
        {
            List<int> Ids = crud.ReadAllMoveIds() ?? new List<int>();

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

        public Move GenerateSuggestion()
        {
            int randomId = GenerateRandomId();

            if (randomId > 0)
            {
                return GetExercise(randomId);
            }

            return null;
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

        public static bool IsValidRate(int input)
        {
            return input >= 1 && input <= 5;
        }

        public Move GetExercise(int id)
        {
            return crud.ReadMoveById(id);
        }

        public void GetExerciseList()
        {
            Dictionary<int, Move>? movements = crud.ReadAllMoves();
            
            if (movements != null)
            {
                foreach (var kvp in movements)
                {
                    int exerciseId = kvp.Key;
                    Move exercise = kvp.Value;

                    exerciseList.Add(exerciseId, exercise);
                }
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

        public void SaveMove (Move newMove)
        {
            crud.CreateMove(newMove);
        }

          
    }
}