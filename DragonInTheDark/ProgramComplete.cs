using System;
using static DragonInTheDark.GameSetup;
using static DragonInTheDark.GameDisplay;
using static DragonInTheDark.GameSave;

namespace DragonInTheDark
{
    public struct Position
    {
        public int X;
        public int Y;
    }

    class Program
    {
        #region SETTINGS

        public const int NO_OF_TRAPS = 2;
        public const int MAP_HEIGHT = 5;
        public const int MAP_WIDTH = 7;

        #endregion

        static void Main(string[] args)
        {
            Console.Title = "Dragon in the Dark";

            char[,] Cavern = new char[MAP_WIDTH, MAP_HEIGHT];
            int Choice = 0;
            Boolean DragonAwake = false;

            Position PortalStonePosition = new Position();
            Position DragonPosition = new Position();
            Position PlayerPosition = new Position();
            Position[] TrapPositions = new Position[NO_OF_TRAPS];

            while (Choice != 9)
            {
                DisplayMenu();
                Choice = GetMainMenuChoice();

                switch (Choice)
                {
                    case 1:
                        SetUpTrainingGame(
                            Cavern,
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake);

                        PlayGame(
                            Cavern,
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake);
                        break;

                    case 2:
                        SetUpGame(
                            Cavern,
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake,
                            true);

                        PlayGame(
                            Cavern,
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake);
                        break;

                    case 3:
                        LoadGame(
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake);

                        SetUpGame(
                            Cavern,
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake,
                            false);

                        PlayGame(
                            Cavern,
                            TrapPositions,
                            ref DragonPosition,
                            ref PlayerPosition,
                            ref PortalStonePosition,
                            ref DragonAwake);
                        break;

                    case 4:
                        SaveGame(
                            TrapPositions,
                            DragonPosition,
                            PlayerPosition,
                            PortalStonePosition,
                            DragonAwake);
                        break;
                }
            }
        }


        public static int GetMainMenuChoice()
        {
            int Choice;

            while (!int.TryParse(Console.ReadLine(), out Choice))
            {
                Console.Write("Please enter a menu number: ");
            }
            Console.WriteLine();

            return Choice;
        }

        public static char GetMove()
        {
            char Move;

            string Input = Console.ReadLine();
            while (String.IsNullOrWhiteSpace(Input) || Input.Trim().Length != 1)
            {
                Console.Write("Please enter one letter: ");
                Input = Console.ReadLine();
            }
            Move = Input.Trim()[0];
            Console.WriteLine();

            return Char.ToUpper(Move);
        }

        public static void MakeMove(
            char[,] Cavern,
            char Direction,
            ref Position PlayerPosition)
        {
            Cavern[
                PlayerPosition.X,
                PlayerPosition.Y] = ' ';

            switch (Direction)
            {
                case 'W':
                    PlayerPosition.Y -= 1;
                    break;

                case 'S':
                    PlayerPosition.Y += 1;
                    break;

                case 'A':
                    PlayerPosition.X -= 1;
                    break;

                case 'D':
                    PlayerPosition.X += 1;
                    break;
            }

            Cavern[
                PlayerPosition.X,
                PlayerPosition.Y] = '*';
        }

        public static Boolean CheckValidMove(
            Position PlayerPosition,
            char Direction)
        {
            Boolean ValidMove;

            ValidMove = true;

            if (!(Direction == 'W' ||
                  Direction == 'S' ||
                  Direction == 'A' ||
                  Direction == 'D' ||
                  Direction == 'M'))
            {
                ValidMove = false;
            }

            switch (Direction)
            {
                case 'W':
                    if (PlayerPosition.Y == 0)
                    {
                        ValidMove = false;
                    }
                    break;

                case 'S':
                    if (PlayerPosition.Y == MAP_HEIGHT - 1)
                    {
                        ValidMove = false;
                    }
                    break;

                case 'A':
                    if (PlayerPosition.X == 0)
                    {
                        ValidMove = false;
                    }
                    break;

                case 'D':
                    if (PlayerPosition.X == MAP_WIDTH - 1)
                    {
                        ValidMove = false;
                    }
                    break;
            }

            return ValidMove;
        }

        public static Boolean CheckIfSameCell(
            Position FirstCellPosition,
            Position SecondCellPosition)
        {
            Boolean InSameCell = false;

            if (
                FirstCellPosition.X ==
                    SecondCellPosition.X &&
                FirstCellPosition.Y ==
                    SecondCellPosition.Y)
            {
                InSameCell = true;
            }

            return InSameCell;
        }

