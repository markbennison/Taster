using System;
using static DragonInTheDark.Program;

namespace DragonInTheDark
{
    internal class GameSetup
    {
        public static Position GetNewRandomPosition()
        {
            Position NewPosition = new Position();
            Random rnd = new Random();

            do
            {
                NewPosition.X = rnd.Next(0, MAP_WIDTH);
                NewPosition.Y = rnd.Next(0, MAP_HEIGHT);
            }
            while (NewPosition.X == 0 && NewPosition.Y == 0);

            return NewPosition;
        }

        public static void ResetCavern(char[,] Cavern)
        {
            int X;
            int Y;

            for (X = 0; X < MAP_WIDTH; X++)
            {
                for (Y = 0; Y < MAP_HEIGHT; Y++)
                {
                    Cavern[X, Y] = ' ';
                }
            }
        }

        public static void SetPositionOfItem(
           char[,] Cavern,
           ref Position ObjectPosition,
           char Item,
           Boolean NewGame)
        {
            Position Position = new Position();

            if (NewGame && Item != '*')
            {
                do
                {
                    Position = GetNewRandomPosition();
                }
                while (Cavern[Position.X, Position.Y] != ' ');

                ObjectPosition = Position;
            }

            Cavern[ObjectPosition.X, ObjectPosition.Y] = Item;
        }

        public static void SetUpGame(
            char[,] Cavern,
            Position[] TrapPositions,
            ref Position DragonPosition,
            ref Position PlayerPosition,
            ref Position PortalStonePosition,
            ref Boolean DragonAwake,
            Boolean NewGame)
        {
            int Count;

            ResetCavern(Cavern);

            if (NewGame)
            {
                PlayerPosition.X = 0;
                PlayerPosition.Y = 0;
                DragonAwake = false;
            }

            for (Count = 0; Count < NO_OF_TRAPS; Count++)
            {
                SetPositionOfItem(Cavern, ref TrapPositions[Count], 'T', NewGame);
            }

            SetPositionOfItem(Cavern, ref DragonPosition, 'D', NewGame);
            SetPositionOfItem(Cavern, ref PortalStonePosition, 'P', NewGame);
            SetPositionOfItem(Cavern, ref PlayerPosition, '*', NewGame);
        }

        public static void SetUpTrainingGame(
            char[,] Cavern,
            Position[] TrapPositions,
            ref Position DragonPosition,
            ref Position PlayerPosition,
            ref Position PortalStonePosition,
            ref Boolean DragonAwake)
        {
            ResetCavern(Cavern);

            PlayerPosition.X = 4;
            PlayerPosition.Y = 2;

            DragonAwake = false;

            TrapPositions[0].X = 6;
            TrapPositions[0].Y = 1;

            TrapPositions[1].X = 4;
            TrapPositions[1].Y = 3;

            DragonPosition.X = 3;
            DragonPosition.Y = 0;

            PortalStonePosition.X = 5;
            PortalStonePosition.Y = 4;

            SetUpGame(
                Cavern,
                TrapPositions,
                ref DragonPosition,
                ref PlayerPosition,
                ref PortalStonePosition,
                ref DragonAwake,
                false);
        }
    }
}
