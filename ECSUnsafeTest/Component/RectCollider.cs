using ECSUnsafeTest.Attributes;
using ECSUnsafeTest.core;

namespace ECSUnsafeTest.Component
{
    [AllocateMemory(0x02)]
    public struct RectCollider
    {
        public Vector2 center;
        public Vector2 halfExtent;
    }
}
