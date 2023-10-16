using Tetris.Collision;
using Tetris.Objects;
using Tetris.Structs;

namespace Tetris
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var (worldX, worldY) = World.GetDimensions();

            List<Stone> placedStones = new List<Stone>();

            Stone fallingStone = World.SpawnStone();

            while (true)
            {
                Console.Clear();

                World.Render(
                    fallingStone.GetPositionX(),
                    fallingStone.GetDynamicLayout().GetLength(1)
                );

                fallingStone.Render();

                foreach (Stone stone in placedStones)
                {
                    stone.Render();
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.W:
                            fallingStone.Rotate();
                            break;

                        case ConsoleKey.DownArrow
                            when (
                                !fallingStone.IsAtBottom()
                                && !StoneCollision.HasCollisionWithOneOfDown(
                                    fallingStone,
                                    placedStones
                                )
                            ):
                        case ConsoleKey.S
                            when (
                                !fallingStone.IsAtBottom()
                                && !StoneCollision.HasCollisionWithOneOfDown(
                                    fallingStone,
                                    placedStones
                                )
                            ):
                            fallingStone.MoveDown();
                            break;

                        case ConsoleKey.RightArrow
                            when !StoneCollision.HasCollisionWithOneOfRight(
                                fallingStone,
                                placedStones
                            ):
                        case ConsoleKey.D
                            when !StoneCollision.HasCollisionWithOneOfRight(
                                fallingStone,
                                placedStones
                            ):

                            fallingStone.MoveRight();
                            break;

                        case ConsoleKey.LeftArrow
                            when !StoneCollision.HasCollisionWithOneOfLeft(
                                fallingStone,
                                placedStones
                            ):
                        case ConsoleKey.A
                            when !StoneCollision.HasCollisionWithOneOfLeft(
                                fallingStone,
                                placedStones
                            ):

                            fallingStone.MoveLeft();
                            break;
                        default:
                            break;
                    }
                }

                if (
                    fallingStone.IsAtBottom()
                    || StoneCollision.HasCollisionWithOneOfDown(fallingStone, placedStones)
                )
                {
                    // Game Over
                    if (fallingStone.GetPositionY() == 1)
                    {
                        World.GameOver();
                        break;
                    }

                    placedStones.Add(fallingStone);
                    fallingStone = World.SpawnStone();
                }
                else
                {
                    fallingStone.MoveDown();
                }

                int fullRowY = CheckForFullRow(placedStones);
                if (fullRowY >= 0)
                {
                    // Increase points for successful row complishement.
                    World.IncreasePoints(500);

                    foreach (Stone stone in placedStones)
                    {
                        List<Vector2> coords = stone.GetCoords();
                        int positionY = stone.GetPositionY();

                        foreach (Vector2 coord in coords)
                        {
                            if (fullRowY == coord.Y)
                            {
                                /*
                                 * Exclude row of stone that is now goona be destroyed
                                 */
                                int index = coord.Y - positionY;

                                stone.ExcludeRow(index);
                            }
                        }

                        /*
                         *
                         * Re aligns all stones on top (We move them down 1 row)
                         */
                        bool needsReplacement = false;

                        foreach (Vector2 coord in coords)
                        {
                            if (fullRowY > coord.Y)
                            {
                                needsReplacement = true;
                            }
                        }

                        if (needsReplacement)
                        {
                            stone.MoveDown();
                        }
                    }
                }

                Thread.Sleep(100);
            }
        }

        public static void WriteLineAtPosition(string text, int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.WriteLine(text);
        }

        /**
         * Scans the whole world and checks if theres a row full of blocks.
         * Returns an integer for the row that was found.
         *
         * Returns -1 if no full row was found
         */
        private static int CheckForFullRow(List<Stone> stones)
        {
            if (stones.Count == 0)
            {
                return -1;
            }

            var (width, height) = World.GetDimensions();

            for (int y = 1; y < height - 1; y++)
            {
                bool isFullRow = true;

                for (int x = 1; x < width - 1; x++)
                {
                    bool isTileSet = false;

                    foreach (Stone stone in stones)
                    {
                        if (stone.HasTileAtPosition(new Vector2(x, y)))
                        {
                            isTileSet = true;
                            continue;
                        }
                    }

                    if (!isTileSet)
                    {
                        isFullRow = false;
                        break;
                    }
                }
                if (isFullRow)
                {
                    return y;
                }
            }

            return -1;
        }
    }
}
