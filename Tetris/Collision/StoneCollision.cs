using Tetris.Objects;
using Tetris.Structs;

namespace Tetris.Collision
{
    /**
     * Represents helper functions to detect collisions between stones.
     * 
     * Uses under the hood the Vector Collision class.
     * 
     * Stone representation in examples:
     * 
     * x Markers = source Stone
     * o Markers = a target Stone
     */
	public static class StoneCollision
	{

        /**
         *  Check if stone collides with a stone on the right side
         *  
         *  Would return true:
         *   x   o
         *  xxxooo
         *  
         *  
         *  Would return false:
         *  
         *   x    o
         *  xxx ooo
         */
		public static bool HasCollisionWithOneOfRight(Stone sourceStone, List<Stone> targetStones)
		{
            List<Vector2> sourceStoneCoords = sourceStone.GetCoords();
            return targetStones.Exists(targetStone => VectorCollision.HasVectorCollisionRight(sourceStoneCoords, targetStone.GetCoords()));
        }

        /**
         *  Check if stone collides with a stone on the left side
         *  
         *  Would return true:
         *  
         *    o x
         *  oooxxxx
         *  
         *  
         *  Would return false:
         *  
         *    o  x
         *  ooo xxxx
         */
        public static bool HasCollisionWithOneOfLeft(Stone sourceStone, List<Stone> targetStones)
        {
            List<Vector2> sourceStoneCoords = sourceStone.GetCoords();
            return targetStones.Exists(targetStone => VectorCollision.HasVectorCollisionLeft(sourceStoneCoords, targetStone.GetCoords()));
        }

        /**
         *  Check if stone collides with a stone down under:
         *  
         *  Would return true:
         *  
         *      x
         *     xxx
         *      o
         *    ooo
         *  
         *  Would return false:
         *  
         *      x
         *     xxx
         *
         *      o
         *    ooo
         */
        public static bool HasCollisionWithOneOfDown(Stone sourceStone, List<Stone> targetStones)
        {
            List<Vector2> sourceStoneCoords = sourceStone.GetCoords();
            return targetStones.Exists(targetStone => VectorCollision.HasVectorCollisionDown(sourceStoneCoords, targetStone.GetCoords()));
        }
    }
}

