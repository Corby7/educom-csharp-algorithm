using BornToMove.Business;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove
{
    public class View
    {
        public void WelcomeMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to BornToMove!");
            Console.WriteLine();
            Console.WriteLine("Looks like you have been inactive for too long, it's time to start moving!");
            Console.WriteLine();
        }

        public void AskInitialChoice()
        {
            Console.WriteLine("Which option do you prefer?");
            Console.WriteLine();
            Console.WriteLine("Press [1] for an exercise suggestion.");
            Console.WriteLine("Press [2] to choose from a list with exercises.");
            Console.WriteLine();
        }

        public void DisplaySuggestion(Move exercise)
        {
            Console.WriteLine("--- You opted for an exercise suggestion (option [1]) ---");
            Console.WriteLine();
            Console.WriteLine($"Suggested Exercise: {exercise.Id}, {exercise.Name}");
            Console.WriteLine($"Description: {exercise.Description}");
            Console.WriteLine($"Sweat Rate: {exercise.SweatRate}");
            Console.WriteLine();
        }

        public void DisplayChoices(Dictionary<int, Move> exerciseList)
        {
            Console.WriteLine("--- You opted to choose from a list with exercices (option [2]) ---");
            Console.WriteLine();
            Console.WriteLine("Exercises to choose from: ");
            Console.WriteLine();
            Console.WriteLine("[0]: --- add your own exercise ---");

            foreach (var kvp in exerciseList)
            {
                int exerciseId = kvp.Key;
                Move exercise = kvp.Value;

                Console.WriteLine($"[{exerciseId}]: {exercise.Name}, sweatrate: {exercise.SweatRate}");
            }

            Console.WriteLine();
        }

        public void DisplayChosenExercise(Move exercise)
        {

            Console.WriteLine();
            Console.WriteLine($"--- You have chosen exercise: {exercise.Id}, {exercise.Name} ---");
            Console.WriteLine();
            Console.WriteLine($"Suggested Exercise: {exercise.Id}, {exercise.Name}");
            Console.WriteLine($"Description: {exercise.Description}");
            Console.WriteLine($"Sweat Rate: {exercise.SweatRate}");
            Console.WriteLine();
        }

        public void AskInput(string question)
        {
            Console.WriteLine();
            Console.WriteLine(question + ", followed by 'Enter' to continue: ");
        }

        public void AskNumberKey()
        {
            Console.WriteLine("Press number key of the option you want to choose, followed by 'Enter' to continue: ");
        }

        public void InvalidError(string error)
        {
            string errortext = "Unknown error.";

            switch(error)
            {
                case ("name"):
                    errortext = "Invalid name: must only contain letters and spaces, and be longer than 4 characters.";
                    break;

                case ("name exists"):
                    errortext = "Invalid name: name already exists.";
                    break;

                case ("description"):
                    errortext = "Invalid description: don't use special characters or numbers.";
                    break;

                case ("sweatrate"):
                    errortext = "Invalid sweatrate: enter a number from 1 to 5.";
                    break;

                case ("input"):
                    errortext = "Invalid input: please enter a valid number.";
                    break;

                case ("option"):
                    errortext = "Please choose option 1 or 2.";
                    break;

                case ("not found"):
                    errortext = "Exercise not found in list.";
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("--- " + errortext + " Try again ---");
        }
    }


}
