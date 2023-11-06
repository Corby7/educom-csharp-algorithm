using System.Text.RegularExpressions;
/*using BornToMove;*/

namespace BornToMove.Business
{
    public class BuMove
    {
        public static int Randomizer(DBConnect connection)
        {
            List<int> Ids = connection.getAllIds();

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

        public static void DisplayExercises(Dictionary<int, Move> exerciseArray)
        {
            foreach (var kvp in exerciseArray)
            {
                int exerciseId = kvp.Key;
                Move exercise = kvp.Value;

                Console.WriteLine($"Exercise: {exercise.Name}");
                Console.WriteLine($"Description: {exercise.Description}");
                Console.WriteLine($"Sweat Rate: {exercise.SweatRate}");
                Console.WriteLine();
            }
        }

        public static void DisplayExerciseList(Dictionary<int, Tuple<string, int>> exerciseList)
        {
            int i = 0;
            foreach (KeyValuePair<int, Tuple<string, int>> exerciseEntry in exerciseList)
            {
                int exerciseId = exerciseEntry.Key;
                string exerciseName = exerciseEntry.Value.Item1;
                int sweatRate = exerciseEntry.Value.Item2;
                Console.WriteLine($"[{exerciseId}]: {exerciseName}, sweatrate: {sweatRate}");
                i++;
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