        public static void MovePortalStone(
            char[,] Cavern,
            Position NewCellForPortalStone,
            ref Position PortalStonePosition)
        {
            Cavern[
                NewCellForPortalStone.X,
                NewCellForPortalStone.Y] = 'P';

            Cavern[
                PortalStonePosition.X,
                PortalStonePosition.Y] = ' ';

            PortalStonePosition = NewCellForPortalStone;
        }

        public static void MakeDragonMove(
            char[,] Cavern,
            ref Position DragonPosition,
            ref Position PortalStonePosition,
            Position PlayerPosition)
        {
            // Remember where the dragon started this turn
            Position OriginalDragonPosition = DragonPosition;

            // Remove the dragon from its old position
            Cavern[DragonPosition.X, DragonPosition.Y] = ' ';

            // The dragon has blindsight and always knows
            // where the player is.

            if (DragonPosition.X < PlayerPosition.X)
            {
                DragonPosition.X += 1;
            }
            else if (DragonPosition.X > PlayerPosition.X)
            {
                DragonPosition.X -= 1;
            }
            else if (DragonPosition.Y < PlayerPosition.Y)
            {
                DragonPosition.Y += 1;
            }
            else if (DragonPosition.Y > PlayerPosition.Y)
            {
                DragonPosition.Y -= 1;
            }

            // Has the dragon stepped on the Portal Stone?
            Boolean DragonMovedToSameCellAsPortalStone = CheckIfSameCell(DragonPosition, PortalStonePosition);

            if (DragonMovedToSameCellAsPortalStone)
            {
                // The dragon kicks the Portal Stone into
                // the cell it has just moved from.
                MovePortalStone(
                    Cavern,
                    OriginalDragonPosition,
                    ref PortalStonePosition);
            }

            Cavern[
                DragonPosition.X,
                DragonPosition.Y] = 'D';
        }

        public static void PlayGame(
            char[,] Cavern,
            Position[] TrapPositions,
            ref Position DragonPosition,
            ref Position PlayerPosition,
            ref Position PortalStonePosition,
            ref Boolean DragonAwake)
        {
            Boolean Eaten = false;
            Boolean PortalStoneFound = false;
            Boolean ValidMove = false;

            int Count = 0;
            char MoveDirection = ' ';

            DisplayCavern(Cavern, DragonAwake);

            while (!(Eaten ||
                     PortalStoneFound ||
                     MoveDirection == 'M'))
            {
                ValidMove = false;

                while (!ValidMove)
                {
                    DisplayMoveOptions();
                    MoveDirection = GetMove();

                    ValidMove =
                        CheckValidMove(
                            PlayerPosition,
                            MoveDirection);
                }

                if (MoveDirection != 'M')
                {
                    MakeMove(
                        Cavern,
                        MoveDirection,
                        ref PlayerPosition);

                    DisplayCavern(
                        Cavern,
                        DragonAwake);

                    PortalStoneFound =
                        CheckIfSameCell(
                            PlayerPosition,
                            PortalStonePosition);

                    if (PortalStoneFound)
                    {
                        DisplayWonGameMessage();
                    }

                    Eaten =
                        CheckIfSameCell(
                            DragonPosition,
                            PlayerPosition);

                    if (!DragonAwake &&
                        !PortalStoneFound &&
                        !Eaten)
                    {
                        DragonAwake =
                            CheckIfSameCell(
                                PlayerPosition,
                                TrapPositions[0]);

                        if (!DragonAwake)
                        {
                            DragonAwake =
                                CheckIfSameCell(
                                    PlayerPosition,
                                    TrapPositions[1]);
                        }

                        if (DragonAwake)
                        {
                            DisplayTrapMessage();

                            DisplayCavern(
                                Cavern,
                                DragonAwake);
                        }
                    }

                    if (DragonAwake &&
                        !Eaten &&
                        !PortalStoneFound)
                    {
                        Count = 0;

                        // The dragon moves twice for every
                        // one move made by the player.
                        while (Count < 2 && !Eaten && !PortalStoneFound)
                        {
                            MakeDragonMove(
                                Cavern,
                                ref DragonPosition,
                                ref PortalStonePosition,
                                PlayerPosition);

                            Eaten =
                                CheckIfSameCell(
                                    DragonPosition,
                                    PlayerPosition);

                            PortalStoneFound = !Eaten && CheckIfSameCell(
                                PlayerPosition, PortalStonePosition);

                            if (PortalStoneFound)
                            {
                                DisplayWonGameMessage();
                            }

                            Console.WriteLine();
                            Console.WriteLine(
                                "The dragon moves through the darkness...");
                            Console.WriteLine(
                                "Press Enter to continue");

                            Console.ReadLine();

                            DisplayCavern(
                                Cavern,
                                DragonAwake);

                            Count = Count + 1;
                        }
                    }

                    if (Eaten)
                    {
                        DisplayLostGameMessage();
                    }
                }
            }
        }
    }
}