namespace Tetris.Objects.BlockTypes
{
	public class OBlock: Stone
	{
        public OBlock(int x, int y) : base(x, y) { }

        protected override ConsoleColor GetStoneColor()
        {
            return ConsoleColor.Yellow;
        }

        protected override byte[,] GetLayout(Direction direction)
        {
            // Is the same layout for every direction
            return new byte[,] {
                { 1, 1 },
                { 1, 1 }
            };
        }
	}
}

