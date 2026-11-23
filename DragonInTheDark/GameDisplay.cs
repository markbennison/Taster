using System;
using static DragonInTheDark.Program;

namespace DragonInTheDark
{
    internal class GameDisplay
    {
        public static void DisplayCavern(
            char[,] Cavern,
            Boolean DragonAwake)
        {
            int X;
            int Y;

            for (Y = 0; Y < MAP_HEIGHT; Y++)
            {
                Console.WriteLine(
                    "  --------------------------- ");

                for (X = 0; X < MAP_WIDTH; X++)
                {
                    if (
                        Cavern[X, Y] == ' ' ||
                        Cavern[X, Y] == '*' ||
                        (Cavern[X, Y] == 'D' &&
                         DragonAwake))
                    {
                        Console.Write(
                            " | " + Cavern[X, Y]);
                    }
                    else
                    {
                        // Traps and the Portal Stone remain hidden.
                        Console.Write(" |  ");
                    }
                }

                Console.WriteLine(" | ");
            }

            Console.WriteLine(
                "  --------------------------- ");
            Console.WriteLine();
        }

        public static void DisplayMenu()
        {
            Console.WriteLine("DRAGON IN THE DARK");
            Console.WriteLine();
            Console.WriteLine("MAIN MENU");
            Console.WriteLine();
            Console.WriteLine("1. Training Game");
            Console.WriteLine("2. New Game");
            //Console.WriteLine("3. Load Game");
            //Console.WriteLine("4. Save Game");
            Console.WriteLine("9. Quit");
            Console.WriteLine();
            Console.Write("Please enter your choice: ");
        }
        public static void DisplayLostGameMessage()
        {
            Console.WriteLine();
            Console.WriteLine("ARGHHHHHH! The dragon has found you!");
            Console.WriteLine("Unfortunately, adventurers are a dragon's favourite snack.");
            Console.WriteLine();
            Console.WriteLine("GAME OVER.");
            Console.WriteLine("Perhaps you'll have better luck next time in DRAGON IN THE DARK!");
            Console.WriteLine();
        }

        public static void DisplayMoveOptions()
        {
            Console.WriteLine();
            Console.WriteLine("Enter W to move UP");
            Console.WriteLine("Enter S to move DOWN");
            Console.WriteLine("Enter A to move LEFT");
            Console.WriteLine("Enter D to move RIGHT");
            Console.WriteLine("Enter M to return to the Main Menu");
            Console.WriteLine();
        }

        public static void DisplayTrapMessage()
        {
            Console.WriteLine();
            Console.WriteLine("CLICK! You have triggered an ancient trap!");
            Console.WriteLine();
            Console.WriteLine("A pair of glowing eyes opens somewhere in the darkness...");
            Console.WriteLine("The dragon is awake - and its blindsight means it knows exactly where you are!");
            Console.WriteLine();
            Console.WriteLine("RUN!");
            Console.WriteLine();
        }

        public static void DisplayWonGameMessage()
        {
            Console.WriteLine();
            Console.WriteLine("You have found the legendary Portal Stone!");
            Console.WriteLine("Its ancient magic flares to life and you escape the dungeon.");
            Console.WriteLine();
            Console.WriteLine("Congratulations - you survived DRAGON IN THE DARK!");
            Console.WriteLine();
        }


    }
}
