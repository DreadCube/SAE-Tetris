using Tetris.Objects;
using Tetris.Objects.BlockTypes;

namespace Tetris
{
    public static class World
    {
        private static (int, int) dimensions = (30, 30);

        private static int points = 0;

        public static (int, int) GetDimensions()
        {
            return dimensions;
        }

        public static void Render(int cursorX, int cursorLength)
        {
            (int x, int y) = dimensions;

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (i == 0 || i + 1 == y || j == 0 || j + 1 == x)
                    {
                        if (cursorX <= j && j < cursorX + cursorLength)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }
                        Console.Write("█");
                        Console.ResetColor();
                    }
                    else
                    {
                        if (j > cursorX - 1 && j < cursorX + cursorLength)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                        }
                        Console.Write(" ");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }

            Program.WriteLineAtPosition("POINTS: " + points, x + 5, 0);
        }

        public static void IncreasePoints(int Points)
        {
            points += Points;
        }

        public static void GameOver()
        {
            (int x, int y) = dimensions;

            Program.WriteLineAtPosition("GAME OVER!", x + 5, 2);

            Console.ReadKey();
        }

        public static Stone SpawnStone()
        {
            Random rd = new Random();

            int stoneRand = rd.Next(0, 7);

            (int x, int y) = dimensions;

            switch (stoneRand)
            {
                case 0:
                    return new IBlock(x / 2, 1);
                case 1:
                    return new JBlock(x / 2, 1);
                case 2:
                    return new LBlock(x / 2, 1);
                case 3:
                    return new OBlock(x / 2, 1);
                case 4:
                    return new SBlock(x / 2, 1);
                case 5:
                    return new TBlock(x / 2, 1);
            }

            return new ZBlock(x / 2, 1);
        }
    }
}
