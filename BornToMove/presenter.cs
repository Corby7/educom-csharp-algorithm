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
        private readonly View view;
        private readonly BuMove buMove;

        public Presenter(View view, BuMove buMove) {
            this.view = view;
            this.buMove = buMove;
        }
        
        public void RunProgram()
        {
            buMove.FillEmptyDB();

            view.WelcomeMessage();
            
            view.AskInitialChoice();
            GetInitialChoice();
        }


        public void GetInitialChoice()
        {
            bool validOptionSelected = false;

            while (!validOptionSelected)
            {
                view.AskNumberKey();

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
                            view.InvalidError("option");
                            break;

                    }
                }
                else
                {
                    view.InvalidError("input");
                }
            }
        }

        public void FirstInitialOption()
        {
            buMove.GenerateRandomSuggestion();
            
            view.DisplayExercise(buMove.SelectedMove, buMove.SelectedMoveRating, "suggestion");

            AskForRatings();
        }

        public void SecondInitialOption()
        {
            buMove.GetExerciseList();

            view.DisplayChoices(buMove.ExerciseList);
        }

        public void GetChoiceFromList()
        {
            bool validOptionSelected = false;

            view.AskNumberKey();

            while (!validOptionSelected)
            {
                if (int.TryParse(Console.ReadLine(), out int selectedExerciseId))
                {
                    if (selectedExerciseId == 0)
                    {
                        UserAddMove();

                        //succesfully added exercise message
                    }
                    else if (buMove.ExerciseList.TryGetValue(selectedExerciseId, out Move selectedExercise))
                    {
                        validOptionSelected = true;
                        
                        buMove.GetExercise(selectedExerciseId);

                        view.DisplayExercise(buMove.SelectedMove, buMove.SelectedMoveRating, "list");

                        AskForRatings();
                    }
                    else
                    {
                        view.InvalidError("not found");
                    }
                }
                else
                {
                    view.InvalidError("input");
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
            view.AskInput("Type the name of the exercise you want to add");
            string name = Console.ReadLine();

            var nameCheck = buMove.IsValidName(name);

            while (!nameCheck.isValid)
            {
                view.InvalidError(nameCheck.error);
                name = Console.ReadLine();

                nameCheck = buMove.IsValidName(name);
            }
      
            return name;
        }

        private string GetMoveDescription()
        {
            view.AskInput("Type a description of the exercise");
            string description = Console.ReadLine();

            while(!BuMove.IsValidString(description))
            {
                view.InvalidError("description");
                description = Console.ReadLine();
            }

            return description;
        }

        private int GetMoveSweatrate()
        {
            view.AskInput("Type the sweatrate of your exercise on a scale from 1-5 (integers)");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int sweatrate) && BuMove.IsValidRateInt(sweatrate))
                {
                    return sweatrate;
                }
                else
                {
                    view.InvalidError("sweatrate");
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
            Console.WriteLine("Congratulations, you finished the exercise!");
            Console.WriteLine("On a scale from 1-5, how much did you like this exercise?");
            view.AskInput("Type a number to rate this exercise on a scale from 1.0-5.0 (1 decimal allowed)");

            while (true)
            {
                if (double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out double rating) && BuMove.IsValidRateDouble(rating))
                {
                    return rating;
                }
                else
                {
                    Console.WriteLine($"input: {rating}");
                    view.InvalidError("rating");
                }
            }
        }

        //decimal double have to fix.

        private double AskIntensityVote()
        {
            Console.WriteLine("And on a scale from 1-5, how intense did you find exercise?");
            view.AskInput("Type a number to rate the intensity of this exercise on a scale from 1.0-5.0 (1 decimal allowed)");

            while (true)
            {
                if (double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out double vote) && BuMove.IsValidRateDouble(vote))
                {
                    return vote;
                }
                else
                {
                    view.InvalidError("rating");
                }
            }
        }

    }
}