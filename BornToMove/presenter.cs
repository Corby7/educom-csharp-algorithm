using BornToMove.Business;
using BornToMove.DAL;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BornToMove
{
    public class Presenter
    {
        private View view;
        private BuMove buMove;

        public Presenter(View view, BuMove buMove) {
            this.view = view;
            this.buMove = buMove;
        }
        
        public void RunProgram()
        {
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

        public void firstInitialOption()
        {
            Move exercise = buMove.GenerateSuggestion();

            view.DisplaySuggestion(exercise);

            BuMove.AskForRatings();
        }

        public void secondInitialOption()
        {
            buMove.GetExerciseList();
            Dictionary<int, Move> exerciseList = buMove.exerciseList;

            view.DisplayChoices(exerciseList);
        }

        public void getChoiceFromList()
        {
            bool validOptionSelected = false;

            while (!validOptionSelected)
            {
                view.AskNumberKey();

                if (int.TryParse(Console.ReadLine(), out int selectedExerciseId))
                {
                    if (selectedExerciseId == 0)
                    {
                        UserAddMove();
                    }
                    else if (buMove.exerciseList.TryGetValue(selectedExerciseId, out Move selectedExercise))
                    {
                        validOptionSelected = true;
                        Move exercise = buMove.GetExercise(selectedExerciseId);

                        view.DisplayChosenExercise(exercise);

                        BuMove.AskForRatings();
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

            buMove.SaveMove(new Move()
            {
                Name = name,
                Description = description,
                SweatRate = sweatrate
            });
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
            int sweatrate = Convert.ToInt16(Console.ReadLine());

            while (!BuMove.IsValidRate(sweatrate))
            {
                view.InvalidError("sweatrate");

                sweatrate = Convert.ToInt16(Console.ReadLine());
            }

            return sweatrate;
        }

    }
}
