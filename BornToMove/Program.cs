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
            int id = Randomizer(connection);
            Console.WriteLine("Randomly selected exercise ID: " + id);



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
                        case 1:
                            Console.WriteLine("You entered, " + key + " as your login key!");
                            validOptionSelected = true;
                            break;
                        case 2:
                            Console.WriteLine("You entered, " + key + " as your login key!");
                            validOptionSelected = true;
                            break;
                        default:
                            Console.WriteLine("Please choose option 1 or 2, try again.");
                            break;
                    }
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

    }
}