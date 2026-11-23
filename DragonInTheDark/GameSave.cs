using System;
using System.IO;
using static DragonInTheDark.Program;

namespace DragonInTheDark
{
    public static class GameSave
    {
        public static void LoadGame(
            Position[] TrapPositions,
            ref Position DragonPosition,
            ref Position PlayerPosition,
            ref Position PortalStonePosition,
            ref Boolean DragonAwake)
        {
            string Filename;
            BinaryReader br;

            Console.Write("Enter the name of the file to load: ");
            Filename = Console.ReadLine();
            Console.WriteLine();

            br = new BinaryReader(new FileStream(Filename, FileMode.Open));

            TrapPositions[0].X = br.ReadInt32();
            TrapPositions[0].Y = br.ReadInt32();
            TrapPositions[1].X = br.ReadInt32();
            TrapPositions[1].Y = br.ReadInt32();

            DragonPosition.X = br.ReadInt32();
            DragonPosition.Y = br.ReadInt32();

            PlayerPosition.X = br.ReadInt32();
            PlayerPosition.Y = br.ReadInt32();

            PortalStonePosition.X = br.ReadInt32();
            PortalStonePosition.Y = br.ReadInt32();

            DragonAwake = br.ReadBoolean();

            br.Close();
        }

        public static void SaveGame(
            Position[] TrapPositions,
            Position DragonPosition,
            Position PlayerPosition,
            Position PortalStonePosition,
            Boolean DragonAwake)
        {
            string Filename;
            BinaryWriter bw;

            Console.Write("Enter new file name: ");
            Filename = Console.ReadLine();
            Console.WriteLine();

            bw = new BinaryWriter(new FileStream(Filename, FileMode.Create));

            bw.Write(TrapPositions[0].X);
            bw.Write(TrapPositions[0].Y);
            bw.Write(TrapPositions[1].X);
            bw.Write(TrapPositions[1].Y);

            bw.Write(DragonPosition.X);
            bw.Write(DragonPosition.Y);

            bw.Write(PlayerPosition.X);
            bw.Write(PlayerPosition.Y);

            bw.Write(PortalStonePosition.X);
            bw.Write(PortalStonePosition.Y);

            bw.Write(DragonAwake);

            bw.Close();
        }
    }
}