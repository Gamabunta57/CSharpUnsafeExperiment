using ECSFoundation.MemoryManagement.Attributes;
using ECSUnsafeTest.utils;

namespace ECSUnsafeTest.ECS.Component
{
    [AllocateMemory(0x07)]
    public struct Position
    {
        public Vector2 Value;
    }
}
