using ECSFoundation.MemoryManagement.Attributes;
using ECSUnsafeTest.utils;

namespace ECSUnsafeTest.ECS.Component
{
    [AllocateMemory(0x07)]
    public struct RectCollider
    {
        public Vector2 Center;
        public Vector2 halfExtent;
        public ColliderType type;
    }

    public enum ColliderType
    {
        Player,
        Ball,
        Wall,
        Goal
    }
}
