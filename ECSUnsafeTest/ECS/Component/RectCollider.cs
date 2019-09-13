using ECSUnsafeTest.MemoryManagement.Attributes;
using ECSUnsafeTest.core;

namespace ECSUnsafeTest.ECS.Component
{
    [AllocateMemory(0x03)]
    public struct RectCollider
    {
        public Vector2 center;
        public Vector2 halfExtent;
    }
}
