using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace BornToMove
{
    internal class Program
    {
        static void Main(string[] args)
        {
 
            var connection = new DBConnect();
            bool validOptionSelected = false;

            Console.WriteLine();
            Console.WriteLine("Welcome to BornToMove!");
            Console.WriteLine();
            Console.WriteLine("Looks like you have been inactive for too long, it's time to start moving!");
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
                            validOptionSelected = true;
                            Console.WriteLine("--- You opted for an exercise suggestion (option [1]) ---");

                            int id = Randomizer(connection);
                            Console.WriteLine();
                            Console.WriteLine("Randomly selected exercise ID: " + id);
                            Console.WriteLine();
                            Dictionary<int, Move> exerciseOption1 = connection.getExercise(id);
                            Console.WriteLine($"Suggested Exercise: {id}");
                            DisplayExercises(exerciseOption1);
                            Console.WriteLine();
                            AskForRatings();

                            break;

                        case 2: // Choose from list
                            Console.WriteLine("--- You opted to choose from a list with exercices (option [2]) ---");                            
                            Console.WriteLine();
                            Console.WriteLine("Exercises to choose from: ");
                            Console.WriteLine();

                            Dictionary<int, Tuple<string, int>> exerciseList = connection.getExercices();

                            Console.WriteLine("[0]: --- add your own exercise ---");


                            DisplayExerciseList(exerciseList);
                            Console.WriteLine();

                            while (!validOptionSelected)
                            {
                                Console.WriteLine("Press number key of the option you want to choose, followed by 'Enter' to continue: ");                               

                                if (int.TryParse(Console.ReadLine(), out int selectedExerciseId))
                                {   
                                    if (selectedExerciseId == 0) // User adds own exercise
                                    {
                                        bool validName = false;

                                        while (!validName)
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Type the name of the exercise you want to add, followed by 'Enter' to continue: ");
                                            string name = Console.ReadLine();

                                            if (IsValidString(name))
                                            {
                                                bool exists = connection.nameExists(name);
                                                if (exists) // Name exists in DB already, try again
                                                {
                                                    Console.WriteLine();
                                                    Console.WriteLine("--- Exercise: " + name + " already exists. Try another one. ---");
                                                }
                                                else // Name does not exist > enter description
                                                {
                                                    validName = true;
                                                    bool validDescription = false;

                                                    while (!validDescription)
                                                    {
                                                        Console.WriteLine();
                                                        Console.WriteLine("Type a description of the exercise, followed by 'Enter' to continue: ");
                                                        string description = Console.ReadLine();

                                                        if (IsValidString(description)) // Valid description > enter sweatrate
                                                        {
                                                            validDescription = true;
                                                            bool validSweatRate = false;

                                                            while (!validSweatRate)
                                                            {
                                                                Console.WriteLine();
                                                                Console.WriteLine("Type the sweatrate of your exercise on a scale from 1-5 (integers), followed by 'Enter' to continue: ");
                                                                if (int.TryParse(Console.ReadLine(), out int sweatrate) && IsValidRate(sweatrate)) // Valid sweatrate > save user input
                                                                { 
                                                                    validSweatRate = true;
                                                                    validOptionSelected = true;

                                                                    //enter save to db
                                                                    int rowsAffected = connection.saveExercise(name, description, sweatrate);
                                                                    Console.WriteLine("Rowsaffected: " + rowsAffected);

                                                                    Console.WriteLine();
                                                                    Console.WriteLine("Everything is valid, save user input as Name: " + name + " Description: " + description + " Sweatrate: " + sweatrate);
                                                                }
                                                                else
                                                                {
                                                                    Console.WriteLine();
                                                                    Console.WriteLine("--- Invalid sweatrate: enter a number from 1 to 5. Try again ---");
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine();
                                                            Console.WriteLine("--- Invalid description: don't use special characters or numbers. Try again ---");
                                                        }                                                       
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("--- Invalid: don't use special characters or numbers. Name & description must be longer than 4 characters. Try again ---");
                                            }
                                        }
                                    }
                                    else if (exerciseList.TryGetValue(selectedExerciseId, out Tuple<string, int> selectedExercise)) // User chooses exercise from list
                                    {
                                        validOptionSelected = true;

                                        Console.WriteLine();
                                        Console.WriteLine("--- You have chosen exercise: " + selectedExerciseId + ", " + selectedExercise.Item1 + " ---");
                                        Console.WriteLine();
                                        Dictionary<int, Move> exerciseOption2 = connection.getExercise(selectedExerciseId);
                                        DisplayExercises(exerciseOption2);
                                        Console.WriteLine();
                                        AskForRatings();
                                    }
                                    else
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("--- Exercise not found in list, try again. ---");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("--- Invalid input. Please enter a valid number. ---");
                                }
                            }

                            break;

                        default:
                            Console.WriteLine("--- Please choose option 1 or 2; try again. ---");
                            Console.WriteLine();
                            break;
                    }

                } 
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("--- Invalid input. Please enter a valid number. ---");
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