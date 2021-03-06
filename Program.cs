﻿using System;
using System.Diagnostics;
namespace PhotoFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Environment.GetCommandLineArgs().Length <= 1)
            {
                new ContextMenuEditor().EditRegistry();
                Console.WriteLine("Press any button to exit");
                Console.ReadKey();
            }
            else if (Environment.GetCommandLineArgs()[1] == "-clear")
            {
                new ContextMenuEditor().ClearRegistry();
                Console.WriteLine("Press any button to exit");
                Console.ReadKey();
            }
            else
            {
                string PathToFile = "";

                for (int i = 1; i < Environment.GetCommandLineArgs().Length; ++i)
                {
                    PathToFile += Environment.GetCommandLineArgs()[i] + " ";
                }
                Process.Start(FindImage.GetUriImage(PathToFile)); // if this will not work try this 
                                                                  //Process.Start("cmd", $"/c start {FindImage.GetUriImage(PathToFile)}");
            }
        }
    }
}
