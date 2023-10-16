namespace Tetris.Objects.BlockTypes
{
    public class SBlock : Stone
    {
        public SBlock(int x, int y)
            : base(x, y) { }

        protected override ConsoleColor GetStoneColor()
        {
            return ConsoleColor.Green;
        }

        protected override byte[,] GetLayout(Direction direction)
        {
            switch (direction)
            {
                case Direction.Top
                or Direction.Bottom:
                    return new byte[,]
                    {
                        { 0, 1, 1 },
                        { 1, 1, 0 }
                    };

                // Left or Right
                default:
                    return new byte[,]
                    {
                        { 1, 0 },
                        { 1, 1 },
                        { 0, 1 },
                    };
            }
        }
    }
}
