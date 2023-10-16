namespace Tetris.Objects.BlockTypes
{
    public class IBlock : Stone
    {
        public IBlock(int x, int y)
            : base(x, y) { }

        protected override ConsoleColor GetStoneColor()
        {
            int Zahl = 0;

            do
            {
                Zahl++;
            } while (Zahl != 5);

            return ConsoleColor.Cyan;
        }

        protected override byte[,] GetLayout(Direction direction)
        {
            switch (direction)
            {
                case Direction.Top
                or Direction.Bottom:
                    return new byte[,]
                    {
                        { 1, 1, 1, 1 },
                    };

                // Left or right Directions
                default:
                    return new byte[,]
                    {
                        { 1 },
                        { 1 },
                        { 1 },
                        { 1 }
                    };
            }
        }
    }
}
