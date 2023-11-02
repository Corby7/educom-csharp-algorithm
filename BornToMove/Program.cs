using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BornToMove
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            var connection = new DBConnect();
            
            bool validOptionSelected = false;

            Console.WriteLine("Hi there! You have been inactive for too long, it's time to start moving!");
            Console.WriteLine();
            Console.WriteLine("Which option do you prefer?");
            Console.WriteLine();
            Console.WriteLine("Press [1] for an exercise suggestion.");
            Console.WriteLine("Press [2] to choose from a list with exercises.");
            Console.WriteLine();

            while (!validOptionSelected)
            {
                Console.WriteLine("Press number key, followed by 'Enter' to continue: ");

                if (int.TryParse(Console.ReadLine(), out int key))
                {
                    Console.WriteLine();

                    switch (key)
                    {
                        case 1: // Exercise suggestion
                            Console.WriteLine("You opted for an exercise suggestion (option [1])");
                            validOptionSelected = true;

                            int id = Randomizer(connection);
                            Console.WriteLine();
                            Console.WriteLine("Randomly selected exercise ID: " + id);
                            Console.WriteLine();
                            Dictionary<int, Move> exerciseArray = connection.getExercise(id);
                            DisplayExercises(exerciseArray);
                            Console.WriteLine();

                            break;

                        case 2: // Choose from list
                            Console.WriteLine("You opted to choose from a list with exercies (option [2])");
                            validOptionSelected = true;
                            Console.WriteLine();
                            break;

                        default:
                            Console.WriteLine("Please choose option 1 or 2, try again.");
                            Console.WriteLine();
                            break;
                    }

                    //nog functionaliteit toevoegen
                    Console.WriteLine("Congratulations, you finished the exercise!");
                    Console.WriteLine("On a scale from 1-5, how do you rate this exercise?");
                    Console.WriteLine("And on a scale from 1-5, how intense was this exercise?");
                    Console.WriteLine();
                } 
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }

        }

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

        public static void DisplayExercises(Dictionary<int, Move> exerciseArray)
        {
            foreach (var kvp in exerciseArray)
            {
                int exerciseId = kvp.Key;
                Move exercise = kvp.Value;

                Console.WriteLine($"Suggested Exercise: { exerciseId}");
                Console.WriteLine($"Name: {exercise.Name}");
                Console.WriteLine($"Description: {exercise.Description}");
                Console.WriteLine($"Sweat Rate: {exercise.SweatRate}");
                Console.WriteLine();
            }
        }

    }
}