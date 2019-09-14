using ECSFoundation.MemoryManagement.Attributes;
using ECSUnsafeTest.utils;

namespace ECSUnsafeTest.ECS.Component
{
    [AllocateMemory(0x07)]
    public struct RectCollider
    {
        public Vector2 Center;
        public Vector2 halfExtent;
        public CollisionLayer type;
    }

    public enum CollisionLayer
    {
        Player,
        Ball,
        Wall,
        GoalPlayer1,
        GoalPlayer2
    }
}
