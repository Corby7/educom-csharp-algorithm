using BornToMove.Business;
using BornToMove.DAL;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove
{
    public class Presenter
    {
        private readonly BuMove buMove;

        public Presenter(BuMove buMove) {
            this.buMove = buMove;
        }
        
        public void RunProgram()
        {
            buMove.FillEmptyDB();

            View.WelcomeMessage();
            
            View.AskInitialChoice();
            GetInitialChoice();
        }


        public void GetInitialChoice()
        {
            bool validOptionSelected = false;

            while (!validOptionSelected)
            {
                View.AskNumberKey();

                if (int.TryParse(Console.ReadLine(), out int keyinput))
                {
                    Console.WriteLine();

                    switch (keyinput)
                    {
                        case 1:

                            validOptionSelected = true;
                            FirstInitialOption();

                            break;

                        case 2:

                            SecondInitialOption();
                            GetChoiceFromList();
                            validOptionSelected = true;
                            break;

                        default:
                            View.InvalidError("option");
                            break;

                    }
                }
                else
                {
                    View.InvalidError("input");
                }
            }
        }

        public void FirstInitialOption()
        {
            buMove.GenerateRandomSuggestion();
            
            View.DisplayExercise(buMove.SelectedMove, buMove.SelectedMoveRating, "suggestion");

            View.PressContinue();

            AskForRatings();
        }

        public void SecondInitialOption()
        {
            buMove.GetExerciseList();

            View.DisplayChoices(buMove.ExerciseList);
        }

        public void GetChoiceFromList()
        {
            bool validOptionSelected = false;

            View.AskNumberKey();

            while (!validOptionSelected)
            {
                if (int.TryParse(Console.ReadLine(), out int selectedExerciseId))
                {
                    if (selectedExerciseId == 0)
                    {
                        UserAddMove();

                        View.ExerciseAdded();
                    }
                    else
                    {
                        MoveAverageRating selectedExercise = buMove.ExerciseList.FirstOrDefault(exercise => exercise.Move.Id == selectedExerciseId);

                        if (selectedExercise != null)
                        {
                            validOptionSelected = true;

                            buMove.GetExercise(selectedExerciseId);

                            View.DisplayExercise(buMove.SelectedMove, buMove.SelectedMoveRating, "list");

                            View.PressContinue();

                            AskForRatings();
                        }
                        else
                        {
                            View.InvalidError("not found");
                        }
                    }
                }
                else
                {
                    View.InvalidError("input");
                }
            }
        }

        public void UserAddMove()
        {
            string name = GetMoveName();
            string description = GetMoveDescription();
            int sweatrate = GetMoveSweatrate();

            buMove.SaveMove(name, description, sweatrate);
        }

        private string GetMoveName()
        {
            View.AskInput("Type the name of the exercise you want to add");
            string name = Console.ReadLine();

            var nameCheck = buMove.IsValidName(name);

            while (!nameCheck.isValid)
            {
                View.InvalidError(nameCheck.error);
                name = Console.ReadLine();

                nameCheck = buMove.IsValidName(name);
            }
      
            return name;
        }

        private string GetMoveDescription()
        {
            View.AskInput("Type a description of the exercise");
            string description = Console.ReadLine();

            while(!BuMove.IsValidString(description))
            {
                View.InvalidError("description");
                description = Console.ReadLine();
            }

            return description;
        }

        private int GetMoveSweatrate()
        {
            View.AskInput("Type the sweatrate of your exercise on a scale from 1-5 (integers)");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int sweatrate) && BuMove.IsValidRateInt(sweatrate))
                {
                    return sweatrate;
                }
                else
                {
                    View.InvalidError("sweatrate");
                }
            }
        }

        private void AskForRatings()
        {
            double rating = AskRating();
            Console.WriteLine();
            double vote = AskIntensityVote();
            buMove.SaveMoveRating(rating, vote);
        }

        private double AskRating()
        {
            View.AskUserRating();
            View.AskInput("Type a number to rate this exercise on a scale from 1.0-5.0 (1 decimal allowed)");

            while (true)
            {
                if (double.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out double rating) && BuMove.IsValidRateDouble(rating))
                {
                    return rating;
                }
                else
                {
                    View.InvalidError("rating");
                }
            }
        }

        private double AskIntensityVote()
        {
            View.AskUserIntensity();
            View.AskInput("Type a number to rate the intensity of this exercise on a scale from 1.0-5.0 (1 decimal allowed)");

            while (true)
            {
                if (double.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture, out double vote) && BuMove.IsValidRateDouble(vote))
                {
                    return vote;
                }
                else
                {
                    View.InvalidError("rating");
                }
            }
        }

    }
}