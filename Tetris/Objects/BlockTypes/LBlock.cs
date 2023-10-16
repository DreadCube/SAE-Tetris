namespace Tetris.Objects.BlockTypes
{
	public class LBlock: Stone
	{

        public LBlock(int x, int y) : base(x, y) { }

        protected override ConsoleColor GetStoneColor()
        {
            return ConsoleColor.DarkYellow;
        }

        protected override byte[,] GetLayout(Direction direction)
        {

            switch(direction)
            {
                case Direction.Top:
                    return new byte[,] {
                        { 1, 0 },
                        { 1, 0 },
                        { 1, 1 }
                    };

                case Direction.Bottom:
                    return new byte[,] {
                        { 1, 1 },
                        { 0, 1 },
                        { 0, 1 }
                    };

                case Direction.Left:
                    return new byte[,] {
                        { 0, 0, 1 },
                        { 1, 1, 1 }
                    };

                // Direction right
                default:
                    return new byte[,] {
                        { 1, 1, 1 },
                        { 1, 0, 0 }
                    };
            }
           
        }
	}
}

