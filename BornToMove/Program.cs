using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using BornToMove.Business;
using BornToMove.DAL;

namespace BornToMove
{
    internal class Program
    {

        static void Main(string[] args)
        {
            MoveContext context = new MoveContext();
            MoveCrud crud = new MoveCrud(context);
            BuMove buMove = new BuMove(crud);
            Presenter presenter = new Presenter(buMove);

            presenter.RunProgram();

        }



    }
}