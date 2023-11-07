using BornToMove.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove
{
    public class Presenter
    {
        private BuMove buMove;

        public Presenter(BuMove buMove) {
            this.buMove = buMove;
        }

        public static void welcomeMessage() //naar view verplaatsen
        {
            Console.WriteLine();//welcome message
            Console.WriteLine("Welcome to BornToMove!");
            Console.WriteLine();
            Console.WriteLine("Looks like you have been inactive for too long, it's time to start moving!");
            Console.WriteLine();
        }

        public static void askInitialChoice()
        {
            Console.WriteLine("Which option do you prefer?");//ask initial choice
            Console.WriteLine();
            Console.WriteLine("Press [1] for an exercise suggestion.");
            Console.WriteLine("Press [2] to choose from a list with exercises.");
            Console.WriteLine();
        }

        public void getInitialChoice()
        {
            bool validOptionSelected = false;

            while (!validOptionSelected)
            {
                Console.WriteLine("Press number key, followed by 'Enter' to continue: ");//getuserchoice

                if (int.TryParse(Console.ReadLine(), out int key))
                {
                    Console.WriteLine();

                    switch (key)
                    {
                        case 1:

                            validOptionSelected = true;
                            firstInitialOption();

                            break;

                        case 2:

                            secondInitialOption();
                            getChoiceFromList();
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

        public void firstInitialOption()
        {
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
        }

        public void secondInitialOption()
        {
            Console.WriteLine("--- You opted to choose from a list with exercices (option [2]) ---");
            Console.WriteLine();
            Console.WriteLine("Exercises to choose from: ");
            Console.WriteLine();
            Console.WriteLine("[0]: --- add your own exercise ---");

            Dictionary<int, Move> exerciseList = buMove.getExerciseList();
            
            foreach (var kvp in exerciseList)
            {
                int exerciseId = kvp.Key;
                Move exercise = kvp.Value;

                Console.WriteLine($"[{exerciseId}]: {exercise.Name}, sweatrate: {exercise.SweatRate}");
            }

            Console.WriteLine();

        }

        public void getChoiceFromList()
        {
            bool validOptionSelected = false;

            while (!validOptionSelected)
            {
                Console.WriteLine("Press number key of the option you want to choose, followed by 'Enter' to continue: ");//get user choice

                if (int.TryParse(Console.ReadLine(), out int selectedExerciseId))
                {
                    switch (selectedExerciseId)
                    {
                        case 0:
                            break;

                        case 1:
                            break;

                        default:
                            Console.WriteLine();
                            Console.WriteLine("--- Exercise not found in list, try again. ---");
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
