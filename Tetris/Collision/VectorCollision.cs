using Tetris.Structs;

namespace Tetris.Collision
{
    /**
     * Represents helper functions to detect collisions between Vectors.
     *
     * We can compare a List of 2D Vectors agains another List of 2D Vectors to check
     * if we have colliding coordinates.
     *
     * Vector representation in examples: (x, y)
     * x = X Coordinate
     * y = Y Coordinate
     */
    public static class VectorCollision
    {
        /**
         * Checks for a collison on the right.
         *
         * Would return true:
         *
         * source Vector: (0, 0)
         * target Vector: (1, 0)
         *
         *
         * Would return false:
         *
         * source Vector: (0, 0)
         * target Vector: (2, 0)
         *
         */
        public static bool HasVectorCollisionRight(
            List<Vector2> sourceList,
            List<Vector2> targetList
        )
        {
            return sourceList.Exists(
                source =>
                    targetList.Exists(target => source.X + 1 == target.X && source.Y == target.Y)
            );
        }

        /**
         * Checks for a collison on the left.
         *
         * Would return true:
         *
         * source Vector: (1, 0)
         * target Vector: (0, 0)
         *
         *
         * Would return false:
         *
         * source Vector: (2, 0)
         * target Vector: (0, 0)
         *
         */
        public static bool HasVectorCollisionLeft(
            List<Vector2> sourceList,
            List<Vector2> targetList
        )
        {
            return sourceList.Exists(
                source =>
                    targetList.Exists(target => source.X - 1 == target.X && source.Y == target.Y)
            );
        }

        /**
         * Checks for a collison on the bottom.
         *
         * Would return true:
         *
         * source Vector: (0, 0)
         * target Vector: (0, 1)
         *
         *
         * Would return false:
         *
         * source Vector: (0, 0)
         * target Vector: (0, 2)
         *
         */
        public static bool HasVectorCollisionDown(
            List<Vector2> sourceList,
            List<Vector2> targetList
        )
        {
            return sourceList.Exists(
                source =>
                    targetList.Exists(target => source.Y + 1 == target.Y && source.X == target.X)
            );
        }
    }
}
