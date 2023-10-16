using Tetris.Structs;

namespace Tetris.Objects
{
    /**
     * The Stone Class represents a single stone
     * The logic of every Stone type is the same and
     * will be handled here.
     * 
     * Every Block Type extends from the Stone Class.
     * A Block Type holds only two things:
     * 
     * 1. A Color that represents the Block
     * 2. The actual layout of the Block Type.
     * 
     */ 
    public class Stone
    {
        /**
         * Position of the Stone. 
         * 
         * The Position is always
         * top / left of the Stone.
         */
        public Vector2 Position = new Vector2(0, 0);


        /**
         * Every Stone can be rotated and so we have
         * different Directions.
         */
        public enum Direction
        {
            Top,
            Right,
            Bottom,
            Left,
        }

        public Stone(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }

        private Direction currentDirection = Direction.Bottom;


        /**
         * excludedRows is a list of row indexes of the stone.
         * 
         * Every time we fullfill a row in the world,
         * the destroyed row index of the stone will be adedd
         * to excludedRows. We need that information to Render
         * the destroyed stone correctly (and still do math / collisions correctly).
         * An example:
         * 
         * x = World bounds
         * o = stone tile of IBlock
         * l = stone tile of LBlock
         * 
         * 
         * xxxxxxxxx
         * x       x
         * x      lx
         * xoooolllx
         * xxxxxxxxx
         * 
         * 
         * We fullfilled a row in the world. So We render now
         * following:
         * 
         * xxxxxxxxx
         * x       x
         * x       x
         * x      lx
         * xxxxxxxxx
         * 
         * The IBlock has following exluded Rows ({ 0 })
         * The LBlock has following exluded Rows ({ 1 })
         * 
         */
        private List<int> excludedRows = new List<int>();

        /**
         * Rotates a stone clock wise.
         */
        public void Rotate()
        {
            if (currentDirection == Direction.Left)
            {
                currentDirection = Direction.Top;
               
            } else
            {
                currentDirection += 1;
            }

            var (worldX, worldY) = World.GetDimensions();

            /**
             * It's possible that a Block after rotation can clip out on the right side of
             * the World. Because our anchor of the Block is always top left.
             * After a rotation the width of our Block can change in size.
             * 
             * So we gonna move back our Block as long we have cooordinates that
             * would clipping out of the world width.
             * 
             * For the left side theres no reassigning currently needed
             * because the world bounds on the left side is at x position 0.
             * Would needed if we padding the left word bounds more to center
             * of console or similar.
             */
            while(GetCoords().FindAll(coord => coord.X + 1 >= worldX).Count > 0)
            {
                Position.X -= 1;
            }
        }

        public int GetPositionX()
        {
            return Position.X;
        }

        public int GetPositionY()
        {
            return Position.Y;
        }


        /**
         * Checks if the current stone has a sub tile at requested Vector Position
         */
        public bool HasTileAtPosition(Vector2 Position)
        {
            List<Vector2> coords = GetCoords();
            return coords.Exists(coord => coord.X == Position.X && coord.Y == Position.Y);
        }

        /**
         * Should be overrided of every Block Type.
         * But defining here the Stone Color default to "Black".
         */
        protected virtual ConsoleColor GetStoneColor()
        {
            return ConsoleColor.Black;
        }

        /**
         * GetLayout should be ovverided for every Block Type.
         * It doesn't make sense the have a default Block Layout.
         * here in the upper Stone Class.
         */
        protected virtual byte[,] GetLayout(Direction direction)
        {
            Console.WriteLine("GetLayout: Needs implementation of specific stone");
            return new byte[,] { { } };
        }

        /**
         * Get all Coordinate Points for all our (still existing)
         * stone Tiles. Will be used for collision checking etc.
         */
        public List<Vector2> GetCoords()
        {
            List<Vector2> coords = new List<Vector2>();

            byte[,] layout = GetDynamicLayout();

            for (int y = 0; y < layout.GetLength(0); y++)
            {
                for (int x = 0; x < layout.GetLength(1); x++)
                {
                    if (layout[y, x] == 1)
                    {
                        coords.Add(new Vector2(Position.X + x, Position.Y + y));
                    }
                }
            }

            return coords;
        }


        /**
         * Works similar as GetLayout. Constructs a 2 dimensional array 
         * of the stones structure
         * but takes destroyed rows of a stone into account.
         */
        public byte[,] GetDynamicLayout()
        {
            byte[,] layout = GetLayout(currentDirection);
            byte[,] dynamicLayout = new byte[layout.GetLength(0), layout.GetLength(1)];

            for (int y = 0; y < layout.GetLength(0); y++)
            {
                bool isExcludedRow = false;
                foreach (int excludedRow in excludedRows)
                {
                    if (y == excludedRow)
                    {
                        isExcludedRow = true;
                    }
                }

                if (isExcludedRow)
                {
                    continue;
                }

                for (int x = 0; x < layout.GetLength(1); x++)
                {
                    dynamicLayout[y, x] = layout[y, x];
                }
            }


            return dynamicLayout;
        }

        /*
         * Moves the whole stone one position to the Left.
         * 
         * Used for controlling the falling stone.
         */
        public void MoveLeft()
        {
            if (Position.X <= 1)
            {
                return;
            }
            Position.X--;
        }

        /*
         * Moves the whole stone one position to the right.
         * 
         * Used for controlling the falling stone.
         */
        public void MoveRight()
        {
            var (worldX, worldY) = World.GetDimensions();
            byte[,] layout = GetLayout(currentDirection);

            int width = layout.GetLength(1);

            if (Position.X + 2 + width > worldX)
            {
                return;
            }

            Position.X += 1;
        }

        /**
         * Moves the whole stone one position down.
         * Used for controlling the falling stone.
         */
        public void MoveDown()
        {
            Position.Y += 1;
        }

        /**
         * Checks if the current stone does touch the bottom
         * of the world bounds.
         */
        public bool IsAtBottom()
        {
            var (worldX, worldY) = World.GetDimensions();

            byte[,] layoutArr = GetLayout(currentDirection);
            int height = layoutArr.GetLength(0);

            if (Position.Y + 1 + height == worldY)
            {
                return true;
            }
            return false;
        }

        /**
         * Add provided index to the list of exluded Rows.
         */
        public void ExcludeRow(int index)
        {
            excludedRows.Add(index);
        }

        /**
         * Renders the visual representation of the stone at the
         * actual stone position.
         */
        public void Render()
        {
            byte[,] layoutArr = GetDynamicLayout();

            Console.ForegroundColor = GetStoneColor();

            for (int y = 0; y < layoutArr.GetLength(0); y++)
            {
                for (int x = 0; x < layoutArr.GetLength(1); x++)
                {
                    if (layoutArr[y,x] == 1)
                    {
                        Program.WriteLineAtPosition("█", Position.X + x, Position.Y + y);
                    }
                }
            }

            Console.ResetColor();
        }
    }
}

