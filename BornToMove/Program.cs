using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using BornToMove.Business;


namespace BornToMove
{
    internal class Program
    {

        /* static void Main(string[] args)
         {
             BuMove buMove = new BuMove();

             int id = buMove.Randomizer();
             Console.WriteLine(id);
         }*/

        static void Main(string[] args)
        {
            BuMove buMove = new BuMove();
            Presenter presenter = new Presenter(buMove);

            bool validOptionSelected = false;

            Presenter.welcomeMessage();

            Presenter.askInitialChoice();
            presenter.getInitialChoice();
            
            while (!validOptionSelected)
            {
                Console.WriteLine("Press number key, followed by 'Enter' to continue: ");//getuserchoice

                if (int.TryParse(Console.ReadLine(), out int key))
                {
                    Console.WriteLine();

                    switch (key)
                    {
                        case 1: // Exercise suggestion
                            validOptionSelected = true;
                            Console.WriteLine("--- You opted for an exercise suggestion (option [1]) ---");

                            int id = buMove.Randomizer();

                            Console.WriteLine();
                            Console.WriteLine("Randomly selected exercise ID: " + id);
                            Console.WriteLine();
                            Console.WriteLine($"Suggested Exercise: {id}");
                            Console.WriteLine();

                            buMove.getExercise(id);
                           
                            Console.WriteLine();

                            BuMove.AskForRatings();

                            break;

                        case 2: // Choose from list
                            Console.WriteLine("--- You opted to choose from a list with exercices (option [2]) ---");
                            Console.WriteLine();
                            Console.WriteLine("Exercises to choose from: ");
                            Console.WriteLine();

                            Console.WriteLine("[0]: --- add your own exercise ---");

                            List<string> exerciseList = buMove.getExerciseList();
                            foreach (string exerciseInfo in exerciseList)
                            {
                                Console.WriteLine(exerciseInfo);
                            }

                            Console.WriteLine();

                            while (!validOptionSelected)
                            {
                                Console.WriteLine("Press number key of the option you want to choose, followed by 'Enter' to continue: ");//get user choice

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

                                            if (BuMove.IsValidString(name))
                                            {
                                                Console.WriteLine("Valid");
                                            }

                                            else
                                            {
                                                Console.WriteLine("--- Invalid: don't use special characters or numbers. Name & description must be longer than 4 characters. Try again ---");
                                            }
                                        }
                                    }
                                    else if (exerciseList.TryGetValue(selectedExerciseId, out Move selectedExercise) // User chooses exercise from list
                                    {
                                        validOptionSelected = true;

                                        Console.WriteLine();
                                        Console.WriteLine("--- You have chosen exercise: " + selectedExerciseId + ", " + selectedExercise.Name + " ---");
                                        Console.WriteLine();

                                        buMove.getExercise(selectedExerciseId);

                                        Console.WriteLine();
                                        BuMove.AskForRatings();
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



    }
}