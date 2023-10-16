namespace Tetris.Objects.BlockTypes
{
	public class ZBlock: Stone
	{

        public ZBlock(int x, int y) : base(x, y) { }

        protected override ConsoleColor GetStoneColor()
        {
            return ConsoleColor.Red;
        }

        protected override byte[,] GetLayout(Direction direction)
        {
            switch(direction)
            {
                case Direction.Top or Direction.Bottom:
                    return new byte[,] {
                        { 0, 1 },
                        { 1, 1 },
                        { 1, 0 }
                    };

                // Left or right
                default:
                    return new byte[,] {
                        { 1, 1, 0 },
                        { 0, 1, 1 },
                    };
            }
           
        }
	}
}

